using System;
using System.Diagnostics;
using System.IO;

namespace Net48Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            Console.WriteLine("Serialize Benchmarks, .NET 4.8");
            Console.WriteLine("Mutliplatform JSON libraries");
            Console.WriteLine($"Newtonsoft - {newtonsoftJson}");
            Console.WriteLine($"Utf8JSON - {utf8json}");
            Console.WriteLine($"ServiceStack - {serviceStackJson}");
            Console.WriteLine("Mutliplatform Binary libraries");
            Console.WriteLine($"MessagePack - {messagePack}");
            Console.WriteLine($"Protobuf - {protobuf}");
            Console.ReadLine();
        }
    }
}