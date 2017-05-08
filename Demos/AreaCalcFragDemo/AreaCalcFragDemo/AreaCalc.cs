using System;

namespace AreaCalcFragDemo
{
	public class AreaCalc
	{
		public AreaCalc ()
		{
		}

		public double calcRectangleArea(double height, double width)
		{
			return height * width;
		}

		public double calcTriangleArea(double height, double b)
		{
			return height * b / 2.0;
		}

		public double calcCircleArea(double radius)
		{
			return Math.PI * Math.Pow(radius, 2.0);
		}
	}
}

