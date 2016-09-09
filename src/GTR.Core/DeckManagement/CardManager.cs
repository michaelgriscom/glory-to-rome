﻿#region

using System;
using System.Collections.Generic;
using GTR.Core.Marshalling;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;
using GTR.Core.Services;

#endregion

namespace GTR.Core.DeckManagement
{
    internal class CardManager
    {
        private readonly IDeckIo _deckIo;
        private IResourceProvider _resourceProvider;
        private ICardFactory _cardFactory;

        public CardManager(IResourceProvider resourceProvider, IDeckIo deckIo, ICardFactory cardFactory)
        {
            _deckIo = deckIo;
            _resourceProvider = resourceProvider;
            var builtInDecks = GetBuiltinDecks();
            var customDecks = GetCustomDecks();

            CardMaker = CardSetSerializer.Deserialize(resourceProvider.CardXml);
            _cardFactory = cardFactory;
        }

        public IOrderCardMaker CardMaker { get; }

        private HashSet<DeckType> GetCustomDecks()
        {
            HashSet<DeckType> customDecks = new HashSet<DeckType>();
            foreach (var deckCsv in _deckIo.GetCustomDecks())
            {
                AddDeck(deckCsv, customDecks);
            }
            return customDecks;
        }

        private HashSet<DeckType> GetBuiltinDecks()
        {
            HashSet<DeckType> builtInDecks = new HashSet<DeckType>();

            AddDeck(_deckIo.RepublicDeckSerialization, builtInDecks);
            AddDeck(_deckIo.ImperiumDeckSerialization, builtInDecks);
            return builtInDecks;
        }

        private void AddDeck(string deckSerialization, HashSet<DeckType> deckTypes)
        {
            DeckType deckType = DeckTypeSerializer.Deserialize(deckSerialization);
            deckTypes.Add(deckType);
        }

        public OrderDeck CreateOrderCardDeck(DeckType deckVersion)
        {
            OrderDeck orderDeck = new OrderDeck();
            foreach (string cardName in deckVersion.GetCardNames())
            {
                int cardCount = deckVersion.GetCount(cardName);
                for (int i = 0; i < cardCount; i++)
                {
                    OrderCardModel cardModel = _cardFactory.CreateOrderCard(CardMaker, cardName);
                    orderDeck.Add(cardModel);
                }
            }
            return orderDeck;
        }

        private OrderCardModel ConvertToOrder(OrderCardModel cardModel)
        {
            throw new NotImplementedException();
        }
    }
}