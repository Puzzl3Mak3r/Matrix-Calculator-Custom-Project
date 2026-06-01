using System;
using System.Text;

namespace Matrix_Calculator
{
    public class Matrix
    {
        private double[,] _grid;
        private int _rows;
        private int _cols;

        // Gets the number of rows in the matrix.
        
        public int Rows => _rows;

        // Gets the number of columns in the matrix.
        
        public int Cols => _cols;

        // Initializes a new instance of the Matrix class with specified dimensions.
        
        // <param name="rows">Number of rows.</param>
        // <param name="cols">Number of columns.</param>
        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Matrix dimensions must be greater than zero.");

            _rows = rows;
            _cols = cols;
            _grid = new double[rows, cols];
        }

        // Gets the value at the specified row and column.
        
        public double GetValue(int r, int c)
        {
            if (r < 0 || r >= _rows || c < 0 || c >= _cols)
                throw new IndexOutOfRangeException("Matrix indices are out of bounds.");
            return _grid[r, c];
        }

        // Sets the value at the specified row and column.
        
        public void SetValue(int r, int c, double val)
        {
            if (r < 0 || r >= _rows || c < 0 || c >= _cols)
                throw new IndexOutOfRangeException("Matrix indices are out of bounds.");
            _grid[r, c] = val;
        }

        // Creates a memento containing a snapshot of the current matrix state.
        
        // <returns>A MatrixMemento object.</returns>
        public MatrixMemento CreateMemento()
        {
            return new MatrixMemento(_grid);
        }

        // Restores the matrix state from a given memento.
        
        // <param name="memento">The memento to restore from.</param>
        public void RestoreFromMemento(MatrixMemento memento)
        {
            var snapshot = memento.StateSnapshot;
            _rows = snapshot.GetLength(0);
            _cols = snapshot.GetLength(1);
            _grid = new double[_rows, _cols];
            Array.Copy(snapshot, _grid, snapshot.Length);
        }

        // Converts the matrix to a LaTeX bmatrix string representation.
        
        // <returns>LaTeX formatted string.</returns>
        public string ToLatexString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\\begin{bmatrix} ");
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _cols; c++)
                {
                    sb.Append(_grid[r, c].ToString());
                    if (c < _cols - 1)
                        sb.Append(" & ");
                }
                if (r < _rows - 1)
                    sb.Append(" \\\\ ");
            }
            sb.Append(" \\end{bmatrix}");
            return sb.ToString();
        }
    }
}