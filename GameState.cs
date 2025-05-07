using System;

namespace a1;

public class GameState
{
    public int BoardSize { get; set; }
    public int[][] Grid { get; set; }
    public List<int> Player1Numbers { get; set; }
    public List<int> Player2Numbers { get; set; }
    public string Player2Name { get; set; }
    public PlayerType Player2Type { get; set; }
    public int CurrentPlayerIndex { get; set; }
}
