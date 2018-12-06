using System;
using System.Windows.Forms;
using System.Drawing;

namespace game
{
	class Game
	{
		private static BufferedGraphicsContext contex;
		public static BufferedGraphics Buffer;

		public static int Width { get; set; }
		public static int Height { get; set; }

		public static BaseObject[] objs;

		public static void Init(Form form)
		{
			Graphics g;

			contex = BufferedGraphicsManager.Current;

			g = form.CreateGraphics();

			Width = form.ClientSize.Width;
			Height = form.ClientSize.Height;

			Buffer = contex.Allocate(g, new Rectangle(0, 0, Width, Height));

			Load();

			Timer timer = new Timer { Interval = 1 };
			timer.Start();
			timer.Tick += Timer_Tick;
		}

		public static void Draw()
		{
			Buffer.Graphics.Clear(Color.Black);
			foreach (BaseObject obj in objs)
			{
				obj.Draw();
			}
			Buffer.Render();
		}

		public static void Update()
		{
			foreach (BaseObject obj in objs)
			{
				obj.Update();
			}
		}

		public static void Load()
		{
			objs = new BaseObject[45];
			for (int i = 0; i < objs.Length/3; i++)
			{
				objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i-1,-i-1), new Size(10, 10));
			}

			for (int i = objs.Length / 3; i < objs.Length / 3 * 2; i++)
			{
				objs[i] = new Star(new Point(600, (i - objs.Length / 3)*40), new Point(-i, 0), new Size(5, 5));
			}


			for (int i = objs.Length / 3 * 2; i < objs.Length ; i++)
			{
				objs[i] = new Dot(new Point(600, (i - objs.Length / 3 * 2) * 40+20), new Point(-objs.Length*4/3 + i , 0), new Size(4, 4));
			}
		}

		private static void Timer_Tick(object sender, EventArgs e)
		{
			Draw();
			Update();
		}
	}
}
