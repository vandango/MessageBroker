using System;
using System.Collections.Generic;
using System.Text;

namespace Messaging
{
	public class YourCustomType
	{
		public YourCustomType()
		{
			this.Text = "Hallo Welt!";
		}

		public string Text { get; set; }

		public override string ToString()
		{
			return this.Text;
		}
	}
}
