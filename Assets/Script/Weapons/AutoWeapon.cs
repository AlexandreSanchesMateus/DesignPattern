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

		// pool of bullet
		[SerializeField] private ObjectPool _bulletPool;

		[SerializeField, Foldout("Event")] private UnityEvent _onStartShooting;
        [SerializeField, Foldout("Event")] private UnityEvent _onContinueShooting;
        [SerializeField, Foldout("Event")] private UnityEvent _onStopChooting;

        private bool _firstBullet;

        public override void PullTrigger()
        {
            if (_isReloading) return;

            InvokeRepeating(nameof(InstanceBullet), 0F, _fireRate);
            _onStartShooting?.Invoke();
        }

        public override void ReleaseTrigger()
        {
            if (_isReloading) return;

            CancelInvoke();
            _onStopChooting?.Invoke();
        }

        private void InstanceBullet()
        {
			Bullet bullet = _bulletPool.Pool.Get();
            bullet.Init(_firePoint.transform.position, Direction, 300);

			_onContinueShooting?.Invoke();

            if(--_currentMagSize <= 0)
            {
                StartCoroutine(ReloadWeapon());
                CancelInvoke();
            }
        }
    }
}
