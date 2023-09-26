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

        //[SerializeField] private IPool pool;
        // pool of bullet


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
            // Instanciate bullet from pool
            // Move Bullet to fire point
            // Orient Bullet to Weapon Direction

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
