using System;

namespace a1;

public class GameController
{
    public static Game Create(int gameType, int boardSize, Player player1, Player player2)
    {
        switch(gameType)
        {
            case 1:
                GameFactories ticTacToeController = new NumTicTacToeFactory();
                return ticTacToeController.Initialize(boardSize, player1, player2);
            case 2:
                GameFactories notaktoController = new NotaktoFactory();
                return notaktoController.Initialize(boardSize, player1, player2);
            case 3:
                GameFactories gomokuController = new GomokuFactory();
                return gomokuController.Initialize(boardSize, player1, player2);
            default:
                throw new ArgumentException("Unsupported game type.");
        }
    }
}
