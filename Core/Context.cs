namespace MonoECS.Core
{
	using System.Collections.Generic;
	using MonoECS.Utilities;

	public class Context
	{
		public HashSet<Entity> entities;
		Entity[] entitiesCache;

		Stack<Entity> entityPool;
		Stack<int> reusableIDs;
		int creationIndex;

		Dictionary<ComponentMatcher, Node> nodes;
		List<Node>[] nodesForComponentId;

		public Context()
		{
			entityPool = new Stack<Entity>();
			entities = new HashSet<Entity>();
			reusableIDs = new Stack<int>();
			nodes = new Dictionary<ComponentMatcher, Node>();
			nodesForComponentId = new List<Node>[ComponentTypeIndexContainer.ComponentCount];
		}

		public Entity CreateEntity()
		{
			Entity entity;
			int id;

			if (reusableIDs.Count > 0)
				id = reusableIDs.Pop();
			else
				id = creationIndex++;

			if (entityPool.Count > 0)
			{
				entity = entityPool.Pop();
				entity.Reset(id);
			}
			else
				entity = new Entity(this, id);

			entities.Add(entity);
			entitiesCache = null;
			return entity;
		}

		public void RemoveEntity(Entity entity)
		{
			if (entities.Contains(entity))
			{
				entity.Destroy();
				entityPool.Push(entity);
				entities.Remove(entity);
				entitiesCache = null;
				reusableIDs.Push(entity.ID);
			}
		}

		public Entity[] GetAllEntities()
		{
			if (entitiesCache == null)
			{
				entitiesCache = new Entity[entities.Count];
				entities.CopyTo(entitiesCache);
			}
			return entitiesCache;
		}

		public Node GetNode(ComponentMatcher componentMatcher)
		{
			if (!nodes.TryGetValue(componentMatcher, out Node node))
			{
				node = new Node(componentMatcher);
				var entities = GetAllEntities();
				for (int i = 0; i < entities.Length; i++)
				{
					node.HandleEntity(entities[i]);
				}
				nodes.Add(componentMatcher, node);

				var matcherIndices = componentMatcher.GetIndices();
				for (int i = 0; i < matcherIndices.Length; i++)
				{
					var componentID = matcherIndices[i];
					if (nodesForComponentId[componentID] == null)
					{
						var nodeList = new List<Node>();
						nodesForComponentId[componentID] = nodeList;
					}
					nodesForComponentId[componentID].Add(node);
				}
			}
			return node;
		}

		internal void UpdateConcernedNodeForEntityChanged(Entity entity, int componentID)
		{
			var listOfConcernedNodes = nodesForComponentId[componentID];
			if (listOfConcernedNodes != null)
			{
				foreach (var node in listOfConcernedNodes)
					node.HandleEntity(entity);
			}
		}
	}
}
