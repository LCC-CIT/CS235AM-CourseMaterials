using System;
using System.Threading;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PigGame;

namespace PigAndroid
{
	[Activity (Label = "@string/app_name", MainLauncher = true)]
	public class MainActivity : TabActivity
	{
		public Pig_Game Game;
		public ScoreSheet GameScores;

		public bool ScoreSaved;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			CreateGameTab();

			MainActivityRetainedState previousState = LastNonConfigurationInstance as MainActivityRetainedState;
			if (previousState != null)
			{
				GameScores = previousState.TheScoreSheet;
				Game = previousState.Game;
				ScoreSaved= previousState.ScoreSaved;

				if (Game != null)
				{
					CreatePlayerTab();

					if (!Game.GameOver)
						TabHost.CurrentTab = 1;
				}

			}
		}

		public override Java.Lang.Object OnRetainNonConfigurationInstance ()
		{
			MainActivityRetainedState savedState = new MainActivityRetainedState(GameScores, Game, ScoreSaved);
			return savedState;
		}

		private void CreateGameTab()
		{
			TabHost.ClearAllTabs();
		
			Intent intent;
			TabHost.TabSpec spec1;

			//this sets the MainGame tab
			intent = new Intent(this, typeof (GameTab));
			intent.AddFlags(ActivityFlags.NewTask);
			spec1 = TabHost.NewTabSpec("TheMenu");
			spec1.SetIndicator("Game of Pig");
			spec1.SetContent(intent);
			TabHost.AddTab(spec1);
		}

		private void CreatePlayerTab()
		{
			Intent intent;
			TabHost.TabSpec spec2;
			
			//this sets the Player1 Tab
			intent = new Intent(this, typeof (PlayerTab));
			intent.AddFlags(ActivityFlags.NewTask);
			spec2 = TabHost.NewTabSpec("TheGame" + DateTime.Now.Millisecond.ToString());
			spec2.SetIndicator(Game.Player1Name + " vs " + Game.Player2Name);
			spec2.SetContent(intent);
			TabHost.AddTab(spec2);
		}

		public void CreateGame(string p1, bool p1comp, string p2, bool p2comp)
		{
			CreateScoreSheet(p1, p2);
			if (Game == null || Game.GameOver)
			{
				Game = null;
				Game = new Pig_Game(p1, p1comp, p2, p2comp);
				ScoreSaved = false;
				CreateGameTab();
				CreatePlayerTab();
				Game.Player1Roll();
				TabHost.CurrentTab = 1;
			}
		}

		public void UpdateGame()
		{
			if (Game.GameOver && !ScoreSaved)
			{
				ScoreSaved = true;
				GameScores.SetPlayer1Stats(Game.Player1Total, (Game.Player1Total >= Game.WinningTotal));
				GameScores.SetPlayer2Stats(Game.Player2Total, (Game.Player2Total >= Game.WinningTotal));
			}
		}

		private void CreateScoreSheet(string p1, string p2)
		{
			if (GameScores == null)
			{
				GameScores = new ScoreSheet(p1, p2);
			}
			else
			{
				if (!GameScores.Player1.Equals(p1) || !GameScores.Player2.Equals(p2))
					GameScores = new ScoreSheet(p1, p2);
			}
		}
	}
}


