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
	[Activity( Label = "My Activity", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation )]
	public class Settings : Activity
	{
		private int temp_sides = 6;
		private int temp_dies = 1;
		private int temp_goal = 100;
		protected override void OnCreate ( Bundle bundle )
		{
			base.OnCreate( bundle );
			SetContentView( Resource.Layout.Settings );

			Spinner spinner = FindViewById<Spinner>( Resource.Id.spinner );

			spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>( spinner_ItemSelected );
			var adapter = ArrayAdapter.CreateFromResource( this, Resource.Array.die_numbers_array, Android.Resource.Layout.SimpleSpinnerItem );

			adapter.SetDropDownViewResource( Android.Resource.Layout.SimpleSpinnerDropDownItem );
			spinner.Adapter = adapter;
			spinner.SetSelection( adapter.GetPosition( "6" ) );

			RadioButton rb1 = FindViewById<RadioButton>( Resource.Id.radioButton1 );
			RadioButton rb2 = FindViewById<RadioButton>( Resource.Id.radioButton2 );
			RadioButton rb3 = FindViewById<RadioButton>( Resource.Id.radioButton3 );

			rb1.Click += rb_Click;
			rb2.Click += rb_Click;
			rb3.Click += rb_Click;

			EditText et = FindViewById<EditText>( Resource.Id.scoreinput );
			et.Text = temp_goal.ToString();
			et.KeyPress += et_KeyPress;

			Button commit = FindViewById<Button>( Resource.Id.Commit );
			commit.Click += commit_Click;
		}

		protected override void OnResume ()
		{
			base.OnResume();
			summary();
			Button commit = FindViewById<Button>( Resource.Id.Commit );
			if ( Pig.playing == true )
				commit.Enabled = false;
			else
				commit.Enabled = true;
		}

		void commit_Click ( object sender, EventArgs e )
		{
			if(Pig.playing == false)
			{
				Pig.sides = temp_sides;
				Pig.numberOfDies = temp_dies;
				Pig.goal = temp_goal;
			}
		}

		private void spinner_ItemSelected ( object sender, AdapterView.ItemSelectedEventArgs e )
		{
			Spinner spinner = ( Spinner ) sender;
			temp_sides = e.Position + 1;

			string toast = string.Format( "Number of sides: {0}", spinner.GetItemAtPosition( e.Position ) );
			Toast.MakeText( this, toast, ToastLength.Short ).Show();
			summary();
		}

		void rb_Click ( object sender, EventArgs e )
		{
			RadioButton rb = (RadioButton) sender;
			for ( int i = 1; i <= 3; i++ )
			{
				if ( i.ToString() == rb.Text.ToString() )
					temp_dies = i;
			}
			string toast = string.Format( "Number of Dies to roll: {0}", rb.Text );
			Toast.MakeText( this, toast, ToastLength.Short ).Show();
			summary();
		}

		void et_KeyPress ( object sender, View.KeyEventArgs e )
		{
			EditText et = (EditText) sender;
			if ( e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter )
			{
				temp_goal = int.Parse( et.Text.ToString() );
			}
			else
			{
				e.Handled = false;
			}
			summary();
		}

		private void summary ()
		{
			TextView tv = FindViewById<TextView>( Resource.Id.Summary );
			tv.Text = String.Format( "Number of dies: {0}\nNumber of Sides: {1}\nGoal: {2}", temp_dies, temp_sides, temp_goal );
		}

		protected override void OnSaveInstanceState ( Bundle outState )
		{
			base.OnSaveInstanceState( outState );
			outState.PutInt( "sides", temp_sides );
			outState.PutInt( "dies", temp_dies );
		}

		protected override void OnRestoreInstanceState ( Bundle savedInstanceState )
		{
			base.OnRestoreInstanceState( savedInstanceState );
			temp_dies = savedInstanceState.GetInt( "sides" );
			temp_sides = savedInstanceState.GetInt( "dies" );
		}
	}
}