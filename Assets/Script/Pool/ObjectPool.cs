using UnityEngine;
using UnityEngine.Pool;
using NaughtyAttributes;

namespace Game
{
	public class BulletPool : ObjectPool<Bullet>
	{

	}

	// This example spans a random number of Bullets using a pool so that old systems can be reused.
	public class ObjectPool<T> : MonoBehaviour where T : Component
	{
		[SerializeField, Required] private T m_prefabToSpawn;
		
		public enum PoolType
		{
			Stack,
			LinkedList
		}

		public PoolType poolType;

		// Collection checks will throw errors if we try to release an item that is already in the pool.
		public bool collectionChecks = true;
		public int maxPoolSize = 10;

		private IObjectPool<T> m_pool;

		public IObjectPool<T> Pool
		{
			get
			{
				if (m_pool == null)
				{
					if (poolType == PoolType.Stack)
						m_pool = new UnityEngine.Pool.ObjectPool<T>(
							CreatePooledItem,
							OnTakeFromPool,
							OnReturnedToPool,
							OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
					else
						m_pool = new LinkedPool<T>(
							CreatePooledItem,
							OnTakeFromPool,
							OnReturnedToPool,
							OnDestroyPoolObject, collectionChecks, maxPoolSize);
				}
				return m_pool;
			}
		}

		private T CreatePooledItem ()
		{
			T bullet = Instantiate (m_prefabToSpawn);

			// This is used to return Bullets to the pool when they have stopped.
			//bullet.ReturnToPool.pool = Pool;

			return bullet;
		}

		// Called when an item is returned to the pool using Release
		private void OnReturnedToPool ( T _system )
		{
			_system.gameObject.SetActive(false);
		}

		// Called when an item is taken from the pool using Get
		private void OnTakeFromPool ( T _system )
		{
			_system.gameObject.SetActive(true);
		}

		// If the pool capacity is reached then any items returned will be destroyed.
		// We can control what the destroy behavior does, here we destroy the GameObject.
		private void OnDestroyPoolObject ( T _system )
		{
			Destroy(_system.gameObject);
		}

		void OnGUI ()
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
		}
	}
}
