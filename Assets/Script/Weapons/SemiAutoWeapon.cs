using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapon
{
    public class SemiAutoWeapon : Weapon
    {
        [SerializeField, BoxGroup("Semi Weapon Spec")] private float _shotTime;

        [SerializeField] private ObjectPool<Bullet> _bulletPool;
        //[SerializeField] private ObjectPool<Bullet> _bulletPool2;

        [SerializeField, Foldout("Event")] private UnityEvent _onShoot;
        [SerializeField, Foldout("Event")] private UnityEvent _onTriggerRelease;

        private bool _isBetweenShot = false;

        public override void PullTrigger()
        {
            if (_isBetweenShot || _isReloading) return;

            InstanceBullet();
            StartCoroutine(ShotDelay());
        }

        public override void ReleaseTrigger()
        {
            if (_isBetweenShot) return;

            _onTriggerRelease?.Invoke();
        }

        private void InstanceBullet()
        {
            Bullet bullet = _bulletPool.Pool.Get();
            bullet.Init(_firePoint.transform.position, Direction, 300);
			//bullet.onBulletHit += () => bullet.ObjectPool.Pool.Release(bullet);
			bullet.name = "bullet1";

			/*Bullet bullet2 = _bulletPool2.Pool.Get();
			bullet2.Init(_firePoint.transform.position, Direction, 200);
			bullet2.name = "bullet2";*/

			if (--_currentMagSize <= 0)
                StartCoroutine(ReloadWeapon());
        }

        private IEnumerator ShotDelay()
        {
            _isBetweenShot = true;
            yield return new WaitForSeconds(_shotTime);
            _isBetweenShot = false;
        }
    }
}
