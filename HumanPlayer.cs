using System;
using static System.Console;
namespace a1;

public class HumanPlayer : Player
{
    //Constructor of Human Player
    public HumanPlayer(string name, bool isNumGame, bool isFirstPlayer, int boardSize, char? symbol = null) : base(name, isNumGame, symbol)
    {
        if (isNumGame)
        {
            int maxNumber = boardSize * boardSize;
            for (int i = 1; i <= maxNumber; i++)
            {
                if (isFirstPlayer && i % 2 != 0) AvailableNumbers.Add(i);
                if (!isFirstPlayer && i % 2 == 0) AvailableNumbers.Add(i);
            }
        }
    }

    public override void MakeMove(Game game)
    {
        while (true)
        {
            WriteLine($"{Name}'s turn{(IsNumGame ? $". Your numbers: {string.Join(", ", AvailableNumbers)}" : $" ({Symbol})")}.");
            Write($"Enter move ({(IsNumGame ? "row col number" : "row col")}) or help for other commands: ");

            string[] input = ReadLine()?.Split() ?? Array.Empty<string>();

            if (input.Length == 1)
            {
                if (input[0].ToLower() == "save")
                {
                    Write("Enter filename to save (default: save.json): ");
                    string pathInput = ReadLine()?.Trim();
                    string filename = string.IsNullOrEmpty(pathInput) ? "save.json" : pathInput;
                    game.SaveGame(filename);
                    game.DisplayBoards();
                    continue;
                }
                else if (input[0].ToLower() == "undo")
                {
                    game.Undo();
                    game.DisplayBoards();
                    continue;
                }
                else if (input[0].ToLower() == "redo")
                {
                    game.Redo();
                    game.DisplayBoards();
                    continue;
                }
                else if (input[0].ToLower() == "help")
                {
                    game.DisplayHelpMenu();
                    continue;
                }
                
            }

            // Validation for move input
            if (IsNumGame)
            {
                if (input.Length != 3 ||
               !int.TryParse(input[0], out int row) ||
               !int.TryParse(input[1], out int col) ||
               !int.TryParse(input[2], out int value))
                {
                    WriteLine("Invalid input. Enter move (row col value) or help for other commands: ");
                    continue;
                }
                row--; col--;

                if (!HasNumber(value))
                {
                    WriteLine("You don't have that number.");
                }
                else
                {
                    if (!game.Board.PlaceMove(row, col, this, value))
                    {
                        WriteLine("Invalid move.");
                    }
                    else
                    {
                        UseNumber(value);
                        return;
                    }
                }
            }
            else
            {
                if (input.Length != 2 ||
                !int.TryParse(input[0], out int row) ||
                !int.TryParse(input[1], out int col))
                {
                    WriteLine("Invalid input. Format: row col");
                    continue;
                }
                row--; col--;
                if (!game.Board.PlaceMove(row, col, this))
                {
                    WriteLine("Invalid move.");
                }
                else return;
            }
        }

    }
}

