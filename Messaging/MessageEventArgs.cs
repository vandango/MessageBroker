using System;

namespace Messaging
{
	public class MessageEventArgs : EventArgs
	{
		// Leerer Konstruktor
		public MessageEventArgs() { }

		// Konstruktor mit Nachrichtentext
		public MessageEventArgs(string message)
		{
			this.Message = message;
		}

		// Konstruktor mit Nachrichtentext und sender
		public MessageEventArgs(object sender, string message)
		{
			this.Sender = sender;
			this.Message = message;
		}


		public object Sender { get; set; }

		public string Message { get; set; }
	}
}
