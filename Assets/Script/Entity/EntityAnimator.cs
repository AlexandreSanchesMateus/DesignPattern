using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    [SerializeField, Required, BoxGroup("Dependencies")] Animator _animator;

    [SerializeField, Required, BoxGroup("Dependencies")] EntityMovement _movement;

    [BoxGroup("Animator Param")]
    [SerializeField, AnimatorParam(nameof(_animator), AnimatorControllerParameterType.Bool)]
    int _isWalkingParam;
    [BoxGroup("Animator Param")]
    [SerializeField, AnimatorParam(nameof(_animator), AnimatorControllerParameterType.Trigger)]
    int _attackTriggerParam;

    #region Editor
#if UNITY_EDITOR
    void Reset()
    {
        _animator = GetComponent<Animator>() ?? GetComponentInChildren<Animator>();
        _movement = GetComponentInParent<Entity>().GetComponentInChildren<EntityMovement>();
    }
#endif
    #endregion

    void Start()
    {
        // Move
        _movement.OnStartWalking += MoveStart;
        _movement.OnStopWalking += MoveStop;
        // Attack
    }

    void OnDestroy()
    {
        // Move
        _movement.OnStartWalking -= MoveStart;
        _movement.OnStopWalking -= MoveStop;
    }

    void MoveStart() => _animator.SetBool(_isWalkingParam, true);
    void MoveStop() => _animator.SetBool(_isWalkingParam, false);
    void Attack() => _animator.SetTrigger(_attackTriggerParam);

}
