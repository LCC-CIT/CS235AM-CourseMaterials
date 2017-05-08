
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

namespace PigAndroid
{
	[Activity (Label = "GameTab")]			
	public class GameTab : Activity
	{
		MainActivity parent;

		Button StartGameButton;
		EditText p1textbox, p2textbox;
		CheckBox p1checkbox, p2checkbox;
		TextView P1ScoreText, P2ScoreText;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.GameTab);

			StartGameButton = FindViewById<Button>(Resource.Id.StartGame);
			p1textbox = FindViewById<EditText>(Resource.Id.Player1Text);
			p1checkbox = FindViewById<CheckBox>(Resource.Id.Player1ComputerBox);
			p2textbox = FindViewById<EditText>(Resource.Id.Player2Text);
			p2checkbox = FindViewById<CheckBox>(Resource.Id.Player2ComputerBox);
			P1ScoreText = FindViewById<TextView>(Resource.Id.ScoreSheetPlayer1);
			P2ScoreText = FindViewById<TextView>(Resource.Id.ScoreSheetPlayer2);

			parent = (MainActivity)this.Parent;

			UpdateText();

			parent.TabHost.TabChanged += (sender, e) =>
			{
				UpdateText();
			};

			StartGameButton.Click += (sender, e) => 
			{
				parent.CreateGame(p1textbox.Text, p1checkbox.Checked, p2textbox.Text, p2checkbox.Checked);
				UpdateText();
			};
		}

		private void UpdateText()
		{
			if (parent.GameScores != null)
			{
				P1ScoreText.Text = parent.GameScores.Player1Info;
				P2ScoreText.Text = parent.GameScores.Player2Info;
			}
		}
	}
}

