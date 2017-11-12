using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoECS.Core;

namespace Test
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		Systems systems;
		Context context;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}
		
		protected override void Initialize()
		{
			Resources.Initialize(Content);

			graphics.PreferredBackBufferWidth = 480;
			graphics.PreferredBackBufferHeight = 504;
			graphics.ApplyChanges();
			base.Initialize();
		}
		
		protected override void LoadContent()
		{
			context = new Context();
			systems = new Systems();
			systems
				.Add(new EntitySpawnSystem(context, Window.ClientBounds.Width, Window.ClientBounds.Height))
				.Add(new InputSystem(context))
				.Add(new BoundsSystem(context, Window))
				.Add(new MovementSystem(context))
				.Add(new CollisionSystem(context, Window.ClientBounds.Width, Window.ClientBounds.Height))
				.Add(new RenderSystem(GraphicsDevice, context));

			systems.Initialize();
		}
		
		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			
			systems.Update(gameTime);

			base.Update(gameTime);
		}
	}
}
