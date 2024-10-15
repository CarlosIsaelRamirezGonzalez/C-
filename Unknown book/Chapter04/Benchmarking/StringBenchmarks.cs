using BenchmarkDotNet.Attributes; // [Benchmark]
public class StringBenchmarks
{
    int[]? numbers;
    public StringBenchmarks()
    {
        numbers = Enumerable.Range(start: 1, count: 20).ToArray();
    }

    [Benchmark(Baseline = true)] // . Este atributo indica que este método será utilizado como el estándar de comparación en las pruebas de rendimiento. El parámetro Baseline = true establece este método como la referencia de rendimiento base para comparar con otros métodos marcados con [Benchmark].
    public string StringConcatenationTest()
    {
        string s = String.Empty;
        for (int i = 0; i < numbers?.Length; i++)
        {
            s += numbers[i] + ", ";
        }
        return s;
    }

    [Benchmark]
    public string StringBuilderTest()
    {
        System.Text.StringBuilder builder = new();
        for (int i = 0; i < numbers?.Length; i++)
        {
            builder.Append(numbers[i]);
            builder.Append(", ");
        }
        return builder.ToString();
    }
}
