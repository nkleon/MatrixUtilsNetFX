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

namespace MatrixDeterminant
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

        private void btnGenerateGrid_Click(object sender, RoutedEventArgs e)
        {
            subGrid.Children.Clear();

            subGrid.RowDefinitions.Clear();
            subGrid.ColumnDefinitions.Clear();

            long dimensions = long.Parse(txtDimensions.Text);

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
                    var textBox = new TextBox
                    {
                        Width = 50,
                        Height = 50,
                        Name = $"TextBox_R{i + 1}C{j + 1}"
                    };
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    subGrid.Children.Add(textBox);
                }
            }
            btnGetDeterminant.IsEnabled = true;
        }

        private void btnGetDeterminant_Click(object sender, RoutedEventArgs e)
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

            long dimensions = long.Parse(txtDimensions.Text);

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

            MessageBox.Show($"The determinant is {determinant(matrix)}", "Answer");
        }

        private void txtDimensions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDimensions.Text) || !long.TryParse(txtDimensions.Text, out _) || long.Parse(txtDimensions.Text) < 1)
            {
                btnGenerateGrid.IsEnabled = false;
                btnGetDeterminant.IsEnabled = false;
                return;
            }
            btnGenerateGrid.IsEnabled = true;
        }

        private decimal determinant(decimal[,] matrix)
        {
            int dimensions = matrix.GetLength(0);
            decimal det = 1;
            decimal constant = new decimal();
            decimal[] temp = new decimal[dimensions];

            for (int i = 1; i < dimensions; i++)
            {
                for (int j = i; j < dimensions; j++)
                {
                    for (int k = 0; k < dimensions; k++)
                    {
                        for (int p = 0; p < dimensions; p++)
                        {
                            if (matrix[i - 1, p] != 0)
                            {
                                constant = matrix[j, p] / matrix[i - 1, p];
                                break;
                            }
                        }
                    }

                    for (int k = 0; k < dimensions; k++)
                    {
                        for (int m = 0; m < dimensions; m++)
                        {
                            temp[m] = matrix[i - 1, m];
                        }
                        temp[k] *= constant;
                        matrix[j, k] -= temp[k];
                    }
                }
            }

            for (int i = 0; i < dimensions; i++)
            {
                det *= matrix[i, i];
            }

            return det;
        }
    }
}
