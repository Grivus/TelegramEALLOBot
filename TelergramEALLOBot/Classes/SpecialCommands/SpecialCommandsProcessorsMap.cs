using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelergramEALLOBot.Classes.SpecialCommands;

namespace TelergramEALLOBot.Classes
{
	public static class SpecialCommandsProcessorsMap
	{
		public static Dictionary<MessageRequestType, Func<ParsedMessage, ISpecialCommandBuilder> > specialCommandsProcessors = new Dictionary<MessageRequestType, Func<ParsedMessage, ISpecialCommandBuilder>>()
		{
			{ MessageRequestType.SpecialCommand_FindSong, BuildSongResponse.GetBuilder },
			{ MessageRequestType.SpecialCommand_BadJoke, BuildStringResponse.GetBuilder },
			{ MessageRequestType.SpecialCommand_GetKittenWeather, BuildCatsWeatherResponse.GetBuilder }

		};
	}
}
