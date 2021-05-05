using System;
using System.Collections.Generic;
using System.Text;

namespace BSK2project.Classes
{
    public class CiphertextAutokeyService
    {
        public string Encode(string word, string start, string polynomial)
        {
            byte[] bytes = new byte[word.Length];


            for (int i = 0; i < word.Length; i++)
            {
                bytes[i] = (byte)(word[i] - 48);
            }

            var LSFR = new LFSRService(start, polynomial);
            string result = "";

            foreach (var b in bytes)
            {
                byte resulByte = LSFR.Shift(b);
                result += (char)(resulByte + 48);
            }

            return result;
        }
        public string Decode(string word, string start, string polynomial)
        {
            byte[] bytes = new byte[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                bytes[i] = (byte)(word[i] - 48);
            }

            var lfsr = new LFSRService(start, polynomial);
            string result = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                byte resulByte = lfsr.NextDecryptValue(bytes[i]);
                result += (char)(resulByte + 48);
            }

            return result;
        }
    }
}
