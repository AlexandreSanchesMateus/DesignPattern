using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DistanceAIBrain : MonoBehaviour
    {
        [SerializeField, BoxGroup("Special Dependency")] PlayerReference _playerEntity;

        [SerializeField, BoxGroup("Dependencies")] private EnemyEntity _root;
        [SerializeField, BoxGroup("Dependencies")] private EntityMovement _movement;
        [SerializeField, BoxGroup("Dependencies")] private GameObject _weaponObject;

        private IWeaponable _weapon;

        [SerializeField, BoxGroup("Conf")] private float _stopDistance;
        [SerializeField, BoxGroup("Conf")] private float _shootingTime;
        [SerializeField, BoxGroup("Conf")] private float _aroundShootingTime;

        bool IsPlayerTooNear => Vector3.Distance(_root.transform.position, _playerEntity.Instance.transform.position) < _stopDistance;

        private EAIStat _currentState;
        private EAIStat _previousState;

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_root.transform.position, _stopDistance);
        }
#endif

        private enum EAIStat
        {
            MOVING,
            SHOOTING
        }

        private void Start()
        {
            _currentState = EAIStat.MOVING;
            _previousState = _currentState;

            _weapon = _weaponObject.GetComponent<IWeaponable>();
        }

        private void Update()
        {
            switch (_currentState)
            {
                case EAIStat.MOVING:
                    if (IsPlayerTooNear)
                    {
                        _currentState = EAIStat.SHOOTING;
                        break;
                    }

                    _movement.MoveToward(_playerEntity.Instance.transform);
                    break;

                case EAIStat.SHOOTING:

                    if (_previousState == _currentState) break;

                    _previousState = _currentState;
                    _movement.Move(Vector2.zero);
                    StartCoroutine(Shooting());
                    break;
            }
        }

        private IEnumerator Shooting()
        {
            _weapon.PullTrigger();
            float shootingTime = Random.Range(_shootingTime - _aroundShootingTime, _shootingTime + _aroundShootingTime);

            while (shootingTime > 0)
            {
                shootingTime -= Time.deltaTime;
                _weapon.SetOrientation((_playerEntity.Instance.transform.position - _root.transform.position).normalized);
                yield return null;
            }

            _weapon.ReleaseTrigger();
            _weapon.Reload();
            yield return new WaitForSeconds(2);
            _currentState = EAIStat.MOVING;
        }
    }
}
