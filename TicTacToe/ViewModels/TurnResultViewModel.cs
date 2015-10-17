using TicTacToe.Core.Enums;

namespace TicTacToe.Web.ViewModels
{
    public class TurnResultViewModel
    {
        public string status { get; set; }
        public string errorText { get; set; }
        public bool isGameDone { get; set; }
        public PlayerCode winner { get; set; }
        public int opponentMove { get; set; }

        public TurnResultViewModel()
        {
            opponentMove = -1;
        }
    }

    public static class RusultStatus
    {
        public static string Error = "error";
        public static string Success = "success";
    }
}