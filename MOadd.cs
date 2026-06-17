using System;
namespace Matrix_Calculator
{
    public class MOadd : MO
    {

        public override MatrixData ExecuteTwo(MatrixData matrixA, MatrixData matrixB)
        {
            // Define Result matrixdimensions
            MatrixData result = new MatrixData();
            result.rows = matrixA.rows;
            result.cols = matrixA.cols;
            result.matrix = new double[result.rows, result.cols];

            // Run Addition
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result.matrix[i, j] = matrixA.matrix[i, j] + matrixB.matrix[i, j];
                }
            }

            // Return result
            return result;
        }
    }
}