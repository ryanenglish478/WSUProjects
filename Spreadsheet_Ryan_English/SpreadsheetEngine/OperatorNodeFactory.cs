// <copyright file="OperatorNodeFactory.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace ExpressionTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// The class for the creation of the specific operator nodes.
    /// </summary>
    public class OperatorNodeFactory
    {
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Creates the specific operator node based on char op.
        /// </summary>
        /// <param name="op">The operator node's character (i.e. +,- etc.).</param>
        /// <returns>The correct operatorNode class object.</returns>
        public OperatorNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new Exception("Unhandled operator");
        }

        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            // get the type declaration of OperatorNode
            Type operatorNodeType = typeof(OperatorNode);

            // Iterate over all loaded assemblies:
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our OepratorNode class using LINQ
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));
                foreach (var type in operatorTypes)
                {
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(type);
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
