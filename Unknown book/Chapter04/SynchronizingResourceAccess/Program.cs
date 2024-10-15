using System.Diagnostics; // Stopwatch
WriteLine("Please wait for the tasks to complete.");
Stopwatch watch = Stopwatch.StartNew();

Task a = Task.Factory.StartNew(MethodA); // No es asincrono pero lo hacemos asincrono envolviendolo (Wraped)
Task b = Task.Factory.StartNew(MethodB);

Task.WaitAll(new Task[] {a, b});
WriteLine();
WriteLine($"Results: {SharedObjects.Message}.");
WriteLine($"{watch.ElapsedMilliseconds:N0} elapsed milliseconds.");
WriteLine($"{SharedObjects.Counter} string modifications.");

