using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(ReturnEnemyToPool))]
	public class EnemyPool : ObjectPool<EnemyEntity>
	{
        protected override void OnTakeFromPool(EnemyEntity _system)
        {
            base.OnTakeFromPool(_system);
            m_prefabToSpawn.Health.Revive(true);
        }
    }
}
