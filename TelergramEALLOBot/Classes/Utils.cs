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
	}
}
