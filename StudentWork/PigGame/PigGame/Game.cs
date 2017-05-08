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
	public class Game : Activity
	{

		protected override void OnCreate( Bundle bundle )
		{
			base.OnCreate( bundle );
			SetContentView( Resource.Layout.Game );
			// Create your application here
			var parent = (Main)this.Parent;
			var data = FindViewById<TextView>( Resource.Id.dataText );
			var die = FindViewById<ImageButton>( Resource.Id.dieRollButton );
			var endButton = FindViewById<Button>( Resource.Id.endTurnButton );
			var finishButton = FindViewById<Button>( Resource.Id.backToFirst );

			endButton.Enabled = false;
			finishButton.Visibility = Android.Views.ViewStates.Invisible;

			die.Click += delegate
			{
				endButton.Enabled = true;

				Pig.roll();
				if ( Pig.sides == 6 )
				{
					switch ( Pig.currentRoll )
					{
						case 1:
							die.SetImageResource( Resource.Drawable.Die1 );
							break;
						case 2:
							die.SetImageResource( Resource.Drawable.Die2 );
							break;
						case 3:
							die.SetImageResource( Resource.Drawable.Die3 );
							break;
						case 4:
							die.SetImageResource( Resource.Drawable.Die4 );
							break;
						case 5:
							die.SetImageResource( Resource.Drawable.Die5 );
							break;
						case 6:
							die.SetImageResource( Resource.Drawable.Die6 );
							break;
					}
				}
				if ( Pig.totalRoll != 0 ) Pig.canRoll = true;
				else die.Enabled = false;

				data.Text = Pig.displayData();
			};

			endButton.Click += delegate
			{
				Pig.pScore += Pig.totalRoll;
				if ( Pig.pScore < 100 ) Pig.computer();
				data.Text = Pig.displayData();
				if ( Pig.winner() == false )
				{
					die.Enabled = true;
					data.Text = Pig.displayData();
				}
				else
				{
					data.Text = Pig.finished();
					die.Enabled = false;
					finishButton.Visibility = Android.Views.ViewStates.Visible;
					Pig.playing = false;
					Pig.played = true;
				}
				endButton.Enabled = false;
				die.SetImageResource( Resource.Drawable.FullDice );
			};

			finishButton.Click += delegate
			{
				Pig.games++;
				Pig.pScore = Pig.cScore = 0;
				if ( Pig.pScore >= 100 ) Pig.wins++;
				parent.TabHost.CurrentTab = 0;
				finishButton.Visibility = Android.Views.ViewStates.Invisible;
			};
		}

		protected override void OnResume()
		{
			base.OnResume();

			var parent = (Main)this.Parent;
			var data = FindViewById<TextView>( Resource.Id.dataText );
			var die = FindViewById<ImageButton>( Resource.Id.dieRollButton );
			var endButton = FindViewById<Button>( Resource.Id.endTurnButton );
			var finishButton = FindViewById<Button>( Resource.Id.backToFirst );

			if ( Pig.winner() == true )
			{
				finishButton.Visibility = ViewStates.Visible;
				die.Enabled = false;
				endButton.Enabled = false;
				data.Text = Pig.displayData();
			}
			else finishButton.Visibility = ViewStates.Invisible;

			if ( Pig.playing != true )
			{
				endButton.Enabled = false;
				die.Enabled = false;
				data.Text = Pig.mustStart();
				if ( Pig.played == true ) die.Enabled = true;
			}
			else
			{
				data.Text = Pig.displayData();
				if ( Pig.currentRoll == 1 ) die.Enabled = false;
				else die.Enabled = true;
			}

			

		}
	}
}