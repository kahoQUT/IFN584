using System;
using static System.Console;
namespace a1;

public abstract class Board
{
    public abstract List<(int, int)> GetEmptyCells();

    public abstract bool PlaceMove(int row, int col, Player player, int? value = null);

    public abstract void Display();

    public abstract bool CheckWin(Player player);

    public abstract bool ResetNumber(int row, int col); // Only for numerical games
}

public class NumericalBoard : Board
{
    public int Size { get; }
    public int[,] Grid { get; set; }
    public int WinNum
    {
        get { return Size * (Size * Size + 1) / 2; }
    }

    //Constructor for new Board
    public NumericalBoard(int size)
    {
        Size = size;
        Grid = new int[size, size];
    }

    public override List<(int, int)> GetEmptyCells()
    {
        List<(int, int)> empty = new();
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                if (Grid[i, j] == 0)
                    empty.Add((i, j));
        return empty;
    }

    public string AddSpacesAround(string input)
    {
        return $" {input} ";
    }

    //Visualise the board
    public override void Display()
    {
        WriteLine();
        Write("    ");
        for (int i = 0; i < Size; i++)
        {
            Write($" C{i + 1} ");
        }
        WriteLine();
        for (int i = 0; i < Size; i++)
        {
            Write($"R{i + 1} ");
            for (int j = 0; j < Size; j++)
            {
                string cell = Grid[i, j] == 0 ? " . " : AddSpacesAround(Grid[i, j].ToString());
                Write($"|{cell}");
            }
            WriteLine("|");
        }
        WriteLine();
    }

    public bool IsCellEmpty(int row, int col)
    {
        return Grid[row, col] == 0;
    }

    //Place a number in cell
    public override bool PlaceMove(int row, int col, Player player, int? value = null)
    {
        if (!IsCellEmpty(row, col) || value == null) return false;
        Grid[row, col] = value.Value;
        return true;
    }

    //Reset 0 in cell
    public override bool ResetNumber(int row, int col)
    {
        Grid[row, col] = 0;
        return true;
    }

    // //Check the Board is it full with numbers
    // public bool IsFull()
    // {
    //     for (int i = 0; i < Size; i++)
    //     {
    //         for (int j = 0; j < Size; j++)
    //         {
    //             if (Grid[i, j] == 0)
    //                 return false;
    //         }
    //     }
    //     return true;
    // }

    public int[,] GetGrid()
    {
        return Grid;
    }

    //Check the rows, cols, diagonal
    public override bool CheckWin(Player player)
    {
        // Check rows
        for (int i = 0; i < Size; i++)
        {
            int sum = 0;
            bool rowFull = true;

            for (int j = 0; j < Size; j++)
            {
                int value = Grid[i, j];
                if (value == 0)
                {
                    rowFull = false;
                    break;
                }
                sum += value;
            }

            if (rowFull && sum == WinNum)
                return true;
        }

        // Check columns
        for (int j = 0; j < Size; j++)
        {
            int sum = 0;
            bool colFull = true;

            for (int i = 0; i < Size; i++)
            {
                int value = Grid[i, j];
                if (value == 0)
                {
                    colFull = false;
                    break;
                }
                sum += value;
            }

            if (colFull && sum == WinNum)
                return true;
        }

        // Check main diagonal
        int diag1Sum = 0;
        bool diag1Full = true;
        for (int i = 0; i < Size; i++)
        {
            int value = Grid[i, i];
            if (value == 0)
            {
                diag1Full = false;
                break;
            }
            diag1Sum += value;
        }

        if (diag1Full && diag1Sum == WinNum)
            return true;

        // Check anti-diagonal
        int diag2Sum = 0;
        bool diag2Full = true;
        for (int i = 0; i < Size; i++)
        {
            int value = Grid[i, Size - 1 - i];
            if (value == 0)
            {
                diag2Full = false;
                break;
            }
            diag2Sum += value;
        }

        if (diag2Full && diag2Sum == WinNum)
            return true;

        return false;
    }

}

public class NotaktoBoard : Board
{
    public int[,] Grid { get; set; }
    public int NumOfRow = 9;
    public int NumOfCol = 3;
    public int piece = 1;

    public NotaktoBoard()
    {
        // first 3 row is board 1, middle 3 row is board 2, last 3 row is board 3
        Grid = new int[NumOfRow, NumOfCol];
    }

    public override List<(int, int)> GetEmptyCells()
    {
        var empty = new List<(int, int)>();
        for (int b = 0; b < 3; b++)
        {
            int offset = b * 3;
            if (BoardIsDead(offset)) continue;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < NumOfCol; j++)
                    if (Grid[offset + i, j] == 0)
                        empty.Add((offset + i, j));
        }
        return empty;
    }

    public string AddSpacesAround(string input)
    {
        return $" {input} ";
    }

    //Visualise the board
    public override void Display()
    {
        WriteLine();
        Write("    ");
        for (int i = 0; i < NumOfCol; i++)
        {
            Write($" C{i + 1} ");
        }
        WriteLine();
        for (int i = 0; i < NumOfRow; i++)
        {
            Write($"R{i + 1} ");
            for (int j = 0; j < NumOfCol; j++)
            {
                string cell = Grid[i, j] == 0 ? " . " : AddSpacesAround(Grid[i, j].ToString());
                Write($"|{cell}");
            }
            WriteLine("|");
            if ((i+1)%NumOfCol == 0)  WriteLine();
        }
        WriteLine();
    }

    public bool IsCellEmpty(int row, int col)
    {
        return Grid[row, col] == 0;
    }

    //Place a X in cell
    public override bool PlaceMove(int row, int col, Player player, int? value = null)
    {
        int boardIndex = row / 3;
        int offset = boardIndex * 3;
        if (BoardIsDead(offset)) return false;   // cannot play in a dead board
        if (!IsCellEmpty(row, col)) return false;
        Grid[row, col] = piece;
        return true;
    }

    //Reset 0 in cell
    public override bool ResetNumber(int row, int col)
    {
        Grid[row, col] = 0;
        return true;
    }

    public int[,] GetGrid()
    {
        return Grid;
    }

    private bool BoardIsDead(int rowOffset)
    {
        // rows
        for (int i = 0; i < 3; i++)
            if (Grid[rowOffset + i, 0] != 0 &&
                Grid[rowOffset + i, 1] != 0 &&
                Grid[rowOffset + i, 2] != 0)
                return true;
        // cols
        for (int j = 0; j < 3; j++)
            if (Grid[rowOffset + 0, j] != 0 &&
                Grid[rowOffset + 1, j] != 0 &&
                Grid[rowOffset + 2, j] != 0)
                return true;
        // diags
        if (Grid[rowOffset + 0, 0] != 0 &&
            Grid[rowOffset + 1, 1] != 0 &&
            Grid[rowOffset + 2, 2] != 0)
            return true;
        if (Grid[rowOffset + 0, 2] != 0 &&
            Grid[rowOffset + 1, 1] != 0 &&
            Grid[rowOffset + 2, 0] != 0)
            return true;
        return false;
    }

    // todo remove player param
    public override bool CheckWin(Player player)
    {
        int deadCount = 0;
        for (int b = 0; b < 3; b++)
            if (BoardIsDead(b * 3)) deadCount++;
        return deadCount == 3;
    }
    

}