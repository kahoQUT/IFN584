using System;

namespace a1;

public enum PlayerType
{
    Human,
    Computer
}

public abstract class Player
{
    public string Name { get; protected set; }
    public bool IsComputer { get; protected set; }

    protected Player(string name, bool isComputer)
    {
        Name = name;
        IsComputer = isComputer;
    }

    // Gets the player's next move
    public abstract (int row, int col, int? value) GetMove(Board board);
}

public class HumanPlayer : Player
{
    public HumanPlayer(string name) : base(name, false) { }

    public override (int row, int col, int? value) GetMove(Board board)
    {
        Console.WriteLine($"{Name}, enter your move (row col [value]):");
        string input = Console.ReadLine()?.Trim()?.ToLower() ?? "";
        var parts = input.Split(' ');

        if (parts.Length < 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
        {
            Console.WriteLine("Invalid input. Try again.");
            return GetMove(board);
        }

        int? value = null;
        if (parts.Length == 3 && int.TryParse(parts[2], out int val))
            value = val;

        return (row - 1, col - 1, value);
    }
}


public class ComputerPlayer : Player
{
    public List<int> AvailableNumbers { get; set; } = new();

    public ComputerPlayer(string name) : base(name, true) { }

    public override (int row, int col, int? value) GetMove(Board board)
    {
        var empty = board.GetEmptyCells();
        if (empty.Count == 0)
            return (-1, -1, null);

        Random rnd = new Random();
        var (row, col) = empty[rnd.Next(empty.Count)];

        int? value = null;
        if (AvailableNumbers.Count > 0)
        {
            // Try winning moves here in advanced versions
            value = AvailableNumbers[rnd.Next(AvailableNumbers.Count)];
        }

        return (row, col, value);
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
