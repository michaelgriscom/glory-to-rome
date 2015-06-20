using GTR.Core.CardCollections;
using GTR.Core.Model;

namespace GTR.Core.Action
{
    public interface IMove<out T> : IAction where T : CardModelBase
    {
        T Card { get; }
        ICardLocation<T> Destination { get; }
        ICardLocation<T> Source { get; }
    }
}