using System;
using static System.Console;
namespace a1;

public class GomokuBoard : Board
{
    //Constant of board size for gomokuBoard
    private const int SIZE = 15;
    //Constant of winning count in a row
    private const int WINCOUNT = 5;

    public GomokuBoard()
    {
        Grid = new int[SIZE, SIZE];
    }

    public bool IsCellEmpty(int row, int col)
    {
        return Grid[row, col] == 0;
    }

    public override bool PlaceMove(int row, int col, Player player, int? value = null)
    {
        if (!IsCellEmpty(row, col) || player.Symbol == null)
            return false;

        Grid[row, col] = player.Symbol == 'X' ? 1 : 2;
        return true;
    }

    //Get Empty Cell from the existing Grid
    public override List<(int, int)> GetEmptyCells()
    {
        List<(int, int)> empty = new();
        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
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
        //Displaying Column number
        for (int i = 0; i < SIZE; i++)
        {
            Write(" " + (i + 1).ToString("D2") + " ");
        }
        WriteLine();
        //Displaying Row number
        for (int i = 0; i < SIZE; i++)
        {
            Write("R" + (i + 1).ToString("D2") + " ");
            for (int j = 0; j < SIZE; j++)
            {
                switch (Grid[i, j])
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
        //Checking cell value whether match target value
        int target = 0;
        if(player.Symbol == 'X') target = 1;
        if(player.Symbol == 'O') target = 2;

        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (Grid[i, j] != target) continue;

                // Horizontal
                if (CheckDirection(i, j, 0, 1, target) ||
                // Vertical
                    CheckDirection(i, j, 1, 0, target) || 
                // Diagonal down-right
                    CheckDirection(i, j, 1, 1, target) || 
                // Diagonal down-left
                    CheckDirection(i, j, 1, -1, target))  
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

            if (r < 0 || r >= SIZE || c < 0 || c >= SIZE || Grid[r, c] != target)
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
