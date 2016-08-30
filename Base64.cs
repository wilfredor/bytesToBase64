///<remarks>
///   Class:          Base64
///   Author:         Wilfredo Rodríguez (wilfredor@gmail.com)          
///   Date: 24/07/2016
///</remarks>
/// <summary>
/// This algorithm covert a byte array multiple of 3 on a Base64
/// </summary>
using System;

public static class Base64
{
    /// <summary>
    /// Get char from array
    /// </summary>
    /// <param name="flag">array flag</param>
    private static char GetBase(bool[] flag)
    {
        char[] alphabet = new char[64] {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9','/','+'
        };

        return alphabet[BitArrayToInt(flag)];
    }
    /// <summary>
    /// Convert array of bit to int.
    /// </summary>
    /// <param name="bitArray">Array of bits</param>
    private static int BitArrayToInt(bool[] bitArray)
    {
        int val = 0;
        Array.Reverse(bitArray);

        for (var i = 0; i < bitArray.Length; ++i)
            if (bitArray[i]) val |= 1 << i;
        return val;
    }
    /// <summary>
    /// Create 24 bits Array concatenating 3 bytes
    /// </summary>
    /// <param name="source">Array of bytes [3]</param>
    private static bool[] ByteArrayToBoolArray(byte[] source)
    {
        bool[] sourceArray = new bool[24];
        int i = 0; int j = 0;

        while (i < 3)
        {
            sourceArray = OrderBits(sourceArray, source[i], j);
            j += 8;
            i++;
        }

        return sourceArray;
    }

    private static bool[] OrderBits(bool[] b, byte nbyte, int flagBegin)
    {
        int bits = 7; int bytes = 1;

        while (bits >= 0)
        {
            b[flagBegin + bits] = ((nbyte & bytes) != 0);
            bits--;
            bytes += bytes;
        }

        return b;
    }

    private static string BinaryToBase64(IList<bool[]> outputArray)
    {
        char[] cArray = new char[4];
        for (int i = 0; i < 4; ++i)
            cArray[i] = GetBase(outputArray[i]);
        return new string(cArray);
    }

    public static string Encode(byte[] source)
    {
        var outputArray = new bool[4][];

        string result = "";

        for (int n = 0; n <= source.Length - 3; n += 3)
        {
            var sourceArray = ByteArrayToBoolArray(new byte[3] { source[n], source[n + 1], source[n + 2] });

            var group = 0;
            int bit = 0;

            while (group < 4) // 4 groups
            {
                outputArray[group] = new bool[6]; // 6 bits
                Array.Copy(sourceArray, bit, outputArray[group], 0, 6);
                bit += 6;
                group++;
            }
            result += BinaryToBase64(outputArray);
        }
        return result;
    }
}
