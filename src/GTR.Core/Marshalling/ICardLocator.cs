using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.DeckManagement;
using GTR.Core.Game;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

namespace GTR.Core.Marshalling
{
    public interface ICardLocator //<T> where T : CardSerialization
    {
        T Locate<T>(int id) where T : CardModelBase;

        CardType DetermineType(int id);

        //OrderCardModel Locate(OrderCardSerialization orderCardDto);

        //JackCardModel Locate(JackCardSerialization jackCardDto);

        //BuildingSite Locate(BuildingFoundationSerialization buildingFoundationDto);
    }

    public interface ICardCollectionLocator // where T : CardModelBase
    {
        ICardCollection<T> Locate<T>(int id) where T : CardModelBase;
    }


    public interface ICardFactory
    {
        OrderCardModel CreateOrderCard(IOrderCardMaker cardMaker, string buildingName);
        JackCardModel CreateJackCard();
        BuildingSite CreateBuildingSite(MaterialType materialType, SiteType siteType);
    }

    public class CardFactory : ICardLocator, ICardFactory
    {
        private int nextIndex = 0;
        private Dictionary<int, OrderCardModel> orderCards;
        private Dictionary<int, JackCardModel> jackCards;
        private Dictionary<int, BuildingSite> foundationCards;

        public CardFactory()
        {
            orderCards = new Dictionary<int, OrderCardModel>();
            jackCards = new Dictionary<int, JackCardModel>();
            foundationCards = new Dictionary<int, BuildingSite>();
        }

        public OrderCardModel Locate(int id)
        {
            return orderCards[id];
        }

        public OrderCardModel CreateOrderCard(IOrderCardMaker cardMaker, string buildingName)
        {
            var orderCard = cardMaker.MakeCard(buildingName);
            orderCard.Id = nextIndex;
            nextIndex++;
            return orderCard;
        }

        public JackCardModel CreateJackCard()
        {
            var jackCard = new JackCardModel();
            jackCard.Id = nextIndex;
            nextIndex++;
            return jackCard;
        }

        public BuildingSite CreateBuildingSite(MaterialType materialType, SiteType siteType)
        {
            var buildingSite = new BuildingSite(materialType, siteType);
            buildingSite.Id = nextIndex;
            nextIndex++;
            return buildingSite;
        }

        public T Locate<T>(int id) where T : CardModelBase
        {
            if (orderCards.ContainsKey(id))
            {
                return orderCards[id] as T;
            }
            if (jackCards.ContainsKey(id))
            {
                return jackCards[id] as T;
            }
            if (foundationCards.ContainsKey(id))
            {
                return foundationCards[id] as T;
            }
            throw new KeyNotFoundException("Unknown card " + id);
        }

        public CardType DetermineType(int id)
        {
            if (orderCards.ContainsKey(id))
            {
                return CardType.Order;
            }
            if (jackCards.ContainsKey(id))
            {
                return CardType.Jack;
            }
            if (foundationCards.ContainsKey(id))
            {
                return CardType.BuildingSite;
            }
            throw new KeyNotFoundException("Unknown card " + id);
        }
    }

    public class CardCollectionLocatorFactory
    {
        public ICardCollectionLocator MakeLocator(Model.Game game)
        {
            CardCollectionLocator locator = new CardCollectionLocator();
            locator.Add(game.GameTable.OrderDeck);
            locator.Add(game.GameTable.JackDeck);
            locator.Add(game.GameTable.Pool);

            foreach (var siteDeck in game.GameTable.SiteDecks)
            {
                locator.Add(siteDeck);
            }
            foreach (var player in game.GameTable.Players)
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
    }

    public class CardCollectionLocator : ICardCollectionLocator
    {
        private Dictionary<int, ICardCollection<OrderCardModel>> orderLocations;
        private Dictionary<int, ICardCollection<JackCardModel>> jackLocations;
        private Dictionary<int, ICardCollection<BuildingSite>> foundationLocations;

        public CardCollectionLocator()
        {
            this.orderLocations = new Dictionary<int, ICardCollection<OrderCardModel>>();
            this.jackLocations = new Dictionary<int, ICardCollection<JackCardModel>>();
            this.foundationLocations = new Dictionary<int, ICardCollection<BuildingSite>>();
        }

        public void Add(ICardCollection<OrderCardModel> collection)
        {
            orderLocations.Add(collection.Id, collection);
        }
        public void Add(ICardCollection<JackCardModel> collection)
        {
            jackLocations.Add(collection.Id, collection);
        }
        public void Add(ICardCollection<BuildingSite> collection)
        {
            foundationLocations.Add(collection.Id, collection);
        }

        public ICardCollection<T> Locate<T>(int id) where T : CardModelBase
        {
            if (orderLocations.ContainsKey(id))
            {
                return orderLocations[id] as ICardCollection<T>;
            }
            if (jackLocations.ContainsKey(id))
            {
                return jackLocations[id] as ICardCollection<T>;
            }
            if (foundationLocations.ContainsKey(id))
            {
                return foundationLocations[id] as ICardCollection<T>;
            }
            throw new KeyNotFoundException("Unknown location " + id);
        }
    }

}
