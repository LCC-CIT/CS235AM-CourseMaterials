using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace EvolveLite
{
	public static class ImageExtensions
	{
		public static UIImage CorrectOrientation (this UIImage image)
		{
			if (image.Orientation == UIImageOrientation.Up)
				return image; 
			
			UIGraphics.BeginImageContextWithOptions (image.Size, false, image.CurrentScale);
			image.Draw (new RectangleF (0, 0, image.Size.Width, image.Size.Height));
			UIImage img = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return img;
		}
	}
}

