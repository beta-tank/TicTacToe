using System;
using System.Linq;
using TicTacToe.Core.Enums;
using static System.String;

namespace TicTacToe.Core
{
    public class Field : IEntity
    {
        public int Id { get; set; }
        public virtual Game Game { get; set; }
        public string CellsString {
            get { return Join(";", Cells.Select(n => (byte)n));}
            set { Cells = value.Split(';').Select(n =>(PlayerCode) Convert.ToByte(n)).ToArray(); }
        }
        public PlayerCode[] Cells { get; set; }
        public PlayerCode this[int index] {
            get { return Cells[index]; }
            set { Cells[index] = value; }
        }

        public Field()
        {
            Cells = new PlayerCode[9];
        }

        public bool Move(PlayerCode player, byte cell)
        {
            if (!IsPossibleMove(cell))
                return false;
            Cells[cell] = player;
            return true;
        }

        private bool IsPossibleMove(byte cell)
        {
            return cell <= 8 && Cells[cell] == PlayerCode.None;
        }
    }
}