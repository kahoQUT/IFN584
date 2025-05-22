using System;
namespace a1;

public abstract class Board
{
    public int[,] Grid { get; set; }
    
    public abstract List<(int, int)> GetEmptyCells();

    public abstract bool PlaceMove(int row, int col, Player player, int? value = null);

    public abstract void Display();

    public abstract bool CheckWin(Player player);

    public abstract bool ResetNumber(int row, int col);

    
}