// <copyright file="Spreadsheet.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace Spreadsheet_Ryan_English
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// Will serve as the container for the 2D array of cells in the spreadsheet.
    /// </summary>
    public class Spreadsheet : INotifyPropertyChanged
    {
        private Cell[,] cells;
        private int columnCount;
        private int rowCount;
        private IDictionary<char, int> letterToInt = new Dictionary<char, int>()
        {
            { 'A', 0 },
            { 'B', 1 },
            { 'C', 2 },
            { 'D', 3 },
            { 'E', 4 },
            { 'F', 5 },
            { 'G', 6 },
            { 'H', 7 },
            { 'I', 8 },
            { 'J', 9 },
            { 'K', 10 },
            { 'L', 11 },
            { 'M', 12 },
            { 'N', 13 },
            { 'O', 14 },
            { 'P', 15 },
            { 'Q', 16 },
            { 'R', 17 },
            { 'S', 18 },
            { 'T', 19 },
            { 'U', 20 },
            { 'V', 21 },
            { 'W', 22 },
            { 'X', 23 },
            { 'Y', 24 },
            { 'Z', 25 },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the spreadsheet.</param>
        /// <param name="columns">The number of columns in the spreadsheet.</param>
        public Spreadsheet(int columns, int rows)
        {
            this.columnCount = columns;
            this.rowCount = rows;
            this.cells = new Cell[columns, rows];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    this.cells[i, j] = new SpreadsheetCell(i, j, string.Empty);
                    
                    this.cells[i, j].PropertyChanged += this.CellPropertyChanged;
                    this.GetCell(i, j).Text = string.Empty;
                   
                }
            }

            
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the number of columns in the spreadsheet.
        /// </summary>
        public int ColumnCount
        {
            get { return this.columnCount; }
        }

        /// <summary>
        /// Gets the number of rows in the spreadsheet.
        /// </summary>
        public int RowCount
        {
            get { return this.rowCount; }
        }

        /// <summary>
        /// Returns the cell at the specified indices.
        /// </summary>
        /// <param name="columnIndex">The columnIndex of the cell.</param>
        /// <param name="rowIndex">The rowIndex of the cell.</param>
        /// <returns>The cell at the specified indices.</returns>
        public Cell GetCell(int columnIndex, int rowIndex)
        {
            if (rowIndex > this.rowCount || columnIndex > this.columnCount)
            {
                return null;
            }
            else
            {
                return this.cells[columnIndex, rowIndex];
            }
        }

        /// <summary>
        /// Updates the value of the cell if the text is changed.
        /// </summary>
        /// <param name="sender">The cell.</param>
        /// <param name="e">Arguments.</param>
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell senderCell;
            int rowI = ((Cell)sender).RowIndex;
            int colI = ((Cell)sender).ColumnIndex;
            if (sender is Cell)
            {
                senderCell = this.GetCell(colI, rowI);
                SpreadsheetCell updateCell;

                // If text is not set, text = ""
                if(senderCell.Text == null || senderCell.Text == String.Empty)
                {
                    updateCell = new SpreadsheetCell(colI, rowI, "");
                    this.cells[colI, rowI] = updateCell;
                }
                // If the text of the sendercell begins with '=', we evaluate the cell that comes after '='.
                else if (senderCell.Text[0] == '=')
                {
                    string a = senderCell.Text;
                    int b = this.letterToInt[senderCell.Text[1]];
                    int c = int.Parse(senderCell.Text.Substring(2));

                    int updateColI = this.letterToInt[senderCell.Text[1]];
                    int updateRowI = int.Parse(senderCell.Text.Substring(2));
                    string updateVal = this.GetCell(updateColI, updateRowI).Value;
                    updateCell = new SpreadsheetCell(colI, rowI, updateVal);
                    if (updateVal != null)
                    {
                        senderCell.Text = updateVal;
                    }
                    this.cells[colI, rowI] = updateCell;
                }
                else
                {
                    updateCell = new SpreadsheetCell(colI, rowI, senderCell.Text);
                    updateCell.Text = senderCell.Text;
                    this.cells[colI, rowI] = updateCell;
                }


                this.PropertyChanged(updateCell, new PropertyChangedEventArgs(senderCell.RowIndex.ToString() + "," + senderCell.ColumnIndex.ToString()
                    + "," + senderCell.Value));
            }
        }
    }
}
