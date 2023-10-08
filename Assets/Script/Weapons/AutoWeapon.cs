using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapon
{
    public class AutoWeapon : Weapon
    {
        [SerializeField, BoxGroup("Auto Weapon Spec")] private float _fireRate;

		// m_pool of bullet
		[SerializeField] private BulletPool _bulletPool;

		[SerializeField, Foldout("Event")] private UnityEvent _onStartShooting;
        [SerializeField, Foldout("Event")] private UnityEvent _onContinueShooting;
        [SerializeField, Foldout("Event")] private UnityEvent _onStopChooting;

        private bool _isTriggerPressed = false;

        protected override void Start()
        {
	        base.Start();
            _onFinishReloading.AddListener(CheckTriggerAfterReload);
        }

        private void OnDestroy()
        {
            _onFinishReloading.RemoveListener(CheckTriggerAfterReload);
        }

        public override void PullTrigger()
        {
            _isTriggerPressed = true;

            if (_isReloading) return;

            InvokeRepeating(nameof(InstanceBullet), 0F, _fireRate);
            _onStartShooting?.Invoke();
        }

        public override void ReleaseTrigger()
        {
            _isTriggerPressed = false;

            if (_isReloading) return;

            CancelInvoke();
            _onStopChooting?.Invoke();
        }

        private void InstanceBullet()
        {
			Bullet bullet = _bulletPool.Pool.Get();
			bullet.Init(_firePoint.transform.position, Direction, 300);

            _onContinueShooting?.Invoke();

            m_model.transform.transform.DOPunchScale(Vector3.one * (_recoilEffectIntensity / 10), 0.1f);

			if (--_currentMagSize <= 0)
            {
                StartCoroutine(ReloadWeapon());
                CancelInvoke();
            }
        }

        private void CheckTriggerAfterReload ()
        {
            if (_isTriggerPressed)
                InvokeRepeating(nameof(InstanceBullet), 0F, _fireRate);
        }
    }
}
