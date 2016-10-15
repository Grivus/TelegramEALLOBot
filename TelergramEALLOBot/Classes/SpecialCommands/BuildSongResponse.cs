using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes.SpecialCommands
{
	public class BuildSongResponse
	{
		public BuildSongResponse( string aSubject )
		{
			subject = aSubject;
		}

		private HtmlNode FindRecursive( string idClass, HtmlNode currentNode )
		{
			foreach ( var child in currentNode.ChildNodes )
			{
				if ( child.Attributes[ "class" ] != null )
				{
					Debug.Print( child.Attributes[ "class" ].Value );

					if ( child.Attributes[ "class" ].Value == idClass )
						return child;

				}
				var result = FindRecursive( idClass, child );
				if ( result != null )
					return result;
			}

			return null;
		}

		public string GetMessage()
		{
			string result = "";

			string Url = "http://pleer.net/search?q=" + subject;
			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load( Url );
			HtmlNode playlist = FindRecursive( "playlist", doc.DocumentNode );
			var nodes = playlist.ChildNodes["ol"].ChildNodes;

			List<string> links = new List<string>();
			foreach ( var node in nodes )
			{
				if ( node.Attributes[ "link" ] != null )
					links.Add( "http://pleer.net/tracks/" + node.Attributes[ "link" ].Value );
			}

			int randomIndex = new Random(DateTime.Now.Millisecond).Next( 0, links.Count );
			int randomSongsIndex = new Random(DateTime.Now.Millisecond).Next( 0, ResponseDataBase.responseTypeText[ RequestType.Songs ].Count );
			result = ResponseDataBase.responseTypeText[RequestType.Songs][ randomSongsIndex ] + links[ randomIndex ];

			return result;
		}

		private string subject;
		public string Subject
		{
			get
			{
				return subject;
			}
		}
	}
}
