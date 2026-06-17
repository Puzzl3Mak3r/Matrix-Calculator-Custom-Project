using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    // Default take in 2 2D float arrays, and return a 2D float array, can be changed to take in more matrices or different data structures if needed
    public abstract class MO
    {
        // By making these virtual, concrete classes only need to override the methods they support.
        public virtual MatrixData ExecuteTwo(MatrixData matrixA, MatrixData matrixB) // 2 Matrix Operation
        { throw new NotImplementedException("This operation is not supported for 2 matrices"); }
        public virtual MatrixData ExecuteOne(MatrixData matrixA) // 1 Matrix Operation
        { throw new NotImplementedException("This operation is not supported for 1 matrix"); }
    }
}