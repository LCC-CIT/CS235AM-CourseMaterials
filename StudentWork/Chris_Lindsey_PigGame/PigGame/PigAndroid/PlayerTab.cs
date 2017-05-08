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
using PigGame;

namespace PigAndroid
{
	[Activity (Label = "PlayerOneTab")]			
	public class PlayerTab : Activity
	{
		MainActivity parent;

		Button P1Hold, P1Roll, P2Hold, P2Roll;
		TextView P1InfoText, P2InfoText, GameInfoText;
		ListView listview;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			parent = (MainActivity)this.Parent;

			SetContentView (Resource.Layout.PlayerTab);

			GameInfoText = FindViewById<TextView>(Resource.Id.textviewMainInfo);
			GameInfoText.Text = parent.Game.GameInfo;

			P1Hold = FindViewById<Button>(Resource.Id.P1HoldButton);
			P1Hold.Click += (sender, e) => {parent.Game.Player1Hold(); UpdateView();};

			P1Roll = FindViewById<Button>(Resource.Id.P1RollButton);
			P1Roll.Click += (sender, e) => {parent.Game.Player1Roll(); UpdateView();};

			P1InfoText = FindViewById<TextView>(Resource.Id.P1InfoText);
			P1InfoText.Text = parent.Game.Player1Info;

			P2Hold = FindViewById<Button>(Resource.Id.P2HoldButton);
			P2Hold.Click += (sender, e) => {parent.Game.Player2Hold(); UpdateView();};

			P2Roll = FindViewById<Button>(Resource.Id.P2RollButton);
			P2Roll.Click += (sender, e) => {parent.Game.Player2Roll(); UpdateView();};
			
			P2InfoText = FindViewById<TextView>(Resource.Id.P2InfoText);
			P2InfoText.Text = parent.Game.Player2Info;

			listview = FindViewById<ListView>(Resource.Id.listView1);

			listview.Adapter = new PlayerTabAdapter(this, parent.Game.TheRolls.ToArray());

			UpdateView();
		}

		private void UpdateView()
		{
			parent.UpdateGame();
			listview.Adapter = new PlayerTabAdapter(this, parent.Game.TheRolls.ToArray());
			if (parent.Game.TheRolls != null && parent.Game.TheRolls.Count > 0)
				listview.SetSelection(parent.Game.TheRolls.Count - 1);

			GameInfoText.Text = parent.Game.GameInfo;
			P1InfoText.Text = parent.Game.Player1Info;
			P2InfoText.Text = parent.Game.Player2Info;

			if (parent.Game.GameOver)
			{
				P1Hold.Enabled = P2Hold.Enabled = P1Roll.Enabled = P2Roll.Enabled = false;
			}
			else
			{
				P1Hold.Enabled = parent.Game.Player1Turn;
				P1Roll.Enabled = parent.Game.Player1CanContinue;
				P2Hold.Enabled = parent.Game.Player2Turn;
				P2Roll.Enabled = parent.Game.Player2CanContinue;
			}
		}
	}
}