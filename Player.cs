using System;

namespace a1;

public enum PlayerType
{
    Human,
    Computer
}

public class Player
{
    public string Name { get; }
    public PlayerType Type { get; }
    public List<int> AvailableNumbers { get; set; }

    //Constructors for new players
    public Player(string name, PlayerType type, bool isFirstPlayer, int maxNumber)
    {
        Name = name;
        Type = type;
        AvailableNumbers = new List<int>();

        for (int i = 1; i <= maxNumber; i++)
        {
            if (isFirstPlayer && i % 2 != 0) AvailableNumbers.Add(i);
            if (!isFirstPlayer && i % 2 == 0) AvailableNumbers.Add(i);
        }
    }

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
