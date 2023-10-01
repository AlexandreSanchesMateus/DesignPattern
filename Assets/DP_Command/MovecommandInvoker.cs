using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;
using UnityEngine.Rendering;
namespace Game
{
    class RewindObjects
    {
        public GameObject gameObjectToRewind;
        public Stack<IcommandMovement> _commandList = new Stack<IcommandMovement>();
    }
    [Serializable]
    public class MovecommandInvoker
    {
        private List<RewindObjects> _objectsToRewind= new List<RewindObjects>();
        IcommandMovement _onCommand;
        bool _canUndo= true;
        public MovecommandInvoker()
        {
            //_commandList = new Stack<IcommandMovement>();
        }

        public void AddCommand(GameObject objectToSave, IcommandMovement newCommand)
        {

            _objectsToRewind.First( x => x.gameObjectToRewind)
                ._commandList.Push(newCommand);
            newCommand.Execute(objectToSave.transform.position);
            Debug.Log("addlist");
            //return;
        }
        public void UndoCommand()
        {

            foreach (var item in _objectsToRewind)
            {

                if (item._commandList.Count > 0)
                {
                    if (_canUndo)
                    {
                        _canUndo = false;
                        IcommandMovement lastesCommand = item._commandList.Pop();
                        item.gameObjectToRewind.transform.DOMove(lastesCommand.Undo(), 0.15f).SetEase(Ease.Flash).OnComplete(() => _canUndo = true);
                        Debug.Log(item._commandList.Count);
                        Debug.Log("undo");
                    }
                    //item.gameObjectToRewind.transform.position = lastesCommand.Undo();
                    //return lastesCommand.Undo();
                }
            }
            //return Vector3.zero;
        }
        public void AddObjectToRewind(GameObject objectToAdd)
        {
            RewindObjects newRewindObject = new RewindObjects();
            newRewindObject.gameObjectToRewind = objectToAdd;
            _objectsToRewind.Add(newRewindObject);
        }
    }
}
