using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Serialization;
using GTR.Core.Services;
using GTR.Server.Services;
using GTR.Server.DataObjects;
using GTR.Server.Services;
using tiberService.Models;

namespace GTR.Server
{
    public class GameManager
    {
        private static  GameManager instance;

        public static  GameManager Instance
        {
            get { return instance ?? (instance = new GameManager()); }
        }

        private ConcurrentDictionary<string, GameEngine> Games;
         
        private GameManager()
        {
            Games = new ConcurrentDictionary<string, GameEngine>();    
        }

        private IDeckIo deckIo = new DeckIo();
        private IResourceProvider resourceProvider = new ResourceProvider();

        public Game CreateGame(LobbyGame gameInfo)
        {
            Game game = GameFactory.MakeGame(gameInfo.Players, gameInfo.GameOptions, deckIo, resourceProvider);
            PlayerInputService inputService = new PlayerInputService();

            GameEngine gameEngine = new GameEngine(game, new NullMessageProvider(), inputService);

            bool success = Games.TryAdd(game.Id, gameEngine);
            if (!success)
            {
                throw new ArgumentException("Duplicated game");
            }

            return game;
        }

        public Game GetGameInfo(string gameId)
        {
            if (!Games.ContainsKey(gameId))
            {
                return null;
            }
            else
            {
                return Games[gameId].Game;
            }
        }

        public async Task<CompletedGame> RunGame(string gameId)
        {
            var gameEngine = Games[gameId];
           return await gameEngine.PlayGame();
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