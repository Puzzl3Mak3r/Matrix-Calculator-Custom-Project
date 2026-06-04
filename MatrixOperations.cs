using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    // Default take in 2 2D float arrays, and return a 2D float array, can be changed to take in more matrices or different data structures if needed
    public class Operation
    {
        // Overrideable for different operations, can be changed to take different data structures if needed (or in more matrices? Will look into later)
        public static double[][] Add(double[][] matrixA, double[][] matrixB)
        {
            // Run dimension Checker
            if (!CheckDimensions(matrixA, matrixB))
            {
                Console.WriteLine("Error: Matrices must have the same dimensions for addition.");
                return null; // Return null or throw an exception as needed
            }
            else
            {
                // Perform Operation
                int rows = matrixA.Length;
                int cols = matrixA[0].Length;
                double[][] result = new double[rows][];

                return result;
            }
        }

        // Dimension Checker (Overrideable for different operations)
        public static bool CheckDimensions(double[][] matrixA, double[][] matrixB)
        {
            return matrixA.Length == matrixB.Length && matrixA[0].Length == matrixB[0].Length;
        }
    }
}