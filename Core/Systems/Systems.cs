namespace MonoECS.Core
{
	using System.Collections.Generic;
	using Microsoft.Xna.Framework;

	/// <summary>
	/// Class used to handle multiple systems which implements 
	/// IInitializeSystem and/or IUpdateSystem. The systems are handled
	/// in the order they were added.
	/// </summary>
	public class Systems
	{
		List<IInitializeSystem> initializeSystems = new List<IInitializeSystem>();
		List<IUpdateSystem>     updateableSystems = new List<IUpdateSystem>();

		/// <summary> Adds a system to the systems instance and returns itself to allow for method chaining. </summary>
		/// <param name="system"> The system instance.</param>
		public Systems Add(ISystem system)
		{
			var initSystem = system as IInitializeSystem;
			var updateSystem = system as IUpdateSystem;

			if (initSystem != null)
				initializeSystems.Add((initSystem));

			if (updateSystem != null)
				updateableSystems.Add(updateSystem);

			if (initSystem == null && updateSystem == null)
			{
				// TODO: Throw Exception if no correct interface is implemented.
			}
			return this;
		}

		/// <summary> Call this to loop through all initialize systems in the order they were added and call their Initialize().</summary>
		public void Initialize()
		{
			foreach (IInitializeSystem s in initializeSystems)
				s.Initialize();
		}

		/// <summary> Call this to loop through all update systems in the order they were added and call their Update().</summary>
		public void Update(GameTime gameTime)
		{
			foreach (IUpdateSystem s in updateableSystems)
				s.Update(gameTime);
		}
	}
}
