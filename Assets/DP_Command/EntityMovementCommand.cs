using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class EntityMovementCommand : MonoBehaviour, IcommandMovement
    {
        EntityMovement _entityMovement;
 
        Vector2 _pos;
        public EntityMovementCommand(Vector2 pos)
        {
            //_entityMovement = entityMovement;
            _pos = pos;
        }
        public Vector2 Execute(Vector2 position)
        {
            //Debug.Log("exe");
            return position;
        }
        public Vector3 Undo()
        {
            return _pos;
        }
    }
}
