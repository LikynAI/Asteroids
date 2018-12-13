using System;
using System.Drawing;

namespace game
{
	class Counter : BaseObject 
	{
		public int Count;

		public Counter(Point Pos, Point Dir, Size Size, int t) : base(Pos, Dir, Size)
		{
			Count = t;
		}

		public override void Draw()
		{
			Game.Buffer.Graphics.DrawString(Convert.ToString(Count), new Font("Arial",16),Brushes.White,Pos.X,Pos.Y,StringFormat.GenericDefault);
		}

		public void Update(int t)
		{
			Count = t;
		}

		public override void Update()
		{

		}
	}
}
