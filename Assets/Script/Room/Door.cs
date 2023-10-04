using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Door : MonoBehaviour
    {
        [SerializeField, BoxGroup("Dependencies"), Required] private RoomManager _manager;
        [SerializeField, BoxGroup("Dependencies"), Required] private Physics2DInteraction _trigger;

        [SerializeField] private GameObject _doorRender;

        [SerializeField, Foldout("Event")] private UnityEvent _onDoorClose;
        [SerializeField, Foldout("Event")] private UnityEvent _onDoorOpen;


        private void Start()
        {
            if (_doorRender.activeSelf)
                _doorRender.SetActive(false);

            _manager.OnRoomUnlock += OpenDoor;
            _manager.OnRoomLock += CloseDoor;
            _trigger.TriggerEnter2D += TriggerEnter;
        }

        private void OnDestroy()
        {
            _manager.OnRoomUnlock -= OpenDoor;
            _manager.OnRoomLock -= CloseDoor;
            _trigger.TriggerEnter2D -= TriggerEnter;
        }

        private void TriggerEnter(Collider2D other)
        {
            Debug.Log("bite");
            _manager.LockRoom();
        }

        public void CloseDoor()
        {
            _doorRender.SetActive(true);
            _trigger.gameObject.SetActive(false);
            _onDoorClose?.Invoke();
        }

        public void OpenDoor()
        {
            _doorRender.SetActive(false);
            _onDoorOpen?.Invoke();
        }
    }
}
