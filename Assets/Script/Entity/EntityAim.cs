using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapon;

namespace Game
{
    public class EntityAim : MonoBehaviour
    {
        public bool _useMousePosition { get; private set; }

        [SerializeField, BoxGroup("Dependencie")] private EntityWeaponInteraction _weaponInteraction;

        [SerializeField] private GameObject _renderAimPosition;
        [SerializeField] private GameObject _pivotAimDirection;

        public void SetAimPosition(Vector2 position)
        {
            if (!_useMousePosition)
            {
                _useMousePosition = true;
                _pivotAimDirection.transform.rotation = Quaternion.Euler(Vector3.zero);
            }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(position);
            _renderAimPosition.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            _weaponInteraction.SetWeaponOrientation(mousePosition - new Vector2(transform.position.x, transform.position.y));
        }

        public void SetAimDirection(Vector2 direction)
        {
            if (_useMousePosition)
            {
                _useMousePosition = false;
                _renderAimPosition.transform.localPosition = new Vector3(0,3,0);
            }

            _pivotAimDirection.transform.rotation = Quaternion.Euler(0, 0, - Vector2.SignedAngle(direction, Vector2.up));
            _weaponInteraction.SetWeaponOrientation(direction);
        }
    }
}
