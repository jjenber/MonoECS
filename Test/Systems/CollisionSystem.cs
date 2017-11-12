using MonoECS.Core;
using Microsoft.Xna.Framework;

namespace Test
{
	public class CollisionSystem : IUpdateSystem
	{
		Context context;
		ComponentMatcher colliderMatcher;
		int windowWidth;
		int windowHeight;

		public CollisionSystem(Context context, int windowWidth, int windowHeight)
		{
			this.context = context;
			this.windowWidth = windowWidth;
			this.windowHeight = windowHeight;

			colliderMatcher = new ComponentMatcher();
			colliderMatcher.All(typeof(ColliderComponent));
		}

		public void Update(GameTime gameTime)
		{
			var entities = context.GetNode(colliderMatcher).GetEntities();
			for (int i = 0; i < entities.Length; i++)
			{
				var collider = entities[i].GetComponent<ColliderComponent>();
				if (!collider.isStatic)
				{
					var previousLocation = collider.bounds.Location;
					collider.bounds.Location = entities[i].GetComponent<TransformComponent>().position;
					for (int j = 0; j < entities.Length; j++)
						if (entities[i] != entities[j])
							if (IsOverlapping(entities[i], entities[j]))
							{
								OnCollision(entities[i], entities[j]);
								OnCollision(entities[j], entities[i]);
								collider.bounds.Location = previousLocation;
							}
				}
			}
		}

		bool IsOverlapping(Entity entA, Entity entB)
		{
			var colliderA = entA.GetComponent<ColliderComponent>();
			var colliderB = entB.GetComponent<ColliderComponent>();
			return colliderA.bounds.Intersects(colliderB.bounds);
		}

		void OnCollision(Entity ent, Entity other)
		{
			if (ent.GetComponent<BouncableComponent>() != null)
			{
				if (ent.GetComponent<TransformComponent>().Bounds.Bottom > other.GetComponent<TransformComponent>().Bounds.Top)
					ent.GetComponent<VelocityComponent>().direction.Y *= -1;
			}
			if (ent.GetComponent<DestructibleComponent>() != null)
				context.RemoveEntity(ent);
		}
	}
}