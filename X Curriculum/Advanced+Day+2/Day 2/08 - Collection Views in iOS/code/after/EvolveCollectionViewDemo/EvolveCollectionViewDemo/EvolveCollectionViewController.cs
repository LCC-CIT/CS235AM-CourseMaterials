using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace EvolveCollectionViewDemo
{
	public class EvolveCollectionViewController : UICollectionViewController
	{
		static readonly NSString cellId = new NSString ("ImageCell");
		static readonly NSString headerId = new NSString ("Header");

		// used to keep the cell on top of other cells when scaled while highlighting
		int cellZIndex = 1;

		public Speakers Speakers { get; private set; }

		public EvolveCollectionViewController (UICollectionViewLayout layout) : base (layout)
		{
			CollectionView.ContentSize = UIScreen.MainScreen.Bounds.Size;
			CollectionView.BackgroundColor = UIColor.White;

			Speakers = new Speakers ();
		}
            
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
              
			// register the ImageCell so it can be created from a DequeueReusableCell call
			CollectionView.RegisterClassForCell (typeof(ImageCell), cellId);

			// register the class for the supplementary view
			CollectionView.RegisterClassForSupplementaryView (typeof(Header), UICollectionElementKindSection.Header, headerId);
		}
          
		public override int GetItemsCount (UICollectionView collectionView, int section)
		{
			return Speakers.Count;
		}
            
		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			// get an ImageCell from the pool. DequeueReusableCell will create one if necessary
			ImageCell imageCell = (ImageCell)collectionView.DequeueReusableCell (cellId, indexPath);

			// update the image for the speaker
			imageCell.UpdateImage (Speakers [indexPath.Row].ImageFile);
                
			return imageCell;
		}

		public override UICollectionReusableView GetViewForSupplementaryElement (UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
		{
			// get a Header instance to use for the supplementary view
			var headerView = (Header)collectionView.DequeueReusableSupplementaryView (elementKind, headerId, indexPath);
			headerView.Text = "Evolve Speakers";
			return headerView;
		}

		public override void ItemHighlighted (UICollectionView collectionView, NSIndexPath indexPath)
		{
			UICollectionViewCell cell = collectionView.CellForItem (indexPath);

			// animate the cell to scale up when highlighted
			UIView.Animate (
				duration: 0.2, 
				animation: () => { 
					cell.ContentView.Transform = CGAffineTransform.MakeScale (1.1f, 1.1f);
					cell.BackgroundView.Transform = CGAffineTransform.MakeScale (1.4f, 1.4f);
					cell.BackgroundView.BackgroundColor = UIColor.Purple;
					cell.Layer.ZPosition = ++cellZIndex;
				}
			);
		}
		
		public override void ItemUnhighlighted (UICollectionView collectionView, NSIndexPath indexPath)
		{
			// restore the cell to its original scale when unhighlighted
			UICollectionViewCell cell = collectionView.CellForItem (indexPath);
			cell.BackgroundView.BackgroundColor = UIColor.Black;
			cell.ContentView.Transform = CGAffineTransform.MakeScale (0.9f, 0.9f);
			cell.BackgroundView.Transform = CGAffineTransform.MakeScale (1.0f, 1.0f);
		}

		// class to use for cell
		class ImageCell : UICollectionViewCell
		{
			UIImageView imageView;

			[Export ("initWithFrame:")]
			ImageCell (RectangleF frame) : base (frame)
			{
				// create an image view to use in the cell
				imageView = new UIImageView (new RectangleF (0, 0, 100, 100)); 
				imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

				// populate the content view
				ContentView.AddSubview (imageView);

				// scale the content view down so that the background view is visible, effecively as a border
				ContentView.Transform = CGAffineTransform.MakeScale (0.9f, 0.9f);

				// background view displays behind content view and selected background view
				BackgroundView = new UIView{BackgroundColor = UIColor.Black};

				// selected background view displays over background view when cell is selected
				SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Yellow};
			}

		    internal void UpdateImage (string path)
			{
				using (var image = UIImage.FromFile(path)) {
					imageView.Image = image;
				}
			}
		}

		// class to use for supplementary view
		class Header : UICollectionReusableView
		{
			UILabel label;

			// string to display in the label
			internal string Text {
				set {
					label.Text = value;
				}
			}
			
			[Export ("initWithFrame:")]
		    Header (RectangleF frame) : base (frame)
			{
				label = new UILabel (){
					Frame = new RectangleF (0,0,UIScreen.MainScreen.Bounds.Width, 50),  
					BackgroundColor = UIColor.Purple,
					TextColor = UIColor.White,
					TextAlignment = UITextAlignment.Center,
					AutoresizingMask = UIViewAutoresizing.FlexibleWidth
				};

				AddSubview (label);
			}
		}
	}          
}

