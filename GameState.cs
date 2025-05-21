using System;

namespace a1;

public class GameState
{
    public int GameType { get; set; } // 1=Numerical, 2=Notakto, 3=Gomoku
    public int BoardSize { get; set; }
    public int[][] Grid { get; set; }
    public int[,] Grid2D { get; set; }
    public string Player1Name { get; set; }
    public string Player1Type { get; set; }
    public List<int> Player1Numbers { get; set; }
    public string Player2Name { get; set; }
    public string Player2Type { get; set; }
    public List<int> Player2Numbers { get; set; }
    public int CurrentPlayerIndex { get; set; }
    public bool IsNumGame { get; set; }
}
