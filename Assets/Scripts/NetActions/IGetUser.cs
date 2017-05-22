using System;

using System;
using Data;

public interface IGetUser
{
	void Do(string id, Action<PlayerRecord> rec);
}

