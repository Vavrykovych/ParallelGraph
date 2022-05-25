using ParallelLib;
using ParallelLib.Abstract;
using ParallelLib.Matrix;
using System;
using System.Diagnostics;
using System.Linq;

namespace Parallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Обчислення виразу із чисел.
            object a=10.0, b= 20.0, c= 15.0, d= 4.0, f= 8.0;

            object[] exp = new object[] { "(",a,"+",b,")","*", "(", b, "+", c, ")","+", "(", b, "-", c, ")","*","(",f, "-", d, ")", "*", "(", d, "+", a, ")" };
            Console.WriteLine($"Вираз: {string.Join("", exp)}");


            var tree = ExpressionTree.expressionTree(exp);

            IExpressionExecutor executorParallel2 = new ParallelExpressionExecutor(exp, 1);
            IExpressionExecutor executorParallel = new ParallelExpressionExecutor(exp, 4);
            IExpressionExecutor executor = new ExpressionExecutor(exp);

            Stopwatch stopwatch1 = Stopwatch.StartNew();
            Stopwatch stopwatch2 = Stopwatch.StartNew();
            Stopwatch stopwatch3 = Stopwatch.StartNew();

            stopwatch1.Start();
            var res1 = executorParallel.Execute();
            stopwatch1.Stop();

            stopwatch2.Start();
            var res2 = executor.Execute();
            stopwatch2.Stop();

            stopwatch3.Start();
            var res3 = executorParallel2.Execute();
            stopwatch3.Stop();

            Console.WriteLine($"Час послiдовного обчислення: {stopwatch2.ElapsedMilliseconds}ms");
            Console.WriteLine($"Час паралельного обчислення у 1 потоках: {stopwatch3.ElapsedMilliseconds}ms");
            Console.WriteLine($"Час паралельного обчислення у 4 потоках: {stopwatch1.ElapsedMilliseconds}ms");
            Console.WriteLine("Чи результати збiгаються: " + (res1.Equals(res2) && res1.Equals(res3)));
            Console.WriteLine("Результа: " + res1);


            var arr = ExpressionTree.ToList(tree).ToArray();


            // Обчислення виразу із Матриць.
            Console.WriteLine("Обчислення для випадкових матриць 500x500");
            a = Matrix.CreateRandom(500);
            b = Matrix.CreateRandom(500);
            c = Matrix.CreateRandom(500);
            d = Matrix.CreateRandom(500);
            f = Matrix.CreateRandom(500);

            exp = new object[] { "(", a, "+", b, ")", "*", "(", b, "+", c, ")", "+", "(", b, "-", c, ")", "*", "(", f, "-", d, ")", "*", "(", d, "+", a, ")" };

            executorParallel2 = new ParallelExpressionExecutor(exp, 1);
            executorParallel = new ParallelExpressionExecutor(exp, 4);
            executor = new ExpressionExecutor(exp);

            stopwatch1 = Stopwatch.StartNew();
            stopwatch2 = Stopwatch.StartNew();
            stopwatch3 = Stopwatch.StartNew();

            stopwatch1.Start();
            res1 = executorParallel.ExecuteForMatrix();
            stopwatch1.Stop();

            stopwatch2.Start();
            res2 = executor.ExecuteForMatrix();
            stopwatch2.Stop();

            stopwatch3.Start();
            res3 = executorParallel2.ExecuteForMatrix();
            stopwatch3.Stop();

            Console.WriteLine($"Час послiдовного обчислення: {stopwatch2.ElapsedMilliseconds}ms");
            Console.WriteLine($"Час паралельного обчислення у 1 потоках: {stopwatch3.ElapsedMilliseconds}ms");
            Console.WriteLine($"Час паралельного обчислення у 4 потоках: {stopwatch1.ElapsedMilliseconds}ms");
            Console.WriteLine("Чи результати збiгаються: " + (res1.Equals(res2) && res1.Equals(res3)));
            Console.WriteLine("Результа: " + res1);
        }
    }
}
