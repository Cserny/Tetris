using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris
{
    class Surface : Form
    {
        public PlayingArea area;

        public Label ScoreLabel, SpeedLabel, InstructionsLabel, HighscoreLabel;
        public Timer GravityTimer = new Timer();

        private int squareLength = 18;
        private int yMargin = 50;
        private int xMargin = 200;

        public delegate void SurfaceHandler(object sender, SurfaceHandlerArgs e);
        public event SurfaceHandler OnUserReaction, OnGravity;

        public Surface()
        {
            area = Program.Area;
            Text = "Tetris";
            BackColor = Color.White;

            int ClientWidth = xMargin * 2 + squareLength * area.Field.GetLength(0) + xMargin / 2 + squareLength * 4;
            int ClientHeight = yMargin * 2 + squareLength * area.Field.GetLength(1);
            
            ClientSize = new Size(ClientWidth, ClientHeight);
            
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            Font = new Font("Franklin Gothic", 15);
            
            DoubleBuffered = true;

            GravityTimer.Interval = 500;
            GravityTimer.Enabled = true;
            GravityTimer.Tick += GravityTimer_Tick;
            
            InitLabels();
        }
        private void InitLabels()
        {
            ScoreLabel = new Label();
            SpeedLabel = new Label();
            InstructionsLabel = new Label();
            HighscoreLabel = new Label();
            
            ScoreLabel.SetBounds(0, 0, ClientSize.Width, yMargin);
            SpeedLabel.SetBounds(0, ClientSize.Height - yMargin, ClientSize.Width, yMargin);
            InstructionsLabel.SetBounds(0, yMargin, xMargin, ClientSize.Height - 2 * yMargin);
            HighscoreLabel.SetBounds(ClientSize.Width - xMargin, yMargin, xMargin, ClientSize.Height - 2 * yMargin);

            StringBuilder sb = new StringBuilder();
            sb.Append("Controls: \n");
            sb.Append("Esc - Pause \n");
            sb.Append("Up - Rotate \n");
            sb.Append("Down - Down \n");
            sb.Append("Left - Left \n");
            sb.Append("Right - Right \n");

            ScoreLabel.Text = "Score: 0";
            SpeedLabel.Text = "Speed: " + Math.Round(1000.0 / GravityTimer.Interval, 2) + " blocks / sec";
            InstructionsLabel.Text = sb.ToString();
            HighscoreLabel.Text = "HighscoreLabel";

            ScoreLabel.TextAlign = ContentAlignment.MiddleCenter;
            SpeedLabel.TextAlign = ContentAlignment.MiddleCenter;
            InstructionsLabel.TextAlign = ContentAlignment.TopCenter;
            HighscoreLabel.TextAlign = ContentAlignment.TopCenter;

            Label[] labels = { ScoreLabel, SpeedLabel, InstructionsLabel, HighscoreLabel };

            Controls.AddRange(labels);
        }
        private void GravityTimer_Tick(object sender, EventArgs e)
        {
            if (OnGravity != null)
            {
                OnGravity(this, new SurfaceHandlerArgs());
            }
        }
        private Color ConvertToColor(int sort)
        {
            if (sort > 7)
            {
                sort -= 8;
            }
            switch (sort)
            {
                case -1:
                    return Color.Black;
                case 0:
                    return Color.White;
                case 1:
                    return Color.Aqua;
                case 2:
                    return Color.Blue;
                case 3:
                    return Color.Coral;
                case 4:
                    return Color.Yellow;
                case 5:
                    return Color.LimeGreen;
                case 6:
                    return Color.Purple;
                case 7:
                    return Color.Red;
                default:
                    return Color.Black;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen pen = new Pen(Color.SlateGray);
            SolidBrush brush = new SolidBrush(Color.Black);
            for (int y = 0; y <= area.Field.GetLength(1); y++)
            {
                int distanceToTop = y * squareLength + yMargin;
                Point left = new Point(xMargin, distanceToTop);
                Point right = new Point(xMargin + area.Field.GetLength(0) * squareLength, distanceToTop);
                e.Graphics.DrawLine(pen, left, right);
            }
            for (int x = 0; x <= area.Field.GetLength(0); x++)
            {
                int distanceToLeft = x * squareLength + xMargin;
                Point top = new Point(distanceToLeft, yMargin);
                Point bottom = new Point(distanceToLeft, yMargin + squareLength * area.Field.GetLength(1));
                e.Graphics.DrawLine(pen, top, bottom);
            }
            for (int x = 0; x < area.Field.GetLength(0); x++)
            {
                for (int y = 0; y < area.Field.GetLength(1); y++)
                {
                    brush.Color = ConvertToColor(area.Field[x, y]);
                    Point upperLeftCorner = new Point(xMargin + x * squareLength + 1, yMargin + y * squareLength + 1);
                    Size size = new Size(squareLength - 1, squareLength - 1);
                    Rectangle square = new Rectangle(upperLeftCorner, size);
                    e.Graphics.FillRectangle(brush, square);
                }
            }
            Tile destiny = area.DestinyOfCurrentTile();
            brush.Color = Color.FromArgb(127, ConvertToColor(destiny.Sort));
            for (int i = 0; i < 4; i++)
            {
                if (area.Field[destiny.X[i], destiny.Y[i]] != destiny.Sort) //We don't want the destiny to overlap the current tile
                {
                    Point upperLeftCorner = new Point(xMargin + destiny.X[i] * squareLength + 1, yMargin + destiny.Y[i] * squareLength + 1);
                    Size size = new Size(squareLength - 1, squareLength - 1);
                    Rectangle square = new Rectangle(upperLeftCorner, size);
                    e.Graphics.FillRectangle(brush, square);
                }
            }
            for (int y = 1; y < area.Field.GetLength(1); y++)
            {
                int distanceToTop = y * squareLength + yMargin;
                int distanceToLeft = xMargin + area.Field.GetLength(0) * squareLength;
                Point left = new Point(distanceToLeft + xMargin / 4, distanceToTop);
                Point right = new Point(distanceToLeft + xMargin / 4 + 4 * squareLength, distanceToTop);
                e.Graphics.DrawLine(pen, left, right);
            }
            for (int x = 0; x < 5; x++)
            {
                int distanceToLeft = xMargin + area.Field.GetLength(0) * squareLength + x * squareLength + xMargin / 4;
                Point top = new Point(distanceToLeft, yMargin + squareLength);
                Point bottom = new Point(distanceToLeft, yMargin + squareLength * area.Field.GetLength(1) - squareLength);
                e.Graphics.DrawLine(pen, top, bottom);
            }
            for (int i = 0; i < area.WaitingLine.Count; i++)
            {
                Tile tile = area.WaitingLine[i];
                brush.Color = ConvertToColor(tile.Sort);
                for (int j = 0; j < 4; j++)
                {   
                    Point upperLeftCorner = new Point(xMargin + area.Field.GetLength(0) * squareLength + xMargin / 4 + (tile.X[j] - 7) * squareLength + 1, yMargin + (tile.Y[j] + i * 4) * squareLength + 1);
                    Size size = new Size(squareLength - 1, squareLength - 1);
                    Rectangle square = new Rectangle(upperLeftCorner, size);
                    e.Graphics.FillRectangle(brush, square);
                }    
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (OnUserReaction != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        OnUserReaction(this, new SurfaceHandlerArgs("Rotate"));
                        break;
                    case Keys.Down:
                        OnUserReaction(this, new SurfaceHandlerArgs("Down"));
                        break;
                    case Keys.Left:
                        OnUserReaction(this, new SurfaceHandlerArgs("Left"));
                        break;
                    case Keys.Right:
                        OnUserReaction(this, new SurfaceHandlerArgs("Right"));
                        break;
                    case Keys.Escape:
                        OnUserReaction(this, new SurfaceHandlerArgs("Pause"));
                        break;
                }
            }
        }
        public void UpdateScoreLabel()
        {
            ScoreLabel.Text = "Score: " + Program.CurrentScore;
        }
        public void UpdateSpeedLabel()
        {
            SpeedLabel.Text = "Speed: " + Math.Round(1000.0 / GravityTimer.Interval, 2) + " blocks / sec";
        }
        public void UpdateHighscoreLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Highscores:\n");
            int i = 1;
            foreach (ScoreEntry score in SaveSystem.Scores)
            {
                sb.Append(i + ". " + score.Name + ": " + score.Score + "\n");
                i++;
            }
            HighscoreLabel.Text = sb.ToString();
        }
    }
    public class SurfaceHandlerArgs : EventArgs
    {
        public string Reaction;
        public SurfaceHandlerArgs() {}
        public SurfaceHandlerArgs(string reaction)
        {
            Reaction = reaction;
        }
    }
}
