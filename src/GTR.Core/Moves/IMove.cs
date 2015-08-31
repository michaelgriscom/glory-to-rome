#region

using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

#endregion

namespace GTR.Core.Moves
{
    public interface IMove<T> : IAction where T : CardModelBase
    {
        T Card { get; }
        ICardCollection<T> Destination { get; }
        ICardCollection<T> Source { get; }
    }
}