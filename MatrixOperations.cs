using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    // Default take in 2 2D float arrays, and return a 2D float array, can be changed to take in more matrices or different data structures if needed
    public abstract class Operation
    {
        // Overrideable for different operations, can be changed to take different data structures if needed (or in more matrices? Will look into later)
        public abstract double[][] Execute(double[][] matrixA, double[][] matrixB);

        // Dimension Checker (Overrideable for different operations)
        public virtual bool CheckDimensions(double[][] matrixA, double[][] matrixB)
        {
            // Check if the dimensions are the same
            return matrixA.Length == matrixB.Length && matrixA[0].Length == matrixB[0].Length;
        }
    }
}