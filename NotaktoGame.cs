using System;
using static System.Console;

namespace a1;

public class NotaktoGame : Game
{
    public NotaktoGame(int boardSize, Player p1, Player p2) : base(boardSize, p1, p2)
    {}

    public override void Initialize()
    {
        Board = new NotaktoBoard();
    }
    protected override bool endOfGame()
    {
        return Board.CheckWin(Players[CurrentPlayerIndex]);
    }
    protected override void MakePlay()
    {
        //Player make their corresponding move
        Players[CurrentPlayerIndex].MakeMove(this);
    }
    public override void DisplayBoards()
    {
        Board.Display();
    }
    protected override void EndGame()
    {
        Board.Display();
        WriteLine($"Winner is {Players[(CurrentPlayerIndex + 1) % 2].Name}");
    }
    public override void DisplayHelpMenu()
    {
        //Reading Help Menu from text file
        try
        {
            WriteLine();
            string helpText = File.ReadAllText("Notakto_help.txt");
            WriteLine(helpText);
            WriteLine();
        }
        catch (Exception ex)
        {
            WriteLine("Could not load help menu. Error: " + ex.Message);
        }
    }
}
