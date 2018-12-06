using System;
using System.Drawing;

namespace game
{
	class Asteroid : BaseObject
	{
		public Asteroid(Point Pos, Point Dir, Size Size) : base(Pos, Dir, Size) { }

		public override void Draw()
		{
			Image asteroid = Image.FromFile("asteroid.png");
			Game.Buffer.Graphics.DrawImage(asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);
		}

		public override void Update()
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
