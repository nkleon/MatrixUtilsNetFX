using System;

namespace MatrixUtilsNetFX
{
    internal static class MatrixOperations
    {
        internal static decimal[,] addMatrices(decimal[,] matrixA, decimal[,] matrixB)
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

        internal static decimal[,] subtractMatrices(decimal[,] matrixA, decimal[,] matrixB)
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

        internal static decimal[,] multiplyMatrices(decimal[,] matrixA, decimal[,] matrixB)
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

        internal static decimal calculateDeterminant(decimal[,] matrix)
        {
            int dimensions = matrix.GetLength(0);
            decimal determinant = 1;
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
                determinant *= matrix[i, i];
            }

            return determinant;
        }
    }
}