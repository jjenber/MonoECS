using MonoECS.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test
{
	public class RenderSystem : IUpdateSystem
	{
		GraphicsDevice graphicsDevice;
		SpriteBatch spriteBatch;
		Context context;
		ComponentMatcher matcher;

		public RenderSystem(GraphicsDevice graphicsDevice, Context context)
		{
			this.graphicsDevice = graphicsDevice;
			this.context = context;

			spriteBatch = new SpriteBatch(graphicsDevice);

			matcher = new ComponentMatcher();
			matcher.All(typeof(SpriteComponent));
		}

		public void Update(GameTime gameTime)
		{
			graphicsDevice.Clear(Color.Honeydew);
			spriteBatch.Begin();

			var entities = context.GetNode(matcher).GetEntities();
			for (int i = 0; i < entities.Length; i++)
			{
				var transform = entities[i].GetComponent<TransformComponent>();
				var sprite    = entities[i].GetComponent<SpriteComponent>();

				spriteBatch.Draw(sprite.texture, transform.Bounds, sprite.textureRect, Color.White);
			}

			spriteBatch.End();
		}
	}
}
