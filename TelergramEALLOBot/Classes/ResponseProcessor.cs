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
			if ( SpecialCommandsProcessorsMap.specialCommandsProcessors.ContainsKey( message.messageType ) )
			{
				var response = SpecialCommandsProcessorsMap.specialCommandsProcessors[ message.messageType ].Invoke( message ).GetMessage();
				return response;
			}
		
			var scores = Utils.GetScoresForMessage( message );

			if (scores.Count == 0)
				return Utils.GetRandomResponse( RequestType.SomethingStrange );

			var bestCandidates = Utils.GetBestCandidates( scores );

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
