namespace MonoECS.Core
{
	using System.Collections.Generic;

	public class Node
	{
		Entity[] cachedEntities;
		HashSet<Entity> entities = new HashSet<Entity>();
		ComponentMatcher matcher;

		public Node(ComponentMatcher matcher)
		{
			this.matcher = matcher;
		}

		public bool HandleEntity(Entity entity)
		{
			if (matcher.Matches(entity))
			{
				if (entities.Add(entity))
				{
					cachedEntities = null;
					return true;
				}
			}
			else
				RemoveEntity(entity);
			return false;
		}

		public void RemoveEntity(Entity entity)
		{
			if (entities.Remove(entity))
				cachedEntities = null;
		}

		public Entity[] GetEntities()
		{
			if (cachedEntities == null)
			{
				cachedEntities = new Entity[entities.Count];
				entities.CopyTo(cachedEntities);
			}
			return cachedEntities;
		}
	}
}
