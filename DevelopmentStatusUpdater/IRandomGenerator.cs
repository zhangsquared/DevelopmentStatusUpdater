using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public interface IRandomGenerator
	{
		IEntryTemplate RandomGenerateEntry(string appName);

		int RandomGenerateAppNumber(int totalAppNumber);

		string RandomGenerateApplicationName(string[] appNames);

		bool RandomGenerateComplete(EntryTemplateType type);
	}
}
