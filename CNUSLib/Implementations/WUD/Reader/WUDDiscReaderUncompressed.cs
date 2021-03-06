﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CNUSLib
{
    class WUDDiscReaderUncompressed : WUDDiscReader
    {
        public WUDDiscReaderUncompressed(WUDImage wudFile) : base(wudFile)
        {
        }

        public override void readEncryptedToOutputStream(Stream outputStream, long offset, long size)
        {
            FileStream input = image.fileStream;

            //StreamUtils.skipExactly(input, offset);
            //input.Seek(offset, SeekOrigin.Begin);
            input.Seek(offset, SeekOrigin.Begin);

            int bufferSize = 0x80000;
            byte[] buffer = new byte[bufferSize];
            long totalread = 0;
            do
            {
                int read = input.Read(buffer, 0, bufferSize);
                if (read < 0) break;
                if (totalread + read > size)
                {
                    read = (int)(size - totalread);
                }
                try
                {
                    outputStream.Write(buffer, 0, read);
                }
                catch (IOException)
                {
                    //if (e.getMessage().equals("Pipe closed")) 
                    //{
                    //    break;
                    //} else 
                    //{
                    //    input.close();
                    //    throw e;
                    //}
                }
                totalread += read;
            } while (totalread < size);
            //input.close();
            //outputStream.close();
        }
    }
}
