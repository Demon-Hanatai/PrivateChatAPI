using System.Security.Cryptography;

namespace PrivateChatApi.Aside
{
    public class Token
    {
        public static string Create()
        {
            // Define the length of your token
            int length = 16;

            // Generate a random byte array
            byte[] randomBytes = new byte[length];
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert the byte array to a base64 string
            return Convert.ToBase64String(randomBytes);

        }
    }
}
