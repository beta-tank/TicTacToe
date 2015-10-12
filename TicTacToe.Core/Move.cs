using System;
using TicTacToe.Core.Enums;

namespace TicTacToe.Core
{
    public class Move : IEntity
    {
        public int Id { get; set; }
        public virtual Game Game { get; set; }
        public PlayerCode Player { get; set; }
        public byte Position { get; set; }
        
    }
}