using ParallelLib.Abstract;
using System;

namespace ParallelLib
{
    public class ExpressionExecutor : IExpressionExecutor
    {
        private readonly Node tree;

        public ExpressionExecutor(object[] expression)
        {
            tree = ExpressionTree.expressionTree(expression);
        }
        public object ExecuteForMatrix() => calcFormatrix(tree);
        public object Execute() => calc(tree);


        private static object calc(Node tree)
        {
            if (!ExpressionTree.isOperator(tree.data))
            {
                return tree.data;
            }
            else
            {
                var left = calc(tree.left);
                var right = calc(tree.right);

                if (tree.data.ToString() == "+")
                    return (double)left + (double)right;
                if (tree.data.ToString() == "-")
                    return (double)left - (double)right;
                if (tree.data.ToString() == "*")
                    return (double)left * (double)right;
                if (tree.data.ToString() == "/")
                    return (double)left / (double)right;
                if (tree.data.ToString() == "^")
                    return Math.Pow((double)left, (double)right);
                throw new ArgumentException(nameof(tree));
            }
        }

        private static object calcFormatrix(Node tree)
        {
            if (!ExpressionTree.isOperator(tree.data))
                return tree.data;
            else
            {
                var left = calcFormatrix(tree.left) as Matrix.Matrix;
                var right = calcFormatrix(tree.right) as Matrix.Matrix;

                if (tree.data.ToString() == "+")
                    return left + right;
                if (tree.data.ToString() == "-")
                    return left - right;
                if (tree.data.ToString() == "*")
                    return left * right;
                throw new ArgumentException(nameof(tree));
            }
        }
    }
}
