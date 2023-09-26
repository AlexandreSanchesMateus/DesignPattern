using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        // [SerializeField, BoxGroup("Dependencies")] EntityMovement _command;

        [SerializeField, BoxGroup("Input")] InputActionProperty _moveInput;

        [SerializeField, BoxGroup("Input")] InputActionProperty _aimPositionInput;
        [SerializeField, BoxGroup("Input")] InputActionProperty _aimDirectionInput;

        [SerializeField, BoxGroup("Input")] InputActionProperty _shootInput;
        [SerializeField, BoxGroup("Input")] InputActionProperty _reloadInput;
        [SerializeField, BoxGroup("Input")] InputActionProperty _dropInput;
        [SerializeField, BoxGroup("Input")] InputActionProperty _throwInput;

        private void Start()
        {
            // move
            _moveInput.action.started += UpdateMove;
            _moveInput.action.performed += UpdateMove;
            _moveInput.action.canceled += StopMove;

            // Weapon Interaction
            _shootInput.action.started += UseWeapon;
            _shootInput.action.canceled += UseWeapon;

            _reloadInput.action.started += ReloadWeapon;
            _dropInput.action.started += DropWeapon;
            _throwInput.action.started += ThrowWeapon;

            // AIM
            _aimDirectionInput.action.started += SetAimDirection;
            _aimDirectionInput.action.performed += SetAimDirection;

            _aimPositionInput.action.started += SetAimPosition;
            _aimPositionInput.action.performed += SetAimPosition;
        }

        private void OnDestroy()
        {
            // Move
            _moveInput.action.started -= UpdateMove;
            _moveInput.action.performed -= UpdateMove;
            _moveInput.action.canceled -= StopMove;

            // Weapon Interaction
            _shootInput.action.started -= UseWeapon;
            _shootInput.action.canceled -= UseWeapon;
            _reloadInput.action.started -= ReloadWeapon;
            _dropInput.action.started -= DropWeapon;
            _throwInput.action.started -= ThrowWeapon;

            // AIM
            _aimDirectionInput.action.started -= SetAimDirection;
            _aimDirectionInput.action.performed -= SetAimDirection;

            _aimPositionInput.action.started -= SetAimPosition;
            _aimPositionInput.action.performed -= SetAimPosition;
        }


        private void UpdateMove(InputAction.CallbackContext obj) => _movement.Move(obj.ReadValue<Vector2>().normalized);
        private void StopMove(InputAction.CallbackContext obj) => _movement.Move(Vector2.zero);

        private void UseWeapon(InputAction.CallbackContext obj) => _weaponInteraction.UseWeapon(obj.started);
        private void ReloadWeapon(InputAction.CallbackContext obj) => _weaponInteraction.ReloadWeapon();

        private void DropWeapon(InputAction.CallbackContext obj) => _weaponInteraction.DropWeapon();
        private void ThrowWeapon(InputAction.CallbackContext obj) => _weaponInteraction.ThrowWeapon();

        private void SetAimPosition(InputAction.CallbackContext obj) => _aim.SetAimPosition(obj.ReadValue<Vector2>());
        private void SetAimDirection(InputAction.CallbackContext obj) => _aim.SetAimPosition(obj.ReadValue<Vector2>().normalized);
    }
}
