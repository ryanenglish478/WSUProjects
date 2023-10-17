// <copyright file="SubtractionOperatorNode.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    /// <summary>
    /// The class for the subtraction opNode.
    /// </summary>
    public class SubtractionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionOperatorNode"/> class.
        /// </summary>
        public SubtractionOperatorNode()
        {
        }

        /// <summary>
        /// Gets the char associated with subtraction op.
        /// </summary>
        public static char Operator => '-';

        /// <summary>
        /// Gets the Precedence value of this op.
        /// </summary>
        public static ushort Precedence => 8;

        /// <summary>
        /// Gets the Associatvity value of this op (left).
        /// </summary>
        public static OperatorAssociativity Associativity => OperatorAssociativity.Left;

        /// <summary>
        /// Evaluates the expression.
        /// </summary>
        /// <returns>Left child - right child.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
