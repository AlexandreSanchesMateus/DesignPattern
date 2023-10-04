using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Weapon
{
    public abstract class Weapon : MonoBehaviour, IPickable, IWeaponable
    {
        [SerializeField, BoxGroup("Set up")] protected Transform _firePoint;
        [SerializeField, BoxGroup("Set up")] protected Rigidbody2D _rb;
        [SerializeField, BoxGroup("Set up")] protected Collider2D _collider;
        [SerializeField, BoxGroup("Set up")] protected Sprite _render;

        [SerializeField, BoxGroup("Commun")] protected int _magSize;
        [SerializeField, BoxGroup("Commun")] protected int _reloadTime;
        [SerializeField, BoxGroup("Commun")] protected float _throwForce;


        [SerializeField, Foldout("Event")] private UnityEvent _onPickUp;
        [SerializeField, Foldout("Event")] private UnityEvent _onThrow;
        [SerializeField, Foldout("Event")] private UnityEvent _onDrop;

        [SerializeField, Foldout("Event")] private UnityEvent _onStartReloading;
        [SerializeField, Foldout("Event")] private UnityEvent _onFinishReloading;

        protected int _currentMagSize;
        protected bool _isReloading { get; private set;}

        protected Vector2 Direction { get; private set; }

        private void Start()
        {
            _currentMagSize = _magSize;
        }

        #region Interface IPickable
        public virtual void PickUp(Transform parent)
        {
            _collider.enabled = false;
            _rb.simulated = false;
            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = Vector2.zero;

            if (_currentMagSize <= 0)
                StartCoroutine(ReloadWeapon());
        }

        public virtual void Drop()
        {
            _collider.enabled = true;
            _rb.simulated = true;
            _isReloading = false;
            gameObject.transform.SetParent(null);

            StopAllCoroutines();
            CancelInvoke();
        }

        public void Throw()
        {
            Drop();
            _rb.AddForce(Direction * _throwForce);
            _rb.AddTorque(Random.Range(-30, 30));
        }
        #endregion

        #region Interface IWeaponable
        public abstract void PullTrigger();
        public abstract void ReleaseTrigger();
        public virtual void Reload()
        {
            if(!_isReloading && _currentMagSize < _magSize)
                StartCoroutine(ReloadWeapon());
        }
        public virtual void SetOrientation(Vector2 direction)
        {
            Direction = direction.normalized;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(direction, Vector2.right));

            // Flip Render
        }
        #endregion

        protected IEnumerator ReloadWeapon()
        {
            _onStartReloading?.Invoke();
            _isReloading = true;
            yield return new WaitForSeconds(_reloadTime);
            _currentMagSize = _magSize;
            _isReloading = false;
            _onFinishReloading?.Invoke();
        }
    }
}
