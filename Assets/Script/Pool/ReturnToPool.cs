using UnityEngine;
using UnityEngine.Pool;
using NaughtyAttributes;

namespace Game
{
	// This component returns the particle system to the pool when the OnBulletStopped event is received.
	[RequireComponent(typeof(Bullet))]
	public class ReturnToPool<T> : MonoBehaviour where T : Component
	{
		[SerializeField, Required] protected T m_objectPool;
		public IObjectPool<T> pool;
	}
}