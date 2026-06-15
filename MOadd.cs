using System;

namespace Matrix_Calculator
{
    public class MOadd : Operation
    {
        public override double[][] Execute(double[][] matrixA, double[][] matrixB)
        {
            // Run dimension Checker
            if (!CheckDimensions(matrixA, matrixB))
            {
                Console.WriteLine("Error: Matrices must have the same dimensions for addition.");
                return null;
            }

            // Perform Operation
            int rows = matrixA.Length;
            int cols = matrixA[0].Length;
            double[][] result = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    result[i][j] = matrixA[i][j] + matrixB[i][j];
                }
            }

            return result;
        }
    }
}