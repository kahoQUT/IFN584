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