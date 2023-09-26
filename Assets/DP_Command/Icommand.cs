using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IcommandMovement
    {
        Vector2 Execute(Vector2 direction);
        Vector2 Undo();
    }
}
