using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoECS.Core;
using System;

namespace Test
{
	class InputSystem : IUpdateSystem
	{
		Context context;
		ComponentMatcher matcher;
		public InputSystem(Context context)
		{
			this.context = context;
			matcher = new ComponentMatcher();
			matcher.All(typeof(PlayerControllerComponent), typeof(VelocityComponent));
		}

		public void Update(GameTime gameTime)
		{
			var entities = context.GetNode(matcher).GetEntities();
			for (int i = 0; i < entities.Length; i++)
			{
				var velocity = entities[i].GetComponent<VelocityComponent>();
				var state = Keyboard.GetState();

				bool leftPressed = state.IsKeyDown(Keys.Left);
				bool rightPressed = state.IsKeyDown(Keys.Right);
				if (leftPressed)
					velocity.direction.X = -1;
				if (rightPressed)
					velocity.direction.X = 1;

				else if (!leftPressed && !rightPressed)
					velocity.direction = Vector2.Zero;
			}
		}
	}
}
