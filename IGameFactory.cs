using System;

namespace a1;

//interface for game behavior
public interface IGameFactory
{
    Game CreateGame(int boardSize, Player player1, Player player2);
}
//Concrete implementation for Game
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