using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PigGame
{
	[Activity( Label = "My Activity" )]
	public class Menu : Activity
	{
		protected override void OnCreate( Bundle bundle )
		{
			base.OnCreate( bundle );
			SetContentView( Resource.Layout.Menu );
			var parent = (Main)this.Parent;
			var playsText = FindViewById<TextView>( Resource.Id.gamePlaysText );
			var playerWinText = FindViewById<TextView>( Resource.Id.playerWinsText );
			var startButton = FindViewById<ImageButton>( Resource.Id.StartButton );

			// Create your application here
			startButton.Click += delegate
			{
				if ( Pig.playing != true )
				{
					Pig.playNew();
					parent.TabHost.CurrentTab = 1;
				}
				else parent.TabHost.CurrentTab = 1;
			};

		}

		protected override void OnResume()
		{
			base.OnResume();
			var parent = (Main)this.Parent;
			var playsText = FindViewById<TextView>( Resource.Id.gamePlaysText );
			var playerWinText = FindViewById<TextView>( Resource.Id.playerWinsText );

			if ( Pig.played )
			{
				playsText.Text = Pig.totalPlays();

				playerWinText.Text = Pig.playerWins();
				
				if ( Pig.playing ) FindViewById<TextView>( Resource.Id.GameLabel ).Text = "Get back to playing!";
				else FindViewById<TextView>( Resource.Id.GameLabel ).Text = GetString( Resource.String.GameLabel );
				
			}
			else
			{
				playsText.Text = GetString( Resource.String.countLabel );
				playerWinText.Text = GetString( Resource.String.winLabel );
			}

			var startButton = FindViewById<ImageButton>( Resource.Id.StartButton );
			startButton.Click += delegate
			{
				if ( Pig.playing != true )
				{
					Pig.playNew();
					parent.TabHost.CurrentTab = 1;
				}
				else parent.TabHost.CurrentTab = 1;
			};

		}
	}
}