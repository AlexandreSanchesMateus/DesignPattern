using UnityEngine;
using UnityEngine.Pool;
using NaughtyAttributes;

namespace Game
{
	// This component returns the particle system to the m_pool when the OnBulletStopped event is received.
	public class ReturnToPool<T> : MonoBehaviour where T : Component
	{
		[SerializeField, Required] protected T m_objectPool;
		protected IObjectPool<T> m_pool;

		public void SetPoolObject( IObjectPool<T> _newPool)
		{
			m_pool = _newPool;
		}

		protected void ReleasePoolObject ()
		{
			m_pool.Release(m_objectPool);
		}
	}
}