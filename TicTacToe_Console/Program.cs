using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace TicTacToe_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isExit = false;
            while (!isExit)
            {
                isExit = PlayTicTacToe();
            }
        }

        private static bool PlayTicTacToe()
        {
            char[,] squares = new char[,] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
            bool isGameOver = false;
            int playerNumber = 1;
            while (!isGameOver)
            {
                bool isInputValid = false;
                while (!isInputValid)
                {
                    DisplayCurrentBoard(squares);
                    isInputValid = PromptForField(playerNumber, squares);
                }
                isGameOver = CheckForWin(squares, playerNumber);
                playerNumber = playerNumber == 1 ? playerNumber+1 : playerNumber-1;
            }
            Console.WriteLine("Would you like to play again? (Y / N)");
            var response = Console.ReadLine();
            var normalizedResponse = response.ToLower();
            return !(normalizedResponse == "y" || normalizedResponse == "yes");
        }

        private static void DisplayCurrentBoard(char[,] squares)
        {
            int fieldIndex = 1;
            int row = 0;
            int col = 0;
            int xMod3 = 0;
            int yMod3 = 0;
            Console.Clear();

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    xMod3 = x % 3;
                    yMod3 = y % 3;
                    if (xMod3 == 1 && yMod3 == 1)
                    {
                        Console.Write(squares[row, col]);
                        fieldIndex++;
                        row = GetRowFromInput(fieldIndex - 1);
                        col = GetColFromInput(fieldIndex - 1);
                    }
                    else
                    {
                        if (xMod3 == 0 && x > 0)
                        {
                            Console.Write("|");
                        }
                        if (yMod3 == 2 && y < 8)
                        {
                            Console.Write("___|___|___");
                            break;
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine("");
            }
        }

        private static bool PromptForField(int playerNumber, char [,] squares)
        {
            Console.WriteLine("");
            Console.WriteLine("Player {0}: Choose your Field!", playerNumber);
            var input = Console.ReadLine();
            int field = 0;
            int value = 0;
            try
            {
                if (!int.TryParse(input, out field) || !int.TryParse(squares[GetRowFromInput(field-1), GetColFromInput(field-1)].ToString(), out value))
                {
                    Console.WriteLine("Please enter a valid field!");
                    Console.ReadLine();
                    return false;
                }
                else
                {
                    char newValue = playerNumber == 1 ? 'X' : 'O';
                    squares[GetRowFromInput(field-1), GetColFromInput(field-1)] = newValue;
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid field!");
                Console.ReadLine();
                return false;
            }
        }

        private static int GetRowFromInput(int input)
        {
            return input / 3;
        }

        private static int GetColFromInput(int input)
        {
            return input % 3;
        }

        private static bool CheckForWin(char [,] squares, int playerNumber)
        {
            bool isGameOver = false;
            bool isWinner = false;

            isWinner = ((squares[0, 0] == squares[0, 1] && squares[0, 0] == squares[0, 2]) ||
                        (squares[0, 0] == squares[1, 0] && squares[0, 0] == squares[2, 0]) ||
                        (squares[0, 1] == squares[1, 1] && squares[0, 1] == squares[2, 1]) ||
                        (squares[1, 0] == squares[1, 1] && squares[1, 0] == squares[1, 2]) ||
                        (squares[0, 2] == squares[1, 2] && squares[0, 2] == squares[2, 2]) ||
                        (squares[2, 0] == squares[2, 1] && squares[2, 0] == squares[2, 2]) ||
                        (squares[0, 0] == squares[1, 1] && squares[0, 0] == squares[2, 2]) ||
                        (squares[2, 0] == squares[1, 1] && squares[2, 0] == squares[0, 2]));
            isGameOver = isWinner;
            if (!isGameOver)
            {
                int value = 0;
                for(int input = 1; input <= 9; input++)
                {
                    if (int.TryParse(squares[GetRowFromInput(input-1), GetColFromInput(input-1)].ToString(), out value))
                        return false;
                }
                isGameOver = true;
            }

            if(isGameOver)
            {
                Console.Write("Game Over!");
                if(isWinner)
                {
                    Console.Write("  Player {0} has won!", playerNumber);
                }
                Console.WriteLine();
            }
            return isGameOver;
        }
    }
}
