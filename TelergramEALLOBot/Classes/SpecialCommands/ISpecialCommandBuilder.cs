using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes.SpecialCommands
{
	public interface ISpecialCommandBuilder
	{
		string GetMessage();
	}
}
