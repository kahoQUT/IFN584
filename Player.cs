using System;
using static System.Console;
namespace a1;

public abstract class Player
{
    public string Name { get; }
    public char? Symbol { get; }
    public List<int> AvailableNumbers { get; set; }
    public bool IsNumGame { get; }

    public Player(string name, bool isNumGame, char? symbol = null)
    {
        Name = name;
        IsNumGame = isNumGame;
        Symbol = symbol;
        AvailableNumbers = new List<int>();
    }

    public abstract void MakeMove(Game game);

    //Check player containing numbers
    public bool HasNumber(int number)
    {
        return AvailableNumbers.Contains(number);
    }

    //Remove player used numbers
    public void UseNumber(int number)
    {
        AvailableNumbers.Remove(number);
    }

    //Return a random avaliable number
    public int ChooseRandomNumber()
    {
        Random ranGenerator = new Random();
        return AvailableNumbers[ranGenerator.Next(AvailableNumbers.Count)];
    }
}
public class HumanPlayer : Player
{
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
                if (input[0].Equals("save", StringComparison.OrdinalIgnoreCase))
                {
                    Write("Enter filename to save (default: save.json): ");
                    string filename = ReadLine()?.Trim() ?? "save.json";
                    game.SaveGame(filename);
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
                    if (!game.GameBoard.PlaceMove(row, col, this, value))
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
                if (!game.GameBoard.PlaceMove(row, col, this))
                {
                    WriteLine("Invalid move.");
                }
                else return;
            }
        }

    }
}

public class ComputerPlayer : Player
{
    private Random random = new();

    public ComputerPlayer(string name, bool isNumGame, int boardSize, char? symbol = null) : base(name, isNumGame, symbol)
    {
        if (isNumGame)
        {
            int maxNumber = boardSize * boardSize;
            for (int i = 2; i <= maxNumber; i += 2)
                AvailableNumbers.Add(i);
        }
    }

    public override void MakeMove(Game game)
    {
        WriteLine($"{Name}'s turn {(IsNumGame ? "" : $"({Symbol})")}. Thinking...");

        var emptyCells = game.GameBoard.GetEmptyCells();

        if (IsNumGame)
        {
            foreach (var (r, c) in emptyCells)
            {
                foreach (int num in AvailableNumbers)
                {
                    if (game.GameBoard.PlaceMove(r, c, this, num))
                    {
                        if (game.GameBoard.CheckWin(this))
                        {
                            UseNumber(num);
                            WriteLine($"Computer places {num} at ({r + 1}, {c + 1})");
                            return;
                        }
                        game.GameBoard.ResetNumber(r, c);
                    }
                }
            }

            var (row, col) = emptyCells[random.Next(emptyCells.Count)];
            int choice = ChooseRandomNumber();
            game.GameBoard.PlaceMove(row, col, this, choice);
            UseNumber(choice);
            WriteLine($"Computer randomly places {choice} at ({row + 1}, {col + 1}).");
        }
        else
        {
            var (row, col) = emptyCells[random.Next(emptyCells.Count)];
            game.GameBoard.PlaceMove(row, col, this);
            WriteLine($"Computer places symbol at ({row + 1}, {col + 1}).");
        }
    }
}