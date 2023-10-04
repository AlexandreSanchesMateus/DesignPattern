using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapon;

namespace Game
{
    public class DistanceAIBrain : MonoBehaviour
    {
        [SerializeField, BoxGroup("Special Dependency")] PlayerReference _playerEntity;

        [SerializeField, BoxGroup("Dependencies")] private EnemyEntity _root;
        [SerializeField, BoxGroup("Dependencies")] private EntityMovement _movement;
        [SerializeField, BoxGroup("Dependencies")] private  EntityWeaponInteraction _weaponInteraction;

        [SerializeField] private float _shootingTime;
        [SerializeField] private float _aroundShootingTime;

        [SerializeField, BoxGroup("Conf")] private float _stopDistance;
        [SerializeField, BoxGroup("Conf")] private float _attackRecover;

        bool IsPlayerTooNear => Vector3.Distance(_root.transform.position, _playerEntity.Instance.transform.position) < _stopDistance;

        private void Update()
        {
            _weaponInteraction.SetWeaponOrientation((_playerEntity.Instance.transform.position - _root.transform.position).normalized);

            if (IsPlayerTooNear)
            {
                _movement.Move(Vector2.zero);
            }
            // Move To Player
            else
            {
                _movement.MoveToward(_playerEntity.Instance.transform);
            }
        }
    }
}
