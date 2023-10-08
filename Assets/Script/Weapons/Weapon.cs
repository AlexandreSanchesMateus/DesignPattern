using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace Game.Weapon
{
    public abstract class Weapon : MonoBehaviour, IPickable, IWeaponable
    {
        [SerializeField, BoxGroup("Set up")] protected Transform _firePoint;
        [SerializeField, BoxGroup("Set up")] private Rigidbody2D _rb;
        [SerializeField, BoxGroup("Set up")] private Collider2D _collider;
        [SerializeField, BoxGroup("Set up")] private SpriteRenderer _render;
        [SerializeField, BoxGroup("Set up")] protected GameObject m_model;

        [SerializeField, BoxGroup("Commun")] private int _magSize;
        [SerializeField, BoxGroup("Commun")] private float _reloadTime;
        [SerializeField, BoxGroup("Commun")] private float _throwForce;

        [SerializeField, Foldout("Event")] protected UnityEvent _onPickUp;
        [SerializeField, Foldout("Event")] protected UnityEvent _onThrow;
        [SerializeField, Foldout("Event")] protected UnityEvent _onDrop;

        [SerializeField, Foldout("Event")] protected UnityEvent _onStartReloading;
        [SerializeField, Foldout("Event")] protected UnityEvent _onFinishReloading;

        protected int _currentMagSize;
        protected bool _isReloading { get; private set;}

        protected Vector2 Direction { get; private set; }

        private Tween _idleAnim;
        private Vector3 _defaultScale;
        private Tween _reloadAnim;

		protected virtual void Start()
        {
            _currentMagSize = _magSize;
            _defaultScale = this.transform.localScale;
            WaitingPickup();
        }

        private void OnEnable()
        {
            _currentMagSize = _magSize;
            _isReloading = false;
        }

        #region Interface IPickable
        public virtual void PickUp(Transform parent)
        {
	        KillTweenOnWeapon();

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

            WaitingPickup();
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

            // Update SpriteRenderer
            if (Vector2.Dot(direction, Vector2.right) >= 0)
                _render.flipY = false;
            else
                _render.flipY = true;
        }
        #endregion

        protected virtual IEnumerator ReloadWeapon()
        {
            _onStartReloading?.Invoke();
            _isReloading = true;

            _reloadAnim = m_model.transform.DOLocalRotate(new Vector3(360, 360, 0), _reloadTime, RotateMode.FastBeyond360)
                .SetRelative(true).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(_reloadTime);
            _currentMagSize = _magSize;
            _isReloading = false;
            _onFinishReloading?.Invoke();
        }

        private void WaitingPickup ()
        {
	        _idleAnim = m_model.transform.DOScale(1.2f, 1).From(_defaultScale).SetLoops(-1, LoopType.Yoyo);
        }

        private void KillTweenOnWeapon ()
        {
	        _idleAnim.Kill();
	        _reloadAnim.Kill();
			m_model.transform.DOScale(_defaultScale, 0.2f);
        }

        private void OnDestroy()
        {
	        _idleAnim.Kill();
	        _reloadAnim.Kill();
		}
	}
}
