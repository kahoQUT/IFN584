using System;
using static System.Console;
namespace a1;

public class NumTicTacToeGame : Game
{
    public NumTicTacToeGame(int boardSize, Player p1, Player p2) : base(boardSize, p1, p2) { }
    // public NumTicTacToeGame(int boardSize, Player p1, Player p2, GameState state) : base(boardSize, p1, p2, state) { }
    public override void Initialize()
    {
        Board = new NumericalBoard(BoardSize);
    }
    protected override bool endOfGame()
    {
        if (Board.CheckWin(Players[CurrentPlayerIndex]) || Board.GetEmptyCells().Count == 0) return true;
        else return false;
    }
    protected override void MakePlay()
    {
        Player currentPlayer = Players[CurrentPlayerIndex];
        currentPlayer.MakeMove(this);
    }
    public override void DisplayBoards()
    {
        Board.Display();
    }
    protected override void EndGame()
    {
        if (Board.CheckWin(Players[CurrentPlayerIndex]))
        {
            WriteLine($"{Players[CurrentPlayerIndex].Name} wins!");
        }
        else
        {
            WriteLine("It's a tie!");
        }
    }
    public override void DisplayHelpMenu()
    {
        try
        {
            WriteLine();
            string helpText = File.ReadAllText("NumTicTacToe_help.txt");
            WriteLine(helpText);
            WriteLine();
        }
        catch (Exception ex)
        {
            WriteLine("Could not load help menu. Error: " + ex.Message);
        }
    }
}
