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
        public void AddCommand()
        {
            foreach (RewindObjects item in _objectsToRewind)
            {
                //item.gameObjectToRewind;

                IcommandMovement storedCommand = new EntityMovementCommand(item.gameObjectToRewind.transform.position);
                //Debug.Log("RECORDING" + item.gameObjectToRewind.name + item._commandList.Count+ item.gameObjectToRewind.transform.position);
                item._commandList.Push(storedCommand);

                //item._commandList.Push(storedCommand);
                storedCommand.Execute(item.gameObjectToRewind.transform.position);
            }
            //Debug.Log("addlist");
            //return;
        }


        public void UndoCommand(bool input)
        {
            
            _isRewinding = input;
            StartCoroutine(RewindCouro());
            IEnumerator RewindCouro()
            {
                yield return new WaitForSeconds(0.1f);
                if (_canUndo)
                    foreach (var item in _objectsToRewind)
                    {
                        Debug.Log("rewinding" + item.gameObjectToRewind.name+ item._commandList.Count);
                    
                    
                        if (item._commandList.Count > 0)
                        {
                            _canUndo = false;
                            IcommandMovement lastesCommand = item._commandList.Pop();
                            Sequence RewindSequence = DOTween.Sequence();
                            RewindSequence.Append(item.gameObjectToRewind.transform.DOMove(lastesCommand.Undo(), 0.15f).SetEase(Ease.Flash)).OnComplete(() => _canUndo = true);

                        }

                    
                    }
                if(_isRewinding)
                    StartCoroutine(RewindCouro());
            }
        }
        public void AddObjectToRewind(GameObject objectToAdd)
        {
            RewindObjects newRewindObject = new RewindObjects();
            newRewindObject.gameObjectToRewind = objectToAdd;
            _objectsToRewind.Add(newRewindObject);
        }
        public void DeleteFromRewind(GameObject objectToAdd)
        {
            _objectsToRewind.Remove(_objectsToRewind.First(x => x.gameObjectToRewind == objectToAdd));
        }
    }
}
