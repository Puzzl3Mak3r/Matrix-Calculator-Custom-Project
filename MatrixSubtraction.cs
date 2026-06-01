using System;

namespace Matrix_Calculator
{
    
    // Concrete strategy for matrix subtraction.
    
    public class MatrixSubtraction : IMathOperation
    {
        
        // Executes matrix subtraction. Requires both matrices to have identical dimensions.
        
        // <param name="a">The matrix to subtract from.</param>
        // <param name="b">The matrix to subtract.</param>
        // <returns>A new Matrix object containing the difference.</returns>
        // <exception cref="InvalidOperationException">Thrown if matrix dimensions mismatch.</exception>
        public Matrix Execute(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols)
            {
                throw new InvalidOperationException("Matrix dimensions must match for subtraction.");
            }

            Matrix result = new Matrix(a.Rows, a.Cols);
            for (int r = 0; r < a.Rows; r++)
            {
                for (int c = 0; c < a.Cols; c++)
                {
                    result.SetValue(r, c, a.GetValue(r, c) - b.GetValue(r, c));
                }
            }
            return result;
        }
    }
}