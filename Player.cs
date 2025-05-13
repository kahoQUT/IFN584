using System;

namespace a1;

public abstract class Player
{
    public string Name { get; }
    public char Symbol { get; }

    protected Player(string name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }

    public abstract void MakeMove(Board board);
}
public class HumanPlayer : Player
{
    public HumanPlayer(string name, char symbol) : base(name, symbol) { }

    public override void MakeMove(Board board)
    {
        Console.WriteLine($"{Name}'s turn ({Symbol}). Enter your move (row and column):");
        Console.Write("Row: ");
        int row = int.Parse(Console.ReadLine());
        Console.Write("Col: ");
        int col = int.Parse(Console.ReadLine());

        board.PlaceSymbol(row, col, Symbol);
    }
}

public class ComputerPlayer : Player
{
    private readonly Random _random = new();

    public ComputerPlayer(string name, char symbol) : base(name, symbol) { }

    public override void MakeMove(Board board)
    {
        Console.WriteLine($"{Name}'s turn ({Symbol}). Thinking...");

        (int row, int col) = board.GetRandomAvailableMove();
        board.PlaceSymbol(row, col, Symbol);

        Console.WriteLine($"Computer chose ({row}, {col})");
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
