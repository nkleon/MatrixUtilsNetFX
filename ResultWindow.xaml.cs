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
using System.Windows.Shapes;

namespace MatrixUtilsNetFX
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow()
        {
            InitializeComponent();
        }

        public ResultWindow(decimal[,] entry)
        {
            InitializeComponent();

            int rows = entry.GetLength(0);
            int cols = entry.GetLength(1);

            decimal[,] result = new decimal[rows, cols];

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    result[i, j] = entry[i, j];
                }
            }

            for (int x = 0; x < rows; ++x)
            {
                ResultGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < cols; ++y)
            {
                ResultGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    var textBox = new TextBox
                    {
                        //Width = 40,
                        Height = 25,
                        MinWidth = 25,
                        Name = $"TextBox_R{i + 1}C{j + 1}",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        IsReadOnly = true
                    };
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    ResultGrid.Children.Add(textBox);
                }
            }

            foreach (TextBox textBox in ResultGrid.Children)
            {
                for (int i = 0; i < rows; ++i)
                {
                    for (int j = 0; j < cols; ++j)
                    {
                        if (textBox.Name == $"TextBox_R{i + 1}C{j + 1}")
                        {
                            textBox.Text = result[i, j].ToString();
                        }
                    }
                }
            }
        }
    }
}
