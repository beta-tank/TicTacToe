using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.Enums;

namespace TicTacToe.Core
{
    public class Game : IEntity
    {
        public int Id { get; set; }
        public string[] Players { get; set; }
        public Field Field { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
        public GameStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public PlayerCode Winner { get; set; }
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

        public Game()
        {
            Status = GameStatus.NoteDone;
            StartTime = DateTime.UtcNow;
            Winner = PlayerCode.None;
            Field = new Field();
            Players = new string[2];
        }

        public bool Move(PlayerCode player, byte cell)
        {
            if (!Field.Move(player, cell)) return false;
            Moves.Add(new Move() { Game = this, Player = player, Position = cell });
            return true;
        }

        public GameStatus IsDone()
        {
            foreach (var comb in WinCombinations.Where(comb => Field[comb[0]] == Field[comb[1]] && Field[comb[1]] == Field[comb[2]]))
            {
                Winner = Field[comb[0]];
                EndTime = DateTime.UtcNow;
                Status = GameStatus.Done;
                return GameStatus.Done;
            }
            return GameStatus.NoteDone;
        }

        public string GetName(PlayerCode player)
        {
            switch (player)
            {
                case PlayerCode.One:
                    return Players[0];
                case PlayerCode.Two:
                    return Players[1];
                default:
                    return null;
            } 
        }
    }
}