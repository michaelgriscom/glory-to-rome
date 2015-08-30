using GTR.Core.Serialization;

namespace GTR.Core.Model
{
    public class JackCardModel : HandCardModel
    {
        public override string Name
        {
            get { return "Jack"; }
        }

        public override CardSerialization ToDto()
        {
            return new CardSerialization()
            {
                CardType = CardType.Jack,
                Id = Id
            };
        }
    }
}