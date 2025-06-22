using System.Windows;
using System.Windows.Controls;

namespace MatrixUtilsNetFX
{
    internal static class MultiplyTabLogic
    {
        internal static bool validateDimensionsTextBoxes(string txtRowsA, string txtColsA, string txtRowsB, string txtColsB)
        {
            return !(
                string.IsNullOrEmpty(txtRowsA) ||
                string.IsNullOrEmpty(txtColsA) ||
                string.IsNullOrEmpty(txtRowsB) ||
                string.IsNullOrEmpty(txtColsB) ||

                !long.TryParse(txtRowsA, out _) ||
                !long.TryParse(txtColsA, out _) ||
                !long.TryParse(txtRowsB, out _) ||
                !long.TryParse(txtColsB, out _) ||

                long.Parse(txtRowsA) < 1 ||
                long.Parse(txtColsA) < 1 ||
                long.Parse(txtRowsB) < 1 ||
                long.Parse(txtColsB) < 1 ||

                long.Parse(txtRowsA) > 7 ||
                long.Parse(txtColsA) > 7 ||
                long.Parse(txtRowsB) > 7 ||
                long.Parse(txtColsB) > 7 ||

                !txtColsA.Equals(txtRowsB)
            );
        }

        internal static void generateEntryGrids(Grid subGridA, Grid subGridB, string txtRowsA, string txtColsA, string txtRowsB, string txtColsB)
        {
            subGridA.Children.Clear();
            subGridB.Children.Clear();

            subGridA.RowDefinitions.Clear();
            subGridA.ColumnDefinitions.Clear();
            subGridB.RowDefinitions.Clear();
            subGridB.ColumnDefinitions.Clear();

            long rowsA = long.Parse(txtRowsA);
            long colsA = long.Parse(txtColsA);
            long rowsB = long.Parse(txtRowsB);
            long colsB = long.Parse(txtColsB);

            for (int x = 0; x < rowsA; x++)
            {
                subGridA.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < colsA; y++)
            {
                subGridA.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int x = 0; x < rowsB; x++)
            {
                subGridB.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < colsB; y++)
            {
                subGridB.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsA; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        Height = 25,
                        MinWidth = 25,
                        Name = $"TextBox_R{i + 1}C{j + 1}",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MaxLength = 7
                    };
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    subGridA.Children.Add(textBox);
                }
            }

            for (int i = 0; i < rowsB; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        Height = 25,
                        MinWidth = 25,
                        Name = $"TextBox_R{i + 1}C{j + 1}",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        MaxLength = 7
                    };
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    subGridB.Children.Add(textBox);
                }
            }
        }

        internal static void findProduct(Grid subGridA, Grid subGridB, string txtRowsA, string txtColsA, string txtRowsB, string txtColsB)
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

            long rowsA = long.Parse(txtRowsA);
            long colsA = long.Parse(txtColsA);
            long rowsB = long.Parse(txtRowsB);
            long colsB = long.Parse(txtColsB);

            decimal[,] matrixA = new decimal[rowsA, colsA];
            decimal[,] matrixB = new decimal[rowsB, colsB];

            foreach (TextBox txt in subGridA.Children)
            {
                for (int i = 0; i < rowsA; i++)
                {
                    for (int j = 0; j < colsA; j++)
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
                for (int i = 0; i < rowsB; i++)
                {
                    for (int j = 0; j < colsB; j++)
                    {
                        if (txt.Name == $"TextBox_R{i + 1}C{j + 1}")
                        {
                            matrixB[i, j] = decimal.Parse(txt.Text);
                        }
                    }
                }
            }

            new ResultWindow(MatrixOperations.multiplyMatrices(matrixA, matrixB)).ShowDialog();
        }
    }
}