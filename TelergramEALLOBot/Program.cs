using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelergramEALLOBot.Classes;

namespace TelergramEALLOBot
{
	class Program
	{
		private static TelegramBotClient Bot;


		static void Main( string[] args )
		{
			Console.WriteLine( "Enter a token:" );
			string token = Console.ReadLine();

			Bot = new TelegramBotClient( token );

			Bot.OnMessage += BotOnMessageReceived;
			Bot.OnReceiveError += BotOnReceiveError;
			//Bot.OnUpdate += 

			var me = Bot.GetMeAsync().Result;
			System.Console.WriteLine( "Hello my name is " + me.FirstName );

			Bot.StartReceiving();

			// для heroku
			int port = 1234;

			try
			{
				port = Int32.Parse( System.Environment.GetEnvironmentVariable( "PORT" ) );
			}
			catch(Exception ex)
			{
				Console.WriteLine( ex.ToString() );
			}

			TcpListener server = new TcpListener( IPAddress.Parse( "127.0.0.1" ), port );
			server.Start();

			while ( true )
			{
				string line = Console.ReadLine();
				if ( line == "exit" )
					break;
			}

			Bot.StopReceiving();
			server.Stop();
		}

		private static async void BotOnMessageReceived( object sender, MessageEventArgs messageEventArgs )
		{
			var message = messageEventArgs.Message;
			if ( message == null || message.Type != MessageType.TextMessage )
				return;

			MessageParser parser = new MessageParser( message );

			var parsedMessage = parser.Parse();

			if ( parsedMessage.IsMessageForMe )
			{

				ResponseProcessor processor = new ResponseProcessor( parsedMessage );

				string response = processor.GetResponse();

				await Bot.SendTextMessageAsync( message.Chat.Id, response,
					replyMarkup: new ReplyKeyboardHide() );
			}

			await Task.FromResult( true );
		}

		private static void BotOnReceiveError( object sender, ReceiveErrorEventArgs receiveErrorEventArgs )
		{
			Debugger.Break();
		}

	}
}
