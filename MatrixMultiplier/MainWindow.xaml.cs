using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatrixMultiplier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtRowsA.Text) ||
                string.IsNullOrEmpty(txtColsA.Text) ||
                string.IsNullOrEmpty(txtRowsB.Text) ||
                string.IsNullOrEmpty(txtColsB.Text)
                )
            {
                MessageBox.Show("Ensure all textboxes are filled.");
                return;
            }

            if (!long.TryParse(txtRowsA.Text, out _) ||
                !long.TryParse(txtColsA.Text, out _) ||
                !long.TryParse(txtRowsB.Text, out _) ||
                !long.TryParse(txtColsB.Text, out _)
                )
            {
                MessageBox.Show("Ensure all textboxes contain only integers.");
                return;
            }

            if (long.Parse(txtRowsA.Text) < 1 ||
                long.Parse(txtColsA.Text) < 1 ||
                long.Parse(txtRowsB.Text) < 1 ||
                long.Parse(txtColsB.Text) < 1
                )
            {
                MessageBox.Show("Ensure all textboxes contain only positive integers.");
                return;
            }

            if (!txtColsA.Text.Equals(txtRowsB.Text))
            {
                MessageBox.Show("Ensure number of columns in matrix A is equal to that of rows im matrix B");
                return;
            }

            generateEntryGrid();
        }

        private void generateEntryGrid()
        {
            subGridA.Children.Clear();
            subGridB.Children.Clear();

            subGridA.RowDefinitions.Clear();
            subGridA.ColumnDefinitions.Clear();
            subGridB.RowDefinitions.Clear();
            subGridB.ColumnDefinitions.Clear();

            long rowsA = long.Parse(txtRowsA.Text);
            long colsA = long.Parse(txtColsA.Text);
            long rowsB = long.Parse(txtRowsB.Text);
            long colsB = long.Parse(txtColsB.Text);

            for (int x = 0; x < rowsA; ++x)
            {
                subGridA.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < colsA; ++y)
            {
                subGridA.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int x = 0; x < rowsB; ++x)
            {
                subGridB.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < colsB; ++y)
            {
                subGridB.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rowsA; ++i)
            {
                for (int j = 0; j < colsA; ++j)
                {
                    var textBox = new TextBox
                    {
                        Width = 40,
                        Height = 40,
                        Name = $"TextBox_R{i + 1}C{j + 1}"
                    };
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    subGridA.Children.Add(textBox);
                }
            }

            for (int i = 0; i < rowsB; ++i)
            {
                for (int j = 0; j < colsB; ++j)
                {
                    var textBox = new TextBox
                    {
                        Width = 40,
                        Height = 40,
                        Name = $"TextBox_R{i + 1}C{j + 1}"
                    };
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    subGridB.Children.Add(textBox);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (TextBox txt in subGridA.Children)
            {
                if (string.IsNullOrEmpty(txt.Text))
                {
                    MessageBox.Show("Ensure all textboxes are filled.");
                    return;
                }
                else if (!decimal.TryParse(txt.Text, out _))
                {
                    MessageBox.Show("Ensure all textboxes contain only integers or real numbers.");
                    return;
                }
            }

            long rowsA = long.Parse(txtRowsA.Text);
            long colsA = long.Parse(txtColsA.Text);
            long rowsB = long.Parse(txtRowsB.Text);
            long colsB = long.Parse(txtColsB.Text);

            decimal[,] matrixA = new decimal[rowsA, colsA];
            decimal[,] matrixB = new decimal[rowsB, colsB];

            foreach (TextBox txt in subGridA.Children)
            {
                for (int i = 0; i < rowsA; ++i)
                {
                    for (int j = 0; j < colsA; ++j)
                    {
                        if (txt.Name == $"TextBox_R{i + 1}C{j + 1}")
                        {
                            matrixA[i, j] = decimal.Parse(txt.Text);
                        }
                    }
                }
            }

            foreach (TextBox txt in subGridB.Children)
            {
                for (int i = 0; i < rowsB; ++i)
                {
                    for (int j = 0; j < colsB; ++j)
                    {
                        if (txt.Name == $"TextBox_R{i + 1}C{j + 1}")
                        {
                            matrixB[i, j] = decimal.Parse(txt.Text);
                        }
                    }
                }
            }

            new ResultWindow(multiply(matrixA, matrixB)).ShowDialog();
        }

        private decimal[,] multiply(decimal[,] matrixA, decimal[,] matrixB)
        {
            int ans_rows = matrixA.GetLength(0);
            int ans_cols = matrixB.GetLength(1);
            decimal[,] ans = new decimal[ans_rows, ans_cols];
            for (int row = 0; row < ans_rows; ++row)
            {
                for (int col = 0; col < ans_cols; ++col)
                {
                    for (int n = 0; n < ans_cols; ++n)
                    {
                        ans[row, col] += matrixA[row, n] * matrixB[n, col];
                    }
                }
            }
            return ans;
        }
    }
}
