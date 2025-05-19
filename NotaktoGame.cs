using System;

namespace a1;

public class NotaktoGame : Game
{
    public NotaktoGame(int boardSize, Player p1, Player p2) : base(boardSize, p1, p2) { }
    public NotaktoGame(int boardSize, Player p1, Player p2, GameState state) : base(boardSize, p1, p2, state) { }

    protected override void Initialize()
    {
        WriteLine($"Initialize NotaktoGame");
        Board =new NotaktoBoard();
    }
    protected override bool endOfGame()
    {
        return Board.CheckWin(Players[CurrentPlayerIndex]);
    }
    protected override void MakePlay()
    {
        Players[CurrentPlayerIndex].MakeMove(Board);
    }
    protected override void DisplayBoards()
    {
        Board.Display();
    }
    protected override void EndGame()
    {
        Board.Display();
        WriteLine($"Winner is {Players[CurrentPlayerIndex].Name}");
    }
}
