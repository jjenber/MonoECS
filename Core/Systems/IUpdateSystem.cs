namespace MonoECS.Core
{
	using Microsoft.Xna.Framework;
	
	/// <summary>
	/// Implement this interface for systems that should update every frame.
	/// </summary>
	public interface IUpdateSystem : ISystem
	{
		/// <summary> Called every frame. </summary>
		/// <param name="gameTime"> XNA class passed through MonoGame's Update method. </param>
		void Update(GameTime gameTime);
	}
}
