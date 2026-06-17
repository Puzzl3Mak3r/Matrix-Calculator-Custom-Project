using System;
namespace Matrix_Calculator
{
    public class MOmult : MO
    {
        public override MatrixData ExecuteTwo(MatrixData matrixA, MatrixData matrixB)
        {
            // Define Result matrix dimensions (Rows of A, Cols of B)
            MatrixData result = new MatrixData();
            result.rows = matrixA.rows;
            result.cols = matrixB.cols;
            result.matrix = new double[result.rows, result.cols];

            // Run Multiplication (The triple-nested loop)
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result.matrix[i, j] = 0; // Initialize the cell

                    for (int k = 0; k < matrixA.cols; k++) // Or matrixB.rows
                    {
                        result.matrix[i, j] += matrixA.matrix[i, k] * matrixB.matrix[k, j];
                    }
                }
            }

            // Return result
            return result;
        }
    }
}