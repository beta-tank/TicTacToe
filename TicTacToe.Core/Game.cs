using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.Enums;

namespace TicTacToe.Core
{
    public class Game : IEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// Токен для предотвращения совершения хода игроками, которые её не создавали
        /// </summary>
        public Guid Token { get; set; }
        public string Player { get; set; }
        public virtual Field Field { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
        public GameStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public PlayerCode Winner { get; set; }
        /// <summary>
        /// Массив выйгрышных комбинаций
        /// </summary>
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
            Token = Guid.NewGuid();
        }

        public void CreateField()
        {
            var field = new Field() {Game = this};
            Field = field;
        }

        public bool Move(PlayerCode player, byte cell)
        {
            if (Status == GameStatus.Done || !Field.Move(player, cell)) return false;
            Moves.Add(new Move() { Game = this, Player = player, Position = cell });
            return true;
        }

        public GameStatus IsDone()
        {
            if (Status == GameStatus.Done) return Status;
            // Проверяем не выполнил ли один из игроков выйгрышную комбинацию
            foreach (var comb in WinCombinations.Where(comb => (Field[comb[0]] == PlayerCode.One || Field[comb[0]] == PlayerCode.Two)
                                                            && Field[comb[0]] == Field[comb[1]]
                                                            && Field[comb[1]] == Field[comb[2]]))
            {
                Winner = Field[comb[0]];
                EndTime = DateTime.UtcNow;
                Status = GameStatus.Done;
                return GameStatus.Done;
            }
            return GameStatus.NoteDone;
        }
    }
}