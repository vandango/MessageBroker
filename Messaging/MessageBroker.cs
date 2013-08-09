using System;
using System.Threading;

namespace Messaging
{
	public class MessageBroker
	{
		public delegate void MessageEventHandler(MessageEventArgs e);
		public static event MessageEventHandler MessageFired;
		private static object _lockMessage = new object();

		// Diese Methode schickt eine Nachricht asynchron ab. Hierbei
		// werden alle registrierten Abonnementen benachrichtigt.
		public static void SendMessageAsync(MessageEventArgs eventArgs)
		{
			// Implementierung der SendMessage-Methode
			MessageEventHandler handler;

			// Der Handler muss gesperrt werden, da ihn verschiedene Threads
			// aufrufen können.
			lock(_lockMessage)
			{
				handler = MessageFired;
			}

			// Wenn es keine Abonnementen gibt, ist der Handler NULL.
			if(handler != null)
			{
				// Jeder Abonnement kann in verschiedenen Threads benachrichtigt werden.
				Delegate[] invocationList = handler.GetInvocationList();

				foreach(MessageEventHandler eventHandler in invocationList)
				{
					try
					{
						// Führe nun durch die interne Klasse den Eventhandler in allen
						// nötigen Threads aus.
						MessageEventNotifier eventNotifier = new MessageEventNotifier(eventHandler);
						ThreadPool.QueueUserWorkItem(eventNotifier.Start, eventArgs);
					}
					// Wenn es zu einem Fehler kommen sollte, soll nichts weiter gemacht werden
					// und nur der nächste Handler aufgerufen werden.
					catch { }
				}
			}
		}

		// Diese Klasse ruft die verschiedenen Listener des MessageEventHandler's
		// in allen Threads auf. Sie ist nur für die interne Nachrichten-Verarbeitung
		// innerhalb der MessageBroker Klasse gedacht.
		private class MessageEventNotifier
		{
			// Implementierung des Message Event-Notifiers
			private MessageEventHandler _eventHandler;

			public MessageEventNotifier(MessageEventHandler eventHandler)
			{
				this._eventHandler = eventHandler;
			}

			public void Start(object args)
			{
				MessageEventArgs e = args as MessageEventArgs;

				try
				{
					this._eventHandler.Invoke(e);
				}
				catch { }
			}

		}

		public delegate void ErrorMessageEventHandler(ErrorMessageEventArgs e);
		public static event ErrorMessageEventHandler ErrorMessageFired;
		private static object _lockError = new object();

		// Diese Methode schickt eine Fehlernachricht asynchron ab. Auch hierbei
		// werden wieder alle registrierten Abonnementen benachrichtigt.
		public static void SendErrorMessageAsync(ErrorMessageEventArgs eventArgs)
		{
			// Implementierung der SendErrorMessage-Methode
			ErrorMessageEventHandler handler;

			// Der Handler muss gesperrt werden, da ihn verschiedene Threads
			// aufrufen können.
			lock(_lockError)
			{
				handler = ErrorMessageFired;
			}

			// Wenn es keine Abonnementen gibt, ist der Handler NULL.
			if(handler != null)
			{
				// Jeder Abonnement kann in verschiedenen Threads benachrichtigt werden.
				Delegate[] invocationList = handler.GetInvocationList();

				foreach(ErrorMessageEventHandler eventHandler in invocationList)
				{
					try
					{
						// Führe nun durch die interne Klasse den Eventhandler in allen
						// nötigen Threads aus.
						ErrorMessageEventNotifier eventNotifier = new ErrorMessageEventNotifier(eventHandler);
						ThreadPool.QueueUserWorkItem(eventNotifier.Start, eventArgs);
					}
					// Wenn es zu einem Fehler kommen sollte, soll nichts weiter gemacht werden
					// und nur der nächste Handler aufgerufen werden.
					catch { }
				}
			}
		}

		private class ErrorMessageEventNotifier
		{
			// Implementierung des ErrorMessage Event-Notifiers
			private ErrorMessageEventHandler _eventHandler;

			public ErrorMessageEventNotifier(ErrorMessageEventHandler eventHandler)
			{
				this._eventHandler = eventHandler;
			}

			public void Start(object args)
			{
				ErrorMessageEventArgs e = args as ErrorMessageEventArgs;

				try
				{
					this._eventHandler.Invoke(e);
				}
				catch { }
			}
		}
	}
}
