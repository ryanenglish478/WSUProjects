// <copyright file="VariableNode.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The class for the variables.
    /// </summary>
    internal class VariableNode : ExpressionTreeNode
    {
        private readonly string name;

        private Dictionary<string, double> variables;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="variables">The value of the variable.</param>
        public VariableNode(string name, ref Dictionary<string, double> variables)
        {
            this.name = name;
            this.variables = variables;
        }

        /// <inheritdoc/>
        public override double Evaluate()
        {
            double value = 0.0;
            if (this.variables.ContainsKey(this.name))
            {
                value = this.variables[this.name];
            }

            return value;
        }
    }
}
