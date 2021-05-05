using System;
using System.Collections.Generic;
using System.Text;

namespace BSK2project.Classes
{
    public class LFSRService
    {
        byte[] bytes;
        List<byte> polynomialByte;
        public byte XOR(byte x, byte y)
        {
            if (x == y) return 0;
            return 1;
        }
        public LFSRService(string start, string polynomial)
        {
            bytes = new byte[start.Length];

            polynomialByte = new List<byte>();
            byte index = 0;

            for (int i = 0; i < start.Length; i++)
            {
                bytes[i] = (byte)(start[i] - 48);
            }

            foreach (var p in polynomial)
            {
                if (p == '1')
                    polynomialByte.Add(index);
                index++;
            }
        }

        public byte Shift()
        {
            byte startValue = bytes[polynomialByte[0]];

            for (int i = 1; i < polynomialByte.Count; i++)
            {
                startValue = XOR(startValue, bytes[polynomialByte[i]] );
            }

            byte oldValue = bytes[0];
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i != bytes.Length - 1)
                {
                    oldValue = swap(i, oldValue);
                }
            }

            bytes[0] = startValue;
            return startValue;
        }

        private byte swap(int i, byte oldValue)
        {
            byte pom = bytes[i + 1];
            bytes[i + 1] = oldValue;
            oldValue = pom;
            return oldValue;
        }

        public byte Shift(byte input)
        {
            byte startValue = bytes[polynomialByte[0]];
            startValue = XOR(input, startValue);

            for (int i = 1; i < polynomialByte.Count; i++)
            {
                startValue = XOR(startValue, bytes[polynomialByte[i]]);
            }

            byte oldValue = bytes[0];
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i != bytes.Length - 1)
                {
                    oldValue = swap(i, oldValue);
                }
            }
            bytes[0] = startValue;

            return startValue;
        }

        private void ShiftDecrypt(byte input)
        {
            byte oldValue = bytes[0];
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i != bytes.Length - 1)
                {
                    oldValue = swap(i, oldValue);
                }
            }

            bytes[0] = input;
        }

        public  byte NextDecryptValue(byte input)
        {
            byte value = input;
            foreach (var tap in polynomialByte)
            {
                value = XOR(value, bytes[tap]);
            }

            ShiftDecrypt(input);

            return value;
        }

    }
}
