using MonoECS.Core;
using Microsoft.Xna.Framework;

namespace Test
{
	class BoundsSystem : IUpdateSystem
	{
		Context context;
		GameWindow window;
		ComponentMatcher matcher;
		
		public BoundsSystem(Context context, GameWindow window)
		{
			this.context = context;
			this.window = window;
			matcher = new ComponentMatcher();
			matcher.All(typeof(VelocityComponent));
		}

		public void Update(GameTime gameTime)
		{
			var entities = context.GetNode(matcher).GetEntities();
			for (int i = 0; i < entities.Length; i++)
			{
				var transform = entities[i].GetComponent<TransformComponent>();

				if (transform.Bounds.Left < 0 || transform.Bounds.Right > window.ClientBounds.Width)
					OutOfBoundsX(entities[i]);
				if (transform.Bounds.Top < 0 || transform.Bounds.Bottom > window.ClientBounds.Height)
					OutOfBoundsY(entities[i]);
			}
		}

		public void OutOfBoundsX(Entity entity)
		{
			var velocity  = entity.GetComponent<VelocityComponent>();
			var bouncable = entity.GetComponent<BouncableComponent>();
			if (bouncable != null)
			{
				velocity.direction.X *= -1;
				velocity.speed += bouncable.speedIncrease;
			}

			var transform = entity.GetComponent<TransformComponent>();
			transform.position = transform.previousPosition;

			entity.GetComponent<ColliderComponent>().bounds.Location = transform.position;
		}

		public void OutOfBoundsY(Entity entity)
		{
			var velocity = entity.GetComponent<VelocityComponent>();
			var bouncable = entity.GetComponent<BouncableComponent>();
			if (bouncable != null)
			{
				velocity.direction.Y *= -1;
				velocity.speed += bouncable.speedIncrease;
			}

			var transform = entity.GetComponent<TransformComponent>();
			transform.position = transform.previousPosition;

			entity.GetComponent<ColliderComponent>().bounds.Location = transform.position;
		}
	}
}
