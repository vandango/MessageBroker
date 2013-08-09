using System;
using System.Threading;

namespace Messaging
{
	public class MessageBroker<T>
	{
		public delegate void GenericMessageEventHandler(GenericMessageEventArgs<T> e);
		public static event GenericMessageEventHandler MessageFired;

		private static object _lockGeneric = new object();

		public static void SendMessageAsync(GenericMessageEventArgs<T> eventArgs)
		{
			GenericMessageEventHandler handler;

			lock(_lockGeneric)
			{
				handler = MessageFired;
			}

			if(handler != null)
			{
				Delegate[] invocationList = handler.GetInvocationList();

				foreach(GenericMessageEventHandler eventHandler in invocationList)
				{
					try
					{
						GenericMessageEventNotifier eventNotifier = new GenericMessageEventNotifier(eventHandler);
						ThreadPool.QueueUserWorkItem(eventNotifier.Start, eventArgs);
					}
					catch { }
				}
			}
		}

		private class GenericMessageEventNotifier
		{
			private GenericMessageEventHandler _eventHandler;

			public GenericMessageEventNotifier(GenericMessageEventHandler eventHandler)
			{
				this._eventHandler = eventHandler;
			}
			
			public void Start(object args)
			{
				GenericMessageEventArgs<T> e = args as GenericMessageEventArgs<T>;

				try
				{
					this._eventHandler.Invoke(e);
				}
				catch { }
			}
		}
	}
}
