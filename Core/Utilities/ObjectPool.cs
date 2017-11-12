namespace MonoECS.Utilities
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Class for pooling objects.
	/// </summary>
	public class ObjectPool<T>
	{
		readonly Func<T> factory;
		readonly Stack<T> pool;

		/// <summary>Creates an instance of an object pool. </summary>
		/// <param name="factoryMethod"> 
		/// The method for creating new instances of the object 
		/// if the pool is empty when calling Get().</param>
		public ObjectPool(Func<T> factoryMethod)
		{
			factory = factoryMethod;
			pool = new Stack<T>();
		}

		/// <summary> Retrieve an object from the pool. Creates a new object if the pool is empty.</summary>
		public T Get()
		{
			return pool.Count > 0 ? pool.Pop() : factory();
		}

		/// <summary> Add an instance of T to the pool.</summary>
		/// <param name="obj"> The object to add. </param>
		public void Add(T obj)
		{
			// TODO. Maybe add optional method for resetting an object before it is pushed. 
			pool.Push(obj);
		}
	}
}
