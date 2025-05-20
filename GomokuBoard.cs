using System;
using static System.Console;
namespace a1;

public class GomokuBoard : Board
{
    private int[,] Grid;
    private int Size = 15;
    private const int WINCOUNT = 5;

    public GomokuBoard()
    {
        Grid = new int[15, 15];
    }

    public bool IsCellEmpty(int row, int col)
    {
        return Grid[row, col] == 0;
    }

    public override bool PlaceMove(int row, int col, Player player, int? value = null)
    {
        Console.WriteLine($"[DEBUG] Grid[{row},{col}] = {Grid[row, col]}");

        if (!IsCellEmpty(row, col) || player.Symbol == null)
            return false;

        Grid[row, col] = player.Symbol == 'X' ? 1 : 2;
        return true;
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

    public override void Display()
    {
        WriteLine();
        Write("    ");
        for (int i = 0; i < Size; i++)
        {
            Write(" "+(i + 1).ToString("D2")+" ");
        }
        WriteLine();
        for (int i = 0; i < Size; i++)
        {
            Write("R" + (i + 1).ToString("D2") + " ");
            for (int j = 0; j < Size; j++)
            {
                switch(Grid[i,j])
                {
                    case 1:
                        Write("| X ");
                        break;
                    case 2:
                        Write("| O ");
                        break;
                    default:
                        string cell = Grid[i, j] == 0 ? " . " : AddSpacesAround(Grid[i, j].ToString());
                        Write($"|{cell}");
                        break;
                }
            }
            WriteLine();
        }
    }

    public override bool CheckWin(Player player)
    {
        int target = 0;
        if(player.Symbol == 'X') target = 1;
        if(player.Symbol == 'O') target = 2;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Grid[i, j] != target) continue;

                if (CheckDirection(i, j, 0, 1, target) || // horizontal
                    CheckDirection(i, j, 1, 0, target) || // vertical
                    CheckDirection(i, j, 1, 1, target) || // diagonal down-right
                    CheckDirection(i, j, 1, -1, target))  // diagonal down-left
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CheckDirection(int row, int col, int dr, int dc, int target)
    {
        for (int i = 0; i < WINCOUNT; i++)
        {
            int r = row + dr * i;
            int c = col + dc * i;

            if (r < 0 || r >= Size || c < 0 || c >= Size || Grid[r, c] != target)
                return false;
        }
        return true;
    }

    public override bool ResetNumber(int row, int col)
    {
        Grid[row, col] = 0;
        return true;
    }
}
