using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Test
{
	public sealed class Resources
	{
		static Resources instance = new Resources();
		public static Resources Instance { get { return instance; } }

		Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
		ContentManager content;

		public static void Initialize(ContentManager content)
		{
			if (instance.content == null)
				instance.content = content;
		}

		public static Texture2D GetTexture(string fileName)
		{
			if (!instance.textures.TryGetValue(fileName, out Texture2D texture))
			{
				if (instance.content == null)
					throw new ContentLoadException("ContentManager was null when getting resource. Try calling Resources.Initialize() first.");

				texture = instance.content.Load<Texture2D>(fileName);
				instance.textures.Add(fileName, texture);
			}
			return texture;
		}

		public static void Dispose()
		{
			instance.content.Dispose();
			instance.textures.Clear();
			instance = null;
		}
	}
}
