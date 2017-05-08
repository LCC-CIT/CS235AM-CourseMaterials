`SatelliteMenu` is an unobtrusive button that smoothly expands to a radial 'satellite' menu when tapped.
This control was popularized in Path's iOS app.

To setup a `SatelliteMenu` on iOS (be sure to add your own menu image resources first):

```csharp
using SatelliteMenu;
using System.Drawing;
...

public override void ViewDidLoad ()
{
	base.ViewDidLoad ();
	
	var image = UIImage.FromFile ("menu.png");
	var yPos = View.Frame.Height - image.Size.Height - 10;
	var frame = new RectangleF (10, yPos, image.Size.Width, image.Size.Height);

	var items = new [] { 
		new SatelliteMenuButtonItem (UIImage.FromFile ("icon1.png"), 1, "Search"),
		new SatelliteMenuButtonItem (UIImage.FromFile ("icon2.png"), 2, "Update"),
		new SatelliteMenuButtonItem (UIImage.FromFile ("icon3.png"), 3, "Share"),
		new SatelliteMenuButtonItem (UIImage.FromFile ("icon4.png"), 4, "Post"),
		new SatelliteMenuButtonItem (UIImage.FromFile ("icon5.png"), 5, "Reload"),
		new SatelliteMenuButtonItem (UIImage.FromFile ("icon6.png"), 6, "Settingd")
	};

	var button = new SatelliteMenuButton (View, image, items, frame);

	button.MenuItemClick += (_, args) => {
		Console.WriteLine ("{0} was clicked!", args.MenuItem.Name);
	};

	View.AddSubview (button);
}
```

*An Android version of this component is coming very soon.*

*Created by [IOS BITS LTD](http://iosbits.co.uk/new/).*
