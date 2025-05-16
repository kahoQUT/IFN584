using System;

namespace a1;

public class GameController
{
    private static Dictionary<int, IGameFactory> factories = new()
    {
        { 1, new NumTicTacToeFactory() },
        { 3, new GomokuFactory() }
    };
    public static Game CreateGame(int gameType, int boardSize, Player player1, Player player2)
    {
        if (factories.TryGetValue(gameType, out var factory))
        {
            return factory.CreateGame(boardSize, player1, player2);
        }
        else
        {
            throw new ArgumentException("Unsupported game type.");
        }
    }
}
