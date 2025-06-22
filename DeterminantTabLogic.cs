using System.Windows;
using System.Windows.Controls;

namespace MatrixUtilsNetFX
{
    internal static class DeterminantTabLogic
    {
        internal static bool validateDimensionsTextBox(string dimensionsText)
        {
            return !(
                string.IsNullOrEmpty(dimensionsText) ||
                !long.TryParse(dimensionsText, out _) ||
                long.Parse(dimensionsText) < 1 ||
                long.Parse(dimensionsText) > 7
            );
        }
        internal static void generateEntryGrid(Grid subGrid, string dimensionsText)
        {
            subGrid.Children.Clear();

            subGrid.RowDefinitions.Clear();
            subGrid.ColumnDefinitions.Clear();

            long dimensions = long.Parse(dimensionsText);

            for (int x = 0; x < dimensions; ++x)
            {
                subGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < dimensions; ++y)
            {
                subGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < dimensions; ++i)
            {
                for (int j = 0; j < dimensions; ++j)
                {
                    TextBox textBox = new TextBox
                    {
                        MinWidth = 25,
                        Height = 25,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Name = $"TextBox_R{i + 1}C{j + 1}",
                        MaxLength = 7
                    };
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    subGrid.Children.Add(textBox);
                }
            }
        }

        internal static void findDeterminant(Grid subGrid, string dimensionsText)
        {
            foreach (TextBox txt in subGrid.Children)
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

            long dimensions = long.Parse(dimensionsText);

            decimal[,] matrix = new decimal[dimensions, dimensions];

            foreach (TextBox txt in subGrid.Children)
            {
                for (int i = 0; i < dimensions; ++i)
                {
                    for (int j = 0; j < dimensions; ++j)
                    {
                        if (txt.Name == $"TextBox_R{i + 1}C{j + 1}")
                        {
                            matrix[i, j] = decimal.Parse(txt.Text);
                        }
                    }
                }
            }

            MessageBox.Show($"Determinant: {MatrixOperations.calculateDeterminant(matrix)}", "Answer");
        }
    }
}