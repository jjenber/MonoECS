using MonoECS.Core;
using Microsoft.Xna.Framework;

namespace Test
{
	class VelocityComponent : IComponent
	{
		public Vector2 direction = Vector2.Zero;
		public float speed = 0f;
	}
}
