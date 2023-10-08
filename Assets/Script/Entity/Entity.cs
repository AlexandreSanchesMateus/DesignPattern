using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Entity : MonoBehaviour
    {
        [SerializeField, Required("nop")] protected Health _health;
        [field:SerializeField]public CommandReference command { get; set; }
 
        public Health Health => _health;

        private void OnEnable()
        {
            // Invoker = Command.Instance;
            command.Instance.AddObjectToRewind(gameObject);
        }
        private void Start()
        {
            // Invoker = Command.Instance;
            command.Instance.AddObjectToRewind(gameObject);
        }

        private void OnDisable()
        {
            command.Instance.DeleteFromRewind(gameObject);
        }
    }
}
