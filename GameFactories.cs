using System;

namespace a1;

//Abstract factory class which defines the factory method
public abstract class GameFactories
{
    //Factory Method responsible for object creation
    public abstract IGame CreateGameFactory();

    public Game Initialize(int boardSize, Player player1, Player player2)
    {
        IGame gamefactory = CreateGameFactory();
        return gamefactory.CreateGame(boardSize, player1, player2);
    }
}
public class NumTicTacToeFactory: GameFactories
{
    public override IGame CreateGameFactory()
    {
        return new NumTicTacToe();
    }
}
public class NotaktoFactory: GameFactories
{
    public override IGame CreateGameFactory()
    {
        return new Notakto();
    }
}
public class GomokuFactory: GameFactories
{
    public override IGame CreateGameFactory()
    {
        return new Gomoku();
    }
}
