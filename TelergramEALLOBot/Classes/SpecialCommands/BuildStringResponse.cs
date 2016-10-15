using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes.SpecialCommands
{
	public class BuildStringResponse : ISpecialCommandBuilder
	{
		public static BuildStringResponse GetBuilder( ParsedMessage message )
		{
			return new BuildStringResponse( Utils.GetRandomResponse( RequestType.BadJoke ) );
		}

		public BuildStringResponse( string aResult )
		{
			result = aResult;
		}

		public string GetMessage()
		{
			return result;
		}

		private string result;

	}
}
