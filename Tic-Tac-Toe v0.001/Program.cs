using System;


namespace ConsoleApp1
{
    class Program
    {
        static char[,] table = new char[5, 5];
        static int downArrowRow = 0;
        static int rightArrowCol = 0;

        static void Main(string[] args)
        {
            Console.Title = "Tic-Tac-Toe v0.001";
            Console.WindowHeight = 10;
            Console.WindowWidth = 20;
            Console.BufferHeight = 10;
            Console.BufferWidth = 20;

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < table.GetLength(0); i++) // Fills the table with empty chars for easier showing on the console.
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        table[i, j] = ' ';
                    }
                }


                Random rnd = new Random();
                int playerMove = rnd.Next(0, 2);

                int movesPlayed = 0;

                while (true)
                {

                    int row = 1;
                    int col = 1;


                    char charTurn = '.';
                    if (playerMove % 2 == 0)
                    {
                        charTurn = 'X';
                    }
                    else
                    {
                        charTurn = 'O';
                    }

                    playerMove++;

                    ResetArrows(table);
                    DrawBoard(table, charTurn);


                    while (true)
                    {
                        ConsoleKeyInfo keyPressed = Console.ReadKey();
                        if (keyPressed.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                        if (keyPressed.Key == ConsoleKey.UpArrow && IsInside(row - 1))
                        {
                            table[row, rightArrowCol] = ' ';
                            row -= 1;
                            table[row, rightArrowCol] = '→';
                        }
                        else if (keyPressed.Key == ConsoleKey.DownArrow && IsInside(row + 1))
                        {
                            table[row, rightArrowCol] = ' ';
                            row += 1;
                            table[row, rightArrowCol] = '→';
                        }
                        else if (keyPressed.Key == ConsoleKey.RightArrow && IsInside(col + 1))
                        {
                            table[downArrowRow, col] = ' ';
                            col += 1;
                            table[downArrowRow, col] = '↓';
                        }
                        else if (keyPressed.Key == ConsoleKey.LeftArrow && IsInside(col - 1))
                        {
                            table[downArrowRow, col] = ' ';
                            col -= 1;
                            table[downArrowRow, col] = '↓';
                        }
                        else if (keyPressed.Key == ConsoleKey.Escape)
                        {
                            return;
                        }
                        Console.Clear();
                        DrawBoard(table, charTurn);
                    } // While loop for player choosing where to place a symbol.

                    if (table[row, col] == 'X' || table[row, col] == 'O')
                    {
                        Console.Clear();
                        Console.SetCursorPosition(2, 5);
                        Console.WriteLine("Place is occupied!");
                        playerMove--;
                        continue;
                    } //Check if a player tries to place on occupied place. If yes continues the while loop and it's the same player's turn.

                    table[row, col] = charTurn;

                    int isWon = CheckIfWon(table);  //Gets the returned value from method.
                    movesPlayed++;
                    if (isWon == 1)
                    {
                        Console.Clear();
                        DrawBoard(table, charTurn);
                        Console.WriteLine("Player 'X' Won");
                        break;
                    }
                    else if (isWon == -1)
                    {
                        Console.Clear();
                        DrawBoard(table, charTurn);
                        Console.WriteLine("Player 'O' Won");
                        break;
                    }
                    else if (movesPlayed == 9)
                    {
                        Console.WriteLine("Draw");
                        break;
                    }
                    Console.Clear();
                }
                Console.SetCursorPosition(2, 6);
                Console.WriteLine($"Want a rematch?");
                Console.SetCursorPosition(3, 7);
                Console.WriteLine(@"'Enter' for Yes");
                Console.SetCursorPosition(3, 8);
                Console.WriteLine(@"Any key to leave");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                   continue;
                }
                else
                {
                    break;
                }

            }
        }

        private static void ResetArrows(char[,] table)
        {
            table[0, 1] = '↓';
            table[1, 0] = '→';

            table[0, 2] = ' ';
            table[0, 3] = ' ';
            table[2, 0] = ' ';
            table[3, 0] = ' ';
        }  //Resets the indicating arrows in default position.

        private static bool IsInside(int number)
        {
            return number >= 1 && number <= 3;
        } //Checks if the indicating arrows are inside the play field.

        private static void DrawBoard(char[,] table, char playerTurn)
        {
            Console.SetCursorPosition(2, 0);
            Console.WriteLine($"    {table[0, 1]}   {table[0, 2]}   {table[0, 3]}");
            Console.SetCursorPosition(2, 1);
            Console.WriteLine($"{table[1, 0]} | {table[1, 1]} | {table[1, 2]} | {table[1, 3]} |");
            Console.SetCursorPosition(2, 2);
            Console.WriteLine($"{table[2, 0]} | {table[2, 1]} | {table[2, 2]} | {table[2, 3]} |");
            Console.SetCursorPosition(2, 3);
            Console.WriteLine($"{table[3, 0]} | {table[3, 1]} | {table[3, 2]} | {table[3, 3]} |");
            Console.SetCursorPosition(3, 4);
            Console.WriteLine($"Player's {playerTurn} turn.");
        } //Draws board with player turn.

        private static int CheckIfWon(char[,] table)
        {   //Wining check 'X'
            if (table[1, 1] == 'X' && table[2, 2] == 'X' && table[3, 3] == 'X') //Diagonal Right 'X'
            {
                return 1;
            }
            else if (table[1, 3] == 'X' && table[2, 2] == 'X' && table[3, 1] == 'X') //Diagonal Left 'X'
            {
                return 1;
            }
            else if (table[1, 1] == 'X' && table[1, 2] == 'X' && table[1, 3] == 'X') //Horizontal Top 'X'
            {
                return 1;
            }
            else if (table[2, 1] == 'X' && table[2, 2] == 'X' && table[2, 3] == 'X') //Horizontal Middle 'X'
            {
                return 1;
            }
            else if (table[3, 1] == 'X' && table[3, 2] == 'X' && table[3, 3] == 'X') //Horizontal Bottom 'X'
            {
                return 1;
            }
            else if (table[1, 1] == 'X' && table[2, 1] == 'X' && table[3, 1] == 'X')  //Vertical Left 'X'
            {
                return 1;
            }
            else if (table[1, 3] == 'X' && table[2, 3] == 'X' && table[3, 3] == 'X')  //Vertical Right 'X'
            {
                return 1;
            }
            else if (table[1, 2] == 'X' && table[2, 2] == 'X' && table[3, 2] == 'X')  //Vertical Middle 'X'
            {
                return 1;
            }
            //Wining check 'O'
            if (table[1, 1] == 'O' && table[2, 2] == 'O' && table[3, 3] == 'O') //Diagonal Right 'O'
            {
                return -1;
            }
            else if (table[1, 3] == 'O' && table[2, 2] == 'O' && table[3, 1] == 'O') //Diagonal Left 'O'
            {
                return -1;
            }
            else if (table[1, 1] == 'O' && table[1, 2] == 'O' && table[1, 3] == 'O') //Horizontal Top 'O'
            {
                return -1;
            }
            else if (table[2, 1] == 'O' && table[2, 2] == 'O' && table[2, 3] == 'O') //Horizontal Middle 'O'
            {
                return -1;
            }
            else if (table[3, 1] == 'O' && table[3, 2] == 'O' && table[3, 3] == 'O') //Horizontal Bottom 'O'
            {
                return -1;
            }
            else if (table[1, 1] == 'O' && table[2, 1] == 'O' && table[3, 1] == 'O')  //Vertical Left 'O'
            {
                return -1;
            }
            else if (table[1, 3] == 'O' && table[2, 3] == 'O' && table[3, 3] == 'O')  //Vertical Right 'O'
            {
                return -1;
            }
            else if (table[1, 2] == 'O' && table[2, 2] == 'O' && table[3, 2] == 'O')  //Vertical Middle 'O'
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }  //Checks if 'X' or 'O' has won the game and returns 1 if 'X' has won, -1 if 'O' has won and 0 if no one won this turn.
    }
}