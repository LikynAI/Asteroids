using System;
using System.Drawing;

namespace game
{
	/// <summary>
	/// Базовый класс объекта
	/// </summary>
	abstract class BaseObject : ICollision
	{
		public Point Pos;
		public Point Dir;
		public Size Size;

		public delegate void Message();

		public BaseObject(Point Pos, Point Dir, Size Size)
		{
			this.Pos = Pos;
			this.Dir = Dir;
			this.Size = Size;
		}

		public BaseObject() { }

		/// <summary>
		/// Отрисовывает положение объекта
		/// </summary>
		public abstract void Draw();

		/// <summary>
		/// Обновляет положение объекта
		/// </summary>
		public abstract void Update();

		public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

		public Rectangle Rect => new Rectangle(Pos, Size);
	}
}
