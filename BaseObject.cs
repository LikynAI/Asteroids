using System;
using System.Drawing;

namespace game
{
	class BaseObject
	{
		protected Point Pos;
		protected Point Dir;
		protected Size Size;

		public BaseObject(Point Pos, Point Dir, Size Size)
		{
			this.Pos = Pos;
			this.Dir = Dir;
			this.Size = Size;
		}

		public virtual void Draw()
		{
			Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
		}

		public virtual void Update()
		{
			Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;

			if (Pos.X < 0) { Dir.X = -Dir.X; }
			if (Pos.X > Game.Width) { Dir.X = -Dir.X; }
			if (Pos.Y < 0) { Dir.Y = -Dir.Y; }
			if (Pos.Y > Game.Height) { Dir.Y = -Dir.Y; }
		}
	}
}
