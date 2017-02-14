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

			result.wordsTokens = result.wordsTokens.ConvertAll( x => x = x.ToLower() );
			result.punctuationTokens = ( (messageText.ToCharArray()).Where( c => char.IsPunctuation( c ) ).ToList() );

			result.messageType = messageText.IndexOf( '?' ) != -1 ? MessageRequestType.Question : MessageRequestType.Command;

			var bestCandidates = Utils.GetBestCandidates( Utils.GetScoresForMessage( result ) );
			if ( bestCandidates.Exists( x=> x.Key == RequestType.Songs) )
				result.messageType = MessageRequestType.SpecialCommand_FindSong;
			
			if ( bestCandidates.Exists( x => x.Key == RequestType.GetKittensWeather ) )
				result.messageType = MessageRequestType.SpecialCommand_GetKittenWeather;
			
			if ( bestCandidates.Exists( x => x.Key == RequestType.DoorsLocked && (message.From.Id == 91470612 || message.From.Id == 135320446 )  ) )
				result.messageType = MessageRequestType.SpecialCommand_DoorLocked;

			if ( messageText.Contains( "300" ) || messageText.Contains( "триста" ) )
				result.messageType = MessageRequestType.SpecialCommand_BadJoke;




			if ( (  messageText.Contains( "э! аллё!" ) || messageText.Contains( "э, аллё!" ) || messageText.Contains( "э, аллё" ) || messageText.Contains( "э,аллё" )
				 || messageText.Contains( "э аллё" ) || messageText.Contains( "э алле" ) || messageText.Contains( "э, алле" ) || messageText.Contains( "э! аллё" ) 
				 )
				 || message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private )
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
