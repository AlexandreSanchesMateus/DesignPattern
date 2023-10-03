using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ReturnEnemyToPool : ReturnToPool<EnemyEntity>
	{
        void Start()
        {
	        m_objectPool.Health.OnDie += ReleasePoolObject;
        }
    }
}
