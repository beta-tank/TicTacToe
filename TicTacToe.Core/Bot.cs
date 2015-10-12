using TicTacToe.Core.Enums;

namespace TicTacToe.Core
{
    public static class Bot
    {
        public static bool Move(Game game, PlayerCode player)
        {
            byte cell = 0;
            return game.Move(player, cell);
        }
    }
}