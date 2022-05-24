using ParallelLib;
using System;

namespace Parallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] arguments = new []{2.0, 3.0, 4.0 };

            var exp = new object[] { arguments[0], "*", arguments[1], "+", arguments[2], "*", arguments[0], "-", arguments[1], "/", arguments[2] };

            var tree = ExpressionTree.expressionTree(exp);

            foreach (var i in ExpressionTree.ToList(tree))
            {
                Console.Write(i.data.ToString() + "  ");

            }

            Console.WriteLine();
            Console.WriteLine(ExpressionTree.calc(tree));


            ParallelGraph parallel = new ParallelGraph(new ExpressionGraph(tree));
            var arr = ExpressionTree.ToList(tree).ToArray();
            for (int i = 0; i < parallel.GetAlgorithmTiers.Length; i++)
            {
                Console.WriteLine("Номер яруса для вершини №   {0} - {1}", arr[i].data.ToString(), parallel.GetAlgorithmTiers[i]);
            }

        }
    }
}
