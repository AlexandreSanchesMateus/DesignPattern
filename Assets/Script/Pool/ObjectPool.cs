using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
	// This example spans a random number of Bullets using a pool so that old systems can be reused.
	public class ObjectPool : MonoBehaviour
	{
		[SerializeField] private Bullet m_bullet;
		
		public enum PoolType
		{
			Stack,
			LinkedList
		}

		public PoolType poolType;

		// Collection checks will throw errors if we try to release an item that is already in the pool.
		public bool collectionChecks = true;
		public int maxPoolSize = 10;

		private IObjectPool<Bullet> m_Pool;

		public IObjectPool<Bullet> Pool
		{
			get
			{
				if (m_Pool == null)
				{
					if (poolType == PoolType.Stack)
						m_Pool = new ObjectPool<Bullet>(
							CreatePooledItem,
							OnTakeFromPool,
							OnReturnedToPool,
							OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
					else
						m_Pool = new LinkedPool<Bullet>(
							CreatePooledItem,
							OnTakeFromPool,
							OnReturnedToPool,
							OnDestroyPoolObject, collectionChecks, maxPoolSize);
				}
				return m_Pool;
			}
		}

		private Bullet CreatePooledItem ()
		{
			Bullet bullet = Instantiate (m_bullet);

			// This is used to return Bullets to the pool when they have stopped.
			bullet.ReturnToPool.pool = Pool;

			return bullet;
		}

		// Called when an item is returned to the pool using Release
		private void OnReturnedToPool ( Bullet _system )
		{
			_system.gameObject.SetActive(false);
		}

		// Called when an item is taken from the pool using Get
		private void OnTakeFromPool ( Bullet _system )
		{
			_system.gameObject.SetActive(true);
		}

		// If the pool capacity is reached then any items returned will be destroyed.
		// We can control what the destroy behavior does, here we destroy the GameObject.
		private void OnDestroyPoolObject ( Bullet _system )
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
