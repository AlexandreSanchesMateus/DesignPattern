using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    public class WeaponProxy : MonoBehaviour, IPickable, IWeaponable
    {
        [SerializeField, Required] private Weapon _target;

        public void Drop() => _target.Drop();
        public void PickUp(Transform parent) => _target.PickUp(parent);
        public void PullTrigger() => _target.PullTrigger();


        public void ReleaseTrigger() => _target.ReleaseTrigger();
        public void Reload() => _target.Reload();
        public void SetOrientation(Vector2 direction) => _target.SetOrientation(direction);
        public void Throw() => _target.Throw();
    }
}
