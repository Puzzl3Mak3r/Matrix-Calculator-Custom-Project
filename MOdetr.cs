using System;
namespace Matrix_Calculator
{
    public class MOdetr : MO
    {
        public override MatrixData ExecuteOne(MatrixData matrixA)
        {
            // Define Result matrix dimensions
            MatrixData result = new MatrixData();
            result.rows = matrixA.rows;
            result.cols = matrixA.rows; // Its a square matrix
            result.matrix = new double[result.rows, result.cols];

            // Run determinant
            for (int i = 0; i < matrixA.rows; i++)
            {
                for (int j = 0; j < matrixA.cols; j++)
                {
                    
                }
            }

            // Return result
            return result;
        }
    }
}