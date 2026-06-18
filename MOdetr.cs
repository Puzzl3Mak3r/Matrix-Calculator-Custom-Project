using System;
namespace Matrix_Calculator
{
    public class MOdetr
    {
        public double CalculateDeterminant(MatrixData matrixA)
        {
            double determinant = 0;

            // Only supports 2x2 and 3x3 matrices
            if (matrixA.rows == 2 && matrixA.cols == 2)
            {
                determinant = (matrixA.matrix[0, 0] * matrixA.matrix[1, 1]) - (matrixA.matrix[0, 1] * matrixA.matrix[1, 0]);
            }
            else if (matrixA.rows == 3 && matrixA.cols == 3)
            {
                determinant = 
                      matrixA.matrix[0, 0] * ((matrixA.matrix[1, 1] * matrixA.matrix[2, 2]) - (matrixA.matrix[1, 2] * matrixA.matrix[2, 1]))
                    - matrixA.matrix[0, 1] * ((matrixA.matrix[1, 0] * matrixA.matrix[2, 2]) - (matrixA.matrix[1, 2] * matrixA.matrix[2, 0]))
                    + matrixA.matrix[0, 2] * ((matrixA.matrix[1, 0] * matrixA.matrix[2, 1]) - (matrixA.matrix[1, 1] * matrixA.matrix[2, 0]));
            }

            return determinant;
        }
    }
}