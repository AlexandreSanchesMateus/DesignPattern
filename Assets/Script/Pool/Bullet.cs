using System;
using UnityEngine;
using NaughtyAttributes;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
	    public event Action onBulletHit;

		public ReturnToPool ReturnToPool => m_returnToPool;
	    [SerializeField] private ReturnToPool m_returnToPool;
		
		public ObjectPool<Bullet> ObjectPool => m_objectPool;
		[SerializeField] private ObjectPool<Bullet> m_objectPool;

		public Rigidbody2D Rigidbody2D => Rigidbody2D;
		[SerializeField] private Rigidbody2D m_rigidbody2D;

		public void Init(Vector2 _posToSpawn, Vector2 _dir, float _speed, Quaternion? _rotation = null)
		{
			this.transform.position = _posToSpawn;
			this.transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(_dir, Vector2.up));

			m_rigidbody2D.AddForce(_dir.normalized * _speed);
		}

		void OnCollisionEnter2D ( Collision2D _collision )
		{
			onBulletHit?.Invoke();
		}
	}
}
