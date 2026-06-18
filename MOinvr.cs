using System;
namespace Matrix_Calculator
{
    public class MOinvr : MO
    {
        private MOdetr _detr = new MOdetr(); // Initialize determinant

        public override MatrixData ExecuteOne(MatrixData matrixA)
        {
            // Only supports 2x2 and 3x3 matrices
            // Define Result matrix dimensions
            MatrixData result = new MatrixData();
            result.rows = matrixA.rows;
            result.cols = matrixA.cols; // Its a square matrix
            result.matrix = new double[result.rows, result.cols];

            // Get Determinant
            double det = _detr.CalculateDeterminant(matrixA);
            Console.WriteLine($"Determinant: {det}");

            // Run inverting
            if (matrixA.rows == 2)
            {
                Console.WriteLine("2x2 matrix");
                result.matrix[0, 0] = matrixA.matrix[1, 1] / det;
                result.matrix[0, 1] = -matrixA.matrix[0, 1] / det;
                result.matrix[1, 0] = -matrixA.matrix[1, 0] / det;
                result.matrix[1, 1] = matrixA.matrix[0, 0] / det;
            }
            else if (matrixA.rows == 3)
            {
                // Calculate the transposed matrix of cofactors (Adjugate matrix) and divide by the determinant
                Console.WriteLine("3x3 matrix");
                result.matrix[0, 0] = (matrixA.matrix[1, 1] * matrixA.matrix[2, 2] - matrixA.matrix[1, 2] * matrixA.matrix[2, 1]) / det;
                result.matrix[1, 0] = -(matrixA.matrix[1, 0] * matrixA.matrix[2, 2] - matrixA.matrix[1, 2] * matrixA.matrix[2, 0]) / det;
                result.matrix[2, 0] = (matrixA.matrix[1, 0] * matrixA.matrix[2, 1] - matrixA.matrix[1, 1] * matrixA.matrix[2, 0]) / det;

                result.matrix[0, 1] = -(matrixA.matrix[0, 1] * matrixA.matrix[2, 2] - matrixA.matrix[0, 2] * matrixA.matrix[2, 1]) / det;
                result.matrix[1, 1] = (matrixA.matrix[0, 0] * matrixA.matrix[2, 2] - matrixA.matrix[0, 2] * matrixA.matrix[2, 0]) / det;
                result.matrix[2, 1] = -(matrixA.matrix[0, 0] * matrixA.matrix[2, 1] - matrixA.matrix[0, 1] * matrixA.matrix[2, 0]) / det;

                result.matrix[0, 2] = (matrixA.matrix[0, 1] * matrixA.matrix[1, 2] - matrixA.matrix[0, 2] * matrixA.matrix[1, 1]) / det;
                result.matrix[1, 2] = -(matrixA.matrix[0, 0] * matrixA.matrix[1, 2] - matrixA.matrix[0, 2] * matrixA.matrix[1, 0]) / det;
                result.matrix[2, 2] = (matrixA.matrix[0, 0] * matrixA.matrix[1, 1] - matrixA.matrix[0, 1] * matrixA.matrix[1, 0]) / det;
            }

            // Round to avoid floating point precision issues (same as in multiplication)
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result.matrix[i, j] = Math.Round(result.matrix[i, j] * 100000) / 100000;
                }
            }

            // Return result
            return result;
        }
    }
}