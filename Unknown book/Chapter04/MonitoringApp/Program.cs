using Packt.Shared;

/*
WriteLine("Processing. Please wait...");
Recorder.Start();
// simulate a process that requires some memory resources...
int[] largeArrayOfInts = Enumerable.Range(start: 1, count: 10_000).ToArray();
Thread.Sleep(new Random().Next(5, 10) * 1000); // Pausa el programa para simular una operacion que lleva tiempo.
Recorder.Stop();
*/

#region Measuring the efficiency of processing strings

int[] numbers = Enumerable.Range(start: 1, count: 50_000).ToArray();

SectionTitle("Using StringBuilder");
Recorder.Start();
System.Text.StringBuilder builder = new();
for (int i = 0; i < numbers.Length; i++)
{
    builder.Append(numbers[i]);
    builder.Append(", ");
}
Recorder.Stop();
WriteLine();

SectionTitle("Using string with +");
Recorder.Start();
string s = string.Empty; // i.e. ""
for (int i = 0; i < numbers.Length; i++)
{
    s += numbers[i] + ", ";
}
Recorder.Stop();

#endregion