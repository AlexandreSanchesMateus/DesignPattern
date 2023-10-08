using NaughtyAttributes;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

namespace Game
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private Entity _entityRef;

        [SerializeField, Foldout("Events")] private UnityEvent _onTakeDamage;
        [SerializeField, Foldout("Events")] private UnityEvent _onRegen;
        [SerializeField, Foldout("Events")] private UnityEvent _onDie;

        public event UnityAction OnTakeDamager { add => _onTakeDamage.AddListener(value); remove => _onTakeDamage.RemoveListener(value); }
        public event UnityAction OnRegen { add => _onRegen.AddListener(value); remove => _onRegen.RemoveListener(value); }
        public event UnityAction OnDie { add => _onDie.AddListener(value); remove => _onDie.RemoveListener(value); }

        private Sequence _takeDamageSequence;

        /// <summary>
		/// coucou
		/// </summary>
		public int CurrentHealth
        {
            get;
            private set;
        }
        public bool IsDead => CurrentHealth <= 0;
        public int MaxHealth { get => _maxHealth; }


        public void TakeDamage(int amount)
        {
            Assert.IsTrue(amount >= 0);
            if (IsDead) return;

            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            _onTakeDamage?.Invoke();

            DamageEffect();

			if (IsDead)
                InternalDie();
        }

        public void Regen(int amount)
        {
            Assert.IsTrue(amount >= 0);
            if (IsDead) return;
            InternalRegen(amount);
        }

        public void Kill()
        {
            if (IsDead) return;
            InternalDie();
        }

        public void Revive()
        {
            CurrentHealth = _maxHealth;
        }

        void InternalRegen(int amount)
        {
            Assert.IsTrue(amount >= 0);

            var old = CurrentHealth;
            CurrentHealth = Mathf.Min(_maxHealth, CurrentHealth + amount);
            _onRegen?.Invoke();
        }

        void InternalDie()
        {
            CurrentHealth = 0;
            _onDie?.Invoke();
        }

        private void DamageEffect()
        {
            if (_takeDamageSequence.IsActive())
                _takeDamageSequence.Restart();
            else
            {
                _entityRef.transform.localScale = _entityRef.DefaultModelScale;
                _entityRef.SpriteRenderer.color = Color.white;
                _takeDamageSequence = DOTween.Sequence();

                _takeDamageSequence.Append(_entityRef.Model.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f));
                _takeDamageSequence.Append(_entityRef.SpriteRenderer.DOColor(Color.red, 0.1f).From(Color.white)
                    .OnComplete(() => _entityRef.SpriteRenderer.DOColor(Color.white, 0.1f)));
                _takeDamageSequence.OnKill(() => _takeDamageSequence = null);
            }
        }

        private void OnDisable()
        {
            _takeDamageSequence.Kill();
            _entityRef.transform.localScale = _entityRef.DefaultModelScale;
            _entityRef.SpriteRenderer.color = Color.white;
        }
    }
}
