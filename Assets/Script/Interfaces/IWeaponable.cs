using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IWeaponable
    {
        public void PullTrigger();
        public void ReleaseTrigger();
        public void Reload();
        public void SetOrientation();
    }
}
