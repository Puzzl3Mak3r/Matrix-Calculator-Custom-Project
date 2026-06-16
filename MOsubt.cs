using System;

namespace Matrix_Calculator
{
    public class MOsubt : MatrixOperations
    {

        public override MatrixData Execute(MatrixData matrixA, MatrixData matrixB)
        {
            // Run dimension Checker
            if (!CheckDimensions(matrixA, matrixB))
            {
                Console.WriteLine("Error: Matrices must have the same dimensions for addition.");
                return new MatrixData();
            }

            // Define Result matrix dimensions
            MatrixData result = new MatrixData();
            result.rows = matrixA.rows;
            result.cols = matrixA.cols;
            result.matrix = new double[result.rows, result.cols];

            // Run Addition
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result.matrix[i, j] = matrixA.matrix[i, j] - matrixB.matrix[i, j];
                }
            }
            
            return result;
        }
    }
}