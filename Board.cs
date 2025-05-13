using System;
using static System.Console;
namespace a1;

public class Board
{
    public int Size{ get; }
    public int[,] Grid { get; set; }
    public Dictionary<(int, int), int> Moves { get; set; }
    public int WinNum
    {
        get { return Size * (Size * Size + 1) / 2; }
    }

    //Constructor for new Board
    public Board(int size)
    {
        Size = size;
        Moves = new Dictionary<(int, int), int>();
        Grid = new int[size, size];
    }

    public string AddSpacesAround(string input){
        return $" {input} ";
    }

    //Visualise the board
    public void Display()
    {
        WriteLine();
        Write("    ");
        for (int i = 0; i < Size; i++)
        {
            Write($" C{i+1} ");
        }
        WriteLine();
        for (int i = 0; i < Size; i++)
        {
            Write($"R{i+1} ");
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
    public bool PlaceNumber(int row, int col, int number)
    {
        if (IsCellEmpty(row, col))
        {
            Grid[row, col] = number;
            Moves[(row, col)] = number;
            return true;
        }
        return false;
    }
    
    //Reset 0 in cell
    public bool ResetNumber(int row, int col)
    {
        Grid[row, col] = 0;
        Moves[(row, col)] = 0;
        return true;
    }

    //Check the Board is it full with numbers
    public bool IsFull()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Grid[i, j] == 0)
                    return false;
            }
        }
        return true;
    }

    //Check the rows, cols, diagonal
    public bool CheckWin()
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