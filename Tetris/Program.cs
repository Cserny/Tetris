using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    class Program
    {
        public static PlayingArea Area;
        public static Surface Surface;
        public static SaveSystem SaveSystem;

        public static string CurrentName;
        public static int CurrentScore = 0;

        public static void Main(string[] args)
        {
            CurrentName = new Dialog().Prompt("What's your name?", "Enter below:");

            Area = new PlayingArea();
            Surface = new Surface();
            SaveSystem = new SaveSystem();

            Surface.OnUserReaction += surface_OnUserReaction;
            Surface.OnGravity += surface_OnGravity;
            Area.OnCurrentTileReachedBottom += area_OnCurrentTileReachedBottom;
            Area.OnUserScored += area_OnUserScored;
            Area.OnGameEnds += area_OnGameEnds;

            SaveSystem.ReadHighscore();

            Surface.UpdateHighscoreLabel();

            Application.Run(Surface);
        }

        static void NewGame()
        {
            CurrentScore = 0;
            Area.Restore();
            Surface.GravityTimer.Interval = 500;
            Surface.GravityTimer.Enabled = true;
            Surface.UpdateScoreLabel();
            Surface.UpdateSpeedLabel();
        }
        static void area_OnCurrentTileReachedBottom(object sender, EventArgs e)
        {
            Area.ClearFilledLines();
            Area.SetNewTile();
            Surface.Refresh();
        }
        static void area_OnUserScored(object sender, EventArgs e)
        {
            CurrentScore++;
            if (Surface.GravityTimer.Interval > 100)
            {
                Surface.GravityTimer.Interval -= 10;
                Surface.UpdateSpeedLabel();
            }
            SaveSystem.UpdateHighscore();
            Surface.UpdateHighscoreLabel();
            Surface.UpdateScoreLabel();
            Surface.Refresh();
        }
        static void area_OnGameEnds(object sender, EventArgs e)
        {
            //Dialog needed
            NewGame();
        }
        static void surface_OnGravity(object sender, SurfaceHandlerArgs e)
        {
            Area.MoveCurrentTileDown();
            Surface.Refresh();
        }
        static void surface_OnUserReaction(object sender, SurfaceHandlerArgs e)
        {
            switch (e.Reaction)
            {
                case "Rotate":
                    if (Surface.GravityTimer.Enabled)
                    {
                        Area.RotateCurrentTile();
                    }
                    break;
                case "Down":
                    if (Surface.GravityTimer.Enabled)
                    {
                        Area.MoveCurrentTileDown();
                    }
                    break;
                case "Left":
                    if (Surface.GravityTimer.Enabled)
                    {
                        Area.MoveCurrentTileLeft();
                    }
                    break;
                case "Right":
                    if (Surface.GravityTimer.Enabled)
                    {
                        Area.MoveCurrentTileRight();
                    }
                    break;
                case "Pause":
                    Surface.GravityTimer.Enabled = !Surface.GravityTimer.Enabled;
                    break;
            }
            Surface.Refresh();
        }
    }
}
