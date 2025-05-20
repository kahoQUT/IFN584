using System;
using static System.Console;

namespace a1;

public class GomokuBoard: Board
{
    public int Size { get; }

    public GomokuBoard(int size)
    {
        Size = size;
        Grid = new int[size, size];
    }

    public override List<(int, int)> GetEmptyCells()
    {
        List<(int, int)> empty = new();
        return empty;
    }

    public override void Display()
    {
    }

    public bool IsCellEmpty(int row, int col)
    {
        return Grid[row, col] == 0;
    }

    public override bool PlaceMove(int row, int col, Player player, int? value = null)
    {
        return true;
    }

    //Reset 0 in cell
    public override bool ResetNumber(int row, int col)
    {
        Grid[row, col] = 0;
        return true;
    }

    public override bool CheckWin(Player player)
    {
        return false;
    }
}
