using System.Diagnostics; // Stopwatch
OutputThreadInfo();
Stopwatch timer = Stopwatch.StartNew();
timer.Restart();
// SectionTitle("Running methods synchronously on one thread."); 
// timer.Restart();
// Task taskA = new (MethodA);
// taskA.Start();
// Task taskB = Task.Factory.StartNew(MethodB);
// Task taskC = Task.Run(MethodC);
// Task[] tasks = {taskA, taskB, taskC};
// Task.WaitAll(tasks);
// WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");



// SectionTitle("Passing the result of one task as an input into another."); 
// Task<string> taskServiceThenSProc = Task.Factory
//     .StartNew(CallWebService)
//     .ContinueWith(previousTask => CallStoredProcedure(previousTask.Result));

// WriteLine($"Result: {taskServiceThenSProc.Result}");
// WriteLine($"{timer.ElapsedMilliseconds: #,##0} ms elapsed.");

SectionTitle("Nested and child tasks");
Task outerTask = Task.Factory.StartNew(OuterMethod);
outerTask.Wait();
WriteLine("Console app is stopping.");