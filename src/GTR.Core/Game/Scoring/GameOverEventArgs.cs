namespace GTR.Core.Game
{
    public delegate void GameOverHandler(object sender, GameOverEventArgs args);

    public class GameOverEventArgs
    {
        public string Reason { get; set; }
    }
}