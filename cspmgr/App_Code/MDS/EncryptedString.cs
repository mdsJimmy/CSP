using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

using System.IO;




namespace AESCryptoIPhone
{

    /// <summary>
    /// AES對稱加解密類別 For IPhone(Objective-C) plateform 
    /// </summary>
    public class EncryptedString
    {

        /// <summary>
        /// AES密碼金鑰
        /// </summary>
        private static string passPhrase
        {
            get { return @"ihlih*0037JOHT*)"; }
        }

        /// <summary>
        /// Encrpyts the sourceString, returns this result as an Aes encrpyted, BASE64 encoded string
        /// </summary>
        /// <param name="plainSourceStringToEncrypt">a plain, Framework string (ASCII, null terminated)</param>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <returns>
        /// returns an Aes encrypted, BASE64 encoded string
        /// </returns>
        public static string EncryptString(string plainSourceStringToEncrypt)
        {
            //Set up the encryption objects
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(passPhrase)))
            {
                byte[] sourceBytes = Encoding.ASCII.GetBytes(plainSourceStringToEncrypt);
                ICryptoTransform ictE = acsp.CreateEncryptor();

                //Set up stream to contain the encryption
                MemoryStream msS = new MemoryStream();

                //Perform the encrpytion, storing output into the stream
                CryptoStream csS = new CryptoStream(msS, ictE, CryptoStreamMode.Write);
                csS.Write(sourceBytes, 0, sourceBytes.Length);
                csS.FlushFinalBlock();

                //sourceBytes are now encrypted as an array of secure bytes
                byte[] encryptedBytes = msS.ToArray(); //.ToArray() is important, don't mess with the buffer

                //return the encrypted bytes as a BASE64 encoded string
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        /// <summary>
        /// 將字串轉成字串陣列
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private static byte[] ConvertStringToByte(string inputString)
        {
            if (inputString == null || inputString.Length < 2)
            {
                throw new ArgumentException();
            }
            int l = inputString.Length / 2;
            byte[] result = new byte[l];
            for (int i = 0; i < l; ++i)
            {
                result[i] = Convert.ToByte(inputString.Substring(2 * i, 2), 16);
            }

            return result;
        }
        
        
        /// <summary>
        /// Decrypts a BASE64 encoded string of encrypted data, returns a plain string
        /// </summary>
        /// <param name="base64StringToDecrypt">an Aes encrypted AND base64 encoded string</param>
        /// <param name="passphrase">The passphrase.</param>
        /// <returns>returns a plain string</returns>
        public static string DecryptString(string base64StringToDecrypt)
        {            
            //Set up the encryption objects
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(passPhrase)))
            {
                byte[] RawBytes = null;
                String status = "aaaaaaaaaa000000000";
               
                MemoryStream msD = null;
                CryptoStream csD = null;
                StreamReader sr = null;
              //  string filepath = @"c:\temp\posttestLog\";
                
                    RawBytes = Convert.FromBase64String(base64StringToDecrypt);
                    //System.IO.File.WriteAllText(filepath + "bese64.txt", base64StringToDecrypt + "=RawBytes>"+RawBytes.Length);
                    //byte[] RawBytes = ConvertStringToByte(base64StringToDecrypt);
                    ICryptoTransform ictD = acsp.CreateDecryptor();
                    //status = "aaaaaaaaaa1111111";
                    //RawBytes now contains original byte array, still in Encrypted state

                    //Decrypt into stream
                    msD = new MemoryStream(RawBytes, 0, RawBytes.Length);
                    //status = "aaaaaaaaaa222222222";
                    csD = new CryptoStream(msD, ictD, CryptoStreamMode.Read);
                    //status = "aaaaaaaaaa3333333333";
                    //csD now contains original byte array, fully decrypted
                    sr = new StreamReader(csD);
                    status = sr.ReadToEnd();
                     
                    //status = "aaaaaaaaaa4444444";
                   /* 20120614 add release method */
                  
                    msD.Close();
                    csD.Close();
                    sr.Close();
                    ictD.Dispose();
                    sr = null;
                    msD = null;
                    csD = null;
                    ictD = null;
                    RawBytes = null;
                //return the content of msD as a regular string
                return status;
            }
        }

        private static AesCryptoServiceProvider GetProvider(byte[] key)
        {
            AesCryptoServiceProvider result = new AesCryptoServiceProvider();
            result.BlockSize = 128;
            result.KeySize = 128;
            result.Mode = CipherMode.CBC;
            //result.Padding = PaddingMode.PKCS7;
            result.Padding = PaddingMode.None;
            result.GenerateIV();
            result.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            byte[] RealKey = GetKey(key, result);
            result.Key = RealKey;
            // result.IV = RealKey;
            return result;
        }

        private static byte[] GetKey(byte[] suggestedKey, SymmetricAlgorithm p)
        {
            byte[] kRaw = suggestedKey;
            List<byte> kList = new List<byte>();

            for (int i = 0; i < p.LegalKeySizes[0].MinSize; i += 8)
            {
                kList.Add(kRaw[(i / 8) % kRaw.Length]);
            }
            byte[] k = kList.ToArray();
            kList.Clear();
            kList = null;
            kRaw = null;
            return k;
        }


    }
}