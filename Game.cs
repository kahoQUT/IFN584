using System;
using static System.Console;
using System.Text.Json;

namespace a1;

public abstract class Game
{
    // todo should we go for Board, not array
    protected Board Board;
    protected Player[] Players;
    protected int BoardSize;
    protected int CurrentPlayerIndex;
    public Game(int boardSize, Player player1, Player player2)
    {
        Players = new Player[] { player1, player2 };
        CurrentPlayerIndex = 0;
        BoardSize = boardSize;
    }

    public Game(int boardSize, Player player1, Player player2, GameState state)
    {
        Players = new Player[] { player1, player2 };
        CurrentPlayerIndex = state.CurrentPlayerIndex;
        BoardSize = boardSize;
    }

    //Template method outling the steps
    public void playGame()
    {
        Initialize();

        while (!endOfGame())
        {
            DisplayBoards();
            MakePlay();
            SwitchTurn();
        }

        EndGame();
    }

    protected abstract void Initialize();
    protected abstract bool endOfGame();
    protected abstract void DisplayBoards();
    protected abstract void MakePlay();
    protected abstract void EndGame();
    protected void SwitchTurn()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % 2;
    }
}