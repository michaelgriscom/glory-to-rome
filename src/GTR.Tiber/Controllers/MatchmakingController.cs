using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GTR.Tiber.Services;
using GTR.Core.Action;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Serialization;
using GTR.Core.Services;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using GTR.Tiber.DataObjects;

namespace GTR.Tiber.Controllers
{
    [MobileAppController]
    public class MatchmakingController : ApiController
    {
        public MatchmakingController()
        {
            lobbyTable = new LobbyTable();
        }
        private LobbyTable lobbyTable;

        public async Task<CreateGameResponseSerialization> PostCreate(CreateGameRequest request)
        {
            CreateGameResponseSerialization response = new CreateGameResponseSerialization();
            var playerId = GetPlayerId(request.AuthorizationToken);

            LobbyGame game = new LobbyGame()
            {
                Players = new List<int>() { playerId },
                GameOptions = request.GameOptions,
                HostId = playerId
            };

            response.Success = true;
            game = await lobbyTable.AddLobbyGame(game, Request);

            response.GameId = game.Id;

            return response;
        }

        public async Task<JoinGameResponseSerialization> PostJoin(JoinGameRequest request)
        {
            var response = new JoinGameResponseSerialization();
            response.Success = false;

            // TODO: query
            var allGames = lobbyTable.GetAllLobbyGames(Request);
            var gameInfo = allGames.First(g => g.Id == request.GameId);

            if (gameInfo == null)
            {
                response.Message = ErrorMessages.NonexistentGame;
                return response;
            }
            var playerId = GetPlayerId(request.AuthorizationToken);
            if (gameInfo.Players.Any(pId => pId == playerId))
            {
                response.Message = ErrorMessages.AlreadyJoined;
                return response;
            }
            if (gameInfo.Players.Count >= gameInfo.GameOptions.MaxPlayers)
            {
                response.Message = ErrorMessages.GameFull;
                return response;
            }

            gameInfo.Players.Add(playerId);
            response.Success = true;
            return response;
        }

        public async Task<StartGameResponseSerialization> PostStart(StartGameRequest request)
        {
            var response = new StartGameResponseSerialization();
            response.Success = false;

            // TODO: query
            var allGames = lobbyTable.GetAllLobbyGames(Request);
            var gameInfo = allGames.First(g => g.Id == request.GameId);

            if (gameInfo == null)
            {
                response.Message = ErrorMessages.NonexistentGame;
                return response;
            }
            int playerId = GetPlayerId(request.AuthorizationToken);
            if (playerId != gameInfo.HostId)
            {
                response.Message = ErrorMessages.NotGameHost;
                return response;
            }

            StartGame(gameInfo);
            response.Success = true;
            return response;
        }

        private IDeckIo deckIo = new DeckIo();
        private IResourceProvider resourceProvider = new ResourceProvider();

        private void StartGame(LobbyGame gameInfo)
        {
            Dictionary<string, IPlayerInput> playerInputs =
                gameInfo.Players.ToDictionary<int, string, IPlayerInput>
                (playerId => playerId.ToString(), playerId => new PlayerInput(playerId));

            Game game = new Game(playerInputs, gameInfo.GameOptions, deckIo, resourceProvider, new NullMessageProvider());
            game.PlayGame();
            // TODO: update lobby game table
        }


        private int GetPlayerId(int authorizationToken)
        {
            throw new NotImplementedException();
        }
    }

    internal class PlayerInput : IPlayerInput
    {
        private int _id;

        public PlayerInput(int id)
        {
            this._id = id;
        }

        public Task<RoleType> GetLeadRole(ICollection<RoleType> availableLeads)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<HandCardModel>> SelectCards(ICollection<HandCardModel> cards)
        {
            throw new NotImplementedException();
        }

        public Task<RoleType> GetRole(ICollection<RoleType> collection)
        {
            throw new NotImplementedException();
        }

        public Task<ICardSource<HandCardModel>> GetSource(List<ICardSource<HandCardModel>> availableSources)
        {
            throw new NotImplementedException();
        }

        public Task<ActionType> GetLead()
        {
            throw new NotImplementedException();
        }

        public Task<ActionType> GetFollow()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<HandCardModel>> SelectLeadCards(List<HandCardModel> cardOptions)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<HandCardModel>> SelectFollowCards(List<HandCardModel> cardOptions, RoleType role)
        {
            throw new NotImplementedException();
        }

        public Task<IAction> GetMove(MoveSpace moveSpace)
        {
            throw new NotImplementedException();
        }
    }
}
