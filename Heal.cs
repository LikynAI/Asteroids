using System;
using System.Drawing;

namespace game
{
	class Heal : BaseObject
	{
		public Heal(Point Pos, Point Dir, Size Size) : base(Pos, Dir, Size){	}

		public override void Draw()
		{
			Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
			Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
		}

		/// <summary>
		/// обновляет положение аптечки
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
		/// Спаун аптечки за экраном
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
			Dir.X = r.Next(3);
			Dir.Y = r.Next(3);
		}

		/// <summary>
		/// Проверяет аптечку на столкновение с объектом
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

		public Rectangle Rect => new Rectangle(Pos, Size);
	}
}

