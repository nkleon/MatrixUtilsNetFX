using System.Windows;
using System.Windows.Controls;

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

            ResultGrid.Rows = rows;
            ResultGrid.Columns = cols;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    TextBox textBox = new TextBox
                    {
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Height = 25,
                        MinWidth = 25,
                        Name = $"TextBox_R{i + 1}C{j + 1}",
                        Text = entry[i, j].ToString(),
                        IsReadOnly = true,
                    };
                    ResultGrid.Children.Add(textBox);
                }
            }
        }
    }
}
