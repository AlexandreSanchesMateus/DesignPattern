using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EntityAttack : MonoBehaviour
    {
        [SerializeField] AttackZone _attackZone;

        [SerializeField, Foldout("Events")] private UnityEvent _onAttack;
        public event UnityAction OnAttack { add => _onAttack.AddListener(value); remove => _onAttack.RemoveListener(value); }

        public void LaunchAttack()
        {
            _onAttack?.Invoke();
            foreach (var el in _attackZone.InZone)
            {
                el.TakeDamage(10);
            }
        }

        public void LookToward(Vector2 direction) => _attackZone.gameObject.transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(direction, Vector2.right));
    }
}
