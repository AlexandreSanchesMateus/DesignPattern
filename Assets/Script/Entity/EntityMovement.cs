using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField, BoxGroup("Dependencies")] Rigidbody2D _rb;

        [SerializeField, BoxGroup("Configuration")] float _startSpeed;

        #region Events
        [SerializeField, Foldout("Event")] UnityEvent _onStartWalking;
        [SerializeField, Foldout("Event")] UnityEvent _onContinueWalking;
        [SerializeField, Foldout("Event")] UnityEvent _onStopWalking;
        public event UnityAction OnStartWalking { add => _onStartWalking.AddListener(value); remove => _onStartWalking.RemoveListener(value); }
        public event UnityAction OnContinueWalking { add => _onContinueWalking.AddListener(value); remove => _onContinueWalking.RemoveListener(value); }
        public event UnityAction OnStopWalking { add => _onStopWalking.AddListener(value); remove => _onStopWalking.RemoveListener(value); }
        #endregion

        public Vector2 MoveDirection { get; private set; }
        Vector2 OldVelocity { get; set; }

        public Alterable<float> CurrentSpeed { get; private set; }

        MovecommandInvoker invoker;
        #region EDITOR
#if UNITY_EDITOR
        private void Reset()
        {
            _rb = GetComponentInParent<Rigidbody2D>();
            _startSpeed = 1f;
        }
#endif
        #endregion

        private void Awake()
        {
            invoker = new MovecommandInvoker();
            CurrentSpeed = new Alterable<float>(_startSpeed);

        }

        private void FixedUpdate()
        {
            // FireEvents
            if (MoveDirection.magnitude < 0.01f && OldVelocity.magnitude > 0.01f)
                _onStopWalking?.Invoke();
            else if (MoveDirection.magnitude > 0.01f && OldVelocity.magnitude < 0.01f)
                _onStartWalking?.Invoke();
            else _onContinueWalking?.Invoke();


            if (Input.GetKeyDown(KeyCode.Space))
                MoveDirection = invoker.UndoCommand();
            // Physics
            _rb.AddForce(MoveDirection * _startSpeed * Time.fixedDeltaTime, ForceMode2D.Force);

            // Keep old data
            OldVelocity = _rb.velocity;
        }

        public void Move(Vector2 direction) {
            IcommandMovement storedCommand = new EntityMovementCommand(this);
            MoveDirection=invoker.AddCommand(direction, storedCommand);
            //MoveDirection = storedCommand.Execute(direction);
        }
        public void MoveToward(Transform target) => MoveDirection = (target.position - _rb.transform.position).normalized;

        public void AlterSpeed(float factor)
        {

        }
        public void ResetSpeed()
        {

        }
    }
}
