using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Entity : MonoBehaviour
    {
        [SerializeField, Required("nop")] protected Health _health;
        public Health Health => _health;
    }
}
