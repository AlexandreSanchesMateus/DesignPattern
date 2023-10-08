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

        private RoomManager _manager;

        public override void OnEnable()
        {
            base.OnEnable();
            _health.OnDie += ReportToRoomManager;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _health.OnDie -= ReportToRoomManager;
        }

        public void SetRoomManager(RoomManager manager) => _manager = manager;
        
        private void ReportToRoomManager()
        {
            _manager.CheckRemainingEnemies();
            _manager = null;
        }

        // Object Pool
        public void OnObjectCreatedForPool()
        {
            
        }

        public void OnObjectGetFromPool()
        {
            _health.Revive();
        }

        public void OnObjectReturnToPool()
        {
            
        }
    }
}
