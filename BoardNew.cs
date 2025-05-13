// using System;
// using static System.Console;
// namespace a1;

// public abstract class Board
// {
//     public int Size { get; protected set; }

//     // 2D board grid (you can replace with game-specific representation)
//     protected int[,] Grid;

//     protected Board(int size)
//     {
//         Size = size;
//         Grid = new int[size, size];
//     }

//     // Return a list of empty cell coordinates
//     public virtual List<(int, int)> GetEmptyCells()
//     {
//         List<(int, int)> emptyCells = new();

//         for (int i = 0; i < Size; i++)
//         {
//             for (int j = 0; j < Size; j++)
//             {
//                 if (Grid[i, j] == 0) // assuming 0 means empty
//                 {
//                     emptyCells.Add((i, j));
//                 }
//             }
//         }

//         return emptyCells;
//     }

//     public abstract void Display();

//     public abstract bool IsCellEmpty(int row, int col);

//     public abstract void PlaceMove(int row, int col, Player player, int? value = null);

//     public abstract bool CheckWin(Player player);
// }

// public class NumericalBoard :Board
// {
//     public int Size{ get; }
//     public int[,] Grid { get; set; }
//     public Dictionary<(int, int), int> Moves { get; set; }
//     public int WinNum
//     {
//         get { return Size * (Size * Size + 1) / 2; }
//     }

//     //Constructor for new Board
//     public Board(int size)
//     {
//         Size = size;
//         Moves = new Dictionary<(int, int), int>();
//         Grid = new int[size, size];
//     }

//     public string AddSpacesAround(string input){
//         return $" {input} ";
//     }

//     //Visualise the board
//     public override void Display()
//     {
//         WriteLine();
//         Write("    ");
//         for (int i = 0; i < Size; i++)
//         {
//             Write($" C{i+1} ");
//         }
//         WriteLine();
//         for (int i = 0; i < Size; i++)
//         {
//             Write($"R{i+1} ");
//             for (int j = 0; j < Size; j++)
//             {
//                 string cell = Grid[i, j] == 0 ? " . " : AddSpacesAround(Grid[i, j].ToString());
//                 Write($"|{cell}");
//             }
//             WriteLine("|");
//         }
//         WriteLine();
//     }

//     public override bool IsCellEmpty(int row, int col)
//     {
//         return Grid[row, col] == 0;
//     }

//     //Place a number in cell
//     public override void PlaceMove(int row, int col, Player player, int? number = null)
//     {
//         if (IsCellEmpty(row, col))
//         {
//             Grid[row, col] = number;
//             Moves[(row, col)] = number;
//             return true;
//         }
//         return false;
//     }
    
//     //Reset 0 in cell
//     public bool ResetNumber(int row, int col)
//     {
//         Grid[row, col] = 0;
//         Moves[(row, col)] = 0;
//         return true;
//     }

//     //Check the Board is it full with numbers
//     public bool IsFull()
//     {
//         for (int i = 0; i < Size; i++)
//         {
//             for (int j = 0; j < Size; j++)
//             {
//                 if (Grid[i, j] == 0)
//                     return false;
//             }
//         }
//         return true;
//     }

//     //Check the rows, cols, diagonal
//     public override bool CheckWin(Player player)
//     {
//         // Check rows
//         for (int i = 0; i < Size; i++)
//         {
//             int sum = 0;
//             bool rowFull = true;

//             for (int j = 0; j < Size; j++)
//             {
//                 int value = Grid[i, j];
//                 if (value == 0)
//                 {
//                     rowFull = false;
//                     break;
//                 }
//                 sum += value;
//             }

//             if (rowFull && sum == WinNum)
//                 return true;
//         }

//         // Check columns
//         for (int j = 0; j < Size; j++)
//         {
//             int sum = 0;
//             bool colFull = true;

//             for (int i = 0; i < Size; i++)
//             {
//                 int value = Grid[i, j];
//                 if (value == 0)
//                 {
//                     colFull = false;
//                     break;
//                 }
//                 sum += value;
//             }

//             if (colFull && sum == WinNum)
//                 return true;
//         }

//         // Check main diagonal
//         int diag1Sum = 0;
//         bool diag1Full = true;
//         for (int i = 0; i < Size; i++)
//         {
//             int value = Grid[i, i];
//             if (value == 0)
//             {
//                 diag1Full = false;
//                 break;
//             }
//             diag1Sum += value;
//         }

//         if (diag1Full && diag1Sum == WinNum)
//             return true;

//         // Check anti-diagonal
//         int diag2Sum = 0;
//         bool diag2Full = true;
//         for (int i = 0; i < Size; i++)
//         {
//             int value = Grid[i, Size - 1 - i];
//             if (value == 0)
//             {
//                 diag2Full = false;
//                 break;
//             }
//             diag2Sum += value;
//         }

//         if (diag2Full && diag2Sum == WinNum)
//             return true;

//         return false;
//     }

// }
// public class GomokuBoard : Board
// {
//     private const int WinCount = 5;

//     public GomokuBoard(int size = 15) : base(size)
//     {
//     }

//     public override void Display()
//     {
//         Console.WriteLine();
//         Console.Write("   ");
//         for (int col = 0; col < Size; col++)
//             Console.Write($"{col + 1,3}");
//         Console.WriteLine();

//         for (int row = 0; row < Size; row++)
//         {
//             Console.Write($"{row + 1,3}");
//             for (int col = 0; col < Size; col++)
//             {
//                 string cell = Grid[row, col] switch
//                 {
//                     0 => " . ",
//                     1 => " X ",
//                     2 => " O ",
//                     _ => " ? "
//                 };
//                 Console.Write(cell);
//             }
//             Console.WriteLine();
//         }
//         Console.WriteLine();
//     }

//     public override bool IsCellEmpty(int row, int col)
//     {
//         return Grid[row, col] == 0;
//     }

//     public override void PlaceMove(int row, int col, Player player, int? value = null)
//     {
//         if (!IsCellEmpty(row, col))
//         {
//             Console.WriteLine("Cell already occupied.");
//             return;
//         }

//         int piece = player.Name == "Player 1" ? 1 : 2;
//         Grid[row, col] = piece;
//     }

//     public override bool CheckWin(Player player)
//     {
//         int piece = player.Name == "Player 1" ? 1 : 2;

//         for (int i = 0; i < Size; i++)
//         {
//             for (int j = 0; j < Size; j++)
//             {
//                 if (Grid[i, j] != piece) continue;

//                 // Check 4 directions: →, ↓, ↘, ↙
//                 if (CheckDirection(i, j, 0, 1, piece) ||  // Horizontal
//                     CheckDirection(i, j, 1, 0, piece) ||  // Vertical
//                     CheckDirection(i, j, 1, 1, piece) ||  // Diagonal ↘
//                     CheckDirection(i, j, 1, -1, piece))   // Diagonal ↙
//                 {
//                     return true;
//                 }
//             }
//         }
//         return false;
//     }

//     private bool CheckDirection(int row, int col, int deltaRow, int deltaCol, int piece)
//     {
//         int count = 0;

//         for (int k = 0; k < WinCount; k++)
//         {
//             int r = row + deltaRow * k;
//             int c = col + deltaCol * k;

//             if (r < 0 || r >= Size || c < 0 || c >= Size)
//                 return false;

//             if (Grid[r, c] == piece)
//                 count++;
//             else
//                 break;
//         }

//         return count == WinCount;
//     }
// }
