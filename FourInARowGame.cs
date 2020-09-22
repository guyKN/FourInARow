using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FourInARow
{
    class FourInARowGame
    {
        FourInARowPos pos;
        bool prevError;
        public FourInARowGame()
        {
            pos = new FourInARowPos();
            prevError = false;
        }

        public void playGame()
        {
            while (true) //todo: handle exceptions
            {
                try
                {
                    Console.Clear();
                    pos.printPos();
                    Console.WriteLine("");

                    FourInARowPos.WinState winState = pos.checkWinstate();
                    if (winState == FourInARowPos.WinState.Owins)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("O wins!");
                        break;
                    }
                    else if (winState == FourInARowPos.WinState.Xwins)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("X wins!");
                        break;
                    }
                    else if (winState == FourInARowPos.WinState.tie)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("It's a tie!");
                        break;
                    }
                    else
                    {
                        int row = askForRow();
                        pos.placeChip(row);
                        pos.switchActivePlayer();
                        prevError = false;
                    }
                }
                catch (System.ArgumentException)
                {
                    prevError = true;
                }
                catch (System.FormatException)
                {
                    prevError = true;
                }
            }
        }

        private int askForRow()
        {
            if (prevError)
            {
                Console.WriteLine("Please enter a number between 1 and 7, whoose row is not already full. ");
            }
            else
            {
                Console.WriteLine("");

            }
            if(pos.activePlayer == FourInARowPos.ActivePlayer.X)
            {
                Console.WriteLine("X's turn.");
                Console.WriteLine("Enter where you want to place your chip (1-7):");
            }
            else
            {
                Console.WriteLine("O's turn.");
                Console.WriteLine("Enter where you want to place your chip (1-7):");
            }
            string rowStr = Console.ReadLine();
            int row = Convert.ToInt32(rowStr)-1;
            return row;

        }
    }
}
