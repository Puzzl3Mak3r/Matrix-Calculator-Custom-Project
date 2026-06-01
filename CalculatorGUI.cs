using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    
    // The orchestrator module running the SplashKit window event loop.
    // Intercepts mouse coordinates to identify active matrix cells and routes operational requests to strategies.
    
    public class CalculatorGUI
    {
        private Matrix _matrixA;
        private Matrix _matrixB;
        private Matrix _matrixResult;
        private IMathOperation _activeStrategy;
        private MatrixMemento _internalClipboard;
        private Window _window;

        public CalculatorGUI()
        {
            // Initializing with some default matrices
            _matrixA = MatrixFactory.CreateIdentityMatrix(2);
            _matrixB = MatrixFactory.CreateIdentityMatrix(2);
            _matrixResult = MatrixFactory.CreateZeroMatrix(2, 2);
            _activeStrategy = new MatrixAddition();
            
            _window = new Window("Matrix Calculator", 800, 600);
        }

        
        // Starts the main application loop.
        
        public void Run()
        {
            while (!_window.CloseRequested)
            {
                SplashKit.ProcessEvents();
                Update();
                Draw();
            }
        }

        private void Update()
        {
            // Key-based strategy selection
            if (SplashKit.KeyTyped(KeyCode.AKey))
            {
                _activeStrategy = new MatrixAddition();
                PerformCalculation();
            }
            else if (SplashKit.KeyTyped(KeyCode.SKey))
            {
                _activeStrategy = new MatrixSubtraction();
                PerformCalculation();
            }
            else if (SplashKit.KeyTyped(KeyCode.MKey))
            {
                _activeStrategy = new MatrixMultiplication();
                PerformCalculation();
            }
            
            // Example trigger for LaTeX export
            if (SplashKit.KeyTyped(KeyCode.CKey))
            {
                ClipboardService.CopyText(_matrixResult.ToLatexString());
            }
        }

        private void PerformCalculation()
        {
            try
            {
                _matrixResult = _activeStrategy.Execute(_matrixA, _matrixB);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Calculation Error: " + ex.Message);
            }
        }

        private void Draw()
        {
            _window.Clear(Color.White);

            // A basic layout of matrices and instructions
            SplashKit.DrawText("Matrix Calculator", Color.Black, 10, 10);
            SplashKit.DrawText("Press 'A' (Add), 'S' (Subtract), 'M' (Multiply), 'C' (Copy Result to Clipboard)", Color.Gray, 10, 30);

            DrawMatrix(_matrixA, 50, 100, "Matrix A");
            DrawMatrix(_matrixB, 300, 100, "Matrix B");
            DrawMatrix(_matrixResult, 175, 350, "Result");

            _window.Refresh(60);
        }

        private void DrawMatrix(Matrix m, double x, double y, string title)
        {
            SplashKit.DrawText(title, Color.DarkBlue, x, y - 20);
            for (int r = 0; r < m.Rows; r++)
            {
                for (int c = 0; c < m.Cols; c++)
                {
                    double cellX = x + c * 60;
                    double cellY = y + r * 40;
                    SplashKit.DrawRectangle(Color.Black, cellX, cellY, 50, 30);
                    SplashKit.DrawText(m.GetValue(r, c).ToString("0.##"), Color.Black, cellX + 10, cellY + 10);
                }
            }
        }
    }
}