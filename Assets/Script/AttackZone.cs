using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AttackZone : MonoBehaviour
    {
        HashSet<IHealth> _inZone;

        public IEnumerable<IHealth> InZone => _inZone;

        private void Awake()
        {
            _inZone = new();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IHealth h))
            {
                _inZone.Add(h);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IHealth>(out IHealth h))
            {
                _inZone.Remove(h);
            }
        }
    }
}
