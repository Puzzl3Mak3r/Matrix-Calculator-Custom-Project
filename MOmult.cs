using System;

namespace Matrix_Calculator
{
    public class MOmult : MatrixOperations
    {
        public override MatrixData Execute(MatrixData matrixA, MatrixData matrixB)
        {
            // // Run specific dimension checker for Dot Product compatibility
            // if (matrixA.cols != matrixB.rows)
            // {
            //     Console.WriteLine("Error: Matrix A columns must equal Matrix B rows for multiplication.");
            //     return new MatrixData(); 
            // }

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
            
            return result;
        }
    }
}