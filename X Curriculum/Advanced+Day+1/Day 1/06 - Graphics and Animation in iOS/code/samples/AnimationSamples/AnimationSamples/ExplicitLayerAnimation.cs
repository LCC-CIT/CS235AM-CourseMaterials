
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace AnimationSamples
{
	public partial class ExplicitLayerAnimation : UIViewController
	{
		CALayer layer;

		public ExplicitLayerAnimation () : base ("ExplicitLayerAnimation", null)
		{
		}

		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			layer = new CALayer ();
			layer.Bounds = new RectangleF (0, 0, 50, 50);
			layer.Position = new PointF (50, 50);
			layer.Contents = UIImage.FromFile ("monkey2.png").CGImage;
			layer.ContentsGravity = CALayer.GravityResize;
			layer.BorderWidth = 1.5f;
			layer.BorderColor = UIColor.Green.CGColor;
			
			View.Layer.AddSublayer (layer);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// get the initial value to start the animation from
			PointF fromPt = layer.Position;

			// set the position to coincide with the final animation value
			// to prevent it from snapping back to the starting position
			// after the animation completes
			layer.Position = new PointF (200, 300);

			// create a path for the animation to follow
			CGPath path = new CGPath ();
			path.AddLines (new PointF[] { fromPt, new PointF (50, 300), new PointF (200, 50), new PointF (200, 300) });

			// create a keyframe animation for the position using the path
			CAKeyFrameAnimation animPosition = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("position");
			animPosition.Path = path;
			animPosition.Duration = 2;

			// add the animation to the layer
			// the "position" key is used to overwrite the implicit animation created
			// when the layer positino is set above
			layer.AddAnimation (animPosition, "position");
		}
	}
}

