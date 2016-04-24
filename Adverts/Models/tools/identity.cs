using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

static class identity
{
    private static string StringReverse(string ToReverse)
    {
        Array arr = ToReverse.ToCharArray();
        Array.Reverse(arr);
        char[] c = (char[])arr;
        return (new string(c));
    }

    public static string getHashKey()
    {
        string result = Regex.Replace(Guid.NewGuid().ToString(), "-", "", RegexOptions.IgnoreCase);
        result = StringReverse(result);
        return result;
    }

    public static string getMD5Hash(string strToHash)
    {
        MD5CryptoServiceProvider md5Obj = new MD5CryptoServiceProvider();
        byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(strToHash);

        bytesToHash = md5Obj.ComputeHash(bytesToHash);
        string strResult = "";
        foreach (byte b in bytesToHash)
        {
            strResult += b.ToString("x2");
        }
        return strResult;
    }

    public static string generatePassword()
    {
        int passwordLength = 8;
        string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
        char[] chars = new char[passwordLength];
        Random rd = new Random();

        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }
}