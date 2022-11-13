using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MinimalisticSerializer
{
    public class MinimalisticSerializer
    {
        public unsafe struct Buffer2
        {
            public fixed byte bytes[2];
        }
        public Buffer2 Buf2;
        public unsafe struct Buffer3
        {
            public fixed byte bytes[3];
        }
        public Buffer3 Buf3;
        public unsafe struct Buffer4
        {
            public fixed byte bytes[4];
        }
        public Buffer4 Buf4;
        public unsafe struct Buffer5
        {
            public fixed byte bytes[5];
        }
        public Buffer5 Buf5;
        public unsafe struct Buffer6
        {
            public fixed byte bytes[6];
        }
        public Buffer6 Buf6;
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
