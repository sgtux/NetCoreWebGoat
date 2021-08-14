using System.Security.Cryptography;
using System.Text;

namespace NetCoreWebGoat.Helpers
{
    public class HashHelper
    {
        public static string Md5(string value)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value));
            var sb = new StringBuilder();
            foreach (var i in data)
                sb.Append(i.ToString("x2"));
            return sb.ToString();
        }
    }
}