using System;

namespace Messaging
{
	public class GenericMessageEventArgs<T> : EventArgs
	{
		public GenericMessageEventArgs(T messageObject)
		{
			this.MessageObject = messageObject;
		}

		public GenericMessageEventArgs(object sender, T messageObject)
		{
			this.Sender = sender;
			this.MessageObject = messageObject;
		}

		public object Sender { get; set; }
		public T MessageObject { get; set; }
	}
}
