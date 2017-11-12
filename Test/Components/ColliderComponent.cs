using MonoECS.Core;
using Microsoft.Xna.Framework;

namespace Test
{
	public class ColliderComponent : IComponent
	{
		public Rectangle bounds;
		public bool isStatic;
	}
}
