using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
    public class EnemyEntity : Entity, IPoolableObject<EnemyEntity>
    {
		[SerializeField, BoxGroup("References"), Required] private ReturnEnemyToPool m_returnToPool;
		[SerializeField, BoxGroup("References"), Required] private EnemyPool m_enemyPool;

		public ReturnToPool<EnemyEntity> ReturnToPool => m_returnToPool;
		public EnemyPool EnnemyPool => m_enemyPool;

		public void DeathFeedBack(Action feedBack)
        {
            _health.OnDie += feedBack;
        }
    }
}
