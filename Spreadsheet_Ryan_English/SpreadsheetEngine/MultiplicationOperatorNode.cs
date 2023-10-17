// <copyright file="MultiplicationOperatorNode.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// The class for the multiplication opNode.
    /// </summary>
    public class MultiplicationOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets level of precedence for division.
        /// </summary>
        public static ushort Precedence => 5;

        /// <summary>
        /// Gets denotes the division operator.
        /// </summary>
        public static char Operator
        {
            get
            {
                return '*';
            }
        }

        /// <summary>
        /// Gets the operator associatvity.
        /// </summary>
        public static OperatorAssociativity Associativity => OperatorAssociativity.Left;

        /// <summary>
        /// Evaluates the nodes via multiplication.
        /// </summary>
        /// <returns>the multiplied double value.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
