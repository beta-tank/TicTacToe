using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using TicTacToe.Core.Enums;

namespace TicTacToe.Core
{
    public static class Bot
    {
        private static readonly Random Rand = new Random(DateTime.UtcNow.Millisecond);
        private static readonly byte[] Corners = {0, 2, 6, 8};
        private static readonly byte[][] WinCombinations = {
            new byte[] {0,1,2},
            new byte[] {3,4,5},
            new byte[] {6,7,8},
            new byte[] {0,3,6},
            new byte[] {1,4,7},
            new byte[] {2,5,8},
            new byte[] {0,4,8},
            new byte[] {2,4,6}
            };
        public static int Move(Game game, PlayerCode player)
        {
            var cell = -1;
            // Для первого хода
            if (game.Moves.Count == 1)
            {
                cell = FirstMove(game, player);
            }
            else
            {
                // Пытаемся найти выйгрышную комбинацию
                cell = ThreeInLine(game, player);         
                // Пытаемся заблокировать оппонента
                if (cell == -1)
                {
                    cell = BlockOpponent(game, player);
                    // Если блокировать оппонента не нужно, ищем лучшую комбинацию для себя
                    if (cell == -1)
                    {
                        cell = TwoInLine(game, player);
                        // Если не одна стратегия не сработала, то занимаем любую свободную клетку
                        if (cell == -1)
                        {
                            cell = RandomMove(game, player);
                        }
                    }
                }
                
                
            }
            return game.Move(player, (byte)cell) ? cell : -1;
        }

        private static int FirstMove(Game game, PlayerCode player)
        {          
            // Если не занят центр, то занимаем, иначе любой угол
            return game.Field[4] == PlayerCode.None ? 4 : Corners[Rand.Next(0, 3)]; ;
        }

        private static int BlockOpponent(Game game, PlayerCode player)
        {
            var cell = -1;
            var opponent = player.Opponent();
            var field = game.Field;
            // Ищем выйгрышные комбинации оппонента, которые мы ещё не блокировали, и пытаемся помешать
            foreach (var comb in WinCombinations.Where(comb => ((field[comb[0]] == opponent && field[comb[0]] == field[comb[1]] && field[comb[2]] == PlayerCode.None) ||
                                                                (field[comb[0]] == opponent && field[comb[0]] == field[comb[2]] && field[comb[1]] == PlayerCode.None) ||
                                                                (field[comb[1]] == opponent && field[comb[1]] == field[comb[2]] && field[comb[0]] == PlayerCode.None))))
            {
                // Занимаем свободную клетку в выйгрышной комбинации
                cell = comb.Where(position => field[position] == PlayerCode.None).First();
            }
            return cell;
        }

        private static int ThreeInLine(Game game, PlayerCode player)
        {
            var cell = -1;
            var opponent = player.Opponent();
            var field = game.Field;
            foreach (var comb in WinCombinations.Where(comb => ((field[comb[0]] == player && field[comb[0]] == field[comb[1]] && field[comb[2]] == PlayerCode.None) ||
                                                                    (field[comb[0]] == player && field[comb[0]] == field[comb[2]] && field[comb[1]] == PlayerCode.None) ||
                                                                    (field[comb[1]] == player && field[comb[1]] == field[comb[2]] && field[comb[0]] == PlayerCode.None))))
            {
                // Занимаем свободную клетку в выйгрышной комбинации
                cell = comb.Where(position => field[position] == PlayerCode.None).First();
            }
            return cell;
        }

        private static int TwoInLine(Game game, PlayerCode player)
        {
            var cell = -1;
            var opponent = player.Opponent();
            var field = game.Field;
            foreach (var comb in WinCombinations.Where(comb => ((field[comb[0]] == player && field[comb[1]] == field[comb[2]] && field[comb[1]] == PlayerCode.None) ||
                                                            (field[comb[1]] == player && field[comb[0]] == field[comb[2]] && field[comb[0]] == PlayerCode.None) ||
                                                            (field[comb[2]] == player && field[comb[0]] == field[comb[1]] && field[comb[0]] == PlayerCode.None))))
            {
                // Занимаем любую свободную клетку
                cell = comb.Where(position => field[position] == PlayerCode.None).First();
            }
            return cell;
        }

        private static int RandomMove(Game game, PlayerCode player)
        {
            var cell = -1;
            for (var i = 0; i < 9; i++)
            {
                if (game.Field[i] != PlayerCode.None) continue;
                cell = i;
                break;
            }
            return cell;
        }
    }
}