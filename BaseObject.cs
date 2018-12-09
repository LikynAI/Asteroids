using System;
using System.Drawing;

namespace game
{
	/// <summary>
	/// Базовый класс объекта
	/// </summary>
	abstract class BaseObject 
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
		
		public abstract void Draw();

		/// <summary>
		/// Обновляет положение объекта
		/// </summary>
		public abstract void Update();
	}
}
