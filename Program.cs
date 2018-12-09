using System;
using System.Windows.Forms;

namespace game
{
	class Program
	{
		static void Main(string[] args)
		{
			Form form = new Form();
			try
			{
				form.Width = 1680;
				form.Height = 1050;
			}
			catch
			{
				throw new ArgumentOutOfRangeException();
			}
			Game.Init(form);
			form.Show();
			Game.Draw();
			Application.Run(form);
		}
	}
}
