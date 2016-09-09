namespace GTR.Core.Moves
{
    public interface IAction
    {
        bool Perform(MoveMaker moveMaker);
    }
}