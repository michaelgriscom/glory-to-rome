#region

using GTR.Core.Services;
using GTR.Core.UnitTests.Properties;

#endregion

namespace GTR.Core.UnitTests.Services
{
    internal class ResourceProviderForTest : IResourceProvider
    {
        public string CardXml
        {
            get { return Resources.Cards; }
        }
    }
}