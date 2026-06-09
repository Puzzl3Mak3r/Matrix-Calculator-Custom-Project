using System;

namespace Matrix_Calculator
{
    public static class MatrixFactory
    {
        // Method to create a zero matrix based on user input
        public static MatrixData CreateZMatrix(int rows, int cols)
        {
            MatrixData matrixData = new MatrixData
            {
                // Dimensions
                rows = rows,
                cols = cols,
                // Initialize matrix with zeros
                matrix = new double[rows, cols]
            };

            // Initialize all elements to zero
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrixData.matrix[i, j] = 0;
                }
            }

            // Dev: Print the zero matrix to console for verification
            Console.WriteLine("Zero Matrix:");
            Console.WriteLine();

            // Return the zero matrix
            return matrixData;
        }
    }
}