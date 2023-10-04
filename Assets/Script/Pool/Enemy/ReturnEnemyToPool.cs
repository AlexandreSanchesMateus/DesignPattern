using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ReturnEnemyToPool : ReturnToPool<EnemyEntity>
	{
        void OnEnable()
        {
	        m_objectPool.Health.OnDie += ReleasePoolObject;
        }
        private void OnDisable()
        {
	        m_objectPool.Health.OnDie -= ReleasePoolObject;
        }
    }
}
