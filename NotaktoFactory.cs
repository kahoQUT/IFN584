using System;

namespace a1;

//Concrete implementation for Game
public class NotaktoFactory : IGameFactory
{
    public Game CreateGame(int boardSize, Player player1, Player player2)
    { return new NotaktoGame(boardSize, player1, player2); }
}