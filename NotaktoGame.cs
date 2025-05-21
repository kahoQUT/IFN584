using System;
using static System.Console;

namespace a1;

public class NotaktoGame : Game
{
    public NotaktoGame(int boardSize, Player p1, Player p2) : base(boardSize, p1, p2)
    {
        IsDistinctPieces = false;
    }
    // public NotaktoGame(int boardSize, Player p1, Player p2, GameState state) : base(boardSize, p1, p2, state) { }

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
        Players[CurrentPlayerIndex].MakeMove(this);
    }
    public override void DisplayBoards()
    {
        Board.Display();
    }
    protected override void EndGame()
    {
        Board.Display();
        WriteLine($"Winner is {Players[CurrentPlayerIndex].Name}");
    }
    public override void DisplayHelpMenu()
    {
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
