using System.Windows.Input;
using System.Windows;

namespace MouseJiggler
{
	internal class Program
	{
		private const int _millisecondsTimeout = 100;
		private static System.Drawing.Point _position = System.Windows.Forms.Cursor.Position;
		static void Main(string[] args)
		{
			while (true)
			{
				WaitForJiggleTime();

				ChangePosition(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y + 10);
				Thread.Sleep(_millisecondsTimeout);
				ChangePosition(System.Windows.Forms.Cursor.Position.X + 10, System.Windows.Forms.Cursor.Position.Y);
				Thread.Sleep(_millisecondsTimeout);
				ChangePosition(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y - 10);
				Thread.Sleep(_millisecondsTimeout);
				ChangePosition(System.Windows.Forms.Cursor.Position.X - 10, System.Windows.Forms.Cursor.Position.Y);
				Thread.Sleep(_millisecondsTimeout);
			}
		}

		private static bool WaitForJiggleTime()
		{
			while (true)
			{
				if (Equals(_position, System.Windows.Forms.Cursor.Position))
				{
					return true;
				}
				else
				{
					Thread.Sleep(60_000);
					_position = System.Windows.Forms.Cursor.Position;
					continue;
				}
			}
		}


		private static void ChangePosition(int newX, int newY)
		{
			var newPosition = new System.Drawing.Point(newX, newY);
			System.Windows.Forms.Cursor.Position = newPosition;
		}
	}
}

