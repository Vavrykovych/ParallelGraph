using ParallelLib.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLib
{
    public class ParallelExpressionExecutor : IExpressionExecutor
    {
        private readonly Node parallelTree;
        private readonly ParallelGraph parallelForm;
        private readonly Node[] operations;
        private readonly int threads;
        public ParallelExpressionExecutor(object[] expression, int threadsCount1)
        {
            parallelTree = ExpressionTree.expressionTree(expression);
            parallelForm = new ParallelGraph(new ExpressionGraph(parallelTree));
            operations = ExpressionTree.ToList(parallelTree).ToArray();
            threads = threadsCount1;
        }

        public object Execute()
        {
            var tiersIndexes = parallelForm.GetAlgorithmTiers.ToArray();

            var tiersCount = tiersIndexes.Max();

            List<Node>[] tiers = new List<Node>[tiersCount];
            
            for(int i = 0; i < tiersCount; i++)
            {
                tiers[i] = new List<Node>();
            }

            for (int i = 0; i < tiersIndexes.Length; i++)
            {
                tiers[tiersIndexes[i] - 1].Add(operations[i]);
            }

            for (int i = tiers.Length - 2; i >= 0; i--)
            {
                Console.WriteLine("tier " + i);

                Parallel.ForEach(tiers[i], new ParallelOptions { MaxDegreeOfParallelism = threads }, operation =>
                {
                    CalcExpNode(operation);
                    Console.WriteLine($"Thread ID:{Thread.CurrentThread.ManagedThreadId} TierID: {i}");
                });
            }

            return tiers[0].FirstOrDefault()?.data;
        }

        private static void CalcExpNode(Node tree)
        {
            if (tree.left is null && tree.right is null)
            {
                return;
            }

            var left = tree.left.data;
            var right = tree.right.data;


            if (tree.data.ToString() == "+")
            {
                tree.data = (double)left + (double)right;
            }
            if (tree.data.ToString() == "-")
            {
                tree.data = (double)left - (double)right;
            }
            if (tree.data.ToString() == "*")
            {
                tree.data = (double)left * (double)right;
            }
            if (tree.data.ToString() == "/")
            {
                tree.data = (double)left / (double)right;
            }
            if (tree.data.ToString() == "^")
            {
                tree.data = Math.Pow((double)left, (double)right);
            }
        }
    }
}
