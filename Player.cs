using System;

namespace a1;

public abstract class Player
{
    public string Name { get; }
    public char? Symbol { get; }
    public List<int> AvailableNumbers { get; set; }
    public bool IsNumericalGame { get; }

    protected Player(string name, bool isNumericalGame, char? symbol = null)
    {
        Name = name;
        IsNumericalGame = isNumericalGame;
        Symbol = symbol;
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
    public HumanPlayer(string name, bool isNumericalGame, bool isFirstPlayer, int boardSize, char? symbol) : base(name, isNumericalGame, symbol)
    {
        if (isNumericalGame)
        {
            int maxNumber = boardSize * boardSize;
            for (int i = 1; i <= maxNumber; i++)
            {
                if (isFirstPlayer && i % 2 != 0) AvailableNumbers.Add(i);
                if (!isFirstPlayer && i % 2 == 0) AvailableNumbers.Add(i);
            }
        }
    }

    public override void MakeMove(Board board)
    {
        Console.WriteLine($"{Name}'s turn{(IsNumericalGame ? $". Your numbers: {string.Join(", ", AvailableNumbers)}" : $" ({Symbol})")}.");
        Console.Write("Enter move (row col value): ");
        string[] input = Console.ReadLine()?.Split() ?? Array.Empty<string>();

        if (input.Length != 3 ||
            !int.TryParse(input[0], out int row) ||
            !int.TryParse(input[1], out int col) ||
            !int.TryParse(input[2], out int value))
        {
            Console.WriteLine("Invalid input. Format: row col value");
            return;
        }

        row--; col--;

        if (IsNumericalGame)
        {
            if (!HasNumber(value))
            {
                Console.WriteLine("You don't have that number.");
                return;
            }

            if (!board.PlaceMove(row, col, this, value))
            {
                Console.WriteLine("Invalid move.");
                return;
            }

            UseNumber(value);
        }
        else
        {
            if (!board.PlaceMove(row, col, this))
            {
                Console.WriteLine("Invalid move.");
            }
        }
    }
}

public class ComputerPlayer : Player
{
    private Random random = new();

    public ComputerPlayer(string name, bool isNumericalGame, int boardSize, char? symbol = null): base(name, isNumericalGame, symbol)
    {
        if (isNumericalGame)
        {
            int maxNumber = boardSize * boardSize;
            for (int i = 2; i <= maxNumber; i += 2)
                AvailableNumbers.Add(i);
        }
    }

    public override void MakeMove(Board board)
    {
        Console.WriteLine($"{Name}'s turn {(IsNumericalGame ? "" : $"({Symbol})")}. Thinking...");

        var emptyCells = board.GetEmptyCells();

        if (IsNumericalGame)
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
                            Console.WriteLine($"Computer places {num} at ({r + 1}, {c + 1}) to win.");
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
            Console.WriteLine($"Computer places {choice} at ({row + 1}, {col + 1}).");
        }
        else
        {
            var (row, col) = emptyCells[random.Next(emptyCells.Count)];
            board.PlaceMove(row, col, this);
            Console.WriteLine($"Computer places symbol at ({row + 1}, {col + 1}).");
        }
    }
}



// public class Player
// {
//     public string Name { get; }
//     public PlayerType Type { get; }
//     public List<int> AvailableNumbers { get; set; }

//     //Constructors for new players
//     public Player(string name, PlayerType type, bool isFirstPlayer, int maxNumber)
//     {
//         Name = name;
//         Type = type;
//         AvailableNumbers = new List<int>();

//         for (int i = 1; i <= maxNumber; i++)
//         {
//             if (isFirstPlayer && i % 2 != 0) AvailableNumbers.Add(i);
//             if (!isFirstPlayer && i % 2 == 0) AvailableNumbers.Add(i);
//         }
//     }

//     //Check player containing numbers
//     public bool HasNumber(int number)
//     {
//         return AvailableNumbers.Contains(number);
//     }

//     //Remove player used numbers
//     public void UseNumber(int number)
//     {
//         AvailableNumbers.Remove(number);
//     }

//     //Return a random avaliable number
//     public int ChooseRandomNumber()
//     {
//         Random ranGenerator = new Random();
//         return AvailableNumbers[ranGenerator.Next(AvailableNumbers.Count)];
//     }
// }
