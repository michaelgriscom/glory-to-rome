#region

using System.Threading.Tasks;
using GTR.Core.Game;
using GTR.Core.Serialization;

#endregion

namespace GTR.Core.Services
{
    public class StartGameRequest : Request
    {
        public string GameId;
    }

    public class StartGameResponseSerialization : ResponseSerialization
    {

    }

    public class CreateGameRequest : Request
    {
        public GameOptions GameOptions;
    }

}