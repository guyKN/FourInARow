using System;
using System.Collections.Generic;
using System.Text;

namespace FourInARow
{
    class FourInARowPos
    {

        static readonly int numNeededInRow=4;

        public enum Cell //each cell on the board can either be empty, represented by _, or filled with an X or O, represented by that letter. 
        {
            _ = 0, 
            X = 1,
            O = 2
        }
        public enum ActivePlayer //represents the player whoose turn it currently is
        {
            X=0,
            O=1
        }

        public enum WinState //used to say who is winning.
        {
            noWinner = 0,
            Xwins = 1,
            Owins = 2,
            tie=3
        }

        private Cell[,] pos; // pos is a 7 by 6 array of Cell objects. each cell object can either be X, O or _(which means it is empty.)
        public ActivePlayer activePlayer; // the player whoose turn it currently is
        

        public Cell[,] getPos()
        {
            return pos;
        }

        private void ResetPos() //Resets the Pos variable to be full of empty cells. Called at the start of the game. 
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

        public FourInARowPos() // Constructor for the FourInARowPos class. Creates a pos variable filled with just blank spaces, and sets the current player to X. 
        {
            pos = new Cell[7, 6];
            ResetPos();
            activePlayer = ActivePlayer.X; // X starts
        }

        public void placeChip(int row) //puts a X or O (depending on who the active player is) onto the lowest spot into the declared row. 
        {
            
            Cell activePlayerCell = getActivePlayerCellValue();  // is equal to Cell.X or Cell.O, depending on whoose turn it is. 
            if(row>6 | row < 0) 
            {
                throw new System.ArgumentException("Row must be between 0 and 6. ", "original");
            }
            for (int j = 0; j < pos.GetLength(1); j++) //starts on the lowest cell, if it's empty, adds the player's chip to it and ends the function. Otherwise, goes up by 1 cell. 
            {
                if (pos[row, j] == Cell._)//checks if cell is empty. 
                {
                    pos[row, j] = activePlayerCell;
                    return;
                }
            }
            throw new System.ArgumentException("Can't place chip into full row. ", "original"); // if no cell is empty in that row, then throw an exception, because you can't add a chip to a full row
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

        public void switchActivePlayer() // if the active player is X, sets it to O. If active player is O, sets it to X. 
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

        public void printPos() //prints the current position in a way that's easy to read for the player. 
        {
            for (int j = pos.GetLength(1)-1; j >=0; j--)
            {
                if(j != pos.GetLength(1)-1) //write something diffrent in the first line
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

        private string cellToStr(Cell cell) { //given a cell returns a string so that it can be read by the player. 
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

        private WinState checkWinStateOfCells(Cell cell1, Cell cell2, Cell cell3, Cell cell4){ //given 4 cells, if all of them are O, then O wins. If all of them are X, then X wins. 
            //called by checkWinstateVertical,  checkWinstateHorizontal, checkWinstateDiagonalRightUp,checkWinstateDiagonalRightDown
            if (cell1==Cell.O & cell2 == Cell.O & cell3 == Cell.O & cell4 == Cell.O)
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

        private WinState checkWinstateVertical() //checks every combination of 4 cells vertically in a row, and calls checkWinStateOfCells on them. 
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
            return WinState.noWinner; // if no winner has been found in the entire loop, then there is no winner. 

        }

        private WinState checkWinstateHorizontal()//checks every combination of 4 cells horizontally in a row, and calls checkWinStateOfCells on them.
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
        private WinState checkTie() { // if every cell is filled, then it is a tie. 
            for (int i = 0; i < pos.GetLength(0); i++)
            {
                for (int j = 0; j < pos.GetLength(1); j++)
                {
                    if(pos[i, j] == Cell._)
                    {
                        return WinState.noWinner; // there is a cell that is not filled, so it is not a tie. 
                    }
                }
            }
            return WinState.tie;

        }

        public WinState checkWinstateDiagonalRightUp()//checks every combination of 4 cells horizontally in a diagonal going up-right, and calls checkWinStateOfCells on them.
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

        public WinState checkWinstateDiagonalRightDown()//checks every combination of 4 cells horizontally in a diagonal going down-right, and calls checkWinStateOfCells on them.
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

        public WinState checkWinstateOverall() // Checks if any player has won, vertically, horizontally, or diagonally. Uses the other checkWinner Functions. 

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




