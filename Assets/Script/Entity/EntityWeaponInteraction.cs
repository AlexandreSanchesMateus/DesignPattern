using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EntityWeaponInteraction : MonoBehaviour
    {


        [SerializeField, Foldout("Event")] private UnityEvent _onWeaponOver;
        [SerializeField, Foldout("Event")] private UnityEvent _onWeaponExit;

        public void PickUpWeapon()
        {

        }

        public void DropWeapon()
        {

        }

        public void ThrowWeapon()
        {

        }

        public void UseWeapon(bool isUsing)
        {

        }

        public void ReloadWeapon()
        {

        }
    }
}
