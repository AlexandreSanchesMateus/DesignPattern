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

        [SerializeField, BoxGroup("Commun")] protected int _magSize;
        [SerializeField, BoxGroup("Commun")] protected int _reloadTime;


        [SerializeField, Foldout("Event")] private UnityEvent _onPickUp;
        [SerializeField, Foldout("Event")] private UnityEvent _onThrow;
        [SerializeField, Foldout("Event")] private UnityEvent _onDrop;

        [SerializeField, Foldout("Event")] private UnityEvent _onStartReloading;
        [SerializeField, Foldout("Event")] private UnityEvent _onFinishReloading;

        protected int _currentMagSize;
        protected bool _isReloading { get; private set;}

        protected Vector2 Direction;

        private void Start()
        {
            _currentMagSize = _magSize;
        }

        #region Interface IPickable
        public virtual void PickUp(Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.position = Vector2.zero;
        }

        public virtual void Drop()
        {
            gameObject.transform.SetParent(null);
        }

        public void Throw()
        {
            Drop();
        }
        #endregion

        #region Interface IWeaponable
        public abstract void PullTrigger();

        public abstract void ReleaseTrigger();

        public virtual void Reload() => StartCoroutine(ReloadWeapon());

        public virtual void SetOrientation()
        {
            
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
