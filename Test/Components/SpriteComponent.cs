using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoECS.Core;

namespace Test
{
	public class SpriteComponent : IComponent
	{
		public Texture2D texture;
		public Rectangle textureRect;
	}
}
