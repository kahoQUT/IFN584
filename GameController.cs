using System;

namespace a1;

public class GameController
{
    private static readonly Dictionary<int, IGameFactory> factories = new()
    {
        { 1, new TicTacToeFactory() },
        { 3, new GomokuFactory() }
    };
    public static Game CreateGame(int gameType)
    {
        if (factories.TryGetValue(gameType, out var factory))
        {
            return factory.CreateGame();
        }
        else
        {
            throw new ArgumentException($"Unsupported game type: {gameType}");
        }
    }
}
