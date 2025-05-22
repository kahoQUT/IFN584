using System;

namespace a1;

//interface for game behavior
public interface IGameFactory
{
    Game CreateGame(int boardSize, Player player1, Player player2);
}
