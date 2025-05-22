using System;
using static System.Console;
using System.Text.Json;

namespace a1;

public abstract class Game
{
    public Board Board;
    public Player[] Players;
    protected int BoardSize;
    public int CurrentPlayerIndex;
    // for undo
    protected Stack<GameState> GameStateHistory = new Stack<GameState>();
    // for redo
    protected Stack<GameState> RedoStack = new Stack<GameState>();

    public Game(int boardSize, Player player1, Player player2)
    {
        Players = new Player[] { player1, player2 };
        CurrentPlayerIndex = 0;
        BoardSize = boardSize;
    }

    //Template method outling the steps
    public void playGame()
    {
        while (!EndOfGame())
        {
            DisplayBoards();
            MakePlay();
            if (EndOfGame()) break;
            SwitchTurn();
            SaveState();
        }

        EndGame();
    }

    public abstract void Initialize();
    protected abstract bool EndOfGame();
    public abstract void DisplayBoards();
    protected abstract void MakePlay();
    protected abstract void EndGame();
    public abstract void DisplayHelpMenu();

    protected void SaveState()
    {
        var state = new GameState
        {
            Grid2D = (int[,])Board.Grid.Clone(),
            CurrentPlayerIndex = CurrentPlayerIndex
        };
        GameStateHistory.Push(state);
        RedoStack.Clear();
    }
    public void Undo()
    {
        // Not allow undo at the start of the game
        if (GameStateHistory.Count > 1)
        {
            // undo opponent move & player's previous move
            RedoStack.Push(GameStateHistory.Pop());
            RedoStack.Push(GameStateHistory.Pop());

            // in case back to start of game
            if (GameStateHistory.Count == 0)
            {
                // reset board
                if (Board.Grid != null)
                {
                    for (int i = 0; i < Board.Grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < Board.Grid.GetLength(1); j++)
                        {
                            Board.Grid[i, j] = 0;
                        }
                    }
                }
                CurrentPlayerIndex = 0;
            }
            else
            {
                // restore GameState back to 2 moves (opponent & player) ago
                var previousState = GameStateHistory.Peek();
                Board.Grid = (int[,])previousState.Grid2D.Clone();
                CurrentPlayerIndex = previousState.CurrentPlayerIndex;
            }
            WriteLine("Undo Successful");
        }
        else
        {
            WriteLine("Nothing to undo");
        }
    }

    public void Redo()
    {
        if (RedoStack.Count > 1)
        {
            GameStateHistory.Push(RedoStack.Pop());
            GameStateHistory.Push(RedoStack.Pop());
            var currentState = GameStateHistory.Peek();

            // Apply the redo state
            Board.Grid = (int[,])currentState.Grid2D.Clone();
            CurrentPlayerIndex = currentState.CurrentPlayerIndex;

            WriteLine("Redo Successful");
        }
        else
        {
            WriteLine("Nothing to redo");
        }
    }
    public void SaveGame(string fileName)
    {
        // Convert 2D array to jagged array to be serialized into save file
        int[][] gridAsJagged = new int[Board.Grid.GetLength(0)][];
        for (int i = 0; i < Board.Grid.GetLength(0); i++)
        {
            gridAsJagged[i] = new int[Board.Grid.GetLength(1)];
            for (int j = 0; j < Board.Grid.GetLength(1); j++)
            {
                gridAsJagged[i][j] = Board.Grid[i, j];
            }
        }

        var state = new GameState
        {
            GameType = this is NumTicTacToeGame ? 1 :
                        this is NotaktoGame ? 2 : 3,
            BoardSize = BoardSize,
            Grid = gridAsJagged,
            Player1Name = Players[0].Name,
            Player1Type = Players[0] is HumanPlayer ? "Human" : "Computer",
            Player1Numbers = new List<int>(Players[0].AvailableNumbers),
            Player2Name = Players[1].Name,
            Player2Type = Players[1] is HumanPlayer ? "Human" : "Computer",
            Player2Numbers = new List<int>(Players[1].AvailableNumbers),
            CurrentPlayerIndex = CurrentPlayerIndex,
            IsNumGame = Players[0].IsNumGame
        };

        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(state, options);
        File.WriteAllText(fileName, jsonString);
        WriteLine($"Game saved to {fileName}");
    }

    public static GameState LoadGame(string fileName)
    {
        // create GameState from json file
        string jsonString = File.ReadAllText(fileName);
        var state = JsonSerializer.Deserialize<GameState>(jsonString);
        // Convert jagged array back to 2D array
        int[,] grid = new int[state.Grid.Length, state.Grid[0].Length];
        for (int i = 0; i < state.Grid.Length; i++)
        {
            for (int j = 0; j < state.Grid[i].Length; j++)
            {
                grid[i, j] = state.Grid[i][j];
            }
        }
        state.Grid2D = grid;

        return state;
    }

    protected void SwitchTurn()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % 2;
    }
}