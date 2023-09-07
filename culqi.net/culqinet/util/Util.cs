﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using RestSharp;
namespace culqi.net
{
	public class Util
	{

        public int GetRandomNumber()
        {
            using (RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[6];       // `int32` takes 4 bytes in C#
                rngCrypt.GetBytes(tokenBuffer);
                return BitConverter.ToInt32(tokenBuffer, 0);
            }
        }

        public string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }

    }
}
