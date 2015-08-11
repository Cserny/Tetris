using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class PlayingArea
    {
        public int[,] Field;
        public List<Tile> WaitingLine;
        private Tile CurrentTile;

        public delegate void AreaHandler(object sender, EventArgs e);
        public event AreaHandler OnCurrentTileReachedBottom, OnUserScored, OnGameEnds;

        public PlayingArea()
        {
            Field = new int[18, 32];
            WaitingLine = new List<Tile>();
            Restore();
        }
                
        private void SetTile(Tile tile)
        {
            for(int i = 0; i < 4; i++)
            {
                Field[tile.X[i], tile.Y[i]] = tile.Sort;
            }
        }
        private void DeleteTile(Tile tile)
        {
            for (int i = 0; i < 4; i++)
            {
                Field[tile.X[i], tile.Y[i]] = 0;
            }
        }
        private void MoveTile(Tile tile, int x, int y)
        {
            if (TileIsAbleToMove(tile, x, y))
            {
                DeleteTile(tile);
                tile.Move(x, y);
                SetTile(tile);
            }
            else if (y > 0) //Means tile tried to move down but couldn't
            {
                SetTilePermanently(tile);
                if (OnCurrentTileReachedBottom != null)
                {
                    OnCurrentTileReachedBottom(this, new EventArgs());
                }
            }
        }
        private void RotateTile(Tile tile)
        {
            if (TileIsAbleToRotate(tile))
            {
                DeleteTile(tile);
                tile.Rotate();
                SetTile(tile);
            }
        }
        private void SetTilePermanently(Tile tile)
        {
            DeleteTile(tile);
            tile.Sort += 8;
            SetTile(tile);
        }
        private void MoveRowsDown(int startRow)
        {
            for (int y = startRow; y > 1; y--)
            {
                for (int x = 1; x < Field.GetLength(0) - 1; x++)
                {
                    Field[x, y] = Field[x, y - 1];
                }
            }
        }

        private Tile CopyTile(Tile tile)
        {
            Tile copy = new Tile(0);
            Array.Copy(tile.X, copy.X, 4); //Needed because arrays are reference types
            Array.Copy(tile.Y, copy.Y, 4);
            copy.Sort = tile.Sort;
            copy.Status = tile.Status;
            return copy;
        }
        public Tile DestinyOfCurrentTile()
        {
            Tile copy = CopyTile(CurrentTile);
            while (TileIsAbleToMove(copy, 0, 1))
            {
                copy.Move(0, 1);
            }
            return copy;
        }

        private bool TileIsAbleToSet(Tile tile)
        {
            return (Field[tile.X[0], tile.Y[0]] == tile.Sort || Field[tile.X[0], tile.Y[0]] == 0) &&
                   (Field[tile.X[1], tile.Y[1]] == tile.Sort || Field[tile.X[1], tile.Y[1]] == 0) &&
                   (Field[tile.X[2], tile.Y[2]] == tile.Sort || Field[tile.X[2], tile.Y[2]] == 0) &&
                   (Field[tile.X[3], tile.Y[3]] == tile.Sort || Field[tile.X[3], tile.Y[3]] == 0);
        }
        private bool TileIsAbleToMove(Tile tile, int x, int y)
        {
            Tile copy = CopyTile(tile);
            copy.Move(x, y);
            return TileIsAbleToSet(copy);
        }
        private bool TileIsAbleToRotate(Tile tile)
        {
            Tile copy = CopyTile(tile);
            copy.Rotate();
            return TileIsAbleToSet(copy);
        }

        public void SetNewTile()
        {
            CurrentTile = WaitingLine.First();
            WaitingLine.Remove(CurrentTile);
            WaitingLine.Add(new Tile(new Random().Next(1, 8)));
            if (TileIsAbleToSet(CurrentTile))
            {
                SetTile(CurrentTile);
            }
            else
            {
                if (OnGameEnds != null)
                {
                    OnGameEnds(this, new EventArgs());
                }
            }
        }
        public void RotateCurrentTile()
        {
            RotateTile(CurrentTile);
        }
        public void MoveCurrentTileDown()
        {
            MoveTile(CurrentTile, 0, 1);
        }
        public void MoveCurrentTileLeft()
        {
            MoveTile(CurrentTile, -1, 0);
        }
        public void MoveCurrentTileRight()
        {
            MoveTile(CurrentTile, 1, 0);
        }
        
        public void ClearFilledLines()
        {
            bool whiteSpaceFound;
            for (int y = Field.GetLength(1) - 2; y > 1; y--)
            {
                whiteSpaceFound = false;
                for (int x = 1; x < Field.GetLength(0) - 1; x++)
                {
                    if (Field[x, y] == 0)
                    {
                        whiteSpaceFound = true;
                        break;
                    }
                }
                if (whiteSpaceFound)
                {
                    continue;
                }
                MoveRowsDown(y);
                y++;
                if (OnUserScored != null)
                {
                    OnUserScored(this, new EventArgs());
                }
            }
        }
        public void Restore()
        {
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    Field[i, j] = 0; //Clear whole field
                }
            }
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                Field[i, 0] = -1; //First row
                Field[i, Field.GetLength(1) - 1] = -1; //Last row
            }
            for (int i = 0; i < Field.GetLength(1); i++)
            {
                Field[0, i] = -1; //First column
                Field[Field.GetLength(0) - 1, i] = -1; //Last column
            }
            WaitingLine.Clear();
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                WaitingLine.Add(new Tile(rnd.Next(1, 8)));
            }
            SetNewTile();
        }
    }
}