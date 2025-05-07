using System;
using static System.Console;

namespace a1;

public class Menu
{
    public static void ShowMainMenu()
    {
        string input;

        WriteLine("Let's play Numerical Tic-Tac-Toe!");
        WriteLine("1 - Start A New Game");
        WriteLine("2 - Load Game");
        WriteLine("3 - Exit");

        Write("Enter choice >> ");

        //Validate User Input
        input = ReadLine() ?? ""; //check if it is null

        switch (input.Trim())
        {
            case "1":
                Game.StartNewGame();
                break;
            case "2":
                Game.LoadGame();
                break;
            case "3":
                WriteLine("See you!");
                break;
            default:
                WriteLine("Invalid choice. Please Enter again.");
                ShowMainMenu();
                break;
        }
    }
}
