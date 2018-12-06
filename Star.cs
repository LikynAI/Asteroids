using System;
using System.Drawing;

namespace game
{
	class Star : BaseObject
	{
		public Star(Point Pos, Point Dir, Size Size) : base(Pos, Dir, Size) { }

		public override void Draw()
		{
			Image star = Image.FromFile("star.png");
			Game.Buffer.Graphics.DrawImage(star, Pos.X, Pos.Y, Size.Width, Size.Height);
		}

		public override void Update()
		{
			Pos.X = Pos.X + Dir.X;
			if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
		}
	}
}
