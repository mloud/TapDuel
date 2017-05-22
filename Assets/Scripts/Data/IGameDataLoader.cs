using System;
using Data;
using System.Collections.Generic;


namespace Data
{
	public interface IGameDataLoader
	{
		void Init();
		void GetWorld(Action<World> result);
		void GetPlayers(Action<Players> result);
		void SetErrorOutput(Action<string> output);
	}
}
