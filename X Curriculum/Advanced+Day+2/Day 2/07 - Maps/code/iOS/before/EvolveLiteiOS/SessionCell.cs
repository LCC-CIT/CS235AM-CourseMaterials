using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace EvolveLite
{
	public class SessionCell : UITableViewCell
	{
		UILabel titleLabel, timeLabel, speakerLabel;
		UIImageView bgImgView;

		public Session Session { get; set; }

		public SessionCell (string reuseIdentifier): base(UITableViewCellStyle.Default, reuseIdentifier)
		{
			titleLabel = new UILabel { Font = UIFont.FromName("HelveticaNeue", 12.0f ), BackgroundColor = UIColor.Clear  };
			timeLabel = new UILabel { Font = UIFont.FromName("HelveticaNeue", 12.0f ), TextColor = UIColor.Blue, BackgroundColor = UIColor.Clear };
			speakerLabel = new UILabel { Font = UIFont.FromName("HelveticaNeue", 12.0f ), BackgroundColor = UIColor.Clear  };

			ContentView.AddSubview (titleLabel);
			ContentView.AddSubview (timeLabel);
			ContentView.AddSubview (speakerLabel);

			bgImgView = new UIImageView ();
			BackgroundView = bgImgView;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			float padding = 5.0f;

			titleLabel.Text = Session.Title;
			timeLabel.Text = String.Format ("{0} - {1}", Session.Begins.ToShortTimeString (), Session.Ends.ToShortTimeString ());
			speakerLabel.Text = (Session.Speaker != null) ? Session.Speaker.Name : "";

			RectangleF b = ContentView.Bounds;

			var titleRect = new RectangleF (b.Left + padding, b.Top + padding, b.Width - 2 * padding, b.Height / 3);
			titleLabel.Frame = titleRect;

			var speakerRect = new RectangleF (b.Left + padding, titleRect.Bottom + padding, b.Width/2 - 2 * padding, b.Height/3);
			speakerLabel.Frame = speakerRect;	

			var timeRect = new RectangleF (speakerRect.Right + 5 * padding, titleRect.Bottom + padding, b.Width/2 - 2 * padding, b.Height/3);
			timeLabel.Frame = timeRect;

			UIImage backgroundImage;
			UIGraphics.BeginImageContext (b.Size);
			var g = UIGraphics.GetCurrentContext ();
			CGGradient gr = new MonoTouch.CoreGraphics.CGGradient (CGColorSpace.CreateDeviceRGB (), new CGColor[] {
				UIColor.LightGray.CGColor,
				UIColor.White.CGColor
			});
			g.DrawLinearGradient (gr, new PointF (0, 0), new PointF (b.Width, b.Height), CGGradientDrawingOptions.DrawsBeforeStartLocation);
			backgroundImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();

			bgImgView.Image = backgroundImage;
			bgImgView.Frame = new RectangleF (b.Left + 2 * padding, b.Top, b.Width, b.Height);
		}
	}
}

