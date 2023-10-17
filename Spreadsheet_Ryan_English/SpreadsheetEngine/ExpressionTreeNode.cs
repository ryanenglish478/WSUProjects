// <copyright file="ExpressionTreeNode.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    /// <summary>
    /// Abstract Node class.
    /// </summary>
    public abstract class ExpressionTreeNode
    {
        /// <summary>
        /// Abstract evaluate method implemented in children.
        /// </summary>
        /// <returns>the value of the evaluated child node.</returns>
        public abstract double Evaluate();
    }
}
