// <copyright file="SpreadsheetCell.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace Spreadsheet_Ryan_English
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Seperate class that inherits from cell class to allow Spreadsheet class to create cell instances.
    /// </summary>
    internal class SpreadsheetCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
        /// </summary>
        /// <param name="row">row index of cell.</param>
        /// <param name="column">column index of cell.</param>
        /// <param name="value">value of the cell.</param>
        public SpreadsheetCell(int column, int row, string value)
            : base(column, row)
        {
        }
    }
}
