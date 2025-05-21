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
            //default board size for notakto game
                boardSize = 3;
                break;
            case 3:
            //default board size for gomoku game
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

        char? symbol1 = isNumerical ? null : 'X';
        char? symbol2 = isNumerical ? null : 'O';

        while (true)
        {
            Write("Enter choice (1–2) >> ");
            string input = ReadLine()?.Trim();
            switch (input)
            {
                case "1":
                //Creating Two Human Players
                    return new Player[]
                    {
                        new HumanPlayer("Player 1", isNumerical, true, boardSize, symbol1),
                        new HumanPlayer("Player 2", isNumerical, false, boardSize, symbol2)
                    };
                case "2":
                //Creating Human Player and Computer Player
                    return new Player[]
                    {
                        new HumanPlayer("Player", isNumerical, true, boardSize, symbol1),
                        new ComputerPlayer("Computer", isNumerical, boardSize, symbol2)
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
            
            char? symbol1 = state.IsNumGame ? null : 'X';
            char? symbol2 = state.IsNumGame ? null : 'O';

            // Recreate players based on saved state
            Player player1 = new HumanPlayer(state.Player1Name, state.IsNumGame, true, state.BoardSize, symbol1);

            Player player2 = state.Player2Type == "Human"
                ? new HumanPlayer(state.Player2Name, state.IsNumGame, false, state.BoardSize, symbol1)
                : new ComputerPlayer(state.Player2Name, state.IsNumGame, state.BoardSize, symbol2);

            // Create the appropriate game type

            Game game = GameController.CreateGame(state.GameType, state.BoardSize, player1, player2);
            game.Initialize();

            // Restore available numbers
            game.Players[0].AvailableNumbers = state.Player1Numbers;
            game.Players[1].AvailableNumbers = state.Player2Numbers;

            // Restore the board state
            game.Board.Grid = state.Grid2D;
            game.CurrentPlayerIndex = state.CurrentPlayerIndex;

            // Start playing the loaded game
            game.playGame();
        }
        catch (Exception ex)
        {
            WriteLine($"Failed to load game: {ex.Message}");
        }
    }
}
