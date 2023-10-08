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

        public GameObject Model => _model;
        [SerializeField] private GameObject _model;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Vector3 DefaultModelScale => _defaultModelScale;
        [SerializeField] private Vector3 _defaultModelScale;


        private void Start()
        {
            command.Instance.AddObjectToRewind(gameObject);
            _defaultModelScale = Model.transform.localScale;

            _health.Revive();
        }

        public virtual void OnEnable()
        {
            command.Instance.AddObjectToRewind(gameObject);
        }

        public virtual void OnDisable()
        {
            command.Instance.DeleteFromRewind(gameObject);
        }
    }
}
