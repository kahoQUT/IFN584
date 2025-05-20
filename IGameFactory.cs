using System;

namespace a1;

//interface for game behavior
public interface IGame
{
    Game CreateGame(int boardSize, Player player1, Player player2);
}
//Concrete implementation for Game
public class NumTicTacToe : IGame
{
    public Game CreateGame(int boardSize, Player player1, Player player2)
    { return new NumTicTacToeGame(boardSize, player1, player2);}
    
}
public class Gomoku : IGame
{
    public Game CreateGame(int boardSize, Player player1, Player player2)
    { return new GomokuGame(boardSize, player1, player2); }
}

public class Notakto : IGame
{
    public Game CreateGame(int boardSize, Player player1, Player player2)
    { return new NotaktoGame(boardSize, player1, player2); }
}