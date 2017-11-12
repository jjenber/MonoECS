namespace MonoECS.Utilities
{
	using System;
	using System.Collections.Generic;

	public static class Pools
	{
		static Dictionary<Type, object> pools = new Dictionary<Type, object>();

		public static ObjectPool<T> GetObjectPool<T>() where T : new()
		{
			object objectPool;
			var objectType = typeof(T);
			if (!pools.TryGetValue(objectType, out objectPool))
			{
				objectPool = new ObjectPool<T>(() => new T());
				pools.Add(objectType, objectPool);
			}
			return ((ObjectPool<T>)objectPool);
		}

		public static T Get<T>() where T : new()
		{
			return GetObjectPool<T>().Get();
		}

		public static void Push<T>(T obj) where T : new()
		{
			GetObjectPool<T>().Add(obj);
		}

		public static void Reset()
		{
			pools.Clear();
		}
	}
}
