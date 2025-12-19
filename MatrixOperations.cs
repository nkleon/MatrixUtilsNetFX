using System;

namespace MatrixUtilsNetFX
{
    internal static class MatrixOperations
    {
        internal static decimal[,] AddMatrices(decimal[,] matrixA, decimal[,] matrixB)
        {
            if (matrixA.GetLength(0) != matrixB.GetLength(0) || matrixA.GetLength(1) != matrixB.GetLength(1))
            {
                throw new Exception("Matrix dimensions not equal!");
            }

            decimal[,] sum = new decimal[matrixA.GetLength(0), matrixA.GetLength(1)];

            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixA.GetLength(1); j++)
                {
                    sum[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }

            return sum;
        }

        internal static decimal[,] SubtractMatrices(decimal[,] matrixA, decimal[,] matrixB)
        {
            if (matrixA.GetLength(0) != matrixB.GetLength(0) || matrixA.GetLength(1) != matrixB.GetLength(1))
            {
                throw new Exception("Matrix dimensions not equal!");
            }

            decimal[,] difference = new decimal[matrixA.GetLength(0), matrixA.GetLength(1)];

            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixA.GetLength(1); j++)
                {
                    difference[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            }

            return difference;
        }

        internal static decimal[,] MultiplyMatrices(decimal[,] matrixA, decimal[,] matrixB)
        {
            if (matrixA.GetLength(1) != matrixB.GetLength(0))
            {
                throw new Exception("Matrices not compatible");
            }
            decimal[,] product = new decimal[matrixA.GetLength(0), matrixB.GetLength(1)];
            for (int row = 0; row < matrixA.GetLength(0); row++)
            {
                for (int col = 0; col < matrixB.GetLength(1); col++)
                {
                    for (int n = 0; n < matrixA.GetLength(1); n++)
                    {
                        product[row, col] += matrixA[row, n] * matrixB[n, col];
                    }
                }
            }
            return product;
        }

        internal static decimal[,] MultiplyMatrixByScalar(decimal scalar, decimal[,] matrix)
        {
            decimal[,] product = new decimal[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    product[i, j] = scalar * matrix[i, j];
                }
            }
            return product;
        }

        private static decimal[,] CreateMinorMatrix(decimal[,] matrix, int colToSkip)
        {
            decimal[,] submatrix = new decimal[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j < colToSkip)
                    {
                        submatrix[i - 1, j] = matrix[i, j];
                    }
                    else if (j > colToSkip)
                    {
                        submatrix[i - 1, j - 1] = matrix[i, j];
                    }
                }
            }
            return submatrix;
        }

        internal static decimal CalculateMatrixDeterminant(decimal[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (rows < 1) throw new Exception("Invalid matrix!");
            if (rows != cols) throw new Exception("The matrix must be square");
            if (rows == 1)
            {
                return matrix[0, 0];
            }
            else if (rows == 2)
            {
                return (matrix[0, 0] * matrix[1, 1]) - (matrix[1, 0] * matrix[0, 1]);
            }
            decimal determinant = 0;
            for (int col = 0; col < cols; col++)
            {
                determinant += matrix[0, col] * Convert.ToInt32(Math.Pow(-1, col)) * CalculateMatrixDeterminant(CreateMinorMatrix(matrix, col));
            }
            return determinant;
        }

        internal static decimal[,] CalculateMatrixInverse(decimal[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            if (CalculateMatrixDeterminant(matrix) == 0 || rows != cols)
            {
                throw new Exception("Matrix is not invertible");
            }

            // Augment matrix with identity matrix
            decimal[,] aug = new decimal[rows, 2 * rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    aug[i, j] = matrix[i, j];
                }
                aug[i, i + rows] = 1.0m;
            }

            // Perform Gauss-Jordan elimination
            for (int i = 0; i < rows; i++)
            {
                // Partial Pivoting: Find row with largest absolute value in current column
                int maxRow = i;
                for (int k = i + 1; k < rows; k++)
                {
                    if (Math.Abs(aug[k, i]) > Math.Abs(aug[maxRow, i]))
                    {
                        maxRow = k;
                    }
                }

                // Swap crows if necessary
                if (maxRow != i)
                {
                    for (int k = 0; k < 2 * rows; k++)
                    {
                        decimal temp = aug[i, k];
                        aug[i, k] = aug[maxRow, k];
                        aug[maxRow, k] = temp;
                    }
                }

                // Ensure pivot is non-zero
                decimal pivot = aug[i, i];
                if (pivot == 0m)
                {
                    throw new Exception("Matrix is singular or nearly singular.");
                }

                // Scale pivot row so that pivot element becomes 1
                for (int k = 0; k < 2 * rows; k++)
                {
                    aug[i, k] /= pivot;
                }

                // Eliminate all other entries in current column
                for (int j = 0; j < rows; j++)
                {
                    if (j != i)
                    {
                        decimal factor = aug[j, i];
                        for (int k = 0; k < 2 * rows; k++)
                        {
                            aug[j, k] -= factor * aug[i, k];
                        }
                    }
                }
            }

            // Extract inverse matrix from RHS of augmented matrix
            decimal[,] inverse = new decimal[rows, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    inverse[i, j] = aug[i, j + rows];
                }
            }

            return inverse;
        }
    }
}
