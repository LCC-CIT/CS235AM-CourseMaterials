using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PigGame;

namespace PigAndroid
{
	class MainActivityRetainedState : Java.Lang.Object
	{
		public ScoreSheet TheScoreSheet {get; private set;}
		public Pig_Game Game {get; private set;}
		public bool ScoreSaved {get; private set;}

		public MainActivityRetainedState(ScoreSheet sheet, Pig_Game game, bool scoreSaved)
		{
			TheScoreSheet = sheet;
			Game = game;
			ScoreSaved = scoreSaved;
		}
	}
	/*
	class PlayerTabRetainedState : Java.Lang.Object
	{
		public Pig_Game TheGame {get; private set;}
		public PlayerTabRetainedState(Pig_Game game)
		{
			TheGame = game;
		}
	}
	*/
}

