// <copyright file="Form1.cs" company="Ryan English 11617228">
// Copyright (c) Ryan English 11617228. All rights reserved.
// </copyright>

namespace Spreadsheet_Ryan_English
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// test.
    /// </summary>
    public partial class Form1 : Form
    {
        private Spreadsheet spreadsheet1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();
            this.InitializeDataGrid();
            this.spreadsheet1 = new Spreadsheet(26, 50);
            this.spreadsheet1.PropertyChanged += this.CellChanged;
        }
        
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private void CellChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(sender.GetType());
            Console.WriteLine(e.GetType());
            if (sender is Cell)
            {
                this.dataGridView1.Rows[((Cell)sender).RowIndex].Cells[((Cell)sender).ColumnIndex].Value = ((Cell)sender).Value;
            }
        }

        private void InitializeDataGrid()
        {
            string[] abc = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            foreach (string letter in abc)
            {
                this.dataGridView1.Columns.Add(letter, letter);
            }

            for (int i = 0; i <= 50; i++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
            }
        }

       
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                int randCol = rnd.Next(26);
                int randRow = rnd.Next(49);
                Cell updateCell = this.spreadsheet1.GetCell(randCol, randRow);
                this.spreadsheet1.GetCell(randCol, randRow).Text = "AHHHHHH";
                this.PropertyChanged(updateCell, new PropertyChangedEventArgs("Text"));
            }

            for (int i = this.dataGridView1.RowCount - 1 - 1; i >= 0; i--)
            {
                Cell updateCell = this.spreadsheet1.GetCell(1, i);
                updateCell.Text = "This is cell B" + i.ToString();
                this.PropertyChanged(updateCell, new PropertyChangedEventArgs("Text"));
            }

            for (int i = 0; i < this.dataGridView1.RowCount - 1; ++i)
            {
                Cell updateCell = this.spreadsheet1.GetCell(0, i);
                updateCell.Text = "=B" + i;
                this.PropertyChanged(updateCell, new PropertyChangedEventArgs("Text"));
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            string cellText = this.spreadsheet1.GetCell(e.ColumnIndex, e.RowIndex).Text;
            if (cellText == null)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
            }
            else
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellText;
            }
        }
    }
}
