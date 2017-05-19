using System;
using Data;


namespace Data
{
	public interface IGameDataLoader
	{
		void Init();
		void GetWorld(Action<World> result);
		void SetErrorOutput(Action<string> output);
	}
}
