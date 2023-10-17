// <copyright file="Cell.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace Spreadsheet_Ryan_English
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Cell class to describe the cell in the spreadsheet.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// text field holds the value of the cell.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        private protected string text;
#pragma warning restore SA1401 // Fields should be private
        /// <summary>
        /// Repersents the evaluated value of the cell.
        /// </summary>
        private protected string value;
        private readonly int rowIndex;
        private readonly int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">index of the row.</param>
        /// <param name="columnIndex">index of the column.</param>
        protected Cell(int columnIndex, int rowIndex)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets or sets returns the text
        /// Sets checks if value changed, if it did updates text value and propertychanged event.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value == this.text)
                {
                    return;
                }
                else if (value == null)
                {
                    this.text = "";
                }
                else { this.text = value; }
                this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

        /// <summary>
        /// Gets the value field.
        /// </summary>
        public string Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// Gets returns the rowIndex.
        /// </summary>
        public int RowIndex => this.rowIndex;

        /// <summary>
        /// Gets returns the columnIndex.
        /// </summary>
        public int ColumnIndex => this.columnIndex;
    }
}
