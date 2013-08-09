using System;
using System.Collections.Generic;
using System.Text;

namespace Messaging
{
	public class ErrorMessageEventArgs : EventArgs
	{
		// Leerer Konstruktor
		public ErrorMessageEventArgs() { }

		// Konstruktor mit Fehlertext und einer Exception
		public ErrorMessageEventArgs(string message, Exception exception)
		{
			this.Message = message;
			this.Exception = exception;
		}

		// Konstruktor mit Fehlertext, einer Exception und dem sender
		public ErrorMessageEventArgs(object sender, string message, Exception exception)
		{
			this.Sender = sender;
			this.Message = message;
			this.Exception = exception;
		}


		public object Sender { get; set; }
		public Exception Exception { get; set; }
		public string Message { get; set; }
	}
}
