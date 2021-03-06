﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelergramEALLOBot.Classes.SpecialCommands
{
	
	public class BuildSongResponse : ISpecialCommandBuilder
	{
		public static ISpecialCommandBuilder GetBuilder( ParsedMessage message )
		{
			List<string> tokensAbout = new List<string>() { "про ", "об ", "о ", "споём ", "нам ", "ли ", "спеть ", "спой " };

			int startAbout = -1;
			int i = 0;

			while ( startAbout == -1 && i < tokensAbout.Count )
			{
				startAbout = message.rawMessage.Text.IndexOf( tokensAbout[ i++ ] );
			}

			if ( startAbout == -1 )
				return new BuildStringResponse( Utils.GetRandomResponse( RequestType.NoSongs ) );

			string subject = message.rawMessage.Text.Substring( startAbout + tokensAbout[ --i ].Length );
			return new BuildSongResponse( subject );
		}

		public BuildSongResponse( string aSubject )
		{
			subject = aSubject;
		}

		public string GetMessage()
		{
			string result = "";

			string Url = "http://pleer.net/search?q=" + subject;
			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load( Url );

			try
			{
				HtmlNode playlist = Utils.FindRecursiveByAttributeNameWithout( "class", "playlist", "style", "display: none", doc.DocumentNode );
				//doc.DocumentNode.SelectSingleNode( ".//div[@class='playlist' and not(@style='display: none')]" );//Utils.FindRecursiveByClass( "playlist", doc.DocumentNode );

				var nodes = playlist.ChildNodes[ "ol" ].ChildNodes;

				List<string> links = new List<string>();
				foreach ( var node in nodes )
				{
					if ( node.Attributes[ "link" ] != null )
						links.Add( "http://pleer.net/tracks/" + node.Attributes[ "link" ].Value );
				}

				int randomIndex = new Random( DateTime.Now.Millisecond ).Next( 0, links.Count );
				int randomSongsIndex = new Random( DateTime.Now.Millisecond ).Next( 0, ResponseDataBase.responseTypeText[ RequestType.Songs ].Count );
				result = ResponseDataBase.responseTypeText[ RequestType.Songs ][ randomSongsIndex ] + links[ randomIndex ];
			}
			catch(Exception ex)
			{
				Console.WriteLine( ex.ToString() );
				result = Utils.GetRandomResponse( RequestType.NoSongsFound );
			}

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
