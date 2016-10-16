﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
		private static readonly TelegramBotClient Bot = new TelegramBotClient( "285884384:AAFu72EDmuQvDIrdVXEU5r9Rm6xhhT6czy0" );


		static void Main( string[] args )
		{

			Bot.OnMessage += BotOnMessageReceived;
			Bot.OnReceiveError += BotOnReceiveError;
			//Bot.OnUpdate += 

			var me = Bot.GetMeAsync().Result;
			System.Console.WriteLine( "Hello my name is " + me.FirstName );

			Bot.StartReceiving();

			while ( true )
			{
				string line = Console.ReadLine();
				if ( line == "exit" )
					break;
			}

			Bot.StopReceiving();

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
