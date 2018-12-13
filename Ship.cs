using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace game
{
	class Ship : BaseObject, ICollision
	{
		public Bullet[] Bullets;
		private int BulletNumber;
		public Point[] shippoints;
		Point nose;
		public int hp;
		int angle = 259;

		public Ship(Point Pos, Point Dir, Size Size) : base(Pos, Dir, Size)
		{
			nose = new Point(Pos.X, Pos.Y + Size.Height);

			double tempo = angle / 20.6d;

			shippoints = new Point[] 
			{
				nose
				,
				new Point(Pos.X + Convert.ToInt32(Math.Round(Math.Sin(tempo-2.5) * Size.Height)),
				Pos.Y + Convert.ToInt32(Math.Round(Math.Cos(tempo-2.5) * Size.Height))),

				new Point(Pos.X + (Pos.X - nose.X)/2, Pos.Y + (Pos.Y - nose.Y)/2),

				new Point(Pos.X + Convert.ToInt32(Math.Round(Math.Sin(tempo+2.5) * Size.Height)),
				Pos.Y + Convert.ToInt32(Math.Round(Math.Cos(tempo+2.5) * Size.Height))),

				nose

			};

			Bullets = new Bullet[10];
			BulletNumber = 0;
			hp = 3;
		}

		/// <summary>
		/// Отрисовывает корабль
		/// </summary>
		public override void Draw()
		{
			Game.Buffer.Graphics.DrawLines(Pens.White,shippoints);
		}

		/// <summary>
		/// Обновляет положение корабля в пространстве
		/// </summary>
		public override void Update()
		{
			Pos.X += + Dir.X;
			Pos.Y += + Dir.Y;

			shippoints[0] = nose;
			shippoints[4] = nose;

			for (int i = 0; i < shippoints.Length; i++)
			{
				shippoints[i].X += Dir.X;
				shippoints[i].Y += Dir.Y;
			}

			nose = shippoints[0];

			if (Pos.X < 0) { Dir.X = -Dir.X; }
			if (Pos.X > Game.Width - Size.Width) { Dir.X = -Dir.X; }
			if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
			if (Pos.Y > Game.Height - Size.Height) { Dir.Y = -Dir.Y; }
		}

		/// <summary>
		/// Обрабатывает нажатие клавишь
		/// </summary>
		/// <param name="k"></param>
		public void ButtonPres(object sender, KeyEventArgs k) 
		{
			if (k.KeyCode == Keys.W)
			{
				Dir.X += (nose.X - Pos.X) / 5;
				Dir.Y += (nose.Y - Pos.Y) / 5;
			}
			if (k.KeyCode == Keys.A)
			{
				angle += 2;
		 		Turn();
				Draw();
			}
			if (k.KeyCode == Keys.D)
			{
				angle -= 2;
				Turn();
				Draw();
			}
			if (k.KeyCode == Keys.ShiftKey)
			{
				Shoot();
			}
		}

		/// <summary>
		/// Создает объект пуля
		/// </summary>
		/// <returns></returns>
		public void Shoot()
		{
			Bullets[BulletNumber++] = new Bullet(Pos, new Point(nose.X - Pos.X, nose.Y - Pos.Y), new Size(1,1));
			if (BulletNumber >= 10) { BulletNumber = 0; }
		}

		/// <summary>
		/// Поворот корабля
		/// </summary>
		private void Turn()
		{
			double tempo = angle / 20.6d;
			nose.X = Pos.X + Convert.ToInt32(Math.Round(Math.Sin(tempo) * Convert.ToDouble(Size.Height)));
			nose.Y = Pos.Y + Convert.ToInt32(Math.Round(Math.Cos(tempo) * Convert.ToDouble(Size.Height)));

			shippoints[1].X = Pos.X + Convert.ToInt32(Math.Round(Math.Sin(tempo-2.5) * Convert.ToDouble(Size.Height)));
			shippoints[1].Y = Pos.Y + Convert.ToInt32(Math.Round(Math.Cos(tempo-2.5) * Convert.ToDouble(Size.Height)));

			shippoints[2].X = Pos.X + (Pos.X - nose.X) / 2;
			shippoints[2].Y = Pos.Y + (Pos.Y - nose.Y) / 2;

			shippoints[3].X = Pos.X + Convert.ToInt32(Math.Round(Math.Sin(tempo+2.5) * Convert.ToDouble(Size.Height)));
			shippoints[3].Y = Pos.Y + Convert.ToInt32(Math.Round(Math.Cos(tempo+2.5) * Convert.ToDouble(Size.Height)));
		}

		/// <summary>
		/// понижает показатель Hp на еденицу
		/// </summary>
		public void HpLow()
		{
			hp--;
			if (hp == 0) { MessageDie?.Invoke(); }
		}

		/// <summary>
		/// Повышает показатель Hp на еденицу
		/// </summary>
		public void HpUp() { hp++; }

		public static event Message MessageDie;
	}
}
