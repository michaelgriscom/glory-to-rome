using System.Collections.Generic;
using System.Linq;
using GTR.Core.Model;

namespace GTR.Core.Action
{
    public class ThinkCombo : IAction
    {
        public IList<IMove<OrderCardModel>> ThinkMoves { get; private set; }

        public void Add(IMove<OrderCardModel> thinkMove)
        {
            ThinkMoves.Add(thinkMove);
        }

        public ThinkCombo()
        {
            ThinkMoves = new List<IMove<OrderCardModel>>();
        }

        public ThinkCombo(IEnumerable<IMove<OrderCardModel>> moves)
        {
            ThinkMoves = new List<IMove<OrderCardModel>>(moves);
        } 

        public bool Perform()
        {
            return ThinkMoves.Select(move => move.Perform()).All(success => success);
        }

    }
}