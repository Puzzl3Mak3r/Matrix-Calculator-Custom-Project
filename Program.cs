using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    public struct MatrixData
    {
        public int rows; // rows
        public int cols; // columns
        public double[,] matrix; // 2D array
    }

    public class Program
    {
        // Variables for GUI
        private Window _window;
        private Rectangle _addButton;
        private Rectangle _subtractButton;
        private Rectangle _multiplyButton;
        private Rectangle _transposeButton;
        private Rectangle _inverseButton;

        // Var for entering data into matrix
        private Rectangle _matrixEntryBox;
        int tempRows = 0;
        int tempCols = 0;
        MatrixData tempMatrix;
        string currentKey = ""; // To store the user input
        string previousKey = ""; // To store the previous character for backspace handling

        // State Machine (to handle whats currently happening)
        enum State
        {
            Idle,
            EnteringData,
            ExecutingMath
        };
        State globalState = State.Idle;
        State previousState = State.Idle;

        // Main Method
        public static void Main()
        {
            Program program = new Program();
            program.Run();
        }

        // Constructor
        public Program()
        {
            // Initialize the window and buttons
            _window = new Window("Matrix Calculator", 800, 600);
            _addButton        = SplashKit.RectangleFrom(50, 50, 100, 50);
            _subtractButton   = SplashKit.RectangleFrom(200, 50, 100, 50);
            _multiplyButton   = SplashKit.RectangleFrom(350, 50, 100, 50);
            _transposeButton  = SplashKit.RectangleFrom(500, 50, 100, 50);
            _inverseButton    = SplashKit.RectangleFrom(650, 50, 100, 50);
            _matrixEntryBox   = SplashKit.RectangleFrom(50, 150, 700, 400);
        }

        public void Run()
        {
            // Clear screen to prevent ghosting
            SplashKit.ClearScreen(Color.White);
            DrawUI();

            // Main event loop
            while (!_window.CloseRequested)
            {
                SplashKit.ProcessEvents();

                // Print current key pressed

                if (SplashKit.AnyKeyPressed())
                {
                    KeyCode currentKeyPressed = KeyCode.UnknownKey;

                    // Loop through all available KeyCodes in the SplashKit enum
                    foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                    {
                        // Check which specific key was just typed
                        if (SplashKit.KeyTyped(key))
                        {
                            currentKeyPressed = key;
                            break; // Stop looping once we find the pressed key
                        }
                    }

                    // SplashKit.KeyName() converts the raw KeyCode into a clean string (e.g., "Space")
                    if (previousKey != currentKeyPressed.ToString())
                    {
                        Console.WriteLine($"Key Pressed: {currentKeyPressed}");
                        previousKey = currentKeyPressed.ToString(); // Update previous key
                    }
                }

                // Dev: Show state machine changes
                if (globalState != previousState)
                {
                    Console.WriteLine($"State changed from {previousState} to {globalState}");
                    previousState = globalState; // Update previous state
                }


                // Idle
                if (globalState == State.Idle && SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Point2D mousePos = SplashKit.MousePosition();

                    // Matrix OnClicks Actions

                    // Operation Actions
                    if (SplashKit.PointInRectangle(mousePos, _addButton))
                    {
                        // Handle addition
                        Console.WriteLine("Button Clicked: Add");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _subtractButton))
                    {
                        // Handle subtraction
                        Console.WriteLine("Button Clicked: Subtract");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _multiplyButton))
                    {
                        // Handle multiplication
                        Console.WriteLine("Button Clicked: Multiply");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _transposeButton))
                    {
                        // Handle transpose
                        Console.WriteLine("Button Clicked: Transpose");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _inverseButton))
                    {
                        // Handle inverse
                        Console.WriteLine("Button Clicked: Inverse");
                    }

                    // Adding Matrices OnClick
                    if (SplashKit.PointInRectangle(mousePos, _matrixEntryBox))
                    {
                        // Handle matrix entry box click, maybe open a new window or allow typing in the box
                        Console.WriteLine("Button Clicked: Matrix Entry Box");

                        // Change state to entering matrix
                        globalState = State.EnteringData; // Example for entering matrix A, can be changed based on user selection

                    // Tell SplashKit to start capturing text inside the bounding box
                    SplashKit.StartReadingText(_matrixEntryBox);

                        // // Draw Matrix entry box and grid for entering values
                        // DrawMatrix(3, 3); // Example for a 3x3 matrix,
                    }
                }

                // // Adding Data to Matrix
                // if (globalState == State.EnteringData)
                // {
                //     // Handle data entry for the matrix
                //     if (SplashKit.KeyTyped(KeyCode.BackspaceKey))
                //     {
                //         // Handle backspace, remove last character from current input
                //         Console.WriteLine("Backspace Pressed");
                //         // Implement logic to remove last character from the matrix entry
                //     }
                //     else if (SplashKit.KeyTyped(KeyCode.EnterKey))
                //     {
                //         // Handle enter key, finalize matrix entry
                //         Console.WriteLine("Enter Key Pressed");
                //         // Implement logic to finalize matrix entry and maybe change state back to idle
                //     }
                //     else
                //     {
                //         // Handle other key inputs for matrix entry
                //         currentKey = SplashKit.LastKeyTyped();
                //         Console.WriteLine($"Key Typed: {currentKey}");
                //         // Implement logic to add the typed character to the matrix entry
                //     }
                //     if (previousKey != currentKey)
                //     {
                //         Console.WriteLine($"Current Key: {currentKey}, Previous Key: {previousKey}");
                //         previousKey = currentKey; // Update previous character
                //     }
                // }

                // Update Screen // Remove\Comment out when final project is done to reduce power consumption, only use when debugging
                SplashKit.RefreshScreen();
            }
        }

        private void DrawUI()
        {
            // Draw the buttons and other UI elements here
            DrawButton(_addButton, "Add");
            DrawButton(_subtractButton, "Subtract");
            DrawButton(_multiplyButton, "Multiply");
            DrawButton(_transposeButton, "Transpose");
            DrawButton(_inverseButton, "Inverse");
            
            // Draw Matrix entry box
            SplashKit.FillRectangle(Color.White, _matrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, _matrixEntryBox);
        }

        private void DrawButton(Rectangle rect, string text)
        {
            // Fill button background and draw a border
            SplashKit.FillRectangle(Color.LightGray, rect);
            SplashKit.DrawRectangle(Color.Black, rect);

            // Center the text inside the button
            Font font = SplashKit.GetSystemFont();
            const int fontSize = 14;
            int textWidth = SplashKit.TextWidth(text, font, fontSize);
            int textHeight = SplashKit.TextHeight(text, font, fontSize);
            double x = rect.X + (rect.Width - textWidth) / 2;
            double y = rect.Y + (rect.Height - textHeight) / 2;

            SplashKit.DrawText(text, Color.Black, font, fontSize, x, y);
        }

        private void DrawMatrix(int Rows, int Cols)
        {
            // Update temp rows and cols for later use when storing matrix data
            tempRows = Rows;
            tempCols = Cols;

            // Draw the matrix entry box and the grid for entering matrix values
            SplashKit.FillRectangle(Color.White, _matrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, _matrixEntryBox);

            // Draw the Square Brackets
            int bracketPadding = 20;
            SplashKit.DrawLine(Color.Black, _matrixEntryBox.X + bracketPadding, _matrixEntryBox.Y + bracketPadding, _matrixEntryBox.X + bracketPadding, _matrixEntryBox.Y + _matrixEntryBox.Height - bracketPadding);
            SplashKit.DrawLine(Color.Black, _matrixEntryBox.X + _matrixEntryBox.Width - bracketPadding, _matrixEntryBox.Y + bracketPadding, _matrixEntryBox.X + _matrixEntryBox.Width - bracketPadding, _matrixEntryBox.Y + _matrixEntryBox.Height - bracketPadding);

            // Store matrix
            tempMatrix = MatrixFactory.CreateZMatrix(Rows, Cols);
            Console.WriteLine($"Matrix created with dimensions: {tempMatrix.rows} x {tempMatrix.cols}");

            // Update Screen
            SplashKit.RefreshScreen();
        }

        private void ClearBoard()
        {
            // Clear the matrix entry box
            SplashKit.FillRectangle(Color.White, _matrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, _matrixEntryBox);
            // Reset matrix data
            tempMatrix = new MatrixData();
            tempRows = 0;
            tempCols = 0;
            Console.WriteLine("Matrix cleared");
        }
    }
}
