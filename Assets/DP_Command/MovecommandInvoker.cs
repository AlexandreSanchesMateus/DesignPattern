using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

namespace Game
{
    public class MovecommandInvoker
    {
        Stack<IcommandMovement> _commandList;
        IcommandMovement _onCommand;

        public MovecommandInvoker()
        {
            _commandList = new Stack<IcommandMovement>();
        }

        public Vector2 AddCommand(Vector2 pos,IcommandMovement newCommand)
        {
            _commandList.Push(newCommand);
            return newCommand.Execute(pos);
        }
        public Vector3 UndoCommand()
        {

            if (_commandList.Count > 0)
            {
                IcommandMovement lastesCommand = _commandList.Pop();
                return lastesCommand.Undo();
            }
            return Vector3.zero;
        }
    }
}
