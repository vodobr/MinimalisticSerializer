using Cysharp.Collections;
using InvertedTomato.Compression.Integers.Gen2;
using InvertedTomato.Compression.Integers.Gen3;
using MessagePack.Formatters;
using Microsoft.IO;
using Newtonsoft.Json.Linq;
using ServiceStack.Text.Common;
using ServiceStack.Text.Pools;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Transactions;
using Utf8Json.Internal;

namespace MinimalisticSerializer
{
    public class MinimalisticSerializer
    {
        public unsafe struct Buffer2
        {
            public fixed byte bytes[2];
        }
        public unsafe struct Buffer3
        {
            public fixed byte bytes[3];
        }
        public unsafe struct Buffer4
        {
            public fixed byte bytes[4];
        }
        public unsafe struct Buffer5
        {
            public fixed byte bytes[5];
        }
        public unsafe struct Buffer6
        {
            public fixed byte bytes[6];
        }
        public MinimalisticSerializer() { }
        public unsafe Span<byte> Serialize(Span<byte> bytes, TestClass testClass)
        {
            (byte, int, byte, int, byte, int, byte, bool) buf = (1, testClass.A, 1, testClass.B, 1, testClass.C, 3, testClass.E);
            MemoryMarshal.Write(bytes, ref buf);
            MemoryMarshal.Cast<char, byte>(testClass.D.AsSpan()).CopyTo(bytes);
            return bytes;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected unsafe void Transform(Span<byte> bytes, int property)
        {
            int size = SizeOfInt(property);
            if (size == 1)
            {
                var buffer = new Buffer3();
                buffer.bytes[0] = 1;
                buffer.bytes[1] = 1;
                buffer.bytes[2] = (byte)property;
                MemoryMarshal.Write(bytes, ref buffer);
            } else if (size == 2)
            {
                var buffer = new Buffer4();
                buffer.bytes[0] = 1;
                buffer.bytes[1] = 2;
                buffer.bytes[2] = (byte)(property << 8);
                buffer.bytes[3] = (byte)property;
                MemoryMarshal.Write(bytes, ref buffer);
            } else if (size == 3)
            {
                var buffer = new Buffer5();
                buffer.bytes[0] = 1;
                buffer.bytes[1] = 3;
                buffer.bytes[2] = (byte)(property << 16);
                buffer.bytes[3] = (byte)(property << 8);
                buffer.bytes[4] = (byte)property;
                MemoryMarshal.Write(bytes, ref buffer);
            } else
            {
                var buffer = new Buffer6();
                buffer.bytes[0] = 1;
                buffer.bytes[1] = 3;
                buffer.bytes[2] = (byte)(property << 24);
                buffer.bytes[3] = (byte)(property << 16);
                buffer.bytes[4] = (byte)(property << 8);
                buffer.bytes[5] = (byte)property;
                MemoryMarshal.Write(bytes, ref buffer);
            }
        }
        protected void Transform(Span<byte> bytes, string property)
        {
            byte header = 2;
            MemoryMarshal.Write(bytes, ref header);
            //(bytes, Encoding.Default.CodePage);
        }
        protected void Transform(Span<byte> bytes, bool property)
        {
            (byte, bool) encoded = (3, property);
            MemoryMarshal.Write(bytes, ref encoded);
        }
        protected int SizeOfInt(int value)
        {
            uint v = (uint)value;
            if (v > byte.MaxValue)
            {
                if (v > ushort.MaxValue)
                {
                    if (v > 16777215)
                    {
                        return 4;
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    return 2;
                }
            }
            return 1;
        }
        protected void VLQEncode(Span<byte> bytes, int value)
        {
            byte b1, b2, b3;
            uint v = (uint)value;
            b1 = (byte)v;
            if (v > byte.MaxValue)
            {
                b2 = (byte)(v >> 8);
                if (v > ushort.MaxValue)
                {
                    b3 = (byte)(v >> 16);
                    if (v > 16777215)
                    {
                        MemoryMarshal.Write(bytes, ref value);
                    }
                    else
                    {
                        var encoded = (b1, b2, b3);
                        MemoryMarshal.Write(bytes, ref encoded);
                    }
                }
                else
                {
                    var encoded = (b1, b2);
                    MemoryMarshal.Write(bytes, ref encoded);
                }
            }
            else
            {
                MemoryMarshal.Write(bytes, ref b1);
            }
        }
    }
}
