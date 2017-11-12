namespace MonoECS.Core
{
	/// <summary>
	/// Implement this interface for systems that should run once on startup.
	/// </summary>
	public interface IInitializeSystem : ISystem
	{
		/// <summary>
		/// Called once on startup in the order that the systems were added.
		/// </summary>
		void Initialize();
	}
}
