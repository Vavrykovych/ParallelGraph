using ParallelLib;
using ParallelLib.Abstract;
using ParallelLib.Matrix;
using System;
using System.Diagnostics;

namespace Parallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //double[] arguments = new []
            //{ 
            //    2.0,
            //    3.0,
            //    4.0
            //};

            object[] arguments = new[]
            {
                Matrix.CreateRandom(300),
                Matrix.CreateRandom(300),
                Matrix.CreateRandom(300)
            };

            var exp = new object[] { arguments[0], "*", arguments[1], "+", arguments[2], "*", arguments[0], "+", arguments[2], "*", arguments[0], "+", arguments[2], "*", arguments[0], "+", arguments[2], "*", arguments[0] };

            var tree = ExpressionTree.expressionTree(exp);

            foreach (var i in ExpressionTree.ToList(tree))
            {
                Console.Write(i.data.ToString() + "");
            }

            Console.WriteLine();

            IExpressionExecutor executorParallel = new ParallelExpressionExecutor(exp, 3);
            IExpressionExecutor executor = new ExpressionExecutor(exp);

            Stopwatch stopwatch1 = Stopwatch.StartNew();
            Stopwatch stopwatch2 = Stopwatch.StartNew();


            stopwatch1.Start();
            Console.WriteLine(executorParallel.ExecuteForMatrix());
            stopwatch1.Stop();

            stopwatch2.Start();
            Console.WriteLine(executor.ExecuteForMatrix());
            stopwatch2.Stop();
            Console.WriteLine($"Parallel processing: {stopwatch1.ElapsedMilliseconds}");
            Console.WriteLine($"Linear processing: {stopwatch2.ElapsedMilliseconds}");



            var arr = ExpressionTree.ToList(tree).ToArray();

        }
    }
}
