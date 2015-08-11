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
        public int Status; //For rotating purposes

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
            Status = 0;
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
            switch (Sort)
            {
                case 1: //I-tile
                    switch (Status % 4)
                    {
                        case 0:
                            X[0] += 2;
                            X[1] += 1;
                            X[3] -= 1;
                            Y[0] -= 1;
                            Y[2] += 1;
                            Y[3] += 2;
                            break;
                        case 1:
                            X[0] += 1;
                            X[2] -= 1;
                            X[3] -= 2;
                            Y[0] += 2;
                            Y[1] += 1;
                            Y[3] -= 1;
                            break;
                        case 2:
                            X[0] -= 2;
                            X[1] -= 1;
                            X[3] += 1;
                            Y[0] += 1;
                            Y[2] -= 1;
                            Y[3] -= 2;
                            break;
                        case 3:
                            X[0] -= 1;
                            X[2] += 1;
                            X[3] += 2;
                            Y[0] -= 2;
                            Y[1] -= 1;
                            Y[3] += 1;
                            break;
                    }
                    break;
                case 2: //J-tile
                    switch (Status % 4)
                    {
                        case 0:
                            X[0] += 1;
                            X[2] -= 1;
                            X[3] -= 2;
                            Y[1] -= 1;
                            Y[3] += 1;
                            break;
                        case 1:
                            X[1] += 1;
                            X[3] -= 1;
                            Y[0] += 1;
                            Y[2] -= 1;
                            Y[3] -= 2;
                            break;
                        case 2:
                            X[0] -= 1;
                            X[2] += 1;
                            X[3] += 2;
                            Y[1] += 1;
                            Y[3] -= 1;
                            break;
                        case 3:
                            X[1] -= 1;
                            X[3] += 1;
                            Y[0] -= 1;
                            Y[2] += 1;
                            Y[3] += 2;
                            break;
                    }
                    break;
                case 3: //L-tile
                    switch (Status % 4)
                    {
                        case 0:
                            X[1] += 1;
                            X[3] -= 1;
                            Y[0] += 1;
                            Y[1] -= 2;
                            Y[2] -= 1;
                            break;
                        case 1:
                            X[0] -= 1;
                            X[1] += 2;
                            X[2] += 1;
                            Y[1] += 1;
                            Y[3] -= 1;
                            break;
                        case 2:
                            X[1] -= 1;
                            X[3] += 1;
                            Y[0] -= 1;
                            Y[1] += 2;
                            Y[2] += 1;
                            break;
                        case 3:
                            X[0] += 1;
                            X[1] -= 2;
                            X[2] -= 1;
                            Y[1] -= 1;
                            Y[3] += 1;
                            break;
                    }
                    break;
                case 4: //O-tile - doesn't need to be rotated
                    break;
                case 5: //S-tile
                    switch (Status % 4)
                    {
                        case 0:
                            X[0] += 1;
                            X[2] += 1;
                            Y[1] += 1;
                            Y[2] -= 2;
                            Y[3] -= 1;
                            break;
                        case 1:
                            X[1] -= 1;
                            X[2] += 2;
                            X[3] += 1;
                            Y[0] += 1;
                            Y[2] += 1;
                            break;
                        case 2:
                            X[0] -= 1;
                            X[2] -= 1;
                            Y[1] -= 1;
                            Y[2] += 2;
                            Y[3] += 1;
                            break;
                        case 3:
                            X[1] += 1;
                            X[2] -= 2;
                            X[3] -= 1;
                            Y[0] -= 1;
                            Y[2] -= 1;
                            break;
                    }
                    break;
                case 6: //T-tile
                    switch (Status % 4)
                    {
                        case 0:
                            X[0] += 1;
                            X[3] -= 1;
                            X[1] += 1;
                            Y[1] -= 2;
                            Y[2] -= 1;
                            break;
                        case 1:
                            X[1] += 2;
                            X[2] += 1;
                            Y[0] += 1;
                            Y[1] += 1;
                            Y[3] -= 1;
                            break;
                        case 2:
                            X[0] -= 1;
                            X[3] += 1;
                            X[1] -= 1;
                            Y[1] += 2;
                            Y[2] += 1;
                            break;
                        case 3:
                            X[1] -= 2;
                            X[2] -= 1;
                            Y[0] -= 1;
                            Y[1] -= 1;
                            Y[3] += 1;
                            break;
                    }
                    break;
                case 7: //Z-tile
                    switch (Status % 4)
                    {
                        case 0:
                            X[0] += 2;
                            X[1] += 1;
                            X[3] -= 1;
                            Y[0] -= 1;
                            Y[2] -= 1;
                            break;
                        case 1:
                            X[0] += 1;
                            X[2] += 1;
                            Y[0] += 2;
                            Y[1] += 1;
                            Y[3] -= 1;
                            break;
                        case 2:
                            X[0] -= 2;
                            X[1] -= 1;
                            X[3] += 1;
                            Y[0] += 1;
                            Y[2] += 1;
                            break;
                        case 3:
                            X[0] -= 1;
                            X[2] -= 1;
                            Y[0] -= 2;
                            Y[1] -= 1;
                            Y[3] += 1;
                            break;
                    }
                    break;
            }
            Status++;
        }
    }
}
