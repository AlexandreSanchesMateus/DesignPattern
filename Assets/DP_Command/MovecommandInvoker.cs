using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using DG.Tweening;
using UnityEngine.Rendering;
using System.Collections;

namespace Game
{
    class RewindObjects
    {
        public GameObject gameObjectToRewind;
        public Stack<IcommandMovement> _commandList = new Stack<IcommandMovement>();
    }
    [Serializable]
    public class MovecommandInvoker:MonoBehaviour
    {
        public static MovecommandInvoker instance = new MovecommandInvoker();
        private List<RewindObjects> _objectsToRewind= new List<RewindObjects>();
        IcommandMovement _onCommand;
        bool _canUndo= true;
        bool _isRewinding;
        //public MovecommandInvoker()
        //{
        //    //_commandList = new Stack<IcommandMovement>();
        //}
        private void Awake()
        {
            if(instance==null)
                instance= this;
            else
                Destroy(gameObject);
        }
        public void AddCommand(GameObject objectToSave, IcommandMovement newCommand)
        {

            _objectsToRewind.First( x => x.gameObjectToRewind== objectToSave)
                ._commandList.Push(newCommand);
            newCommand.Execute(objectToSave.transform.position);
            Debug.Log("addlist");
            //return;
        }


        public void UndoCommand(bool input)
        {
            
            _isRewinding = input;
            StartCoroutine(RewindCouro());
            IEnumerator RewindCouro()
            {
                yield return new WaitForSeconds(0.1f);
                foreach (var item in _objectsToRewind)
                {
                    Debug.Log("rewinding" + item.gameObjectToRewind.name+ item._commandList.Count);
                    if (item._commandList.Count > 0)
                    {
                        if (_canUndo)
                        {
                            _canUndo = false;
                            IcommandMovement lastesCommand = item._commandList.Pop();
                            item.gameObjectToRewind.transform.DOMove(lastesCommand.Undo(), 0.15f).SetEase(Ease.Flash).OnComplete(() => _canUndo = true);

                        }

                    }
                }
                if(_isRewinding)
                    StartCoroutine(RewindCouro());
            }
        }
        //public void UndoCommand()
        //{
            
        //    foreach (var item in _objectsToRewind)
        //    {

        //        if (item._commandList.Count > 0)
        //        {
        //            if (_canUndo)
        //            {
        //                _canUndo = false;
        //                IcommandMovement lastesCommand = item._commandList.Pop();
        //                item.gameObjectToRewind.transform.DOMove(lastesCommand.Undo(), 0.15f).SetEase(Ease.Flash).OnComplete(() => _canUndo = true);

        //            }

        //        }
        //    }
        //    //return Vector3.zero;
        //}
        public void AddObjectToRewind(GameObject objectToAdd)
        {
            RewindObjects newRewindObject = new RewindObjects();
            newRewindObject.gameObjectToRewind = objectToAdd;
            _objectsToRewind.Add(newRewindObject);
        }
    }
}
