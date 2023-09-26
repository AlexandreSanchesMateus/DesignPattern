using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EntityAim : MonoBehaviour
    {
        public bool _useMousePosition { get; private set; }

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
        }

        public void SetAimDirection(Vector2 direction)
        {
            if (_useMousePosition)
            {
                _useMousePosition = false;
                _renderAimPosition.transform.localPosition = Vector3.up;
            }

            _pivotAimDirection.transform.rotation = Quaternion.Euler(0,0,Vector3.Angle(direction, Vector3.up));
        }
    }
}
