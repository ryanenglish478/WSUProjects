// <copyright file="ConstantNode.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The class for the constant's nodes.
    /// </summary>
    internal class ConstantNode : ExpressionTreeNode
    {
        private readonly double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">The value of the constant.</param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <inheritdoc/>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
