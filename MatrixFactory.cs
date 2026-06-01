using System;

namespace Matrix_Calculator
{
    
    // Central structural dispatcher for instantly provisioning uniform pre-populated configurations.
    
    public class MatrixFactory
    {
        
        // Allocates a standard blank matrix initialized to 0.0.
        
        public static Matrix CreateZeroMatrix(int rows, int cols)
        {
            return new Matrix(rows, cols);
        }

        
        // Generates a square matrix where index (i, j) is set to 1.0 if i == j, and 0.0 otherwise.
        
        // <param name="size">The length of the rows and columns.</param>
        // <returns>An identity matrix.</returns>
        public static Matrix CreateIdentityMatrix(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Size must be greater than zero for Identity Matrix.");

            Matrix m = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                m.SetValue(i, i, 1.0);
            }
            return m;
        }
    }
}