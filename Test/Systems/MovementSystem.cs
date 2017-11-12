using MonoECS.Core;
using Microsoft.Xna.Framework;

namespace Test
{
	public class MovementSystem : IUpdateSystem
	{
		Context context;
		ComponentMatcher matcher;
		public MovementSystem(Context context)
		{
			this.context = context;

			matcher = new ComponentMatcher();
			matcher.All(typeof(VelocityComponent), typeof(TransformComponent));
		}

		public void Update(GameTime gameTime)
		{
			var entities = context.GetNode(matcher).GetEntities();
			for (int i = 0; i < entities.Length; i++)
			{
				var transform = entities[i].GetComponent<TransformComponent>();
				var velocity  = entities[i].GetComponent<VelocityComponent>();

				transform.previousPosition = transform.position;
				transform.position += (velocity.direction * velocity.speed * (float)gameTime.ElapsedGameTime.TotalSeconds).ToPoint();
			}
		}
	}
}
