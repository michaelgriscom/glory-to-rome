#region

using GTR.Core.DeckManagement;
using GTR.Core.Engine;
using GTR.Core.Marshalling;

#endregion

namespace GTR.Core.Model
{
    public class Game : ObservableModel
    {
        private CardSet _cardSet;
        private GameOptions _gameOptions;
        private GameTable _gameTable;
        private string _id;
        private Player _leadPlayer;
        private int _turnNumber;

        public ICardLocator CardLocator { get; set; }

        public ICardCollectionLocator CardCollectionLocator { get; set; }

        public ICardCollectionLocator GetCardCollectionLocator()
        {
            return GetLocator();
        }

        private CardCollectionLocator GetLocator()
        {
            CardCollectionLocator locator = new CardCollectionLocator();
            locator.Add(GameTable.OrderDeck);
            locator.Add(GameTable.JackDeck);
            locator.Add(GameTable.Pool);

            foreach (var siteDeck in GameTable.SiteDecks)
            {
                locator.Add(siteDeck);
            }
            foreach (var player in GameTable.Players)
            {
                locator.Add(player.CompletedBuildings);
                locator.Add(player.ConstructionZone);
                locator.Add(player.DemandArea);
                locator.Add(player.PlayArea.JackCards);
                locator.Add(player.PlayArea.OrderCards);
                locator.Add(player.Hand.JackCards);
                locator.Add(player.Hand.OrderCards);
                locator.Add(player.Camp.Clientele);
                locator.Add(player.Camp.Vault);
                locator.Add(player.Camp.CompletedFoundations);
                locator.Add(player.Camp.Stockpile);
            }
            return locator;
        }

        public ICardLocator GetCardLocator()
        {
            return GetLocator().GetCardLocator();
        }

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        public GameOptions GameOptions
        {
            get { return _gameOptions; }
            set
            {
                _gameOptions = value;
                RaisePropertyChanged();
            }
        }

        public Player ActionPlayer { get; set; }

        public Player LeadPlayer
        {
            get { return _leadPlayer; }
            set
            {
                _leadPlayer = value;
                RaisePropertyChanged();
            }
        }

        public GameTable GameTable
        {
            get { return _gameTable; }
            set
            {
                _gameTable = value;
                RaisePropertyChanged();
            }
        }

        public int TurnNumber
        {
            get { return _turnNumber; }
            set
            {
                _turnNumber = value;
                RaisePropertyChanged();
            }
        }
    }
}