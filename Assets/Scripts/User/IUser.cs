using System;
using Data;

namespace Usr
{
	public interface IUser
	{
		PlayerRecord Profile { get; }
		void Authorize(Action<bool> finished);
		void SetName(string name, Action<bool> finished);
		void LoadProfile (Action finished);
		bool IsAuthorized();
		string Id();
		string Name();
		void SetErrorOutput(Action<string> output);
	}
}

