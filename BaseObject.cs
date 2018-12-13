using System;
using System.Drawing;

namespace game
{
	/// <summary>
	/// Базовый класс объекта
	/// </summary>
	abstract class BaseObject 
	{
		public Point Pos;
		public Point Dir;
		public Size Size;

		public BaseObject(Point Pos, Point Dir, Size Size)
		{
			this.Pos = Pos;
			this.Dir = Dir;
			this.Size = Size;
		}
		
		/// <summary>
		/// Отрисовывает положение объекта
		/// </summary>
		public abstract void Draw();

		/// <summary>
		/// Обновляет положение объекта
		/// </summary>
		public abstract void Update();
	}
}
