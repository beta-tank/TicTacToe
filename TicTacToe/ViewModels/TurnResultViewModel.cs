using TicTacToe.Core.Enums;

namespace TicTacToe.Web.ViewModels
{
    public class TurnResultViewModel
    {
        public string Status { get; set; }
        public string ErrorText { get; set; }
        public bool IsGameDone { get; set; }
        public PlayerCode Winner { get; set; }
        public byte OpponentMove { get; set; }
    }

    public static class RusultStatus
    {
        public static string Error = "error";
        public static string Success = "success";
    }
}