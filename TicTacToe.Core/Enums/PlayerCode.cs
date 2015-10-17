namespace TicTacToe.Core.Enums
{
    public enum PlayerCode
    {
        None,
        One,
        Two
    }

    static class PlayerCodeExtensions
    {
        public static PlayerCode Opponent(this PlayerCode player)
        {
            switch (player)
            {
                case PlayerCode.One:
                    return PlayerCode.Two;
                case PlayerCode.Two:
                    return PlayerCode.One;
                default:
                    return PlayerCode.None;
            }
        }
    }
}