using System;

namespace Matrix_Calculator
{
    
    // Concrete strategy for matrix multiplication.
    
    public class MatrixMultiplication : IMathOperation
    {
        
        // Executes a matrix multiplication strategy following linear algebra principles.
        // Requires the column dimensions of Matrix A to match the row dimensions of Matrix B.
        
        // <param name="a">The left-hand multiplicand matrix.</param>
        // <param name="b">The right-hand multiplier matrix.</param>
        // <returns>A new Matrix object containing the computed dot product results.</returns>
        // <exception cref="InvalidOperationException">Thrown if matrix dimension constraints are violated.</exception>
        public Matrix Execute(Matrix a, Matrix b)
        {
            if (a.Cols != b.Rows)
            {
                throw new InvalidOperationException("Matrix A columns must match Matrix B rows for multiplication.");
            }

            Matrix result = new Matrix(a.Rows, b.Cols);
            for (int r = 0; r < a.Rows; r++)
            {
                for (int c = 0; c < b.Cols; c++)
                {
                    double sum = 0;
                    for (int k = 0; k < a.Cols; k++)
                    {
                        sum += a.GetValue(r, k) * b.GetValue(k, c);
                    }
                    result.SetValue(r, c, sum);
                }
            }
            return result;
        }
    }
}