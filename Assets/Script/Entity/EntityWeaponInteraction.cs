using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapon
{
    public class EntityWeaponInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _handPosition;

        [SerializeField, Foldout("Event")] private UnityEvent _onWeaponOver;
        [SerializeField, Foldout("Event")] private UnityEvent _onWeaponExit;
        [SerializeField, Foldout("Event")] private UnityEvent _onChangeWeapon;

        private WeaponProxy _weaponHold = null;

        private HashSet<WeaponProxy> _previousWeapons = new HashSet<WeaponProxy>();

        public void SetOverWeapon(WeaponProxy _weaponOver)
        {
            if (_previousWeapons.Contains(_weaponOver)) return;

            if (_weaponHold != null)
                DropWeapon();

            _weaponHold = _weaponOver;
            _weaponHold.PickUp(_handPosition);
        }

        public void ExitWeapon(WeaponProxy _weaponExit)
        {
            if (_weaponExit != _weaponHold && _previousWeapons.Contains(_weaponExit))
            {
                _previousWeapons.Remove(_weaponExit);
            }
        }

        public void DropWeapon()
        {
            if (_weaponHold != null)
                _weaponHold.Drop();

            _previousWeapons.Add(_weaponHold);
            _weaponHold = null;
        }

        public void ThrowWeapon()
        {
            if (_weaponHold != null)
                _weaponHold.Throw();

            _previousWeapons.Add(_weaponHold);
            _weaponHold = null;
        }

        public void UseWeapon(bool isUsing)
        {
            if (_weaponHold != null)
            {
                if (isUsing)
                    _weaponHold.PullTrigger();
                else
                    _weaponHold.ReleaseTrigger();
            }
        }

        public void ReloadWeapon() => _weaponHold?.Reload();

        public void SetWeaponOrientation(Vector2 direction) => _weaponHold?.SetOrientation(direction);
    }
}
