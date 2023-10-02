using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
	// This component returns the particle system to the pool when the OnBulletStopped event is received.
	[RequireComponent(typeof(Bullet))]
	public class ReturnToPool : MonoBehaviour
	{
		public Bullet bullet;
		public IObjectPool<Bullet> pool;

		void Start ()
		{
			bullet = GetComponent<Game.Bullet>();

			// Return to the pool
			//bullet.onBulletHit += () => pool.Release(bullet);
		}
	}
}