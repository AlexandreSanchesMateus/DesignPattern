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

	    public ObjectPool ObjectPool => m_objectPool;
		[SerializeField] private ObjectPool m_objectPool;


        // Just for test 
        void Update()
        {
			transform.position += Vector3.up * Time.deltaTime;
        }

		void OnCollisionEnter2D ( Collision2D _collision )
		{
			onBulletHit?.Invoke();
		}
	}
}
