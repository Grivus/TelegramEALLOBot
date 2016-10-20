using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes
{
	public enum MessageRequestType
	{
		Question,
		Command,
		SpecialCommand_FindSong,
		SpecialCommand_BadJoke,
		SpecialCommand_GetKittenWeather,
		SpecialCommand_DoorLocked,
	}

	public class ParsedMessage
	{
		public List<string> wordsTokens = new List<string>();

		public MessageRequestType messageType;

		public List<char> punctuationTokens = new List<char>();

		public Telegram.Bot.Types.Message rawMessage;

		public bool IsMessageForMe;
	}
}
