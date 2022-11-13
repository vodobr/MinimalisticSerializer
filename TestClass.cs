using ProtoBuf;

namespace Net48Benchmark
{
    [ProtoContract]
    public class TestClass
    {
        [ProtoMember(1)]
        public int A { get; set; }
        [ProtoMember(2)]
        public int B { get; set; }
        [ProtoMember(3)]
        public int C { get; set; }
        [ProtoMember(4)]
        public string D { get; set; }
        [ProtoMember(5)]
        public bool E { get; set; }
    }
}
