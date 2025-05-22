using System;
using static System.Console;
namespace a1;

public class ComputerPlayer : Player
{
    private Random random = new();

    //Constructor of Computer Player
    public ComputerPlayer(string name, bool isNumGame, int boardSize, char? symbol = null) : base(name, isNumGame, symbol)
    {
        if (isNumGame)
        {
            int maxNumber = boardSize * boardSize;
            for (int i = 2; i <= maxNumber; i += 2)
                AvailableNumbers.Add(i);
        }
    }

    public override void MakeMove(Game game)
    {

        var emptyCells = game.Board.GetEmptyCells();

        if (IsNumGame)
        {
            // try to find a winning move
            foreach (var (r, c) in emptyCells)
            {
                foreach (int num in AvailableNumbers)
                {
                    if (game.Board.PlaceMove(r, c, this, num))
                    {
                        if (game.Board.CheckWin(this))
                        {
                            UseNumber(num);
                            WriteLine($"Computer places winning move {num} at ({r + 1}, {c + 1})");
                            return;
                        }
                        game.Board.ResetNumber(r, c); // Undo the test move
                    }
                }
            }

            // If no winning move, make a random move
            var (row, col) = emptyCells[random.Next(emptyCells.Count)];
            int choice = ChooseRandomNumber();
            game.Board.PlaceMove(row, col, this, choice);
            UseNumber(choice);
            WriteLine($"Computer places {choice} at ({row + 1}, {col + 1}).");
        }
        else
        {
            // For non-numerical games (like Gomoku or Notakto)
            // try to find a winning move
            List<(int, int)> losingMoves = new List<(int, int)>(); // for Notakto
            foreach (var (r, c) in emptyCells)
            {
                if (game.Board.PlaceMove(r, c, this))
                {
                    if (game.Board.CheckWin(this))
                    {
                        if (game is not NotaktoGame)
                        {
                            WriteLine($"Computer places winning move at ({r + 1}, {c + 1})");
                            return;
                        }
                        else
                        {
                            losingMoves.Add((r, c));
                        }
                    }
                    game.Board.ResetNumber(r, c); // Undo the test move
                }
            }

            // for Notakto, try to avoid losing move
            if (losingMoves.Count > 0 && emptyCells.Count > losingMoves.Count)
            {
                emptyCells = emptyCells.Except(losingMoves).ToList();
            }

            // If no winning move, make a random move
            var (row, col) = emptyCells[random.Next(emptyCells.Count)];
            game.Board.PlaceMove(row, col, this);
            WriteLine($"Computer places symbol at ({row + 1}, {col + 1}).");
        }
    }
}