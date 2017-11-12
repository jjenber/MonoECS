using System;
using MonoECS.Core;
using Microsoft.Xna.Framework;

namespace Test
{
	class EntitySpawnSystem : IInitializeSystem
	{
		int windowWidth;
		int windowHeight;
		Context context;

		public EntitySpawnSystem(Context context, int windowWidth, int windowHeight)
		{
			this.context = context;
			this.windowWidth = windowWidth;
			this.windowHeight = windowHeight;
		}

		public void Initialize()
		{
			SpawnBlocks();
			SpawnPaddle();
			SpawnBall();
		}

		void SpawnBlocks()
		{
			Random rand = new Random();
			var texture = Resources.GetTexture("texture");
			var blockCountPerRow = windowWidth / 32;

			for (int i = 0; i < 60; i++)
			{
				var e = context.CreateEntity();
				var sprite    = e.AddComponent<SpriteComponent>();
				var transform = e.AddComponent<TransformComponent>();
				var collider  = e.AddComponent<ColliderComponent>();
				e.AddComponent<DestructibleComponent>();

				sprite.texture = texture;
				int randomTile = rand.Next(0, 3);
				sprite.textureRect = new Rectangle((randomTile * 32) + (8 * randomTile), 0, 32, 32);

				transform.size = new Point(32, 32);
				var row = i / blockCountPerRow;
				var column = i % blockCountPerRow;
				transform.position = new Point(column * 32, row * 32);

				collider.bounds = transform.Bounds;
				collider.isStatic = true;
			}
		}

		void SpawnPaddle()
		{
			var paddle = context.CreateEntity();
			var sprite    = paddle.AddComponent<SpriteComponent>();
			var transform = paddle.AddComponent<TransformComponent>();
			var velocity  = paddle.AddComponent<VelocityComponent>();
			var collider  = paddle.AddComponent<ColliderComponent>();
			paddle.AddComponent<PlayerControllerComponent>();

			sprite.texture = Resources.GetTexture("texture");
			sprite.textureRect = new Rectangle(0, 200, 95, 24);

			transform.position = new Point((windowWidth / 2) - 42, windowHeight - 25);
			transform.size = sprite.textureRect.Size;

			velocity.speed = 250f;

			collider.isStatic = false;
			collider.bounds = transform.Bounds;
		}

		void SpawnBall()
		{
			var ball = context.CreateEntity();
			var sprite    = ball.AddComponent<SpriteComponent>();
			var transform = ball.AddComponent<TransformComponent>();
			var velocity  = ball.AddComponent<VelocityComponent>();
			var collider  = ball.AddComponent<ColliderComponent>();
			ball.AddComponent<BouncableComponent>();

			sprite.texture = Resources.GetTexture("texture");
			sprite.textureRect = new Rectangle(160, 200, 16, 16);

			transform.size = new Point(16, 16);
			transform.position = new Point((windowWidth / 2) - 4, windowHeight - 50);

			velocity.direction = new Vector2(-1, -1);
			velocity.speed = 150f;

			collider.isStatic = false;
			collider.bounds = transform.Bounds;
		}
	}
}
