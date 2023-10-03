using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [ExecuteAlways]
    public class RoomManager : MonoBehaviour
    {
        [SerializeField, Foldout("Event")] UnityEvent _onRoomLock;
        [SerializeField, Foldout("Event")] UnityEvent _onRoomUnlock;
        [SerializeField, Foldout("Event")] UnityEvent _onStageChange;

        public event UnityAction OnRoomLock { add => _onRoomLock.AddListener(value); remove => _onRoomLock.RemoveListener(value); }
        public event UnityAction OnRoomUnlock { add => _onRoomUnlock.AddListener(value); remove => _onRoomUnlock.RemoveListener(value); }
        public event UnityAction OnStageChange { add => _onStageChange.AddListener(value); remove => _onStageChange.RemoveListener(value); }

        [Space(3)]
        [SerializeField, Tooltip("List of points delimiting the corners of the room. They must follow each other and clockwise.")] private Transform[] _roomBounds;
        [Header("Wave Settings")]
        [Space(3)]
        [SerializeField] private EnemyPool[] _enemyPools;
        [SerializeField] private Wave[] _roomWaves;


        // ###################### Triangulation l'originale ###################### //
        // Version 1.0                                                 par Alex SM //
        
        private float _totalRoomArea;
        private Triangle[] _triangles;

        private int _currentWave;
        private int _currentEnnemiesNumber;

        [System.Serializable]
        private class Wave
        {
            public EnemyType[] _enemies;

            public void Setup(RoomManager manager)
            {
                for (int i = 0; i < _enemies.Length; i++)
                {
                    _enemies[i]._master = manager;
                }
            }
        }

        [System.Serializable]
        private class EnemyType
        {
            [Dropdown(nameof(GetPoolName))] public int _poolIndex;
            public int _nbEnemies;

            [HideInInspector] public RoomManager _master;

            public DropdownList<int> GetPoolName()
            {
                DropdownList<int> result = new();
                bool isEmpty = true;
                for (int i = 0; i < _master._enemyPools.Length; i++)
                {
                    if (_master._enemyPools[i] == null) continue;

                    result.Add(_master._enemyPools[i].gameObject.name, i);
                    isEmpty = false;
                }

                if (isEmpty)
                    result.Add("No Ennemie Pool", -1);

                return result;
            }
        }

        [System.Serializable]
        private struct Triangle
        {
            public Vector2 _corner1;
            public Vector2 _corner2;
            public Vector2 _corner3;

            public Triangle(Vector2 corn1, Vector2 corn2, Vector2 corn3)
            {
                _corner1 = corn1;
                _corner2 = corn2;
                _corner3 = corn3;
            }

            public float TriangleArea() => Vector3.Cross(_corner2 - _corner1, _corner3 - _corner1).magnitude / 2f;

            public Vector2 RandomPositionInTriangle()
            {
                float r1 = Mathf.Sqrt(Random.Range(0f, 1f));
                float r2 = Random.Range(0f, 1f);

                float m1 = 1 - r1;
                float m2 = r1 * (1 - r2);
                float m3 = r2 * r1;

                return (m1 * _corner1) + (m2 * _corner2) + (m3 * _corner3);
            }
        }

        private void OnValidate()
        {
            if (_roomWaves == null) return;

            for (int i = 0; i < _roomWaves.Length; i++)
            {
                _roomWaves[i].Setup(this);
            }
        }

        private void Start()
        {
            CompileRoom();
            _currentWave = -1;
        }

        [Button]
        private void CompileRoom()
        {
            Color[] col = new[] { Color.red, Color.blue, Color.white, Color.green };

            // Triangulation
            _triangles = new Triangle[_roomBounds.Length - 2];
            List<Transform> vertices = new List<Transform>(_roomBounds);

            for (int i = 0; i < _triangles.Length; i++)
            {
                for (int k = 0; k < vertices.Count; k++)
                {
                    Transform previous = GetPrevious(k);
                    Transform next = GetNext(k);

                    // Angle aigu
                    if (Vector2.Dot(previous.position - vertices[k].position, next.position - vertices[k].position) >= 0)
                    {
                        bool valid = true;
                        for (int j = 0; j < vertices.Count; j++)
                        {
                            if (vertices[j] == vertices[k] || vertices[j] == previous || vertices[j] == next) continue;

                            // Inclusion dans le triangle
                            if (Vector2.Dot(Vector2.Perpendicular(vertices[k].position - previous.position), vertices[j].position - vertices[k].position) <= 0 && Vector2.Dot(Vector2.Perpendicular(next.position - vertices[k].position), vertices[j].position - next.position) <= 0
                                && Vector2.Dot(Vector2.Perpendicular(previous.position - next.position), vertices[j].position - previous.position) <= 0)
                            {
                                valid = false;
                                break;
                            }
                        }

                        if (valid)
                        {
                            // vérifier que aucun point est dans ce triangle
                            _triangles[i] = new Triangle(previous.position, vertices[k].position, next.position);

                            if (Application.isEditor)
                            {
                                Debug.DrawLine(_triangles[i]._corner1, _triangles[i]._corner2, col[i % 4], 5);
                                Debug.DrawLine(_triangles[i]._corner2, _triangles[i]._corner3, col[i % 4], 5);
                                Debug.DrawLine(_triangles[i]._corner3, _triangles[i]._corner1, col[i % 4], 5);
                            }

                            vertices.RemoveAt(k);
                            break;
                        }
                    }
                }
            }

            // Somme des triangles
            _totalRoomArea = 0;
            for (int i = 0; i < _triangles.Length; i++)
            {
                _totalRoomArea += _triangles[i].TriangleArea();
            }

            Transform GetPrevious(int index)
            {
                if (index > 0)
                    return vertices[index - 1];

                return vertices[vertices.Count - 1];
            }

            Transform GetNext(int index)
            {
                if (index < vertices.Count - 1)
                    return vertices[index + 1];

                return vertices[0];
            }
        }

        private Vector2 GetRandomPositionInRoom()
        {
            float rng = Random.Range(0f, _totalRoomArea);

            int index = 0;
            for (; index < _triangles.Length - 1; ++index)
            {
                float trArea = _triangles[index].TriangleArea();

                if (rng < trArea)
                {
                    break;
                }

                rng -= trArea;
            }

            return _triangles[index].RandomPositionInTriangle();
        }

        public void LockRoom()
        {
            _onRoomLock?.Invoke();
            StartNextWave();
        }

        private void UnlockRoom()
        {
            _onRoomUnlock?.Invoke();
        }

        private void StartNextWave()
        {
            if (++_currentWave < _roomWaves.Length)
            {
                _currentEnnemiesNumber = 0;
                foreach (EnemyType ennemies in _roomWaves[_currentWave]._enemies)
                {
                    for (int i = 0; i < ennemies._nbEnemies; i++)
                    {
                        GameObject instance = _enemyPools[ennemies._poolIndex].Pool.Get().gameObject;
                        // Set ennemi manager
                        instance.transform.position = GetRandomPositionInRoom();
                    }
 
                   _currentEnnemiesNumber += ennemies._nbEnemies;
                }
            }
            else
                UnlockRoom();
        }

        public void CheckRemainingEnemies()
        {
            if(--_currentEnnemiesNumber <= 0)
            {
                StartNextWave();
            }
        }
    }
}
