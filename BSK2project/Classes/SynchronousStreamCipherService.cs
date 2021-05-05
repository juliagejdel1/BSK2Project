using System;
using System.Collections.Generic;
using System.Text;

namespace BSK2project.Classes
{
    public class SynchronousStreamCipherService
    {

        private LFSRService LFSR;

        public SynchronousStreamCipherService(string start, string polynomial)
        {
            LFSR = new LFSRService(start, polynomial);
        }

        public string DecodeEncode(string word)
        {
            byte[] bytes = new byte[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                bytes[i] = (byte)(word[i] - 48);
            }

            string result = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                byte resultByte = LFSR.XOR(bytes[i], LFSR.Shift());
                result += (char)(resultByte + 48);
            }

            return result;
        }
    }
}
