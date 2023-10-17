// <copyright file="DivisionOperatorNode.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// The class for the division operator node.
    /// </summary>
    public class DivisionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets level of precedence for division.
        /// </summary>
        public static ushort Precedence => 6;

        /// <summary>
        /// Gets denotes the division operator.
        /// </summary>
        public static char Operator
        {
            get
            {
                return '/';
            }
        }

        /// <summary>
        /// Gets the operator associatvity.
        /// </summary>
        public static OperatorAssociativity Associativity => OperatorAssociativity.Left;

        /// <summary>
        /// Evaluates the nodes via division.
        /// </summary>
        /// <returns>the divided double value.</returns>
        public override double Evaluate()
        {
            if (this.Right.Evaluate() == 0)
            {
                throw new System.Exception("Error! Dividing by 0!");
            }

            return this.Left.Evaluate() / this.Right.Evaluate();
        }
    }
}
