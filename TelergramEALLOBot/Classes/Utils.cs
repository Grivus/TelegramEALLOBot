using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes
{
	public static class Utils
	{
		public static int GetRandomNumber( int aFrom, int aTo )
		{
			return new Random().Next( aFrom, aTo );
		}

		public static int GetRandomIndex( int aTo )
		{
			return new Random().Next( 0, aTo );
		}

		public static string GetRandomResponse( RequestType key )
		{
			return ResponseDataBase.responseTypeText[ key ][ Utils.GetRandomIndex( ResponseDataBase.responseTypeText[ key ].Count ) ];
		}

		public static Dictionary<RequestType, int> GetScoresForMessage( ParsedMessage message )
		{
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

			return scores;
		}

		public static List<KeyValuePair<RequestType, int>> GetBestCandidates( Dictionary<RequestType, int> scores )
		{
			var orderedScores = scores.OrderByDescending( x => x.Value ).ToList();

			List<KeyValuePair<RequestType, int>> bestCandidates = new List<KeyValuePair<RequestType, int>>();

			const int kMinimalDiff = 1;

			for ( int i = 0; i < orderedScores.Count; ++i )
			{
				if ( bestCandidates.Count == 0 )
					bestCandidates.Add( new KeyValuePair<RequestType, int>( orderedScores[ i ].Key, orderedScores[ i ].Value ) );
				else if ( Math.Abs( orderedScores[ i ].Value - bestCandidates[ 0 ].Value ) < kMinimalDiff )
					bestCandidates.Add( new KeyValuePair<RequestType, int>( orderedScores[ i ].Key, orderedScores[ i ].Value ) );
			}

			return bestCandidates;
		}
	}
}
