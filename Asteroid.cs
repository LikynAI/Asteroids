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

		public override void Draw()
		{
			Game.Buffer.Graphics.DrawEllipse(Pens.White,new Rectangle(Pos.X, Pos.Y, Size.Width, Size.Height));
		}

		public void Hit(Asteroid a)
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

		}

		public override void Update()
		{
			Pos.X = Pos.X + Dir.X;
			Pos.Y = Pos.Y + Dir.Y;

			if (Pos.X < 0) { Dir.X = -Dir.X; }
			if (Pos.X > Game.Width - Size.Width) { Dir.X = -Dir.X; }
			if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
			if (Pos.Y > Game.Height - Size.Height) { Dir.Y = -Dir.Y; }
		}

		public void Spawn()
		{

		}

		public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

		public Rectangle Rect => new Rectangle(Pos, Size);
	}
}
