using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MatrixUtilsNetFX
{
    internal static class SubGridUtilities
    {
        internal static void ClearSubGridContents(UniformGrid subGrid)
        {
            subGrid.Children.Clear();
            subGrid.Rows = 0;
            subGrid.Columns = 0;
        }

        internal static void PopulateSubGrid(UniformGrid subGrid, int rows, int cols)
        {
            ClearSubGridContents(subGrid);

            subGrid.Rows = rows;
            subGrid.Columns = cols;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Name = $"TextBox_R{i + 1}C{j + 1}",
                        MaxLength = 7,
                    };
                    textBox.SizeChanged += SubGridTextBoxes_SizeOrTextChanged;
                    textBox.TextChanged += SubGridTextBoxes_SizeOrTextChanged;
                    subGrid.Children.Add(textBox);
                }
            }
        }

        private static void PopulateSubGridFromMatrix(UniformGrid subGrid, string[,] stringMatrix)
        {
            ClearSubGridContents(subGrid);
            int rows = stringMatrix.GetLength(0);
            int cols = stringMatrix.GetLength(1);

            subGrid.Rows = rows;
            subGrid.Columns = cols;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Name = $"TextBox_R{i + 1}C{j + 1}",
                        MaxLength = 7,
                    };
                    textBox.SizeChanged += SubGridTextBoxes_SizeOrTextChanged;
                    textBox.TextChanged += SubGridTextBoxes_SizeOrTextChanged;
                    subGrid.Children.Add(textBox);
                    textBox.Text = stringMatrix[i, j];
                }
            }
        }

        internal static decimal[,] GetSubGridTextBoxValuesDecimal(UniformGrid subGrid)
        {
            int rows = subGrid.Rows;
            int cols = subGrid.Columns;

            decimal[,] matrix = new decimal[rows, cols];

            foreach (TextBox txt in subGrid.Children)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (txt.Name == $"TextBox_R{i + 1}C{j + 1}")
                        {
                            matrix[i, j] = decimal.Parse(txt.Text);
                        }
                    }
                }
            }

            return matrix;
        }

        internal static string[,] GetSubGridTextBoxValues_2dStrArray(UniformGrid subGrid)
        {
            int rows = subGrid.Rows;
            int cols = subGrid.Columns;

            string[,] matrix = new string[rows, cols];

            foreach (TextBox txt in subGrid.Children)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (txt.Name == $"TextBox_R{i + 1}C{j + 1}")
                        {
                            matrix[i, j] = txt.Text;
                        }
                    }
                }
            }

            return matrix;
        }

        internal static string[] GetSubGridTextBoxValues_1dStrArray(UniformGrid subGrid)
        {
            int index = 0;
            string[] array = new string[subGrid.Rows * subGrid.Columns];
            foreach (string str in GetSubGridTextBoxValues_2dStrArray(subGrid))
            {
                array[index++] = str;
            }
            return array;
        }

        internal static void SwapSubGrids(UniformGrid subGridA, UniformGrid subGridB)
        {
            string[,] subGridAValuesString = GetSubGridTextBoxValues_2dStrArray(subGridA);
            string[,] subGridBValuesString = GetSubGridTextBoxValues_2dStrArray(subGridB);

            PopulateSubGridFromMatrix(subGridA, subGridBValuesString);
            PopulateSubGridFromMatrix(subGridB, subGridAValuesString);
        }

        private static void SubGridTextBoxes_SizeOrTextChanged(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;
            if (/*!tb.IsLoaded || */tb.ActualHeight <= 0 || tb.ActualWidth <= 0) return;

            int charCount = tb.Text.Length;
            if (charCount == 0) charCount = 1; // Prevent division by zero

            // Use a placeholder while empty so we don't divide by zero / get a runaway font size
            string text = string.IsNullOrEmpty(tb.Text) ? "0" : tb.Text;

            // Subtract padding/border so text doesn't hug the edges
            double hPad = tb.Padding.Left + tb.Padding.Right + tb.BorderThickness.Left + tb.BorderThickness.Right + 4;
            double vPad = tb.Padding.Top + tb.Padding.Bottom + tb.BorderThickness.Top + tb.BorderThickness.Bottom + 4;

            double availableWidth = Math.Max(1, tb.ActualWidth - hPad);
            double availableHeight = Math.Max(1, tb.ActualHeight - vPad);

            // --- Height constraint (same idea as before) ---
            double maxFontSizeFromHeight = availableHeight * 0.8;
            double minFontSize = Math.Min(8.0, availableHeight * 0.15);

            // --- Width constraint: measure actual text width at a reference size, then scale ---
            double referenceFontSize = 100.0;
            Typeface typeface = new Typeface(tb.FontFamily, tb.FontStyle, tb.FontWeight, tb.FontStretch);
            FormattedText formattedText = new FormattedText(
                    text,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    typeface,
                    referenceFontSize,
                    Brushes.Black,
                    VisualTreeHelper.GetDpi(tb).PixelsPerDip
                );

            double widthAtReferenceSize = formattedText.Width;
            double maxFontSizeFromWidth = (availableWidth / widthAtReferenceSize) * referenceFontSize;

            // --- Combine: whichever dimension is tighter wins ---
            double calculatedSize = Math.Min(maxFontSizeFromHeight, maxFontSizeFromWidth);

            tb.FontSize = Math.Max(minFontSize, calculatedSize);
        }
    }
}
