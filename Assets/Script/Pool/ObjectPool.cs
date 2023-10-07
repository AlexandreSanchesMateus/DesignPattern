using UnityEngine;
using UnityEngine.Pool;
using NaughtyAttributes;
using System;

namespace Game
{
	// This example spans a random number of Bullets using a m_pool so that old systems can be reused.
	public class ObjectPool<T> : MonoBehaviour where T : Component, IPoolableObject<T>
	{
		[SerializeField, Required] protected T m_prefabToSpawn;

		[SerializeField]
		private enum PoolType
		{
			Stack,
			LinkedList
		}

		[SerializeField] private PoolType m_poolType;

		// Collection checks will throw errors if we try to release an item that is already in the m_pool.
		[SerializeField] private bool m_collectionChecks = true;
		[SerializeField] private int m_defaultPoolSize = 10;
		[SerializeField] private int m_maxPoolSize = 10;

		private Transform m_bulletHolder;
		protected IObjectPool<T> m_pool;

		public IObjectPool<T> Pool
		{
			get
			{
				if (m_pool == null)
				{
					m_bulletHolder = new GameObject("Bullet Holder").transform;

					//if (m_poolType == PoolType.Stack)
					{
						m_pool = new UnityEngine.Pool.ObjectPool<T>(
							CreatePooledItem,
							OnTakeFromPool,
							OnReturnedToPool,
							OnDestroyPoolObject, m_collectionChecks, m_defaultPoolSize, m_maxPoolSize);
					}
					//else
					//	m_pool = new LinkedPool<T>(
					//		CreatePooledItem,
					//		OnTakeFromPool,
					//		OnReturnedToPool,
					//		OnDestroyPoolObject, m_collectionChecks, m_maxPoolSize);
				}
				return m_pool;
			}
		}

		private T CreatePooledItem ()
		{
			T system = Instantiate (m_prefabToSpawn, m_bulletHolder);
			system.ReturnToPool.SetPoolObject(Pool);
			system.OnObjectCreatedForPool();
			return system;
		}

		// Called when an item is returned to the m_pool using Release
		private void OnReturnedToPool ( T system )
		{
			system.gameObject.SetActive(false);
			system.OnObjectReturnToPool();
		}

		// Called when an item is taken from the m_pool using Get
		private void OnTakeFromPool ( T system )
		{
			system.gameObject.SetActive(true);
			system.OnObjectGetFromPool();
		}

		// If the m_pool capacity is reached then any items returned will be destroyed.
		// We can control what the destroy behavior does, here we destroy the GameObject.
		private void OnDestroyPoolObject ( T system )
		{
			Destroy(system.gameObject);
		}

		/*void OnGUI ()
		{
			GUILayout.Label("Pool size: " + Pool.CountInactive);
			if (GUILayout.Button("Create Particles"))
			{
				var amount = Random.Range(1, 10);
				for (int i = 0; i < amount; ++i)
				{
					var ps = Pool.Get();
					ps.transform.position = Random.insideUnitSphere * 10;
					//ps.Play();
				}
			}
		}*/
	}
}
