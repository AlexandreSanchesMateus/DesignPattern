using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(ReturnBulletToPool))]
	public class BulletPool : ObjectPool<Bullet>
    {
    }
}
