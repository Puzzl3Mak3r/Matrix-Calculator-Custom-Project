namespace Matrix_Calculator
{
    
    // Strategy interface for matrix mathematical operations.
    
    public interface IMathOperation
    {
        // Executes a mathematical operation on two matrices.
        
        // <param name="a">The first matrix.</param>
        // <param name="b">The second matrix.</param>
        // <returns>A new matrix containing the result.</returns>
        Matrix Execute(Matrix a, Matrix b);
    }
}