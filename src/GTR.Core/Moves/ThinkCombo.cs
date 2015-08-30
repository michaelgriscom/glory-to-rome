#region

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GTR.Core.Model;

#endregion

namespace GTR.Core.Action
{
    public class ThinkCombo : IAction
    {
        public ThinkCombo()
        {
            ThinkMoves = new List<IMove<OrderCardModel>>();
        }

        public ThinkCombo(IEnumerable<IMove<OrderCardModel>> moves)
        {
            ThinkMoves = new List<IMove<OrderCardModel>>(moves);
        }

        public IList<IMove<OrderCardModel>> ThinkMoves { get; }

        public bool Perform()
        {
            return ThinkMoves.Select(move => move.Perform()).All(success => success);
        }

        public void Add(IMove<OrderCardModel> thinkMove)
        {
            ThinkMoves.Add(thinkMove);
        }
    }
}