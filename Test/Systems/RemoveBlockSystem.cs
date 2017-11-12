using System;
using MonoECS.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Test
{
	class RemoveBlockSystem : IUpdateSystem
	{
		Context context;
		KeyboardState previousState;
		Random random = new Random();
		ComponentMatcher matcher = new ComponentMatcher();

		public RemoveBlockSystem(Context context)
		{
			this.context = context;
			matcher.All(typeof(SpriteComponent));
		}

		public void Update(GameTime gameTime)
		{
			var state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space))
			{
				var entities = context.GetNode(matcher).GetEntities();
				if (entities.Length > 0)
				{
					var randomEntitiy = entities[random.Next(0, entities.Length)];
					context.RemoveEntity(randomEntitiy);
				}
			}
			previousState = state;
		}
	}
}
