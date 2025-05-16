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