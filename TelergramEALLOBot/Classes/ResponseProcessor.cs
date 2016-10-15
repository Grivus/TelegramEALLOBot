using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelergramEALLOBot.Classes.SpecialCommands;

namespace TelergramEALLOBot.Classes
{
	public class ResponseProcessor
	{
		public ResponseProcessor( ParsedMessage aMessage )
		{
			message = aMessage;
		}

		public string GetResponse()
		{
			if ( message.messageType == MessageRequestType.SpecialCommand )
			{
				if ( message.wordsTokens.Contains( "спой" ) )
				{
					int startAbout = message.rawMessage.Text.IndexOf( "про " );
					if ( startAbout == -1 )
					{
						return Utils.GetRandomResponse( RequestType.NoSongs );
					}

					string subject = message.rawMessage.Text.Substring( startAbout + 4 );

					BuildSongResponse songBuilder = new BuildSongResponse( subject );
					return songBuilder.GetMessage();
				}
				else if (  ( message.wordsTokens.Contains( "300" ) || message.wordsTokens.Contains( "триста" ) ) )
				{
					return Utils.GetRandomResponse( RequestType.BadJoke );
				}

				return Utils.GetRandomResponse( RequestType.SomethingStrange ); 
			}

			Dictionary<RequestType, int> scores = new Dictionary<RequestType, int>();

			// голосование
			foreach ( var element in RequestsTypeDataBase.requestTypeTags )
			{
				foreach ( var token in message.wordsTokens )
				{
					foreach ( var tag in element.Value )
					{
						if ( token.ToLower() == tag )
						{
							if ( scores.ContainsKey( element.Key ) == false )
								scores.Add( element.Key, 1 );
							else
								scores[ element.Key ] += 1;
						}
					}

				}
			}

			if (scores.Count == 0)
				return Utils.GetRandomResponse( RequestType.SomethingStrange );

			var orderedScores = scores.OrderByDescending( x => x.Value ).ToList();

			List<KeyValuePair < RequestType, int> > bestCandidates = new List<KeyValuePair<RequestType, int>>();

			const int kMinimalDiff = 1;

			for ( int i = 0; i < orderedScores.Count; ++i )
			{
				if ( bestCandidates.Count == 0 )
					bestCandidates.Add( new KeyValuePair<RequestType, int>( orderedScores[ i ].Key, orderedScores[ i ].Value ) );
				else if ( Math.Abs( orderedScores[ i ].Value - bestCandidates[0].Value ) < kMinimalDiff )
					bestCandidates.Add( new KeyValuePair<RequestType, int>( orderedScores[ i ].Key, orderedScores[ i ].Value ) );
			}

			int randomIndex = new Random(DateTime.Now.Millisecond).Next( 0, bestCandidates.Count );

			var possibleResponses = ResponseDataBase.responseTypeText[ bestCandidates[ randomIndex ].Key ];

			randomIndex = new Random(DateTime.Now.Millisecond).Next( 0, possibleResponses.Count );

			return possibleResponses[ randomIndex ];
		}

		private ParsedMessage message;
		public ParsedMessage Message
		{
			get
			{
				return message;
			}
		}
	}
}
