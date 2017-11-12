namespace MonoECS.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using MonoECS.Core;

	/// <summary>
	/// Class for getting and assigning a unique index for each component type (types that implement IComponent).
	/// </summary>
	internal static class ComponentTypeIndexContainer
	{
		static Dictionary<Type, int> ComponentTypeIndices = new Dictionary<Type, int>();

		static int nextIndex = 0;

		static int componentCount = -1;
		/// <summary> Get number of classes that implements IComponent in the entry assembly. </summary>
		public static int ComponentCount
		{
			get
			{
				if (componentCount < 1)
				{
					var interfaces =
						from t in Assembly.GetEntryAssembly().GetTypes()
						where t.GetInterfaces().Contains(typeof(IComponent))
						&& t.GetConstructor(Type.EmptyTypes) != null
						select t;
					componentCount = interfaces.Count();
				}
				return componentCount;
			}
		}

		/// <summary>
		/// Retrieves the unique index for the specific component type and caches it.
		/// </summary>
		/// <typeparam name="T"> The component type to get index for. </typeparam>
		/// <returns> The component type's index. </returns>
		public static int GetIndexFor<T>() where T : IComponent
		{
			if (!ComponentTypeIndices.TryGetValue(typeof(T), out int index))
			{
				ComponentTypeIndices.Add(typeof(T), nextIndex);
				index = nextIndex;
				nextIndex++;
			}
			return index;
		}

		/// <summary>
		/// Retrieves the unique index for the specific component type and caches it.
		/// </summary>
		/// <typeparam name="type"> The component type to get index for. </typeparam>
		/// <returns> The component type's index. </returns>
		public static int GetIndexFor(Type type)
		{
			var componentType = type.GetInterfaces().First(t => t == typeof(IComponent));
			if (componentType == null)
			{
				var message = string.Format("Class of type {0} must implement the IComponent interface.", type.Name);
				throw new ArgumentException(message);
			}
			
			if (!ComponentTypeIndices.TryGetValue(type, out int index))
			{
				ComponentTypeIndices.Add(type, nextIndex);
				index = nextIndex;
				nextIndex++;
			}
			return index;
		}
	}
}
