using System;
using System.Windows.Forms;
using System.Drawing;

namespace game
{
	class Game
	{
		private static BufferedGraphicsContext contex;
		public static BufferedGraphics Buffer;
		static Form form;

		public static int Width { get; set; }
		public static int Height { get; set; }

		public static Asteroid[] Asteroids;
		public static BaseObject[] BackScreen;
		public static Ship ship;
		public static Counter Lifes;
		public static Counter Score;
		public static Heal heal;

		private static Timer timer = new Timer();

		/// <summary>
		/// Запуск игры
		/// </summary>
		/// <param name="form"></param>
		public static void Init(Form _form)
		{
			form = _form;

			form.Focus();

			Graphics g;

			contex = BufferedGraphicsManager.Current;

			g = form.CreateGraphics();

			Width = form.ClientSize.Width;
			Height = form.ClientSize.Height;

			Buffer = contex.Allocate(g, new Rectangle(0, 0, Width, Height));

			Load();

			timer = new Timer { Interval = 10 };
			timer.Start();
			timer.Tick += Timer_Tick;

			form.KeyDown += ship.ButtonPres;

			Ship.MessageDie += endgame;
		}

		/// <summary>
		/// Отрисовывает все объекты
		/// </summary>
		public static void Draw()
		{
			Buffer.Graphics.Clear(Color.Black);
			foreach (BaseObject obj in BackScreen)
			{
				if (obj != null)
				{
					obj.Draw();
				}
			}

			foreach (var Asteroid in Asteroids)
			{
				if (Asteroid != null)
				{
					Asteroid.Draw();
				}
			}

			foreach (var Bullet in ship.Bullets)
			{
				if (Bullet != null)
				{
					Bullet.Draw();
				}
			}

			ship.Draw();

			Lifes.Draw();
			Score.Draw();
			heal.Draw();

			Buffer.Render();	
		}

		/// <summary>
		/// Обновляет Положение всех объектов
		/// </summary>
		public static void Update()
		{
			foreach (BaseObject obj in BackScreen)
			{
				if (obj != null)
				{
					obj.Update();
				}
			}

			foreach (var Asteroid in Asteroids)
			{
				if (Asteroid != null)
				{
					Asteroid.Update();
					if (ship.Bullets != null)
					{
						foreach (var Bullet in ship.Bullets)
						{
							if (Bullet != null && Asteroid.Collision(Bullet))
							{
								Asteroid.Shooted();
								Score.Count++;
							}
						}
					}

					foreach (var Asteroid1 in Asteroids)
					{
						if (Asteroid1 != null && !Equals(Asteroid1, Asteroid) && Asteroid.Collision(Asteroid1))
						{
							Asteroid.Hit(Asteroid1);
						}
					}

					if (Asteroid.Collision(ship))
					{
						Asteroid.Hit(ship);
						ship.HpLow();
						if (ship.hp == 1) { heal.Spawn(); }
						Lifes.Update(ship.hp);
					}

					if (heal.Collision(ship))
					{
						ship.HpUp();
						heal.stop();
					}

					heal.Update();
				}
			}

			foreach (var Bullet in ship.Bullets)
			{
				if (Bullet != null)
				{
					Bullet.Update();
				}
			}
			ship.Update();

			Lifes.Update(ship.hp);
			Score.Update(Score.Count);
		}

		/// <summary>
		/// Загружает все объекты
		/// </summary>
		public static void Load()
		{
			Random r = new Random();

			Asteroids = new Asteroid[20];
			BackScreen = new BaseObject[40];

			for (int i = 0; i < BackScreen.Length / 2; i++)
			{
				BackScreen[i] = new Star(new Point(r.Next(Width), i * 40), new Point(-i/*0*/, 0), new Size(5, 5));
			}

			for (int i = BackScreen.Length / 2; i < BackScreen.Length; i++)
			{
				BackScreen[i] = new Dot(new Point(r.Next(Width), (BackScreen.Length - i) * 40), new Point(-BackScreen.Length + i/*0, 0*/), new Size(4, 4));
			}

			for (int i = 0; i < 5; i++)
			{
				Asteroids[i] = new Asteroid(new Point(r.Next(Width - 40), i * 40), new Point(r.Next(-3, 3), r.Next(-3, 3)), new Size(50, 50));
			}
			ship = new Ship(new Point(400, 300), new Point(0, 0), new Size(20, 20));

			Lifes = new Counter(new Point(10, 10), new Point(), new Size(10, 10), ship.hp);
			Score = new Counter(new Point(950, 10), new Point(), new Size(10, 10), 0);

			heal = new Heal(new Point(-10,-10), new Point(0,0), new Size(10, 10));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Timer_Tick(object sender, EventArgs e)
		{
			Draw();
			Update();
		}

		/// <summary>
		/// Метод окончания игры
		/// </summary>
		public static void endgame()
		{
			timer.Stop();
			Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
			Buffer.Render();
		}
	}
}
