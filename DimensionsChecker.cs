using System;

namespace Matrix_Calculator
{
    public class DimensionChecker
    {
        bool CheckAdd(MatrixData A, MatrixData B)
        {
            bool Check = true;

            

            return Check;
        }

        bool CheckSubtract(MatrixData A, MatrixData B)
        {
            // Uses the same logic as CheckAdd
            return CheckAdd(A, B);
        }

        bool CheckMultiply(MatrixData A, MatrixData B)
        {
            bool Check = true;
            return Check;
        }
        
        // None needed for Transpose or Invert

        bool CheckDeterminant(MatrixData A)
        {
            bool Check = true;
            return Check;
        }
    }
}