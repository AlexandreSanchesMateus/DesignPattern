using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CommandInjector : MonoBehaviour
    {
        [SerializeField] MovecommandInvoker _c;
        [SerializeField] CommandReference _ref;

        ISet<MovecommandInvoker> RealRef => _ref;

        public IReadOnlyList<int> T { get => t; }

        List<int> t;

        private void Awake()
        {
            RealRef.Set(_c);
        }
    }
}
