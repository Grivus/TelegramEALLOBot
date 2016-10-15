using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes
{
	public class MessageParser
	{
		public MessageParser( Telegram.Bot.Types.Message aMessage )
		{
			message = aMessage;
		}

		public ParsedMessage Parse()
		{
			string messageText = message.Text.ToLower();
			ParsedMessage result = new ParsedMessage();
			result.wordsTokens = new List<string> ( messageText.Split( new char[] { '.', ',', ' ', '!', '?' } ) );
			result.messageType = messageText.IndexOf( '?' ) != -1 ? MessageRequestType.Question : MessageRequestType.Command;

			if ( messageText.Contains( "спой мне про" ) )
				result.messageType = MessageRequestType.SpecialCommand;

			if ( messageText.Contains( "300" ) || messageText.Contains( "триста" ) )
				result.messageType = MessageRequestType.SpecialCommand;

			if ( messageText.Contains( "э! аллё!" ) || messageText.Contains( "э, аллё!" ) || messageText.Contains( "э, аллё" ) || messageText.Contains( "э,аллё" )
				|| messageText.Contains( "э аллё" ) || messageText.Contains( "э алле" ) || messageText.Contains( "э, алле" ) || messageText.Contains( "э! аллё" ) )
				result.IsMessageForMe = true;
			else
				result.IsMessageForMe = false;

			result.rawMessage = message;

			return result;
		}

		private Telegram.Bot.Types.Message message;
		public Telegram.Bot.Types.Message Message
		{
			get
			{
				return message;
			}
		}


	}
}
