using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes.SpecialCommands
{
	public class DoorLockedInfo
	{
		public DateTime timeLastLocked;
		public string nameLastLocked;
		public string message;
	}

	public class BuildHomeDoorLockedResponse : ISpecialCommandBuilder
	{
		static DoorLockedInfo info = null;

		bool isResponse;

		public static BuildHomeDoorLockedResponse GetBuilder( ParsedMessage message )
		{
			if (message.punctuationTokens.Contains('?') )
				return new BuildHomeDoorLockedResponse( message, true );
			else
				return new BuildHomeDoorLockedResponse( message, false );
		}

		public BuildHomeDoorLockedResponse( ParsedMessage aMessage, bool aIsResponse )
		{
			message = aMessage;
			isResponse = aIsResponse;
		}

		public string GetMessage()
		{
			if ( !isResponse )
			{
				if( info == null )
					 info = new DoorLockedInfo();

				info.timeLastLocked = DateTime.Now;
				info.nameLastLocked = message.rawMessage.From.FirstName;
				info.message = message.rawMessage.Text;
				return "Ок, я это запомнил.";
			}
			else
			{
				if ( info != null )
				{
					return info.nameLastLocked + " закрыл(а) дверь в последний раз в " + info.timeLastLocked.ToString() + " со словами:\n" + info.message;
				}
				else
					return "Я пока ничего не запомнил :( ";
			}
		}

		private ParsedMessage message;

	}
}
