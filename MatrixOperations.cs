using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    // Default take in 2 2D float arrays, and return a 2D float array, can be changed to take in more matrices or different data structures if needed
    public abstract class MatrixOperations
    {
        // Overrideable for different operations, can be changed to take different data structures if needed (or in more matrices? Will look into later)
        public abstract MatrixData Execute(MatrixData matrixA, MatrixData matrixB);
    }
}