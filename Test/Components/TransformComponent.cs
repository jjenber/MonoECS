using Microsoft.Xna.Framework;
using MonoECS.Core;

namespace Test
{
	class TransformComponent : IComponent
	{
		public Rectangle Bounds { get { return new Rectangle(position, size); } }
		public Point position;
		public Point previousPosition;
		public Point size;
	}
}
