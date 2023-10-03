using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyEntity : Entity, IPoolableObject<EnemyEntity>
    {
		[Header("References")]
		[SerializeField, Required] private ReturnEnemyToPool m_returnToPool;
		public ReturnToPool<EnemyEntity> ReturnToPool => m_returnToPool;

		public EnemyPool BulletPool => m_enemyPool;
		[SerializeField, Required] private EnemyPool m_enemyPool;
	}
}
