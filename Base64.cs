///<remarks>
///   Class:          Base64
///   Author:         Wilfredo Rodríguez (wilfredor@gmail.com)          
///   Date: 24/07/2016
///</remarks>
/// <summary>
/// Convert a byte array multiple of 3 on Base64 encode
/// Convertir un Array de bytes qui sera un multiple de 3 en Base64
/// </summary>
using System;

public static class Base64
{
    /// <summary>
    /// Convertir un Array de bits en une int.
    /// </summary>
    /// <param name="flag">Binary de position du char dans l'alphabet</param>
    private static char getBase(bool[] flag)
    {
        //Alphabet Base64 représentant chacun 6 bits des données originales
        char[] alphabet = new char[64] {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9','/','+'
        };
            
        return alphabet[bitArrayToInt(flag)];
    }
    /// <summary>
    /// Convertir un Array de bits en int.
    /// </summary>
    /// <param name="bitArray">Array de bits</param>
    private static int bitArrayToInt(bool[] bitArray)
    {
        int val = 0;
        Array.Reverse(bitArray); /// En procédant de « droite » à « gauche »
        for (int i = 0; i < bitArray.Length; ++i)
            if (bitArray[i]) val |= 1 << i;
        return val;
    }
    /// <summary>
    /// Array de 24 bits est créé en concaténant 3 bytes
    /// </summary>
    /// <param name="source">Array de bytes d’une longueur de 3</param>
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
    /// <summary>
    /// En procédant de « droite » à « gauche » ranger le groupe de 8 bits (byte) 
    /// </summary>
    /// <param name="b">Array de bits rangé</param>
    /// <param name="nbyte">Array de bits (byte) désordonné</param>
    /// <param name="flagBegin">Flag de départ</param>
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
    /// <summary>
    /// Convertir un Array de Bit en Base64.
    /// 4 caractères de l’alphabet Base64 représentant chacun 6 bits des données originales
    /// Les caractères ainsi référencés sont ajoutés dans le string encodé
    /// </summary>
    /// <param name="outputArray"> Array de bits d’une longueur 4</param>
    private static String BinaryToBase64(bool[][] outputArray) {

        char[] cArray = new char[4];
           
        for (int i = 0; i < 4; ++i)
            cArray[i] = getBase(outputArray[i]);

        return new string(cArray);
    }
    /// <summary>
    /// Encode Base64. Tous les bytes  sont ajoutés dans le string encodé</summary>
    /// <param name="source"> Array de bytes d’une longueur qui sera un multiple de 3</param>
    public static String Encode(byte[] source)
    {

        //Array de 24 bits
        bool[] sourceArray = new bool[24];

        // Array de 24 bits en 4 groupes
        bool[][] outputArray = new bool[4][];

        int group; int bit;

        String result = "";

        // Le processus se répète jusqu’à ce que tous les bytes de la source soient encodés.
        for (int n = 0; n <= source.Length - 3; n += 3)
        {
            // Array de 24 bits est créé en concaténant 3 bytes
            sourceArray = ByteArrayToBoolArray(new byte[3] { source[n], source[n + 1], source[n + 2] });

            group = 0; bit = 0;

            // Array de 24 bits divisés en 4 groupes de 6 bits
            while (group < 4) // 4 groupes
            {
                outputArray[group] = new bool[6]; // 6 bits
                Array.Copy(sourceArray, bit, outputArray[group], 0, 6);
                bit += 6;
                group++;
            }
            //Les caractères ainsi référencés tous les bytes  sont ajoutés dans le string encodé. 
            result += BinaryToBase64(outputArray);
        }

        return result;
    }

}
