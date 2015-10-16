using System;
using System.Runtime.InteropServices.ComTypes;
using TicTacToe.Core.Enums;

namespace TicTacToe.Core
{
    public static class Bot
    {
        private static readonly Random Rand = new Random(DateTime.UtcNow.Millisecond);
        private static readonly byte[] Corners = {0, 2, 6, 8};
        public static int Move(Game game, PlayerCode player)
        {
            var cell = -1;
            // Для первого хода
            if (game.Moves.Count == 1)
            {
                if (game.Field[Corners[0]] == player.Opponent())
                    cell = Corners[3];
                if (game.Field[Corners[1]] == player.Opponent())
                    cell = Corners[2];
                if (game.Field[Corners[2]] == player.Opponent())
                    cell = Corners[1];
                if (game.Field[Corners[3]] == player.Opponent())
                    cell = Corners[0];
                if (cell == -1)
                    game.Field[Corners[Rand.Next(0, 3)]] = player;
                //// Если не занят центр, то занимаем
                //if (game.Field[4] == PlayerCode.None)
                //    cell = 4;
                ////
                //else
                //{
                //    if (game.Field[Corners[0]] == player.Opponent())
                //        cell = Corners[3];
                //    if (game.Field[Corners[1]] == player.Opponent())
                //        cell = Corners[2];
                //    if (game.Field[Corners[2]] == player.Opponent())
                //        cell = Corners[1];
                //    if (game.Field[Corners[3]] == player.Opponent())
                //        cell = Corners[0];
                //    if (cell == -1)
                //        game.Field[Corners[Rand.Next(0, 3)]] = player;
                //}
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (game.Field[i] == PlayerCode.None)
                    {
                        cell = i;
                    }
                }
            }
            return game.Move(player, (byte)cell) ? cell : -1;
        }
    }
}