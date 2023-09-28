using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    public class WeaponInteractionProxy : MonoBehaviour
    {
        [SerializeField, Required] private EntityWeaponInteraction _target;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision != null)
            {
                WeaponProxy proxy = collision.GetComponent<WeaponProxy>();
                if (proxy)
                {
                    _target.SetOverWeapon(proxy);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision != null)
            {
                WeaponProxy proxy = collision.GetComponent<WeaponProxy>();
                if (proxy)
                {
                    _target.ExitWeapon(proxy);
                }
            }
        }
    }
}
