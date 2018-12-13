using System;
using System.Windows.Forms;

namespace game
{
	class Program
	{
		static void Main(string[] args)
		{
			Form form = new Form();
			bool flag = false;

			while (!flag)
			{
				try
				{
					Console.WriteLine("Введите значения ширины экрана");
					flag = int.TryParse(Console.ReadLine(), out int tempo);
					form.Width = tempo;
					if (!flag) { throw new ArgumentException(); }

					Console.WriteLine("Введите значения высоты экрана");
					flag = int.TryParse(Console.ReadLine(), out tempo);
					form.Height = tempo;
					if (!flag) { throw new ArgumentException(); }

					if (form.Width < 0 || form.Height < 1 || form.Width > 1000 || form.Height > 1000)
					{
						flag = false;
						throw new ArgumentException();
					}
					flag = true;
				}
				catch (ArgumentException)
				{
					Console.WriteLine("Ширина и высота целые числа непревышающие 1000");
				}
			}
			form.Height = 1000;
			form.Width = 1000;
			Game.Init(form);
			form.Show();
			Game.Draw();
			Application.Run(form);
		}
	}
}
