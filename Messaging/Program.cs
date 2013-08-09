using System;
using System.Collections.Generic;
using System.Text;

namespace Messaging
{
	class Program
	{
		static void Main(string[] args)
		{
			// An einem Nachrichtenkanal registrieren
			MessageBroker.MessageFired += new MessageBroker.MessageEventHandler(MessageBroker_MessageFired);
			MessageBroker.ErrorMessageFired += new MessageBroker.ErrorMessageEventHandler(MessageBroker_ErrorMessageFired);

			MessageBroker<int>.MessageFired += new MessageBroker<int>.GenericMessageEventHandler(MessageBroker_IntGenericMessageFired);
			//MessageBroker<YourCustomType>.MessageFired += new MessageBroker<YourCustomType>.GenericMessageEventHandler(MessageBroker_YourCustomTypeGenericMessageFired);

			// Message senden
			MessageBroker.SendMessageAsync(new MessageEventArgs("Nachrichtentext"));
			MessageBroker.SendErrorMessageAsync(new ErrorMessageEventArgs("Fehlertext", new Exception("Fehler")));

			MessageBroker<int>.SendMessageAsync(new GenericMessageEventArgs<int>(777));
			MessageBroker<YourCustomType>.SendMessageAsync(new GenericMessageEventArgs<YourCustomType>(new YourCustomType()));


			//// Einen Nachrichtenkanal schliessen
			//MessageBroker.MessageFired -= MessageBroker_MessageFired;
			//MessageBroker.ErrorMessageFired -= MessageBroker_ErrorMessageFired;
			//MessageBroker<int>.MessageFired += MessageBroker_IntGenericMessageFired;
			//MessageBroker<long>.MessageFired += MessageBroker_LongGenericMessageFired;

			Console.Read();
		}

		// Und die Implementierung des Empfangs
		static void MessageBroker_MessageFired(MessageEventArgs e)
		{
			// Verarbeite die Nachricht
			Console.WriteLine("Message: {0}", e.Message);
		}

		static void MessageBroker_ErrorMessageFired(ErrorMessageEventArgs e)
		{
			// Verarbeite die Nachricht
			Console.WriteLine("Error: {0}", e.Message);

			if(e.Exception != null)
			{
				// Verarbeite den Fehler
			}
		}

		static void MessageBroker_IntGenericMessageFired(GenericMessageEventArgs<int> e)
		{
			// Verarbeite die Nachricht
			Console.WriteLine("IntMessage: {0}", e.MessageObject);
		}

		static void MessageBroker_YourCustomTypeGenericMessageFired(GenericMessageEventArgs<YourCustomType> e)
		{
			// Verarbeite die Nachricht
			Console.WriteLine("IntMessage: {0}", e.MessageObject);
		}
	}
}
