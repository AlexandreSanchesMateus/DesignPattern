using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Entity : MonoBehaviour
    {
        [SerializeField, Required("nop")] Health _health;

        /*[SerializeField] ObjectPool _example;*/

        private void Update()
        {
	        /*if (Input.GetKeyDown(KeyCode.F))
	        {
		        Bullet bullet = _example.Pool.Get();
		        bullet.transform.position = this.transform.position;
			}*/
        }
    }
}
