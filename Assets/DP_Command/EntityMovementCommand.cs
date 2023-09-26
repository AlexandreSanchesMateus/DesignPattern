using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Game
{
    public class EntityMovementCommand : MonoBehaviour, IcommandMovement
    {
        EntityMovement _entityMovement;
        Vector2 _direction;
        Vector3 _pos;
        public EntityMovementCommand(EntityMovement entityMovement)
        {
            _entityMovement = entityMovement;
            _direction = -entityMovement.MoveDirection;
            _pos = entityMovement.transform.position;
        }
        public Vector2 Execute(Vector2 direction)
        {
            return direction.normalized;
        }
        public Vector3 Undo()
        {

            return _pos;
            //throw new System.NotImplementedException();
        }
    }
}
