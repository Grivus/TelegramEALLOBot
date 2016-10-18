using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
		static void Main( string[] args )
		{
			string token = System.Environment.GetEnvironmentVariable( "TOKEN" );
			if ( token == null )
			{
				Console.WriteLine( "Enter a token:" );
				token = Console.ReadLine();
			}

			System.Net.ServicePointManager.ServerCertificateValidationCallback += ( o, certificate, chain, errors ) => true;
			System.Security.Cryptography.AesCryptoServiceProvider b = new System.Security.Cryptography.AesCryptoServiceProvider();

			Console.WriteLine( "Before ctor" );
			TelegramBotClient Bot = new TelegramBotClient( token );
			Console.WriteLine( "After ctor" );


			Bot.OnMessage += BotOnMessageReceived;
			Bot.OnReceiveError += BotOnReceiveError;
			//Bot.OnUpdate += 

			var me = Bot.GetMeAsync().Result;
			System.Console.WriteLine( "Hello my name is " + me.FirstName );

			Console.WriteLine( "Bot before start receiving" );
			Bot.StartReceiving();
			Console.WriteLine( "Bot after start receiving" );

			// для heroku
			/*
			int port = 1234;

			var threadServer = new Thread(
			()=>
				{
					while ( true )
					{
						try
						{
							port = Int32.Parse( System.Environment.GetEnvironmentVariable( "PORT" ) );
						}
						catch ( Exception )
						{
							continue;
						}

						TcpListener server = new TcpListener( IPAddress.Parse( "127.0.0.1" ), port );
						server.Start();
						Thread.Sleep( 3000 );
						server.Stop();
					}
				} 
			);
			threadServer.Start();
			*/
			while ( true )
			{
				string line = Console.ReadLine();
				if ( line == "exit" )
					break;
			}

			Bot.StopReceiving();

			//threadServer.Abort();
		}

		private static async void BotOnMessageReceived( object sender, MessageEventArgs messageEventArgs )
		{
			var Bot = sender as TelegramBotClient;
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
