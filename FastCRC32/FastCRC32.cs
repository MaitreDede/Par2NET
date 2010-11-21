﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastCRC32
{
    public class FastCRC32
    {
        private uint[] crcTable = new uint[256];
        private uint[] windowTable = new uint[256];

        public uint windowMask = 0;

        public FastCRC32(ulong window)
        {
            GenerateCRC32Table();
            GenerateWindowTable(window);
            windowMask = ComputeWindowMask(window);
        }

        private void GenerateCRC32Table()
        {
            unchecked
            {
                uint polynomial = 0xEDB88320;

                for (uint i = 0; i <= 255; i++)
                {
                    uint crc = i;

                    for (uint j = 0; j < 8; j++)
                    {
                        crc = (crc >> 1) ^ ((crc & 1) != 0 ? polynomial : 0);
                    }

                    crcTable[i] = crc;
                }
            }
        }

        private void GenerateWindowTable(ulong window)
        {
            unchecked
            {
                for (uint i = 0; i <= 255; i++)
                {
                    uint crc = crcTable[i];

                    for (uint j = 0; j < window; j++)
                    {
                        crc = ((crc >> 8) & 0x00ffffff) ^ crcTable[(byte)crc];
                    }

                    windowTable[i] = crc;
                }
            }
        }

        uint ComputeWindowMask(ulong window)
        {
            unchecked
            {
                //uint result = ~0;
                uint result = 0xFFFFFFFF;

                while (window > 0)
                {
                    result = CRCUpdateChar(result, (char)0);

                    window--;
                }

                //result ^= ~0;
                result ^= 0xFFFFFFFF;

                return result;
            }
        }

        public uint CRCUpdateChar(uint crc, char ch)
        {
            unchecked
            {
                return ((crc >> 8) & 0x00ffffff) ^ crcTable[(byte)crc ^ ch];
            }
        }

        public uint CRCUpdateBlock(uint crc, uint length, byte[] buffer)
        {
            unchecked
            {

                uint start = 0;

                while (length-- > 0)
                {
                    crc = crcTable[(crc ^ buffer[start++]) & 0xFF] ^ (crc >> 8);
                    //crc = ((crc >> 8) & 0x00ffffff) ^ crcTable[(byte)(crc ^ buffer[start++])];
                }

                return crc;
            }
        }

        public uint CRCUpdateBlock(uint crc, uint length)
        {
            unchecked
            {
                while (length-- > 0)
                {
                    crc = ((crc >> 8) & 0x00ffffff) ^ crcTable[(byte)crc];
                }

                return crc;
            }
        }

        // Slide the CRC along a buffer by one character (removing the old and adding the new).
        // The new character is added using the main CCITT CRC32 table, and the old character
        // is removed using the windowtable.
        public uint CRCSlideChar(uint crc, char chNew, char chOld)
        {
            unchecked
            {
                return ((crc >> 8) & 0x00ffffff) ^ crcTable[(byte)crc ^ chNew] ^ windowTable[chOld];
            }
        }


        // Usage
        /*

          char *buffer;
          u64 window;

          //...

          u32 windowtable[256];
          GenerateWindowTable(window, windowtable);
          u32 windowmask = ComputeWindowMask(window);

          u32 crc = ~0 ^ CRCUpdateBlock(~0, window, buffer);
          crc = windowmask ^ CRCSlideChar(windowmask ^ crc, buffer[window], buffer[0], windowtable);

          assert(crc == ~0 ^ CRCUpdateBlock(~0, window, buffer+1));

        */
    }
}
