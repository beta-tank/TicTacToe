using System;

namespace TicTacToe.Web.ViewModels
{
    public class TurnClientViewModel
    {
        public int GameId { get; set; }
        public Guid Token { get; set; }
        public byte Turn { get; set; } 
    }
}