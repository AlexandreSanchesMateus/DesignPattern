using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ReturnBulletToPool : ReturnToPool<Bullet>
    {
        void Start()
        {
            m_objectPool.onBulletHit += () => pool.Release(m_objectPool);
        }
    }
}
