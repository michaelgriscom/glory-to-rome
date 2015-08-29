using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GTR.Core.Action;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Serialization;
using GTR.Core.Services;
using GTR.Tiber.Controllers;
using GTR.Tiber.DataObjects;
using GTR.Tiber.Services;
using tiberService.Models;

namespace GTR.Tiber
{
    public class GameManager
    {
        private static  GameManager instance;

        public static  GameManager Instance
        {
            get { return instance ?? (instance = new GameManager()); }
        }

        private ConcurrentDictionary<string, Game> Games;
         
        private GameManager()
        {
        Games = new ConcurrentDictionary<string, Game>();    
        }

        private IDeckIo deckIo = new DeckIo();
        private IResourceProvider resourceProvider = new ResourceProvider();

        public async void StartGame(LobbyGame gameInfo)

        { 
            var playerInputs = new Dictionary<string, IPlayerInput>();
            foreach (var playerId in gameInfo.Players)
            {
           var playerInput = new PlayerInput(playerId);
                playerInputs.Add(playerId, playerInput);
            }

           
            Game game = new Game(playerInputs, gameInfo.GameOptions, deckIo, resourceProvider, new NullMessageProvider());
           bool success = Games.TryAdd(gameInfo.Id, game);
            if (!success)
            {
                throw new ArgumentException("Duplicated game");
            }

            await game.PlayGame();
        }

        public bool MakeMove(string playerId, MoveSetRequest moveSetRequest)
        {
            //Game game;
            //var success = Games.TryGetValue(moveSetRequest.GameId, out game);
            //if (!success)
            //{
            //    return false;
            //}

            //var player = game.GameTable.Players.First(p => p.PlayerName == playerId);
            //if (player == null)
            //{
            //    return false;
            //}

            //var input = player.InputService as PlayerInput;
            //if (input == null)
            //{
            //    return false;
            //}

            //MoveSet moveSet = new MoveSet();
            //foreach (var moveSerialization in moveSetRequest.MoveSet.Moves)
            //{
            //    var move = new Move<CardModelBase>();
            //}
            return false;
        }
    }

    internal class ServerGame
    {
        private GameEngine game;
        private TiberContext context;

        public ServerGame(Game game, TiberContext context)
        {
            this.game = new GameEngine(game, null, null);
            this.context = context;
        }

        public async void Start()
        {
            await game.PlayGame();

        }
    }
}