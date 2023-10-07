using System;
using UnityEngine;
using NaughtyAttributes;

namespace Game
{
	[RequireComponent(typeof(ReturnBulletToPool))]
	[RequireComponent(typeof(BulletPool))]
	public class Bullet : MonoBehaviour, IPoolableObject<Bullet>
	{
	    public event Action onBulletHit;

		[Header("References")]
	    [SerializeField, Required] private ReturnBulletToPool m_returnToPool;
		public ReturnToPool<Bullet> ReturnToPool => m_returnToPool;
		
		public BulletPool BulletPool => m_bulletPool;
		[SerializeField, Required] private BulletPool m_bulletPool;

		public Rigidbody2D Rigidbody2D => Rigidbody2D;
		[SerializeField, Required] private Rigidbody2D m_rigidbody2D;

		public void Init(Vector2 _posToSpawn, Vector2 _dir, float _speed, Quaternion? _rotation = null)
		{
			this.transform.position = _posToSpawn;
			this.transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(_dir, Vector2.up));

			m_rigidbody2D.AddForce(_dir.normalized * _speed);
		}

		void OnCollisionEnter2D ( Collision2D _collision )
		{
			_collision.gameObject.GetComponent<IHealth>()?.TakeDamage(10);
			onBulletHit?.Invoke();
		}

        public void OnObjectGetFromPool()
        {
            
        }

        public void OnObjectReturnToPool()
        {
            
        }

        public void OnObjectCreatedForPool()
        {
            
        }
    }
}
