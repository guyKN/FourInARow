using System;
using System.Collections.Generic;
using System.Text;

namespace FourInARow
{
    class FourInARowPos
    {

        static readonly int numNeededInRow=4;

        public enum Cell
        {
            _ = 0, // _ represents an empty cell
            X = 1,
            O = 2
        }
        public enum ActivePlayer //represents the player whoose turn it currently is
        {
            X=0,
            O=1
        }

        public enum WinState
        {
            noWinner = 0,
            Xwins = 1,
            Owins = 2,
            tie=3
        }

        private Cell[,] pos;
        public ActivePlayer activePlayer; // the player whoose turn it currently is
        

        public Cell[,] getPos()
        {
            return pos;
        }

        private void ResetPos() //Resets the Pos variable to be full of empty cells. 
        {
            pos = new Cell[7, 6];
            for(int i = 0; i > pos.GetLength(0); i++)
            {
                for (int j = 0; j > pos.GetLength(1); j++)
                {
                    pos[i, j] = Cell._;
                }
            }
        }

        public FourInARowPos()
        {
            pos = new Cell[7, 6];
            ResetPos();
            activePlayer = ActivePlayer.X; // X starts
        }

        public void placeChip(int row)
        {
            
            Cell activePlayerCell = getActivePlayerCellValue();  
            if(row>6 | row < 0) //todo: make dynamicly change
            {
                throw new System.ArgumentException("Row must be between 0 and 6. ", "original");
            }
            for (int j = 0; j < pos.GetLength(1); j++)
            {
                if (pos[row, j] == Cell._)
                {
                    pos[row, j] = activePlayerCell;
                    return;
                }
            }
            throw new System.ArgumentException("Can't place chip into full row. ", "original");
        }

        private Cell getActivePlayerCellValue()
        {
            if (activePlayer == ActivePlayer.O)
            {
                return Cell.O;
            }
            else if (activePlayer == ActivePlayer.X)
            {
                return Cell.X;
            }
            else
            {
                Console.WriteLine("something went wrong, activePlayer is neither X nor O");
                return Cell.X; //todo: handle this error better. 
            }
        }

        public void switchActivePlayer()
        {
            if (activePlayer == ActivePlayer.O)
            {
                activePlayer = ActivePlayer.X;
                return;
            }
            else if (activePlayer == ActivePlayer.X)
            {
                activePlayer = ActivePlayer.O;
            }

        }

        public void printPos()
        {
            for (int j = pos.GetLength(1)-1; j >=0; j--)
            {
                if(j != pos.GetLength(1)-1) //write something else in the first line
                {
                    Console.WriteLine("+---+---+---+---+---+---+---+");
                }
                else
                {
                    Console.WriteLine("+-1-+-2-+-3-+-4-+-5-+-6-+-7-+");
                }
                    Console.WriteLine(String.Format("| {0} | {1} | {2} | {3} | {4} | {5} | {6} |", cellToStr(pos[0,j]), cellToStr(pos[1, j]), cellToStr(pos[2, j]), cellToStr(pos[3, j]), cellToStr(pos[4, j]), cellToStr(pos[5, j]), cellToStr(pos[6, j])));
            }
            Console.WriteLine("+-1-+-2-+-3-+-4-+-5-+-6-+-7-+");
        }

        private string cellToStr(Cell cell) {
            if(cell == Cell.X)
            {
                return "X";
            }
            else if(cell == Cell.O)
            {
                return "O";
            }
            else
            {
                //in case of an empty cell
                return " ";
            }
        }

        private WinState checkWinStateOfCells(Cell cell1, Cell cell2, Cell cell3, Cell cell4)
        {
            if(cell1==Cell.O & cell2 == Cell.O & cell3 == Cell.O & cell4 == Cell.O)
            {
                return WinState.Owins;
            }else if (cell1 == Cell.X & cell2 == Cell.X & cell3 == Cell.X & cell4 == Cell.X)
            {
                return WinState.Xwins;
            }
            else
            {
                return WinState.noWinner;
            }
        }

        private WinState checkWinstateVertical()
        {
            for (int i = 0; i < pos.GetLength(0); i++)
            {
                for (int j = 0; j < pos.GetLength(1)-numNeededInRow+1; j++)
                {
                    WinState winstate = checkWinStateOfCells(pos[i,j], pos[i, j+1], pos[i, j+2], pos[i, j+3]);
                    if(winstate != WinState.noWinner)
                    {
                        return winstate;
                    }
                }
            }
            return WinState.noWinner;

        }

        private WinState checkWinstateHorizontal()
        {
            for (int i = 0; i < pos.GetLength(0) - numNeededInRow + 1; i++)
            {
                for (int j = 0; j < pos.GetLength(1); j++)
                {
                    WinState winstate = checkWinStateOfCells(pos[i, j], pos[i+1, j], pos[i+2, j], pos[i+3, j]);
                    if (winstate != WinState.noWinner)
                    {
                        return winstate;
                    }
                }
            }
            return WinState.noWinner;

        }
        private WinState checkTie() {
            for (int i = 0; i < pos.GetLength(0); i++)
            {
                for (int j = 0; j < pos.GetLength(1); j++)
                {
                    if(pos[i, j] == Cell._)
                    {
                        return WinState.noWinner;
                    }
                }
            }
            return WinState.tie;

        }

        public WinState checkWinstateDiagonalRightUp()
        {
            for (int i = 0; i < pos.GetLength(0) - numNeededInRow + 1; i++)
            {
                for (int j = 0; j < pos.GetLength(1) - numNeededInRow + 1; j++)
                {
                    WinState winstate = checkWinStateOfCells(pos[i, j], pos[i + 1, j+1], pos[i + 2, j+2], pos[i + 3, j+3]);
                    if (winstate != WinState.noWinner)
                    {
                        return winstate;
                    }
                }
            }
            return WinState.noWinner;

        }

        public WinState checkWinstateDiagonalRightDown()
        {
            for (int i = 0; i < pos.GetLength(0) - numNeededInRow + 1; i++)
            {
                for (int j = 0; j < pos.GetLength(1) - numNeededInRow + 1; j++)
                {
                    WinState winstate = checkWinStateOfCells(pos[i+3, j], pos[i + 2, j + 1], pos[i + 1, j + 2], pos[i, j + 3]);
                    if (winstate != WinState.noWinner)
                    {
                        return winstate;
                    }
                }
            }
            return WinState.noWinner;

        }

        public WinState checkWinstate()
        {
            WinState winState = checkWinstateHorizontal();
            if (winState != WinState.noWinner)
            {
                return winState;
            }
            winState = checkWinstateVertical();
            if (winState != WinState.noWinner)
            {
                return winState;
            }
            winState = checkWinstateDiagonalRightUp();
            if (winState != WinState.noWinner)
            {
                return winState;
            }

            winState = checkWinstateDiagonalRightDown();
            if (winState != WinState.noWinner)
            {
                return winState;
            }

            winState = checkTie();
            if (winState != WinState.noWinner)
            {
                return winState;
            }
            return WinState.noWinner;



        }

    }

}




