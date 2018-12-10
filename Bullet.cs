using System;
using System.Drawing;

namespace game
{
	class Bullet : BaseObject, ICollision
	{
		Point p;

		public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
			p = new Point(Pos.X + Dir.X, Pos.Y + Dir.Y);
		}

		public override void Draw()
		{
			Game.Buffer.Graphics.DrawLine(Pens.Orange, Pos,p);
		}

		public override void Update()
		{
			Pos.X += Dir.X;
			Pos.Y += Dir.Y;

			p.X += Dir.X;
			p.Y += Dir.Y;
		}

		public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

		public Rectangle Rect => new Rectangle(Pos, Size);
	}
}

