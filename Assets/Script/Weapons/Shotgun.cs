using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapon
{
    public class Shotgun : Weapon
    {
        [SerializeField, BoxGroup("Shotgun Spec")] private float _shotTime;
        [SerializeField, BoxGroup("Shotgun Spec")] private float _muzzelAngle;
        [SerializeField, BoxGroup("Shotgun Spec")] private int _paletsNb;

        [SerializeField] private BulletPool _bulletPool;

        [SerializeField, Foldout("Event")] private UnityEvent _onShoot;
        [SerializeField, Foldout("Event")] private UnityEvent _onShellReload;
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
            _onTriggerRelease?.Invoke();
        }

        private void InstanceBullet()
        {
            for (int i = 0; i < _paletsNb; i++)
            {
                Bullet bullet = _bulletPool.Pool.Get();

                float degree = Random.Range(Vector2.SignedAngle(Direction, Vector2.up) - _muzzelAngle / 2, Vector2.SignedAngle(Direction, Vector2.up) + _muzzelAngle / 2);
                Vector2 newAngle = new Vector2(Mathf.Sin(degree * Mathf.Deg2Rad), Mathf.Cos(degree * Mathf.Deg2Rad)); ;
                bullet.Init(_firePoint.transform.position, newAngle, 400);
            }
            _onShoot.Invoke();

            if (--_currentMagSize <= 0)
                StartCoroutine(ReloadWeapon());
        }

        private IEnumerator ShotDelay()
        {
            _isBetweenShot = true;
            yield return new WaitForSeconds(_shotTime);
            _onShellReload?.Invoke();
            _isBetweenShot = false;
        }
    }
}
