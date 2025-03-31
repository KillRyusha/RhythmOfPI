using System.Net;
using System.Security.Cryptography;
using System.Text;

public static class IPHasher
{
    public static string GetHashedIP()
    {
        string ip = GetUserIP();
        return ComputeMD5Hash(ip);
    }
    public static string GetUserIP()
    {
        using (WebClient client = new WebClient())
        {
            string ip = client.DownloadString("https://api64.ipify.org").Trim(); 
            return ip.Replace('.', '_');
        }
    }

    private static string ComputeMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}