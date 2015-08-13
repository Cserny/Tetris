using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class Tile
    {
        public int[]  X, Y;
        public int Sort;
        public bool Status; //For rotating purposes

        public Tile(int sort)
        {
            switch (sort)
            {
                case 1: //I-tile
                    X = new int[] {7, 8, 9, 10};
                    Y = new int[] {2, 2, 2, 2};
                    break;
                case 2: //J-tile
                    X = new int[] {7, 7, 8, 9};
                    Y = new int[] {1, 2, 2, 2};
                    break;
                case 3: //L-tile
                    X = new int[] {10, 8, 9, 10};
                    Y = new int[] {1, 2, 2, 2};
                    break;
                case 4: //O-tile
                    X = new int[] {8, 9, 8, 9};
                    Y = new int[] {1, 1, 2, 2};
                    break;
                case 5: //S-tile
                    X = new int[] {8, 9, 7, 8};
                    Y = new int[] {1, 1, 2, 2};
                    break;
                case 6: //T-tile
                    X = new int[] {8, 7, 8, 9};
                    Y = new int[] {1, 2, 2, 2};
                    break;
                case 7: //Z-tile
                    X = new int[] {8, 9, 9, 10};
                    Y = new int[] {1, 1, 2, 2};
                    break;
                default: //For cloning purposes
                    X = new int[4];
                    Y = new int[4];
                    break;
            }
            Sort = sort;
            Status = true;
        }
        public void Move(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                X[i] += x;
                Y[i] += y;
            }
        }
        public void Rotate()
        {
            int sign = (Status ? 1 : -1);
            switch (Sort)
            {
                case 1: //I-tile
                    X[0] += 2 * sign;
                    X[1] += 1 * sign;
                    X[3] -= 1 * sign;
                    Y[0] -= 1 * sign;
                    Y[2] += 1 * sign;
                    Y[3] += 2 * sign;
                    break;  
                case 2: //J-tile
                    X[0] += 1 * sign;
                    X[2] -= 1 * sign;
                    X[3] -= 2 * sign;
                    Y[1] -= 1 * sign;
                    Y[3] += 1 * sign;
                    break;
                case 3: //L-tile
                    X[1] += 1 * sign;
                    X[3] -= 1 * sign;
                    Y[0] += 1 * sign;
                    Y[1] -= 2 * sign;
                    Y[2] -= 1 * sign;
                    break;
                case 4: //O-tile - doesn't need to be rotated
                    break;
                case 5: //S-tile
                    X[0] += 1 * sign;
                    X[2] += 1 * sign;
                    Y[1] += 1 * sign;
                    Y[2] -= 2 * sign;
                    Y[3] -= 1 * sign;
                    break;
                case 6: //T-tile
                    X[0] += 1 * sign;
                    X[3] -= 1 * sign;
                    X[1] += 1 * sign;
                    Y[1] -= 2 * sign;
                    Y[2] -= 1 * sign;
                    break;
                case 7: //Z-tile
                    X[0] += 2 * sign;
                    X[1] += 1 * sign;
                    X[3] -= 1 * sign;
                    Y[0] -= 1 * sign;
                    Y[2] -= 1 * sign;
                    break;
            }
            Status = !Status;
        }
    }
}
