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

    public abstract void MakeMove(Board board);

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

    // todo if invalid move, retry
    public override void MakeMove(Board board)
    {
        while (true)
        {

            WriteLine($"{Name}'s turn{(IsNumGame ? $". Your numbers: {string.Join(", ", AvailableNumbers)}" : $" ({Symbol})")}.");
            Write("Enter move (row col value): ");
            string[] input = ReadLine()?.Split() ?? Array.Empty<string>();

            if (input.Length != 3 ||
                !int.TryParse(input[0], out int row) ||
                !int.TryParse(input[1], out int col) ||
                !int.TryParse(input[2], out int value))
            {
                WriteLine("Invalid input. Format: row col value");
                continue;
            }

            row--; col--;

            if (IsNumGame)
            {
                if (!HasNumber(value))
                {
                    WriteLine("You don't have that number.");
                }
                else
                {
                    if (!board.PlaceMove(row, col, this, value))
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
                if (!board.PlaceMove(row, col, this))
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

    public ComputerPlayer(string name, bool isNumGame, int boardSize, char? symbol = null): base(name, isNumGame, symbol)
    {
        if (isNumGame)
        {
            int maxNumber = boardSize * boardSize;
            for (int i = 2; i <= maxNumber; i += 2)
                AvailableNumbers.Add(i);
        }
    }

    public override void MakeMove(Board board)
    {
        WriteLine($"{Name}'s turn {(IsNumGame ? "" : $"({Symbol})")}. Thinking...");

        var emptyCells = board.GetEmptyCells();

        if (IsNumGame)
        {
            foreach (var (r, c) in emptyCells)
            {
                foreach (int num in AvailableNumbers)
                {
                    if (board.PlaceMove(r, c, this, num))
                    {
                        if (board.CheckWin(this))
                        {
                            UseNumber(num);
                            WriteLine($"Computer places {num} at ({r + 1}, {c + 1})");
                            return;
                        }
                        board.ResetNumber(r, c);
                    }
                }
            }

            var (row, col) = emptyCells[random.Next(emptyCells.Count)];
            int choice = ChooseRandomNumber();
            board.PlaceMove(row, col, this, choice);
            UseNumber(choice);
            WriteLine($"Computer randomly places {choice} at ({row + 1}, {col + 1}).");
        }
        else
        {
            var (row, col) = emptyCells[random.Next(emptyCells.Count)];
            board.PlaceMove(row, col, this);
            WriteLine($"Computer places symbol at ({row + 1}, {col + 1}).");
        }
    }
}