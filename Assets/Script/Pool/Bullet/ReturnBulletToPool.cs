using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(Bullet))]
	[RequireComponent(typeof(BulletPool))]
	public class ReturnBulletToPool : ReturnToPool<Bullet>
    {
        void Start()
        {
            m_objectPool.onBulletHit += ReleasePoolObject;
        }
    }
}
