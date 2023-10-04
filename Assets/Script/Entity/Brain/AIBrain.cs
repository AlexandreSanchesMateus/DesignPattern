using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AIBrain : MonoBehaviour
    {
        [SerializeField, BoxGroup("Special Dependency")] PlayerReference _playerEntity;

        [SerializeField, BoxGroup("Dependencies")] private EnemyEntity _root;
        [SerializeField, BoxGroup("Dependencies")] private EntityMovement _movement;
        [SerializeField, BoxGroup("Dependencies")] private EntityAttack _attack;

        [SerializeField, BoxGroup("Conf")] private float _stopDistance;
        [SerializeField, BoxGroup("Conf")] private float _attackRecover;

        bool IsPlayerTooNear => Vector3.Distance(_root.transform.position, _playerEntity.Instance.transform.position) < _stopDistance;

        private bool _haveLauchAnAttack = false;
        private float _recoverTime = 0;

        #region EDITOR
#if UNITY_EDITOR
        void Reset()
        {
            _playerEntity = null;
            _root = GetComponentInParent<EnemyEntity>();
            _movement = _root.GetComponentInChildren<EntityMovement>();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_root.transform.position, _stopDistance);
        }
#endif
        #endregion

        private void Update()
        {
            if (_haveLauchAnAttack)
            {
                _recoverTime += Time.deltaTime;

                if (_recoverTime >= _attackRecover)
                {
                    _haveLauchAnAttack = false;
                    _recoverTime = 0;
                }
            }
            else
            {
                // Attack orientation
                _attack.LookToward((_playerEntity.Instance.transform.position - _root.transform.position).normalized);

                // Attack
                if (IsPlayerTooNear)
                {
                    _haveLauchAnAttack = true;
                    _movement.Move(Vector2.zero);
                    _attack.LaunchAttack();
                }
                // Move To Player
                else
                {
                    _movement.MoveToward(_playerEntity.Instance.transform);
                }
            }
        }

        /*private void SteeringBehaviour()
        {
            float[] danger = new float[8];
            float[] interest = new float[8];

            // 16 directions
            // get interest
            

            for (int i = 0; i < interest.Length; i++)
            {
                interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
            }
        }*/
    }
}
