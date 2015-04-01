using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ETD.CustomObjects.CustomUIObjects
{
	class Arrow
	{
		//Constants
		const double spokeAngle = 20;
		const double spokeLength = 20;

		//Start and end point coordinates
		double X1;
		double Y1;
		double X2;
		double Y2;

		//Lines
		Line mainLine;
		Line spoke1;
		Line spoke2;

		public Arrow(Canvas Canvas_map, double X1, double Y1, double X2, double Y2)
		{
			BuildLines();
			ShowLines(Canvas_map);
			DrawArrow(X1, Y1, X2, Y2);
		}

		//Creating all lines
		private void BuildLines()
		{
			mainLine = new Line();
			mainLine.Stroke = new SolidColorBrush(Colors.Green);
			mainLine.StrokeThickness = 2;

			spoke1 = new Line();
			spoke1.Stroke = new SolidColorBrush(Colors.Green);
			spoke1.StrokeThickness = 2;

			spoke2 = new Line();
			spoke2.Stroke = new SolidColorBrush(Colors.Green);
			spoke2.StrokeThickness = 2;
		}

		//Adding the lines to the map
		internal void ShowLines(Canvas Canvas_map)
		{
			Canvas_map.Children.Add(mainLine);
			Canvas_map.Children.Add(spoke1);
			Canvas_map.Children.Add(spoke2);
		}

		//Draw line with the same destination
		internal void DrawArrow(double X1, double Y1)
		{
			DrawArrow(X1, Y1, mainLine.X2, mainLine.Y2);
		}

		//Fixing the start and end point of the main line and the 2 spokes of the arrow
		internal void DrawArrow(double X1, double Y1, double X2, double Y2)
		{
			//Fixing main line
			mainLine.X1 = X1;
			mainLine.Y1 = Y1;
			mainLine.X2 = X2;
			mainLine.Y2 = Y2;

			//Getting vector of main line in the opposite direction
			double dx = X1 - X2;
			double dy = Y1 - Y2;

			//Getting length of vector
			double length = Math.Sqrt((dx * dx) + (dy * dy));

			//Normalizing the vector
			double dxUnit = dx / length;
			double dyUnit = dy / length;

			//Setting length of spoke
			double spokeX = dxUnit * spokeLength;
			double spokeY = dyUnit * spokeLength;
			
			//Getting vector for the end point of spoke1
			double radAngle = spokeAngle / 180 * Math.PI;
			double xVector1 = (spokeX * Math.Cos(radAngle)) - (spokeY * Math.Sin(radAngle));
			double yVector1 = (spokeX * Math.Sin(radAngle)) + (spokeY * Math.Cos(radAngle));

			//Getting vector for the end point of spoke2
			double xVector2 = (spokeX * Math.Cos(-radAngle)) - (spokeY * Math.Sin(-radAngle));
			double yVector2 = (spokeX * Math.Sin(-radAngle)) + (spokeY * Math.Cos(-radAngle));

			//Getting end point of the spoke1
			double xSpoke1 = X2 + xVector1;
			double ySpoke1 = Y2 + yVector1;

			//Getting end point of the spoke2
			double xSpoke2 = X2 + xVector2;
			double ySpoke2 = Y2 + yVector2;

			//Creating and fixing spoke1
			spoke1.X1 = X2;
			spoke1.Y1 = Y2;
			spoke1.X2 = xSpoke1;
			spoke1.Y2 = ySpoke1;

			//Creating and fixing spoke2
			spoke2.X1 = X2;
			spoke2.Y1 = Y2;
			spoke2.X2 = xSpoke2;
			spoke2.Y2 = ySpoke2;
		}

		//Removing arrow from map
		public void ClearArrow(Canvas Canvas_map)
		{
			Canvas_map.Children.Remove(mainLine);
			Canvas_map.Children.Remove(spoke1);
			Canvas_map.Children.Remove(spoke2);
		}
	}
}
