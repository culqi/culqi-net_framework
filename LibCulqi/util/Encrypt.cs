﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace culqi.net
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
            Console.WriteLine(publicKey);
             // Serialize the JSON object to a string
            string jsonData = JsonConvert.SerializeObject(data);

            // Convert the string to a byte array
            var jsonBytes = Encoding.UTF8.GetBytes(data);
            string encryptedData = "";
            // Generate random key and IV
            
            byte[] key = new byte[32];
            byte[] iv = new byte[GCM_IV_LENGTH];
            byte[] tag = new byte[GCM_TAG_LENGTH];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

 
            // Create a GCM cipher
            using (var cipher = new AesGcm(key))
            {
                // Encrypt the message
                byte[] encryptedMessage = new byte[data.Length];
                cipher.Encrypt(iv, Encoding.UTF8.GetBytes(data), encryptedMessage, tag);

                // Encode the IV and encrypted message as Base64 strings
                encryptedData = Convert.ToBase64String(encryptedMessage);
            }

            //RSA ENCRYPT SHA256
            string publicKeyString = publicKey;

            byte[] publicKeyBytes = Convert.FromBase64String(publicKeyString);

            // create an RSA object and import the public key bytes
            RSA rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

            // encrypt the data using RSA OAEP SHA256 padding
            byte[] encryptedKey = rsa.Encrypt(key, RSAEncryptionPadding.OaepSHA256);
            byte[] encryptedIv = rsa.Encrypt(iv, RSAEncryptionPadding.OaepSHA256);

            // convert the encrypted data to a base64 string
            string encryptedKeyToJson = Convert.ToBase64String(encryptedKey);
            string encryptedIvToJson = Convert.ToBase64String(encryptedIv);

            // print the encrypted data

             
            return new Dictionary<string, object>
                {
                    { "encrypted_data", encryptedData },
                    { "encrypted_key", encryptedKeyToJson },
                    { "encrypted_iv", encryptedIvToJson }
                };
        }
    }
}
