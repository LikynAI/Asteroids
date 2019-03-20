using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace game
{
	class Game
	{
		private static BufferedGraphicsContext contex;
		public static BufferedGraphics Buffer;
		static Form form;

		public static int Width { get; set; }
		public static int Height { get; set; }

		public static List<Asteroid> Asteroids;
		public static BaseObject[] BackScreen;
		public static Ship ship;
		public static Counter Lifes;
		public static Counter Score;
		public static Heal heal;

		private static int Level;

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

			form.KeyDown += ship.Move;
			form.KeyUp += ship.Shoot;

			form.MouseMove += ship.Rotate;

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

			if (Asteroids.Count == 0)
			{ LoadAteroids(Asteroids, Level++); }

			for (int i = 0; i < Asteroids.Count; i++)
			{
				if (Asteroids[i] != null)
				{
					Asteroids[i].Update();
					if (ship.Bullets != null)
					{
						foreach (var Bullet in ship.Bullets)
						{
							if (Bullet != null && i < Asteroids.Count && Asteroids[i].Collision(Bullet))
							{
								Asteroids.RemoveAt(i);
								Score.Count++;
							}
						}
					}

					foreach (var Asteroid1 in Asteroids)
					{
						if (Asteroid1 != null && i < Asteroids.Count && !Equals(Asteroid1, Asteroids[i]) && Asteroids[i].Collision(Asteroid1))
						{
							Asteroids[i].Hit(Asteroid1);
						}
					}

					if (i < Asteroids.Count && Asteroids[i].Collision(ship))
					{
						Asteroids[i].Hit(ship);
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

		private static void LoadAteroids(List<Asteroid> asteroids, int count)
		{
			for (int i = 0; i < count; i++)
			{
				Asteroids.Add(Asteroid.Spawn());
			}
		}

		/// <summary>
		/// Загружает все объекты
		/// </summary>
		public static void Load()
		{
			Random r = new Random();

			Asteroids = new List<Asteroid>();
			BackScreen = new BaseObject[40];

			Level = 5;

			for (int i = 0; i < BackScreen.Length / 2; i++)
			{
				BackScreen[i] = new Star(new Point(r.Next(Width), i * 40), new Point(-i/*0*/, 0), new Size(5, 5));
			}

			for (int i = BackScreen.Length / 2; i < BackScreen.Length; i++)
			{
				BackScreen[i] = new Dot(new Point(r.Next(Width), (BackScreen.Length - i) * 40), new Point(-BackScreen.Length + i/*0, 0*/), new Size(4, 4));
			}

			for (int i = 0; i < Level; i++)
			{
				Asteroids.Add(new Asteroid(new Point(r.Next(Width - 40), i * 40), new Point(r.Next(-3, 3), r.Next(-3, 3)), new Size(50, 50)));
			}
			ship = new Ship(new Point(400, 300), new Point(0, 0), new Size(20, 20));

			Lifes = new Counter(new Point(10, 10), new Point(), new Size(10, 10), ship.hp);
			Score = new Counter(new Point(900, 10), new Point(), new Size(10, 10), 0);

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
			Buffer.Graphics.DrawString("GAME OVER", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.White, 200, 100);
			Buffer.Render();
		}
	}
}
