using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public interface IRandomGenerator
	{
		string RandomGenerateApplicationName(string[] appNames);

		int RandomGenerateAppNumber(int totalNumber);
	}
}
