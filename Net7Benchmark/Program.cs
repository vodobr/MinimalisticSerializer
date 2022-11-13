using MinimalisticSerializer;
using System.Diagnostics;

Console.WriteLine("Serialize Benchmarks, .NET 7");
Console.WriteLine("");
Console.WriteLine("Mutliplatform JSON libraries:");
Console.WriteLine("");

var testClass = new TestClass();
testClass.A = 124;
testClass.B = 124;
testClass.C = 124;
testClass.D = "34344343";
testClass.E = true;

long newtonsoftJson = 0;

for (int j = 0; j < 11; j++)
{
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < 1000000; i++)
    {
        var result = Newtonsoft.Json.JsonConvert.SerializeObject(testClass);
    }
    sw.Stop();
    if (j > 0)
        newtonsoftJson += sw.ElapsedMilliseconds;
}

newtonsoftJson = newtonsoftJson / 10;

Console.WriteLine($"Newtonsoft - {newtonsoftJson}");

long utf8json = 0;

for (int j = 0; j < 11; j++)
{
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < 1000000; i++)
    {
        var serialized = Utf8Json.JsonSerializer.Serialize(testClass);
    }
    sw.Stop();
    if (j > 0)
        utf8json += sw.ElapsedMilliseconds;
}

utf8json = utf8json / 10;

Console.WriteLine($"Utf8JSON - {utf8json}");

long serviceStackJson = 0;

for (int j = 0; j < 11; j++)
{
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < 1000000; i++)
    {
        var serialized = ServiceStack.Text.JsonSerializer.SerializeToString(testClass);
    }
    sw.Stop();
    if (j > 0)
        serviceStackJson += sw.ElapsedMilliseconds;
}

serviceStackJson = serviceStackJson / 10;

Console.WriteLine($"ServiceStack - {serviceStackJson}");
Console.WriteLine("");
Console.WriteLine("Mutliplatform Binary libraries:");
Console.WriteLine("");

long messagePack = 0;

for (int j = 0; j < 11; j++)
{
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < 1000000; i++)
    {
        var result = MessagePack.MessagePackSerializer.Typeless.Serialize(testClass);
    }
    sw.Stop();
    if (j > 0)
        messagePack += sw.ElapsedMilliseconds;
}

messagePack = messagePack / 10;

Console.WriteLine($"MessagePack - {messagePack}");

long protobuf = 0;

for (int j = 0; j < 11; j++)
{
    var sw = Stopwatch.StartNew();
    using (var stream = new MemoryStream())
    {
        for (var i = 0; i < 1000000; i++)
        {
            ProtoBuf.Serializer.Serialize(stream, testClass);
        }
    }
    sw.Stop();
    if (j > 0)
        protobuf += sw.ElapsedMilliseconds;
}

protobuf = protobuf / 10;

Console.WriteLine($"Protobuf - {protobuf}");
Console.WriteLine("");
Console.WriteLine("Latest .NET JSON libraries:");
Console.WriteLine("");

long textJson = 0;

for (int j = 0; j < 11; j++)
{
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < 1000000; i++)
    {
        var serialized = System.Text.Json.JsonSerializer.Serialize(testClass);
    }
    sw.Stop();
    if (j > 0)
        textJson += sw.ElapsedMilliseconds;
}

textJson = textJson / 10;

Console.WriteLine($"System.Text.Json - {textJson}");

long spanJson = 0;

for (int j = 0; j < 11; j++)
{
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < 1000000; i++)
    {
        var serialized = SpanJson.JsonSerializer.Generic.Utf8.Serialize(testClass);
    }
    sw.Stop();
    if (j > 0)
        spanJson += sw.ElapsedMilliseconds;
}

spanJson = spanJson / 10;

Console.WriteLine($"SpanJson - {spanJson}");

MinimalisticSerializer.MinimalisticSerializer serializer = new MinimalisticSerializer.MinimalisticSerializer();

for (int j = 0; j < 11; j++)
{
    byte[] bytes = new byte[500];
    for (var i = 0; i < 1000000; i++)
    {
        serializer.Serialize(bytes, testClass);
    }
}

Console.ReadLine();











