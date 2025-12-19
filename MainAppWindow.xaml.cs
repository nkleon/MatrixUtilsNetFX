using System;
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
        /// Event handlers for Determinant/Inverse functionality
        /// </summary>

        private void DetInv_cBoxDim_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DetInv_cBoxDim.SelectedItem != null)
            {
                DetInv_btnGenGrid.IsEnabled = true;
                return;
            }
            DetInv_btnGenGrid.IsEnabled = false;
            DetInv_btnFindDet.IsEnabled = false;
            DetInv_btnFindInv.IsEnabled = false;
        }

        private void DetInv_btnClear_Click(object sender, RoutedEventArgs e)
        {
            DetInv_cBoxDim.SelectedIndex = -1;
            SubGridUtilities.ClearSubGridContents(DetInv_subGrid);

            DetInv_btnGenGrid.IsEnabled = false;
            DetInv_btnFindDet.IsEnabled = false;
            DetInv_btnFindInv.IsEnabled = false;
        }

        private void DetInv_btnGenGrid_Click(object sender, RoutedEventArgs e)
        {
            SubGridUtilities.PopulateSubGrid(
                    DetInv_subGrid,
                    (int)DetInv_cBoxDim.SelectedItem,
                    (int)DetInv_cBoxDim.SelectedItem
                );
            DetInv_btnFindDet.IsEnabled = true;
            DetInv_btnFindInv.IsEnabled = true;
        }

        private void DetInv_btnFindDet_Click(object sender, RoutedEventArgs e)
        {
            string[] subGridStrings = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(DetInv_subGrid);
            if (!TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStrings))
            {
                MessageBox.Show(
                        "Ensure all textboxes in the grid are filled and that they only have numeric values.",
                        "Missing or invalid values",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                return;
            }
            decimal determinant = MatrixOperations.CalculateMatrixDeterminant(
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(DetInv_subGrid)
                );
            MessageBox.Show($"Determinant: {determinant}", "Answer");
        }

        private void DetInv_btnFindInv_Click(object sender, RoutedEventArgs e)
        {
            string[] subGridStrings = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(DetInv_subGrid);
            if (!TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStrings))
            {
                MessageBox.Show(
                        "Ensure all textboxes in the grid are filled and that they only have numeric values.",
                        "Missing or invalid values",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                return;
            }
            try
            {
                decimal[,] inverse = MatrixOperations.CalculateMatrixInverse(
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(DetInv_subGrid)
                );

                new ResultWindow(inverse).ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "This matrix is not invertible",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }

        /// <summary>
        /// Event handlers for Addition/Subtraction functionality
        /// </summary>

        private void AddSub_cBoxesDims_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (AddSub_cBoxRow.SelectedItem != null &&
                AddSub_cBoxCol.SelectedItem != null
                )
            {
                AddSub_btnGenGrids.IsEnabled = true;
                return;
            }
            AddSub_btnGenGrids.IsEnabled = false;
            AddSub_btnFindSum.IsEnabled = false;
            AddSub_btnFindDiff.IsEnabled = false;
            AddSub_btnSwapMatrices.IsEnabled = false;
        }

        private void AddSub_btnClear_Click(object sender, RoutedEventArgs e)
        {
            AddSub_cBoxRow.SelectedIndex = -1;
            AddSub_cBoxCol.SelectedIndex = -1;

            SubGridUtilities.ClearSubGridContents(AddSub_subGridA);
            SubGridUtilities.ClearSubGridContents(AddSub_subGridB);

            AddSub_btnGenGrids.IsEnabled = false;
            AddSub_btnFindSum.IsEnabled = false;
            AddSub_btnFindDiff.IsEnabled = false;
            AddSub_btnSwapMatrices.IsEnabled = false;
        }

        private void AddSub_btnGenGrids_Click(object sender, RoutedEventArgs e)
        {
            SubGridUtilities.PopulateSubGrid(
                    AddSub_subGridA,
                    (int)AddSub_cBoxRow.SelectedItem,
                    (int)AddSub_cBoxCol.SelectedItem
                );
            SubGridUtilities.PopulateSubGrid(
                    AddSub_subGridB,
                    (int)AddSub_cBoxRow.SelectedItem,
                    (int)AddSub_cBoxCol.SelectedItem
                );
            AddSub_btnFindSum.IsEnabled = true;
            AddSub_btnFindDiff.IsEnabled = true;
            AddSub_btnSwapMatrices.IsEnabled = true;
        }

        private void AddSub_btnFindSum_Click(object sender, RoutedEventArgs e)
        {
            string[] subGridStringsA = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(AddSub_subGridA);
            string[] subGridStringsB = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(AddSub_subGridB);
            if (!TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStringsA) ||
                !TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStringsB)
                )
            {
                MessageBox.Show(
                        "Ensure all textboxes in the grid are filled and that they only have numeric values.",
                        "Missing or invalid values",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                return;
            }
            decimal[,] sum = MatrixOperations.AddMatrices(
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(AddSub_subGridA),
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(AddSub_subGridB)
                );
            new ResultWindow(sum).ShowDialog();
        }

        private void AddSub_btnFindDiff_Click(object sender, RoutedEventArgs e)
        {
            string[] subGridStringsA = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(AddSub_subGridA);
            string[] subGridStringsB = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(AddSub_subGridB);
            if (!TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStringsA) ||
                !TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStringsB)
                )
            {
                MessageBox.Show(
                        "Ensure all textboxes in the grid are filled and that they only have numeric values.",
                        "Missing or invalid values",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                return;
            }
            decimal[,] difference = MatrixOperations.SubtractMatrices(
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(AddSub_subGridA),
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(AddSub_subGridB)
                );
            new ResultWindow(difference).ShowDialog();
        }

        private void AddSub_btnSwapMatrices_Click(object sender, RoutedEventArgs e)
        {
            SubGridUtilities.SwapSubGrids(AddSub_subGridA, AddSub_subGridB);
        }

        /// <summary>
        /// Event handlers for Scalar Multiplication functionality
        /// </summary>

        private void SMul_cBoxesDims_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (SMul_cBoxRow.SelectedItem != null &&
                SMul_cBoxCol.SelectedItem != null
                )
            {
                SMul_btnGenGrid.IsEnabled = true;
                return;
            }
            SMul_btnGenGrid.IsEnabled = false;
            SMul_btnFindProd.IsEnabled = false;
        }

        private void SMul_txtScalar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextValidationCheckers.AreAllStringsNotNull(SMul_txtScalar.Text) &&
                TextValidationCheckers.AreAllStringsParsableNumeric(
                        TypeCode.Decimal,
                        SMul_txtScalar.Text
                    )
                )
            {
                SMul_btnGenGrid.IsEnabled = true;
                return;
            }
            SMul_btnGenGrid.IsEnabled = false;
            SMul_btnFindProd.IsEnabled = false;
        }

        private void SMul_btnClear_Click(object sender, RoutedEventArgs e)
        {
            SMul_cBoxRow.SelectedIndex = -1;
            SMul_cBoxCol.SelectedIndex = -1;
            SMul_txtScalar.Clear();

            SubGridUtilities.ClearSubGridContents(SMul_subGrid);

            SMul_btnGenGrid.IsEnabled = false;
            SMul_btnFindProd.IsEnabled = false;
        }

        private void SMul_btnGenGrid_Click(object sender, RoutedEventArgs e)
        {
            SubGridUtilities.PopulateSubGrid(
                    SMul_subGrid,
                    (int)SMul_cBoxRow.SelectedItem,
                    (int)SMul_cBoxCol.SelectedItem
                );
            SMul_btnFindProd.IsEnabled = true;
        }

        private void SMul_btnFindProd_Click(object sender, RoutedEventArgs e)
        {
            string[] subGridStrings = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(SMul_subGrid);
            if (!TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStrings))
            {
                MessageBox.Show(
                        "Ensure all textboxes in the grid are filled and that they only have numeric values.",
                        "Missing or invalid values",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                return;
            }
            decimal[,] product = MatrixOperations.MultiplyMatrixByScalar(
                    decimal.Parse(SMul_txtScalar.Text),
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(SMul_subGrid)
                );
            new ResultWindow(product).ShowDialog();
        }

        /// <summary>
        /// Event handlers for Matrix Multiplication functionality
        /// </summary>

        bool MMul_suppressCBoxEvtHandler = false;

        private void MMul_cBoxesDims_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (MMul_suppressCBoxEvtHandler) return;
            if (MMul_cBoxRowA.SelectedItem != null &&
                MMul_cBoxColA.SelectedItem != null &&
                MMul_cBoxRowB.SelectedItem != null &&
                MMul_cBoxColB.SelectedItem != null &&
                MMul_cBoxColA.SelectedItem.Equals(MMul_cBoxRowB.SelectedItem)
                )
            {
                MMul_btnGenGrids.IsEnabled = true;
                return;
            }
            MMul_btnGenGrids.IsEnabled = false;
            MMul_btnFindProd.IsEnabled = false;
            MMul_btnSwapMatrices.IsEnabled = false;
        }
        private void MMul_btnClear_Click(object sender, RoutedEventArgs e)
        {
            MMul_cBoxRowA.SelectedItem = -1;
            MMul_cBoxColA.SelectedItem = -1;
            MMul_cBoxRowB.SelectedItem = -1;
            MMul_cBoxColB.SelectedItem = -1;

            SubGridUtilities.ClearSubGridContents(MMul_subGridA);
            SubGridUtilities.ClearSubGridContents(MMul_subGridB);

            MMul_btnGenGrids.IsEnabled = false;
            MMul_btnFindProd.IsEnabled = false;
            MMul_btnSwapMatrices.IsEnabled = false;
        }

        private void MMul_btnGenGrids_Click(object sender, RoutedEventArgs e)
        {
            SubGridUtilities.PopulateSubGrid(
                    MMul_subGridA,
                    (int)MMul_cBoxRowA.SelectedItem,
                    (int)MMul_cBoxColA.SelectedItem
                );
            SubGridUtilities.PopulateSubGrid(
                    MMul_subGridB,
                    (int)MMul_cBoxRowB.SelectedItem,
                    (int)MMul_cBoxColB.SelectedItem
                );
            MMul_btnFindProd.IsEnabled = true;
            MMul_btnSwapMatrices.IsEnabled = true;
        }

        private void MMul_btnFindProd_Click(object sender, RoutedEventArgs e)
        {
            string[] subGridStringsA = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(MMul_subGridA);
            string[] subGridStringsB = SubGridUtilities.GetSubGridTextBoxValues_1dStrArray(MMul_subGridB);
            if (!TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStringsA) ||
                !TextValidationCheckers.AreAllStringsParsableNumeric(TypeCode.Decimal, subGridStringsB)
                )
            {
                MessageBox.Show(
                        "Ensure all textboxes in the grid are filled and that they only have numeric values.",
                        "Missing or invalid values",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                return;
            }
            decimal[,] product = MatrixOperations.MultiplyMatrices(
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(MMul_subGridA),
                    SubGridUtilities.GetSubGridTextBoxValuesDecimal(MMul_subGridB)
                );
            new ResultWindow(product).ShowDialog();
        }

        private void MMul_btnSwapMatrices_Click(object sender, RoutedEventArgs e)
        {

            if (MMul_cBoxRowA.SelectedIndex != MMul_cBoxRowB.SelectedIndex ||
                MMul_cBoxColA.SelectedIndex != MMul_cBoxColB.SelectedIndex
                )
            {
                MMul_suppressCBoxEvtHandler = true;
                int tmpIdxRow = MMul_cBoxRowA.SelectedIndex;
                int tmpIdxCol = MMul_cBoxColA.SelectedIndex;
                MMul_cBoxRowA.SelectedIndex = MMul_cBoxRowB.SelectedIndex;
                MMul_cBoxColA.SelectedIndex = MMul_cBoxColB.SelectedIndex;
                MMul_cBoxRowB.SelectedIndex = tmpIdxRow;
                MMul_cBoxColB.SelectedIndex = tmpIdxCol;
                MMul_suppressCBoxEvtHandler = false;
            }
            SubGridUtilities.SwapSubGrids(MMul_subGridA, MMul_subGridB);
        }
    }
}
