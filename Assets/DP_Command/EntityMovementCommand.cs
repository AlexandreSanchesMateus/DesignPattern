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
        public EntityMovementCommand(EntityMovement entityMovement)
        {
            _entityMovement = entityMovement;
            _direction = entityMovement.MoveDirection;
        }
        public Vector2 Execute(Vector2 direction)
        {
            return direction.normalized;
        }
        public Vector2 Undo()
        {

            return _direction;
            //throw new System.NotImplementedException();
        }
    }
}
