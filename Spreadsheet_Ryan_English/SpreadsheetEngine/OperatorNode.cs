// <copyright file="OperatorNode.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The operatorNode meant for operators (,),+,-,*,/.
    /// </summary>
    public abstract class OperatorNode : ExpressionTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        public OperatorNode()
        {
            this.Left = this.Right = null;
        }

        /// <summary>
        /// Sets the associativity for postfix order.
        /// </summary>
        public enum OperatorAssociativity
        {
            /// <summary>
            /// right asssociativity for postfix ordr.
            /// </summary>
            Right,

            /// <summary>
            /// Left associativity for postfix order.
            /// </summary>
            Left,
        }

        /// <summary>
        /// Gets or sets the Left child of this node.
        /// </summary>
        ///
        public ExpressionTreeNode Left { get; set; }

        /// <summary>
        /// Gets or sets the Right child of this node.
        /// </summary>
        public ExpressionTreeNode Right { get; set; }
    }
}
