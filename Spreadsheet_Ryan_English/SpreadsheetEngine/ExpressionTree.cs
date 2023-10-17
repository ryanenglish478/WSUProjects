// <copyright file="ExpressionTree.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for expression tokenizing and tree construction.
    /// </summary>
    public partial class ExpressionTree
    {
        private ExpressionTreeNode root;
        private Queue<string> postFixTokens = new Queue<string>();
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private Dictionary<string, int> operators = new Dictionary<string, int>
        {
            { "(", 3 },
            { ")", 4 },
            { "+", 5 },
            { "-", 6 },
            { "*", 7 },
            { "/", 8 },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// Takes the expression, tokenizes expression and then converts to postfix order.
        /// </summary>
        /// <param name="expression">The expression to be evaluated.</param>
        public ExpressionTree(string expression)
        {
            Queue<string> tokens = this.Tokenize(expression);

            Stack<string> ops = new Stack<string>();
            while (tokens.Count != 0)
            {
                // If symbol is operand, push it
                if (!this.operators.ContainsKey(tokens.Peek()))
                {
                    this.postFixTokens.Enqueue(tokens.Dequeue());
                }

                // If symbol is left parenthese, put into operand stack
                else if (tokens.Peek() == "(")
                {
                    ops.Push(tokens.Dequeue());
                }

                //// If symbol is right paranthese, pop all ops until left parenthese
                else if (tokens.Peek() == ")")
                {
                    tokens.Dequeue();
                    string opsToken = ops.Pop();
                    while (opsToken != "(")
                    {
                        this.postFixTokens.Enqueue(opsToken);
                        opsToken = ops.Pop();
                    }
                }
                else if (this.operators.ContainsKey(tokens.Peek()))
                {
                    // If ops stack is empty or top value is (
                    if (ops.Count == 0 || ops.Peek() == "(" || ops.Peek() == tokens.Peek())
                    {
                        ops.Push(tokens.Dequeue());
                    }

                    // If new token's precedence value is higher than ops' top precedence vlaue
                    else if (this.operators[tokens.Peek()] > this.operators[ops.Peek()])
                    {
                        ops.Push(tokens.Dequeue());
                    }
                    ////// If new token's precedence value is lower than ops' top precedence value
                    else if (this.operators[tokens.Peek()] < this.operators[ops.Peek()])
                    {
                        // continually pop until the top value is less than the new token
                        while (this.operators[tokens.Peek()] < this.operators[ops.Peek()])
                        {
                            this.postFixTokens.Enqueue(ops.Pop());
                            if (ops.Count == 0)
                            {
                                break;
                            }
                        }

                        ops.Push(tokens.Dequeue());
                    }
                }
            }
            //Test
            while (ops.Count != 0)
            {
                this.postFixTokens.Enqueue(ops.Pop());
            }
        }

        /// <summary>
        /// Gets or sets dictionary property to store variables and their values.
        /// </summary>
        public Dictionary<string, double> Variables { get => this.variables; set => this.variables = value; }

        /// <summary>
        /// Constructs the tree from the postfix tokens.
        /// </summary>
        public void Construct()
        {
            Stack<ExpressionTreeNode> nodes = new Stack<ExpressionTreeNode>();
            while (this.postFixTokens.Count != 0)
            {
                // If the current token isn't an operator
                if (!this.operators.ContainsKey(this.postFixTokens.Peek()))
                {
                    string currentToken = this.postFixTokens.Dequeue();
                    double constant;

                    // If its a constant, create node, push to stack
                    if (double.TryParse(currentToken, out constant))
                    {
                        ConstantNode node = new ConstantNode(constant);
                        nodes.Push(node);
                    }

                    // If its a variable, create node, push to stack
                    else
                    {
                        if (!this.variables.ContainsKey(currentToken))
                        {
                            this.variables.Add(currentToken, 0);
                        }

                        VariableNode node = new VariableNode(currentToken, ref this.variables);
                        nodes.Push(node);
                    }
                }
                else
                {
                    if (nodes.Count >= 2)
                    {
                        ExpressionTreeNode node1 = nodes.Pop();
                        ExpressionTreeNode node2 = nodes.Pop();
                        OperatorNodeFactory opFactory = new OperatorNodeFactory();
                        OperatorNode opNode = opFactory.CreateOperatorNode(this.postFixTokens.Dequeue().ToCharArray()[0]);
                        opNode.Right = node1;
                        opNode.Left = node2;
                        this.root = opNode;
                        nodes.Push(opNode);
                    }
                    else
                    {
                        throw new Exception("Invalid Sequence");
                    }
                }
            }
        }

        /// <summary>
        /// Method I created for testing construction.
        /// </summary>
        /// <returns>The stack values.</returns>
        public string CheckStack()
        {
            string returnString = string.Empty;
            while (this.postFixTokens.Count != 0)
            {
                returnString += this.postFixTokens.Dequeue();
            }

            return returnString;
        }

        /// <summary>
        /// Evaluates the tree by calling each node's evaluate mehtod.
        /// </summary>
        /// <returns>The value of the evaluated expression tree.</returns>
        public double Evaluate()
        {
            this.Construct();
            return this.root.Evaluate();
        }

        /// <summary>
        /// Takes the string expression and seperated into tokens.
        /// </summary>
        /// <param name="expression">The expression string inputted by the user.</param>
        /// <returns>A queue of tokens from the expression.</returns>
        public Queue<string> Tokenize(string expression)
        {
            Queue<string> tokens = new Queue<string>();
            for (int i = 0; i <= expression.Length - 1; i++)
            {
                string thisToken = string.Empty;
                char current = expression[i];

                // In case of non operator token (i.e. variables)
                if (!this.operators.ContainsKey(expression[i].ToString()))
                {
                    while (true)
                    {
                        thisToken += expression[i];

                        // Checks if next char in string is either end of string or an operator.
                        if ((i + 1) == expression.Length || this.operators.ContainsKey(expression[i + 1].ToString()))
                        {
                            break;
                        }

                        i++;
                    }

                    tokens.Enqueue(thisToken);
                }
                else
                {
                    tokens.Enqueue(expression[i].ToString());
                }
            }

            return tokens;
        }
    }
}
