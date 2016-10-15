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
		SpecialCommand,
	}

	public class ParsedMessage
	{
		public List<string> wordsTokens = new List<string>();

		public MessageRequestType messageType;

		public List<string> punctuationTokens = new List<string>();

		public Telegram.Bot.Types.Message rawMessage;

		public bool IsMessageForMe;
	}
}
