using System;
namespace Matrix_Calculator
{
    public class MOtran : MO
    {

        public override MatrixData ExecuteOne(MatrixData matrixA)
        {
            // Define Result matrix dimensions
            MatrixData result = new MatrixData();
            result.rows = matrixA.cols;
            result.cols = matrixA.rows;
            result.matrix = new double[result.rows, result.cols];

            // Run transposing
            for (int i = 0; i < matrixA.rows; i++)
            {
                for (int j = 0; j < matrixA.cols; j++)
                {
                    result.matrix[j, i] = matrixA.matrix[i, j];
                }
            }

            // Return result
            return result;
        }
    }
}