using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
	public interface IPoolableObject<T> where T : Component
	{
		public ReturnToPool<T> ReturnToPool { get; }

		public void OnObjectGetFromPool();
		public void OnObjectReturnToPool();
		public void OnObjectCreatedForPool();
	}
}
