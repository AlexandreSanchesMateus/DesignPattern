using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EntityAttack : MonoBehaviour
    {
        [SerializeField] AttackZone _attackZone;

        public event UnityAction OnAttack;

        public void LaunchAttack()
        {
            OnAttack?.Invoke();
            foreach (var el in _attackZone.InZone)
            {
                el.TakeDamage(10);
            }
        }
    }
}
