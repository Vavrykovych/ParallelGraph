using ParallelLib;
using ParallelLib.Abstract;
using System;

namespace Parallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] arguments = new []{ 2.0, 3.0, 4.0 };

            var exp = new object[] { arguments[0], "*", arguments[1], "+", arguments[2], "^", arguments[0], "+", arguments[2], "^", arguments[0], "+", arguments[2], "^", arguments[0], "+", arguments[2], "^", arguments[0] };

            var tree = ExpressionTree.expressionTree(exp);

            foreach (var i in ExpressionTree.ToList(tree))
            {
                Console.Write(i.data.ToString() + "");
            }

            Console.WriteLine();

            IExpressionExecutor executorParallel = new ParallelExpressionExecutor(exp, 3);
            IExpressionExecutor executor = new ExpressionExecutor(exp);

            Console.WriteLine(executorParallel.Execute());
            Console.WriteLine(executor.Execute());


            var arr = ExpressionTree.ToList(tree).ToArray();

        }
    }
}
