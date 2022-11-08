using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZeroFormatter;

namespace MinimalisticSerializer
{
    public enum s
    {
        f = 3,
        a = f
    }
    [ZeroFormattable]
    [ProtoContract]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TestClass
    {
        [Index(0)]
        [ProtoMember(1)]
        public int A { get; set; }
        [Index(1)]
        [ProtoMember(2)]
        public int B { get; set; }
        [Index(2)]
        [ProtoMember(3)]
        public int C { get; set; }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst =23)]
        public string D;
        [Index(4)]
        [ProtoMember(5)]
        public bool E { get; set; }
    }
}
