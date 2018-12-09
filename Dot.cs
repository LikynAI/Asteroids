using System;
using System.Drawing;

namespace game
{
	/// <summary>
	/// Класс объекта точка
	/// </summary>
	class Dot : Star
	{
		public Dot(Point Pos, Point Dir, Size Size) : base(Pos, Dir, Size) { }

		public override void Draw()
		{
			Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, new RectangleF(Pos.X, Pos.Y, Size.Width, Size.Height));
		}

		public override void Update()
		{
			Pos.X = Pos.X + Dir.X;
			if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
		}
	}
}
