namespace MonoECS.Core
{
	using System;
	using System.Collections.Generic;
	using MonoECS.Utilities;

	public class Entity
	{
		IComponent[] components;
		int[] indicesCache;

		public bool IsEnabled => isEnabled;
		public int ID => id;
		public int ComponentCount => GetAllComponentIndices().Length;

		bool isEnabled;
		int id;

		Context context;

		public Entity(Context context, int id)
		{
			this.context = context;
			this.id = id;
			components = new IComponent[ComponentTypeIndexContainer.ComponentCount];
		}

		public T AddComponent<T>() where T : IComponent
		{
			int typeIndex = ComponentTypeIndexContainer.GetIndexFor<T>();
			var component = components[typeIndex];

			if (component != null)
			{
				return (T)component;
			}

			var newComponent = (T)Activator.CreateInstance(typeof(T));
			components[typeIndex] = newComponent;
			indicesCache = null;
			context.UpdateConcernedNodeForEntityChanged(this, typeIndex);
			return newComponent;
		}

		public T GetComponent<T>() where T : IComponent
		{
			int typeIndex = ComponentTypeIndexContainer.GetIndexFor<T>();
			return (T)components[typeIndex];
		}

		public void RemoveComponent<T>() where T : IComponent
		{
			int typeIndex = ComponentTypeIndexContainer.GetIndexFor<T>();
			var component = components[typeIndex];
			if (component == null)
			{
				// Error component not found
			}
			components[typeIndex] = null;
			context.UpdateConcernedNodeForEntityChanged(this, typeIndex);
		}

		public bool HasComponent<T>() where T : IComponent
		{
			int typeIndex = ComponentTypeIndexContainer.GetIndexFor<T>();
			return components[typeIndex] != null;
		}
		public bool HasComponents(int[] indices)
		{
			for (int i = 0; i < indices.Length; i++)
				if (components[indices[i]] == null)
					return false;
			return true;
		}

		public int[] GetAllComponentIndices()
		{
			if (indicesCache == null)
			{
				List<int> componentIndices = Pools.Get<List<int>>();
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i] != null)
						componentIndices.Add(i);
				}
				indicesCache = new int[componentIndices.Count];
				indicesCache = componentIndices.ToArray();
			}
			return indicesCache;
		}

		public void RemoveAllComponents()
		{
			var componentIndices = GetAllComponentIndices();
			for (int i = 0; i < componentIndices.Length; i++)
			{
				components[componentIndices[i]] = null;
				context.UpdateConcernedNodeForEntityChanged(this, componentIndices[i]);
			}
		}

		public void Reset(int id)
		{
			this.id = id;
			isEnabled = true;
		}

		public void Destroy()
		{
			RemoveAllComponents();
		}

		public override string ToString()
		{
			string s = "Entity: " + id + " Components: " + ComponentCount + ": ";
			for (int i = 0; i < components.Length; i++)
			{
				var com = components[i];
				if (components[i] != null)
					s = s + com.GetType().Name + ", ";
			}

			return s;
		}
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj) || obj.GetType() != GetType()) return false;
			if (ReferenceEquals(this, obj)) return true;

			var e = obj as Entity;
			return id == e.id;
		}

		public override int GetHashCode()
		{
			return id;
		}

		public static bool operator ==(Entity a, Entity b)
		{
			if (ReferenceEquals(null, a) && ReferenceEquals(null, b)) return true;
			if (ReferenceEquals(null, a) || ReferenceEquals(null, b)) return false;

			return a.id == b.id;
		}

		public static bool operator !=(Entity a, Entity b)
		{
			if (ReferenceEquals(null, a) && ReferenceEquals(null, b)) return false;
			if (ReferenceEquals(null, a) || ReferenceEquals(null, b)) return true;

			return a.id != b.id;
		}
	}
}
