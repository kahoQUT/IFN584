using System;

namespace a1;

public interface IGameFactory
{
    Game CreateGame(int boardSize, Player player1, Player player2);
}
public class NumTicTacToeFactory : IGameFactory
{
    public Game CreateGame(int boardSize, Player player1, Player player2)
    { return new NumTicTacToeGame(boardSize, player1, player2);}
    
}
public class GomokuFactory : IGameFactory
{
    public Game CreateGame(int boardSize, Player player1, Player player2)
    { return new GomokuGame(boardSize, player1, player2); }
}

public class NotaktoFactory : IGameFactory
{
    public Game CreateGame(int boardSize, Player player1, Player player2)
    { return new NotaktoGame(boardSize, player1, player2); }
}