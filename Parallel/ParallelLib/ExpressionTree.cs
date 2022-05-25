using System;
using System.Collections.Generic;
using System.Linq;

namespace ParallelLib
{
    public class Node
    {
        public object data;
        public Node left, right;

        public Node(object d)
        {
            data = d;
            left = null;
            right = null;
        }
    }

    public class ExpressionTree
    {
        public static int GetSize(Node tree)
        {
            return ToList(tree).Count;
        }
        static public List<Node> ToList(Node tree)
        {
            if(tree == null)
            {
                return new List<Node>();
            }
            else
            {
                var res = new List<Node>();
                res.AddRange(ToList(tree.left));
                res.Add(tree);
                res.AddRange(ToList(tree.right));
                return res;
            }
        }

        public static bool isOperator(object ch)
        {
            if (ch.ToString() == "+" || ch.ToString() == "-" || ch.ToString() == "*" || ch.ToString() == "/" || ch.ToString() == "^")
            {
                return true;
            }
            return false;
        }

        public static int getOperatorPriority(Node op)
        {
            if (op.data.ToString() == "^")
                return 1;
            if (op.data.ToString() == "*" || op.data.ToString() == "/")
                return 2;
            if(op.data.ToString() == "+" || op.data.ToString() == "-") 
                return 3;
            return 4;
        }

        public static Node expressionTree(object[] expression)
        {
            Stack<Node> args = new Stack<Node>();
            Stack<Node> ops = new Stack<Node>();
            Node temp;
            for (int i = 0; i < expression.Length; i++)
            {
                temp = new Node(expression[i]);
                if (expression[i].ToString() == "(")
                    ops.Push(temp);
                else if (expression[i].ToString() == ")")
                {
                    while (ops.FirstOrDefault().data.ToString() != "(")
                    {
                        var op = ops.Pop();
                        var arg2 = args.Pop();
                        var arg1 = args.Pop();
                        op.right = arg2;
                        op.left = arg1;
                        args.Push(op);
                    }
                    ops.Pop();
                }
                else if (isOperator(expression[i]))
                {
                    var prevOp = ops.FirstOrDefault();
                    if (prevOp != null && getOperatorPriority(prevOp) < getOperatorPriority(temp))
                    {
                        var arg2 = args.Pop();
                        var arg1 = args.Pop();
                        prevOp = ops.Pop();
                        prevOp.left = arg1;
                        prevOp.right = arg2;
                        args.Push(prevOp);
                        ops.Push(temp);
                    }
                    else
                        ops.Push(temp);
                }
                else
                    args.Push(temp);
            }
            while(ops.Count != 0)
            {
                var op = ops.Pop();
                var arg2 = args.Pop();
                var arg1 = args.Pop();
                op.right = arg2;
                op.left = arg1;
                args.Push(op);
            }
            return args.Pop();
        }
    }
}
