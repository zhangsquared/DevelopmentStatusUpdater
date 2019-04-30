using System;
using System.Collections.Generic;
using System.Text;

namespace DevelopmentStatusUpdater
{
	public interface IEntry
	{
		string Date { get; }
		string User { get; }
		string Application { get; }
		string Description { get; }
		double Hours { get; set; }
		bool? IsDevelopment { get; }
		bool IsComplete { get; set; }

		bool FromLine(string line);
	}
}
