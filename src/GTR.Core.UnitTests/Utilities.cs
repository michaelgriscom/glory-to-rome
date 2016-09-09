#region

using System.IO;
using System.Text;

#endregion

namespace GTR.Core.UnitTests
{
    public class Utilities
    {
        public static Stream ConvertToStream(string str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
    }
}