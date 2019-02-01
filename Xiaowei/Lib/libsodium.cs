using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Xiaowei.Lib
{
   
   public class libsodium
    {
        //crypto_aead_aes256gcm_decrypt
        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
            internal static extern int crypto_aead_aes256gcm_decrypt(
          IntPtr message, out long messageLength, byte[] nsec, byte[] cipher, long cipherLength, byte[] additionalData,
          long additionalDataLength, byte[] nonce, byte[] key);

        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sodium_init();

        private const int KEYBYTES = 32;
        private const int NPUBBYTES = 12;
        private const int ABYTES = 16;

        /// <summary>
        /// Decrypts a cipher with an authentication tag and additional data using AES-GCM.
        /// </summary>
        /// <param name="cipher">The cipher to be decrypted.</param>
        /// <param name="nonce">The 12 byte nonce.</param>
        /// <param name="key">The 32 byte key.</param>
        /// <param name="additionalData">The additional data; may be null, otherwise between 0 and 16 bytes.</param>
        /// <returns>The decrypted cipher.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="NonceOutOfRangeException"></exception>
        /// <exception cref="AdditionalDataOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Decrypt(byte[] cipher, byte[] nonce, byte[] key, byte[] additionalData = null)
        {
            //additionalData can be null
            if (additionalData == null)
                additionalData = new byte[0x00];

            //validate the length of the key
            if (key == null || key.Length != KEYBYTES)
                throw new  Exception(
                    string.Format("key must be {0} bytes in length.", KEYBYTES));

            //validate the length of the nonce
            if (nonce == null || nonce.Length != NPUBBYTES)
                throw new Exception(
                    string.Format("nonce must be {0} bytes in length.", NPUBBYTES));

            //validate the length of the additionalData
            if (additionalData.Length > ABYTES || additionalData.Length < 0)
                throw new Exception(
                  string.Format("additionalData must be between {0} and {1} bytes in length.", 0, ABYTES));

            var message = new byte[cipher.Length - ABYTES];
            var bin = Marshal.AllocHGlobal(message.Length);
            long messageLength;

            var ret = crypto_aead_aes256gcm_decrypt(bin, out messageLength, null, cipher, cipher.Length,
              additionalData, additionalData.Length, nonce, key);

            Marshal.Copy(bin, message, 0, (int)messageLength);
            Marshal.FreeHGlobal(bin);

            if (ret != 0)
                throw new CryptographicException("Error decrypting message.");

            if (message.Length == messageLength)
                return message;

            //remove the trailing nulls from the array
            var tmp = new byte[messageLength];
            //RuntimeShim.Copy(message, 0, tmp, 0, messageLength);

            return tmp;
        }
    }
}
