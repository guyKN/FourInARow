using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FourInARow
{
    class FourInARowGame
    {
        FourInARowPos pos; 
        bool prevError; // If true, you need to notify the user that what they wrote last time was invalid.
        public FourInARowGame()
        {
            pos = new FourInARowPos();
            prevError = false;
        }

        public void playGame()
        {
            while (true) 
            {
                try
                {
                    Console.Clear();
                    pos.printPos();
                    Console.WriteLine("");

                    FourInARowPos.WinState winState = pos.checkWinstateOverall(); //checks wether the current pos is winning losing or a tie. 
                    if (winState == FourInARowPos.WinState.Owins) // if O wins, tell the user that O won and close the program

                    {
                        Console.WriteLine("");
                        Console.WriteLine("O wins!");
                        break;
                    }
                    else if (winState == FourInARowPos.WinState.Xwins)// if X wins, tell the user that O won and close the program
                    {
                        Console.WriteLine("");
                        Console.WriteLine("X wins!");
                        break;
                    }
                    else if (winState == FourInARowPos.WinState.tie)// if there's a tie, tell the user that O won and close the program
                    {
                        Console.WriteLine("");
                        Console.WriteLine("It's a tie!");
                        break;
                    }
                    else // if no one is winning
                    {
                        int row = askForRow();
                        pos.placeChip(row);
                        pos.switchActivePlayer();
                        prevError = false; // no error has ocoured
                    }
                }
                catch (System.ArgumentException)//If the user didn't enter a number between 1 and 7, or if he entered a number of a row that's already full of chips, tell him that next time
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
            if (prevError) // if the user inputed an invalid position last time, then tell him that that happened.  
            {
                Console.WriteLine("Please enter a number between 1 and 7, whoose row is not already full. ");
            }
            else
            {
                Console.WriteLine(""); // otherwise, write empty line. 

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
            string rowStr = Console.ReadLine(); // take the user's input
            int row = Convert.ToInt32(rowStr)-1; // convert it to an intiger, than subtract 1 because arrays start at 0. 
            return row;

        }
    }
}
