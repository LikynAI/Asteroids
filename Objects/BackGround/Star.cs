﻿using System;
using System.Drawing;

namespace game
{
	/// <summary>
	/// Класс объекта звезда
	/// </summary>
	class Star : BaseObject
	{
		public Star(Point Pos, Point Dir, Size Size) : base(Pos, Dir, Size) { }

		/// <summary>
		/// Отрисовка звезды
		/// </summary>
		public override void Draw()
		{
			Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
			Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
		}

		/// <summary>
		/// Обновление положения звезды
		/// </summary>
		public override void Update()
		{
			Pos.X = Pos.X + Dir.X;
			if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
		}
	}
}
