namespace MonoECS.Core
{
	using System;
	using MonoECS.Utilities;

	public class ComponentMatcher
	{
		int[] indices;

		public void All(params Type[] componentTypes)
		{
			indices = new int[componentTypes.Length];
			for (int i = 0; i < componentTypes.Length; i++)
			{
				indices[i] = ComponentTypeIndexContainer.GetIndexFor(componentTypes[i]);
			}
		}

		public bool Matches(Entity entity)
		{
			return entity.HasComponents(indices);
		}

		public int[] GetIndices()
		{
			return indices;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj) || obj.GetType() != GetType()) return false;
			if (ReferenceEquals(this, obj)) return true;

			var matcher = obj as ComponentMatcher;
			return HasSameIndices(indices, matcher.indices);
		}

		static bool HasSameIndices(int[] a, int[] b)
		{
			if ((a == null) != (b == null))
				return false;

			if (a.Length != b.Length)
				return false;

			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
					return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
