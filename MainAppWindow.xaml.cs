using System.Windows;
using System.Windows.Controls;

namespace MatrixUtilsNetFX
{
    /// <summary>
    /// Interaction logic for MainAppWindow.xaml
    /// </summary>
    public partial class MainAppWindow : Window
    {
        public MainAppWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handlers for Determinant functionality
        /// </summary>

        private void Determinant_btnGenerateEntryGrid_Click(object sender, RoutedEventArgs e)
        {
            DeterminantTabLogic.generateEntryGrid(Determinant_subGridMatrix, Determinant_txtDimensions.Text);
            Determinant_btnFindDeterminant.IsEnabled = true;
        }

        private void Determinant_btnFIndDeterminant_Click(object sender, RoutedEventArgs e)
        {
            DeterminantTabLogic.findDeterminant(Determinant_subGridMatrix, Determinant_txtDimensions.Text);
        }

        private void Determinant_txtDimensions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!DeterminantTabLogic.validateDimensionsTextBox(Determinant_txtDimensions.Text))
            {
                Determinant_btnGenerateEntryGrid.IsEnabled = false;
                Determinant_btnFindDeterminant.IsEnabled = false;
                return;
            }
            Determinant_btnGenerateEntryGrid.IsEnabled = true;
        }

        /// <summary>
        /// Event handlers for Addition/Subtraction functionality
        /// </summary>

        private void AddMinus_txtsDimensions_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!AddMinusTabLogic.validateDimensionsTextBoxes(AddMinus_txtRowsA.Text, AddMinus_txtColsA.Text, AddMinus_txtRowsB.Text, AddMinus_txtColsB.Text))
            {
                AddMinus_btnGenerateEntryGrids.IsEnabled = false;
                AddMinus_btnFindSum.IsEnabled = false;
                AddMinus_btnFindDiff.IsEnabled = false;
                return;
            }
            AddMinus_btnGenerateEntryGrids.IsEnabled = true;
        }

        private void AddMinus_btnGenerateEntryGrids_Click(object sender, RoutedEventArgs e)
        {
            AddMinusTabLogic.generateEntryGrids(AddMinus_subGridA, AddMinus_subGridB, AddMinus_txtRowsA.Text, AddMinus_txtColsA.Text, AddMinus_txtRowsB.Text, AddMinus_txtColsB.Text);
            AddMinus_btnFindSum.IsEnabled = true;
            AddMinus_btnFindDiff.IsEnabled = true;
        }

        private void AddMinus_btnFindSum_Click(object sender, RoutedEventArgs e)
        {
            AddMinusTabLogic.findSum(AddMinus_subGridA, AddMinus_subGridB, AddMinus_txtRowsA.Text, AddMinus_txtColsA.Text, AddMinus_txtRowsB.Text, AddMinus_txtColsB.Text);
        }

        private void AddMinus_btnFindDiff_Click(object sender, RoutedEventArgs e)
        {
            AddMinusTabLogic.findDiff(AddMinus_subGridA, AddMinus_subGridB, AddMinus_txtRowsA.Text, AddMinus_txtColsA.Text, AddMinus_txtRowsB.Text, AddMinus_txtColsB.Text);
        }

        private void AddMinus_btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            AddMinus_txtRowsA.Clear();
            AddMinus_txtColsA.Clear();
            AddMinus_txtRowsB.Clear();
            AddMinus_txtColsB.Clear();

            AddMinus_subGridA.Children.Clear();
            AddMinus_subGridB.Children.Clear();

            AddMinus_subGridA.RowDefinitions.Clear();
            AddMinus_subGridA.ColumnDefinitions.Clear();
            AddMinus_subGridB.RowDefinitions.Clear();
            AddMinus_subGridB.ColumnDefinitions.Clear();

            AddMinus_btnFindSum.IsEnabled = false;
            AddMinus_btnFindDiff.IsEnabled = false;
        }

        /// <summary>
        /// Event handlers for Multiplication functionality
        /// </summary>

        private void Multiply_txtsDimensions_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!MultiplyTabLogic.validateDimensionsTextBoxes(Multiply_txtRowsA.Text, Multiply_txtColsA.Text, Multiply_txtRowsB.Text, Multiply_txtColsB.Text))
            {
                Multiply_btnGenerateEntryGrids.IsEnabled = false;
                Multiply_btnFindProduct.IsEnabled = false;
                return;
            }
            Multiply_btnGenerateEntryGrids.IsEnabled = true;
        }

        private void Multiply_btnGenerateEntryGrids_Click(object sender, RoutedEventArgs e)
        {
            MultiplyTabLogic.generateEntryGrids(Multiply_subGridA, Multiply_subGridB, Multiply_txtRowsA.Text, Multiply_txtColsA.Text, Multiply_txtRowsB.Text, Multiply_txtColsB.Text);
            Multiply_btnFindProduct.IsEnabled = true;
        }

        private void Multiply_btnFindProduct_Click(object sender, RoutedEventArgs e)
        {
            MultiplyTabLogic.findProduct(Multiply_subGridA, Multiply_subGridB, Multiply_txtRowsA.Text, Multiply_txtColsA.Text, Multiply_txtRowsB.Text, Multiply_txtColsB.Text);
        }

        private void Multiply_btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            Multiply_txtRowsA.Clear();
            Multiply_txtColsA.Clear();
            Multiply_txtRowsB.Clear();
            Multiply_txtColsB.Clear();

            Multiply_subGridA.Children.Clear();
            Multiply_subGridB.Children.Clear();

            Multiply_subGridA.RowDefinitions.Clear();
            Multiply_subGridA.ColumnDefinitions.Clear();
            Multiply_subGridB.RowDefinitions.Clear();
            Multiply_subGridB.ColumnDefinitions.Clear();

            Multiply_btnFindProduct.IsEnabled = false;
        }
    }
}
