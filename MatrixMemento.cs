
using System;

namespace Matrix_Calculator
{
    
    // An immutable storage object containing a standalone snapshot of a matrix state.
    // Acts as the Memento in the Memento pattern.
    
    public class MatrixMemento
    {
        private readonly double[,] _stateSnapshot;
        private readonly DateTime _timestamp;

        // Gets the snapshot of the matrix state.
        
        public double[,] StateSnapshot
        {
            get
            {
                // Return a copy to maintain immutability
                int rows = _stateSnapshot.GetLength(0);
                int cols = _stateSnapshot.GetLength(1);
                double[,] copy = new double[rows, cols];
                Array.Copy(_stateSnapshot, copy, _stateSnapshot.Length);
                return copy;
            }
        }

        // Initializes a new instance of the MatrixMemento class.
        
        // <param name="gridData">The grid data to snapshot.</param>
        public MatrixMemento(double[,] gridData)
        {
            int rows = gridData.GetLength(0);
            int cols = gridData.GetLength(1);
            _stateSnapshot = new double[rows, cols];
            Array.Copy(gridData, _stateSnapshot, gridData.Length);
            _timestamp = DateTime.Now;
        }
    }
}