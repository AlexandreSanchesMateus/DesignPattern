using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.Weapon;
using UnityEngine.Rendering;
using DG.Tweening;

namespace Game
{
    public class AnimationWait : CustomYieldInstruction
    {
        private Animation a;

        public AnimationWait(Animation a)
        {
            this.a = a;

            if (a.clip.isLooping) throw new System.Exception();

            a.Play();
        }

        public override bool keepWaiting => a.isPlaying;
    }

    public static class AnimationExtension
    {
        public static AnimationWait PlayAndWaitCompletion(this Animation @this)
            => new AnimationWait(@this);
    }



    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField, BoxGroup("Dependencies")] EntityMovement _movement;
        [SerializeField, BoxGroup("Dependencies")] EntityWeaponInteraction _weaponInteraction;
        [SerializeField, BoxGroup("Dependencies")] EntityAim _aim;
        [SerializeField, BoxGroup("Dependencies")] MovecommandInvoker _command;
        [SerializeField, BoxGroup("Dependencies")] GameObject trail;
        [SerializeField, BoxGroup("Dependencies")] private Volume volume;


        [SerializeField, BoxGroup("Input")] InputActionProperty _moveInput;

        [SerializeField, BoxGroup("Input")] InputActionProperty _aimPositionInput;
        [SerializeField, BoxGroup("Input")] InputActionProperty _aimDirectionInput;

        [SerializeField, BoxGroup("Input")] InputActionProperty _shootInput;
        [SerializeField, BoxGroup("Input")] InputActionProperty _reloadInput;
        [SerializeField, BoxGroup("Input")] InputActionProperty _dropInput;
        [SerializeField, BoxGroup("Input")] InputActionProperty _throwInput;

        [SerializeField, BoxGroup("Input")] InputActionProperty _rewindInput;

        private void Start()
        {
            Cursor.visible = false;
            // move
            _moveInput.action.started += UpdateMove;
            _moveInput.action.performed += UpdateMove;
            _moveInput.action.canceled += StopMove;

            // Weapon Interaction
            _shootInput.action.performed += UseWeapon;
            _shootInput.action.canceled += UseWeapon;
            _reloadInput.action.performed += ReloadWeapon;

            _dropInput.action.performed += DropWeapon;
            _throwInput.action.performed += ThrowWeapon;

            // AIM
            _aimDirectionInput.action.started += SetAimDirection;
            _aimDirectionInput.action.performed += SetAimDirection;

            _aimPositionInput.action.started += SetAimPosition;
            _aimPositionInput.action.performed += SetAimPosition;

            // rewind
            _rewindInput.action.performed += RewindTime;
            _rewindInput.action.canceled += StopRewindTime;
        }

        private void OnDestroy()
        {
            // Move
            _moveInput.action.started -= UpdateMove;
            _moveInput.action.performed -= UpdateMove;
            _moveInput.action.canceled -= StopMove;

            // Weapon Interaction
            _shootInput.action.performed -= UseWeapon;
            _shootInput.action.canceled -= UseWeapon;
            _reloadInput.action.performed -= ReloadWeapon;

            _dropInput.action.performed -= DropWeapon;
            _throwInput.action.performed -= ThrowWeapon;

            // AIM
            _aimDirectionInput.action.started -= SetAimDirection;
            _aimDirectionInput.action.performed -= SetAimDirection;

            _aimPositionInput.action.started -= SetAimPosition;
            _aimPositionInput.action.performed -= SetAimPosition;


            // Rewind
            _rewindInput.action.performed -= RewindTime;
            _rewindInput.action.canceled -= StopRewindTime;
        }
        private void UpdateMove(InputAction.CallbackContext obj) => _movement.Move(obj.ReadValue<Vector2>().normalized);
        private void StopMove(InputAction.CallbackContext obj) => _movement.Move(Vector2.zero);
        private void UseWeapon(InputAction.CallbackContext obj) => _weaponInteraction.UseWeapon(obj.performed);
        private void ReloadWeapon(InputAction.CallbackContext obj) => _weaponInteraction.ReloadWeapon();
        private void DropWeapon(InputAction.CallbackContext obj) => _weaponInteraction.DropWeapon();
        private void ThrowWeapon(InputAction.CallbackContext obj) => _weaponInteraction.ThrowWeapon();
        private void SetAimPosition(InputAction.CallbackContext obj) => _aim.SetAimPosition(obj.ReadValue<Vector2>());
        private void SetAimDirection(InputAction.CallbackContext obj) => _aim.SetAimDirection(obj.ReadValue<Vector2>().normalized);

        private void RewindTime(InputAction.CallbackContext obj) 
        {
            DOTween.To(() => volume.weight, x => volume.weight = x, 1, 0.2f);
            trail.SetActive(true);
            _command.UndoCommand(true);
        }
        private void StopRewindTime(InputAction.CallbackContext obj) 
        {
            DOTween.To(() => volume.weight, x => volume.weight = x, 0, 0.2f);
            trail.SetActive(false);
            _command.UndoCommand(false); 
        }
    }
}
