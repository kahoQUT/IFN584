using System;
using static System.Console;
namespace a1;

public class NotaktoBoard : Board
{
    public const int NUM_OF_ROW = 9;
    public const int NUM_OF_COL = 3;
    public const int PIECE = 3;

    //Constructor for new Board
    public NotaktoBoard()
    {
        // first 3 row is board 1, middle 3 row is board 2, last 3 row is board 3
        Grid = new int[NUM_OF_ROW, NUM_OF_COL];
    }

    //Get Empty Cell from non-dead board
    public override List<(int, int)> GetEmptyCells()
    {
        var empty = new List<(int, int)>();
        for (int b = 0; b < 3; b++)
        {
            int offset = b * 3;
            if (BoardIsDead(offset)) continue;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < NUM_OF_COL; j++)
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
        for (int i = 0; i < NUM_OF_COL; i++)
        {
            Write($" C{i + 1} ");
        }
        WriteLine();
        for (int i = 0; i < NUM_OF_ROW; i++)
        {
            Write($"R{i + 1} ");
            for (int j = 0; j < NUM_OF_COL; j++)
            {
                string cell = Grid[i, j] == 0 ? " . " : AddSpacesAround("X");
                Write($"|{cell}");
            }
            WriteLine("|");
            if ((i+1)%NUM_OF_COL == 0)  WriteLine();
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
        Grid[row, col] = PIECE;
        return true;
    }

    //Reset 0 in cell
    public override bool ResetNumber(int row, int col)
    {
        Grid[row, col] = 0;
        return true;
    }

    // check if the board is dead
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

    // check if all three boards are dead
    public override bool CheckWin(Player player)
    {
        int deadCount = 0;
        for (int b = 0; b < 3; b++)
            if (BoardIsDead(b * 3)) deadCount++;
        return deadCount == 3;
    }
    

}