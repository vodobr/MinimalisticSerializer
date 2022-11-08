using MinimalisticSerializer;
using SolTechnology.Avro;
using System.IO.Pipelines;
using System.Runtime.InteropServices;
using System.Text.Json;


var testClass = new TestClass();
testClass.A = 124;
testClass.B = 124;
testClass.C = 124;
testClass.E = true;


MinimalisticSerializer.MinimalisticSerializer serializer = new MinimalisticSerializer.MinimalisticSerializer();

for (int j = 0; j < 10; j++)
{
    byte[] bytes = new byte[500];
    for (var i = 0; i < 1000000; i++)
    {
        serializer.Serialize(bytes, testClass);
    }
}

for (int j = 0; j < 10; j++)
{
    for (var i = 0; i < 1000000; i++)
    {
        var serialized = SpanJson.JsonSerializer.Generic.Utf8.Serialize(testClass);
    }
}


for (int j = 0; j < 10; j++)
{
    for (var i = 0; i < 1000000; i++)
    {
        var result = JsonSerializer.Serialize(testClass);
    }
}

for (int j = 0; j < 10; j++)
{
    for (var i = 0; i < 1000000; i++)
    {
        var result = MessagePack.MessagePackSerializer.Typeless.Serialize(testClass);
    }
}

for (int j = 0; j < 10; j++)
{
    using (var stream = new MemoryStream())
    {
        for (var i = 0; i < 1000000; i++)
        {
            ProtoBuf.Serializer.Serialize(stream, testClass);
        }
    }
}

for (int j = 0; j < 10; j++)
{
    for (var i = 0; i < 1000000; i++)
    {
        var serialized = System.Text.Json.JsonSerializer.Serialize(testClass);
    }
}

for (int j = 0; j < 10; j++)
{
    for (var i = 0; i < 1000000; i++)
    {
        var serialized = Utf8Json.JsonSerializer.Serialize(testClass);
    }
}

for (int j = 0; j < 10; j++)
{
    using (var stream = new MemoryStream())
    {
        for (var i = 0; i < 1000000; i++)
        {
            Binaron.Serializer.BinaronConvert.Serialize(testClass, stream);
        }
    }
}

for (int j = 0; j < 10; j++)
{
    using (var stream = new MemoryStream())
    {
        for (var i = 0; i < 1000000; i++)
        {
            var serialized = ServiceStack.Text.JsonSerializer.SerializeToString(testClass);
        }
    }
}


for (int j = 0; j < 10; j++)
{
    using (var stream = new MemoryStream())
    {
        for (var i = 0; i < 1000000; i++)
        {
            Es.Serializer.TextJsonSerializer.Instance.Serialize(testClass, stream);
        }
    }
}