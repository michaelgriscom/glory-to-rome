#region

using GTR.Core.Services;
using GTR.Windows.Properties;

#endregion

namespace GTR.Windows.Services
{
    public class WpfResourceProvider : IResourceProvider
    {
        public string CardXml
        {
            get { return Resources.Cards; }
        }
    }
}