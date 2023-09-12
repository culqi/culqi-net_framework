using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace culqinet.util
{
    public class Encrypt
    {
        public Encrypt()
        {
        }

        private const int GCM_IV_LENGTH = 12;
        private const int GCM_TAG_LENGTH = 16;

        public Dictionary<string, object> EncryptWithAESRSA(string data, string publicKey, bool isJson)
        {
            // Serialize the JSON object to a string
            string jsonData = isJson ? JsonConvert.SerializeObject(data) : data;

            // Convert the string to a byte array
            var jsonBytes = Encoding.UTF8.GetBytes(jsonData);
            string encryptedData = "";

            // Generate random key and IV
            byte[] key = new byte[32];
            byte[] iv = new byte[GCM_IV_LENGTH];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

            // Encrypt plaintext with AES-256 in CBC mode
            byte[] cipherText;
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor())
                {
                    cipherText = encryptor.TransformFinalBlock(jsonBytes, 0, jsonBytes.Length);
                    encryptedData = Convert.ToBase64String(cipherText);
                }
            }

            // RSA ENCRYPT SHA256
            string publicKeyString = publicKey;

            byte[] publicKeyBytes = Convert.FromBase64String(publicKeyString);

            // create an RSA object and import the public key bytes
            RSA rsa = RSA.Create();
            RSAParameters rsaParams = new RSAParameters
            {
                Modulus = publicKeyBytes, // You might need to set the Exponent as well, depending on your key format
                Exponent = new byte[] { 1, 0, 1 } // Common exponent for RSA keys
            };
            rsa.ImportParameters(rsaParams);

            // encrypt the data using RSA OAEP SHA256 padding
            byte[] encryptedKey = rsa.Encrypt(key, RSAEncryptionPadding.OaepSHA256);
            byte[] encryptedIv = rsa.Encrypt(iv, RSAEncryptionPadding.OaepSHA256);

            // convert the encrypted data to a base64 string
            string encryptedKeyToJson = Convert.ToBase64String(encryptedKey);
            string encryptedIvToJson = Convert.ToBase64String(encryptedIv);

            // return the encrypted data
            return new Dictionary<string, object>
            {
                { "encrypted_data", encryptedData },
                { "encrypted_key", encryptedKeyToJson },
                { "encrypted_iv", encryptedIvToJson }
            };
        }
    }
}
