``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22621
AMD 4700S Desktop Kit, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT


```
|                  Method |     Mean |   Error |  StdDev | Ratio | RatioSD |
|------------------------ |---------:|--------:|--------:|------:|--------:|
| StringConcatenationTest | 283.3 ns | 5.66 ns | 7.15 ns |  1.00 |    0.00 |
|       StringBuilderTest | 211.6 ns | 3.94 ns | 4.84 ns |  0.75 |    0.03 |
