using System;

namespace a1;

//Concrete implementation for Game
public class GomokuFactory : IGameFactory
{
    public Game CreateGame(int boardSize, Player player1, Player player2)
    { return new GomokuGame(boardSize, player1, player2); }
}
