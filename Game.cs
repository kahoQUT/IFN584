using System;
using static System.Console;
using System.Text.Json;

namespace a1;

public abstract class Game
{
    protected Board[] Board;
    protected Player[] Players;
    protected int CurrentPlayerIndex;
    public Game(int boardSize, Player player1, Player player2)
    {
        Players = new[] { player1, player2 };
        CurrentPlayerIndex = 0;
    }

    public Game(int boardSize, Player player1, Player player2, GameState state)
    {
        Players = new[] { player1, player2 };
        CurrentPlayerIndex = state.CurrentPlayerIndex;
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
public class NumTicTacToeGame : Game
{
    public NumTicTacToeGame(int boardSize, Player p1, Player p2) : base(boardSize, p1, p2) { }
    public NumTicTacToeGame(int boardSize, Player p1, Player p2, GameState state) : base(boardSize, p1, p2, state) { }
    protected override void Initialize()
    { }
    protected override bool endOfGame()
    {
        return true;
    }
    protected override void MakePlay()
    {}
    protected override void DisplayBoards()
    {}
    protected override void EndGame()
    {}
}
public class GomokuGame : Game
{
    public GomokuGame(int boardSize, Player p1, Player p2) : base(boardSize, p1, p2) { }
    public GomokuGame(int boardSize, Player p1, Player p2, GameState state) : base(boardSize, p1, p2, state) { }
    protected override void Initialize()
    { }
    protected override bool endOfGame()
    {
        return true;
    }
    protected override void MakePlay()
    {}
    protected override void DisplayBoards()
    {}
    protected override void EndGame()
    {}
}
// public class Game
// {
//     private Board Board;
//     private Player Player1;
//     private Player Player2;
//     private int CurrentPlayerIndex;
//     private Player[] Players;

//     public static void StartNewGame()
//     {
//         int size;
//         string mode;

//         // Board Size
//         Write("Enter board size (>= 3) >> ");
//         while (!int.TryParse(ReadLine(), out size) || size < 3)
//         {
//             WriteLine("Invalid board size. Please Enter again.");
//             StartNewGame();
//         }

//         //Choose Mode
//         WriteLine("Choose mode:");
//         WriteLine("1. Human vs Human");
//         WriteLine("2. Human vs Computer");

//         mode = ReadLine() ?? ""; //check if it is null
//         mode = mode.Trim();

//         while (mode != "1" && mode != "2")
//         {
//             WriteLine("Invalid mode. Please Enter again.");
//             mode = ReadLine() ?? ""; //check if it is null
//             mode = mode.Trim();
//         }

//         //Setting up the board
//         Board board = new Board(size);
//         int maxNumber = size * size;

//         Player player1 = new Player("Player 1 (Odd)", PlayerType.Human, true, maxNumber);
//         Player player2 = mode == "1"
//             ? new Player("Player 2 (Even)", PlayerType.Human, false, maxNumber)
//             : new Player("Computer (Even)", PlayerType.Computer, false, maxNumber);

//         Game game = new Game(board, player1, player2);
//         game.Play();
//     }

//     public static void LoadGame()
//     {
//         Write("Enter save file path: ");
//         string path = ReadLine() ?? ""; //check if it is null

//         if (!File.Exists(path))
//         {
//             WriteLine("File not found.");
//             return;
//         }

//         //Read the save file and turn into GameState object
//         string gameText = File.ReadAllText(path);
//         GameState state = JsonSerializer.Deserialize<GameState>(gameText);
//         if (state == null)
//         {
//             WriteLine("Failed to load.");
//             return;
//         }
//         //Recreating the board
//         Board board = new Board(state.BoardSize);
//         for (int i = 0; i < state.BoardSize; i++)
//         {
//             for (int j = 0; j < state.BoardSize; j++)
//             {
//                 board.PlaceNumber(i, j, state.Grid[i][j]);
//             }
//         }

//         Player player1 = new Player("Player 1 (Odd)", PlayerType.Human, true, state.BoardSize * state.BoardSize)
//         {
//             AvailableNumbers = state.Player1Numbers
//         };
//         Player player2 = new Player(state.Player2Name, state.Player2Type, false, state.BoardSize * state.BoardSize)
//         {
//             AvailableNumbers = state.Player2Numbers
//         };

//         Game game = new Game(board, player1, player2, state.CurrentPlayerIndex);
//         game.Play();
//     }

//     //Constructor for new board
//     private Game(Board board, Player player1, Player player2, int currentPlayer = 0)
//     {
//         Board = board;
//         Player1 = player1;
//         Player2 = player2;
//         CurrentPlayerIndex = currentPlayer;
//         Players = new[] { Player1, Player2 };
//     }

//     private void Play()
//     {
//         while (true)
//         {
//             Board.Display();
//             Player currentPlayer = Players[CurrentPlayerIndex];
//             Player opponentPlayer = Players[(CurrentPlayerIndex + 1) % 2];
//             WriteLine($"{currentPlayer.Name}'s turn.");
//             WriteLine($"Your available numbers: {string.Join(", ", currentPlayer.AvailableNumbers)}");
//             WriteLine($"Opponent available numbers: {string.Join(", ", opponentPlayer.AvailableNumbers)}");

//             //Make Computer move or Get Player move
//             if (currentPlayer.Type == PlayerType.Computer)
//             {
//                 MakeComputerMove(currentPlayer);
//             }
//             else
//             {
//                 string status = GetPlayerMove(currentPlayer);
//                 if (status == "continue")
//                 {
//                     continue;
//                 }
//                 else if(status == "exit")
//                 {
//                     WriteLine("Goodbye! See ya!");
//                     break;
//                 }
//             }

//             if (Board.CheckWin()) //check any winners
//             {
//                 Board.Display();
//                 WriteLine($"{currentPlayer.Name} wins!");
//                 break;
//             }

//             if (Board.IsFull()) //check any empty cells
//             {
//                 Board.Display();
//                 WriteLine("It's a tie!");
//                 break;
//             }

//             CurrentPlayerIndex = (CurrentPlayerIndex + 1) % 2;
//         }
//     }

//     private string GetPlayerMove(Player player)
//     {
//         WriteLine("Enter move by 'row col number', 'save', 'help' or 'exit':");
//         string input = ReadLine() ?? ""; //check if it is null
//         input = input.Trim().ToLower();
//         if (input == "save")
//         {
//             SaveGame();
//             return "exit";
//         }
//         else if (input == "help")
//         {
//             ShowHelp();
//             return "continue";
//         }
//         else if (input == "exit")
//         {
//             return "exit";
//         }

//         //Seperate the row, col and number
//         string[] move = input.Split(' ');
//         if (move.Length != 3 ||
//             !int.TryParse(move[0], out int row) ||
//             !int.TryParse(move[1], out int col) ||
//             !int.TryParse(move[2], out int number))
//         {
//             WriteLine("Invalid input format. Please Enter again.");
//             return "continue";
//         }

//         row--; col--;

//         if (row < 0 || row >= Board.Size || col < 0 || col >= Board.Size)
//         {
//             WriteLine("Out of range.");
//             return "continue";
//         }

//         if (!player.HasNumber(number))
//         {
//             WriteLine("You don't have that number.");
//             return "continue";
//         }

//         if (!Board.PlaceNumber(row, col, number))
//         {
//             WriteLine("Cell is already taken.");
//             return "continue";
//         }

//         player.UseNumber(number);
//         return "next";
//     }

//     private void MakeComputerMove(Player computer)
//     {
//         List<Tuple<int, int>> emptyCells = GetEmptyCells();

//         if (emptyCells == null || emptyCells.Count == 0)
//         {
//             WriteLine("Computer has no places left to play.");
//             return;
//         }
//         if (computer.AvailableNumbers.Count == 0)
//         {
//             WriteLine("Computer has no numbers left to play.");
//             return;
//         }

//         //winning move
//         foreach ( var (r, c) in emptyCells)
//         {
//             foreach (int num in computer.AvailableNumbers)
//             {
//                 Board.PlaceNumber(r, c, num);
//                 if (Board.CheckWin())
//                 {
//                     computer.UseNumber(num);
//                     WriteLine($"Computer places {num} at ({r + 1},{c + 1})");
//                     return;
//                 }
//                 Board.ResetNumber(r, c);
//             }
//         }
//         //random move
//         Random renGenerator = new Random();
//         var (row, col) = emptyCells[renGenerator.Next(emptyCells.Count)];
//         int number = computer.ChooseRandomNumber();
//         Board.PlaceNumber(row, col, number);
//         computer.UseNumber(number);
//         WriteLine($"Computer randomly places {number} at ({row + 1},{col + 1})");
//     }

//     //Get a list of emptycells
//     private List<Tuple<int, int>> GetEmptyCells()
//     {
//         List<Tuple<int, int>> empty = new List<Tuple<int, int>>();
//         for (int i = 0; i < Board.Size; i++)
//             for (int j = 0; j < Board.Size; j++)
//                 if (Board.IsCellEmpty(i, j))
//                     empty.Add(new Tuple<int, int>(i, j));
//         return empty;
//     }

//     //Saving the game status
//     private void SaveGame()
//     {
//         Write("Enter filename to save: ");
//         string filename = ReadLine()?.Trim();

//         if (string.IsNullOrEmpty(filename))
//         {
//             WriteLine("No filename provided.");
//             return;
//         }

//         GameState state = new GameState
//         {
//             BoardSize = Board.Size,
//             Grid = ConvertToJaggedArray(Board.Grid),
//             Player1Numbers = Player1.AvailableNumbers,
//             Player2Numbers = Player2.AvailableNumbers,
//             Player2Name = Player2.Name,
//             Player2Type = Player2.Type,
//             CurrentPlayerIndex = CurrentPlayerIndex
//         };

//         FileStream outFile = new FileStream(filename, FileMode.Create, FileAccess.Write);

//         StreamWriter writer = new StreamWriter(outFile);

//         writer.WriteLine(JsonSerializer.Serialize(state));

//         WriteLine("Game saved to "+filename+"");

//         writer.Close();
//         outFile.Close();
//     }

//     //Help menu
//     private void ShowHelp()
//     {
//         const string FILENAME = "HelpMenu.txt";
//         string readText = File.ReadAllText(FILENAME);
//         WriteLine(readText);
//     }

//     private int[][] ConvertToJaggedArray(int[,] grid)
//     {
//         int[][] result = new int[grid.GetLength(0)][];
//         for (int i = 0; i < grid.GetLength(0); i++)
//         {
//             result[i] = new int[grid.GetLength(1)];
//             for (int j = 0; j < grid.GetLength(1); j++)
//                 result[i][j] = grid[i, j];
//         }
//         return result;
//     }
// }
