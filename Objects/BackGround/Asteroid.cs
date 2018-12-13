using System;
using System.Drawing;

namespace game
{
	class Asteroid : BaseObject, ICollision
	{
		public int Power;

		public Asteroid(Point Pos, Point Dir, Size Size) : base(Pos, Dir, Size)
		{
			Power = 1;
		}

		/// <summary>
		/// Отрисовывает астероид
		/// </summary>
		public override void Draw()
		{
			Game.Buffer.Graphics.DrawEllipse(Pens.White,new Rectangle(Pos.X, Pos.Y, Size.Width, Size.Height));
		}

		/// <summary>
		/// изменяет траектории астероидов при столкновении
		/// </summary>
		/// <param name="a"></param>
		public void Hit(BaseObject a)
		{
			int x = Dir.X;
			int y = Dir.Y;

			Dir.X = a.Dir.X;
			Dir.Y = a.Dir.Y;

			a.Dir.X = x;
			a.Dir.Y = y;
		}

		public void Shooted()
		{
			Spawn();
		}

		/// <summary>
		/// обновляет положение астероида
		/// </summary>
		public override void Update()
		{
			Pos.X = Pos.X + Dir.X;
			Pos.Y = Pos.Y + Dir.Y;

			if (Pos.X < 0) { Dir.X = -Dir.X; }
			if (Pos.X > Game.Width - Size.Width) { Dir.X = -Dir.X; }
			if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
			if (Pos.Y > Game.Height - Size.Height) { Dir.Y = -Dir.Y; }
		}

		/// <summary>
		/// Спаун астероида за экраном
		/// </summary>
		public void Spawn()
		{
			Random r = new Random();
			int t = r.Next(4);

			if (t == 0)
			{
				Pos.X = 20;
				Pos.Y = 20;
			}
			else if (t == 1)
			{
				Pos.X = 20;
				Pos.Y = 900;
			}
			else if (t == 2)
			{
				Pos.X = 900;
				Pos.Y = 20;
			}
			else if (t == 3)
			{
				Pos.X = 900;
				Pos.Y = 900;
			}
		}
	}
}
