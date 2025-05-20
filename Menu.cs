using System;
using System.Net.Http.Headers;
using static System.Console;

namespace a1;

public class Menu
{
    public void ShowMainMenu()
    {
        while (true)
        {
            string input;

            WriteLine("1 - Start A New Game");
            WriteLine("2 - Load Game");
            WriteLine("3 - Exit");

            Write("Enter choice >> ");

            //Validate User Input
            input = ReadLine() ?? ""; //check if it is null

            switch (input.Trim())
            {
                case "1":
                    StartNewGame();
                    break;
                case "2":
                    LoadGame();
                    break;
                case "3":
                    WriteLine("See you!");
                    return;
                default:
                    WriteLine("Invalid choice. Please Enter again.");
                    break;
            }
        }
    }

    public void StartNewGame()
    {
        bool isNumerical = false;
        int gameType = ChooseGameType();
        int boardSize;
        switch (gameType)
        {
            case 1:
                isNumerical = true;
                boardSize = ChooseBoardSize();
                break;
            case 2:
                boardSize = 3;
                break;
            case 3:
                boardSize = 15;
                break;
            default:
                boardSize = 3;
                break;
        }

        Player[] players = ChooseGameMode(boardSize, isNumerical);

        Game game = GameController.CreateGame(gameType, boardSize, players[0], players[1]);
        game.Initialize();
        game.playGame();
    }
    public int ChooseGameType()
    {
        WriteLine("\nSelect Game:");
        WriteLine("1 - Numerical Tic-Tac-Toe");
        WriteLine("2 - Notakto");
        WriteLine("3 - Gomoku");

        while (true)
        {
            Write("Enter choice (1–3) >> ");
            string input = ReadLine()?.Trim();
            if (input == "1" || input == "2" || input == "3")
                return int.Parse(input);
            WriteLine("Invalid input. Try again.");
        }
    }

    public Player[] ChooseGameMode(int boardSize, bool isNumerical)
    {
        WriteLine("\nSelect Mode:");
        WriteLine("1 - Human vs Human");
        WriteLine("2 - Human vs Computer");

        while (true)
        {
            Write("Enter choice (1–2) >> ");
            string input = ReadLine()?.Trim();
            switch (input)
            {
                case "1":
                    return new Player[]
                    {
                    new HumanPlayer("Player 1", isNumerical, true, boardSize),
                    new HumanPlayer("Player 2", isNumerical, false, boardSize)
                    };
                case "2":
                    return new Player[]
                    {
                        new HumanPlayer("Player", isNumerical, true, boardSize),
                        new ComputerPlayer("Computer", isNumerical, boardSize)
                    };
                default:
                    WriteLine("Invalid input. Try again.");
                    break;
            }
        }
    }

    public int ChooseBoardSize()
    {
        Write("\nEnter board size (>= 3) >> ");
        while (true)
        {
            string input = ReadLine()?.Trim();
            if (int.TryParse(input, out int size) && size >= 3)
                return size;
            Write("Invalid size. Enter a number >= 3 >> ");
        }
    }

    public void LoadGame()
    {
        Write("Enter save file path (default: save.json): ");
        string path = ReadLine()?.Trim() ?? "save.json";

        if (!File.Exists(path))
        {
            WriteLine("File not found.");
            return;
        }

        try
        {
            GameState state = Game.LoadGame(path);

            // Recreate players based on saved state
            Player player1 = new HumanPlayer(state.Player1Name, state.IsNumGame, true, state.BoardSize);

            Player player2 = state.Player2Type == "Human"
                ? new HumanPlayer(state.Player2Name, state.IsNumGame, false, state.BoardSize)
                : new ComputerPlayer(state.Player2Name, state.IsNumGame, state.BoardSize);

            // Create the appropriate game type
            Game game = GameController.CreateGame(state.GameType, state.BoardSize, player1, player2);

            // Restore available numbers
            game.Players[0].AvailableNumbers = state.Player1Numbers;
            game.Players[1].AvailableNumbers = state.Player2Numbers;

            // Restore the board state
            game.GameBoard.Grid = state.Grid;
            game.CurrentPlayerIndex = state.CurrentPlayerIndex;

            // Start playing the loaded game
            game.playGame();
        }
        catch (Exception ex)
        {
            WriteLine($"Failed to load game: {ex.Message}");
        }


        //create player
        //         case "1":
        //     return new Player[]
        //     {
        //     new HumanPlayer("Player 1", isNumerical, true, boardSize),
        //     new HumanPlayer("Player 2", isNumerical, false, boardSize)
        //     };
        // case "2":
        //     return new Player[]
        //     {
        //         new HumanPlayer("Player", isNumerical, true, boardSize),
        //         new ComputerPlayer("Computer", isNumerical, boardSize)
        //     };
        //Game game = GameController.CreateGame(gameType, boardSize, players[0], players[1]);

        //Read the save file and turn into GameState object
        // string gameText = File.ReadAllText(path);
        // GameState state = JsonSerializer.Deserialize<GameState>(gameText);
        // if (state == null)
        // {
        //     WriteLine("Failed to load.");
        //     return;
        // }
        // //Recreating the board
        // Board board = new Board(state.BoardSize);
        // for (int i = 0; i < state.BoardSize; i++)
        // {
        //     for (int j = 0; j < state.BoardSize; j++)
        //     {
        //         board.PlaceNumber(i, j, state.Grid[i][j]);
        //     }
        // }

        // Player player1 = new Player("Player 1 (Odd)", PlayerType.Human, true, state.BoardSize * state.BoardSize)
        // {
        //     AvailableNumbers = state.Player1Numbers
        // };
        // Player player2 = new Player(state.Player2Name, state.Player2Type, false, state.BoardSize * state.BoardSize)
        // {
        //     AvailableNumbers = state.Player2Numbers
        // };

        // Game game = new Game(board, player1, player2, state.CurrentPlayerIndex);
        // game.Play();
    }
}
