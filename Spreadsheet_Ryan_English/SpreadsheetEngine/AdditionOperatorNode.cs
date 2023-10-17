// <copyright file="AdditionOperatorNode.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Net.NetworkInformation;
    using System.Text;

    /// <summary>
    /// The class for the addition node.
    /// </summary>
    public class AdditionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionOperatorNode"/> class.
        /// </summary>
        public AdditionOperatorNode()
        {
        }

        /// <summary>
        /// Gets the char associated with addition.
        /// </summary>
        public static char Operator => '+';

        /// <summary>
        /// Gets the precedence of the addition node.
        /// </summary>
        public static ushort Precedence => 7;

        /// <summary>
        /// Gets the Assocativity value of the addition node.
        /// </summary>
        public static OperatorAssociativity Associativity => OperatorAssociativity.Left;

        /// <summary>
        /// Adds the left and right child.
        /// </summary>
        /// <returns>The value of the left child + right child.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
