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
        string tempData = ""; // MultiPurpose string to store user input
        MatrixData tempMatrix;
        string currentKey = ""; // To store the user input
        // State Machine (to handle whats currently happening)
        enum State
        {
            Idle,
            EnteringDimensions,
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
                currentKey = ""; // Reset current key each frame

                // ---------------------------------------------------------
                // Print current key pressed
                // ---------------------------------------------------------

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
                            Console.WriteLine($"Key Pressed: {currentKeyPressed}"); // Dev: Print the raw KeyCode to console for debugging
                            break; // Stop looping once we find the pressed key
                        }
                    }

                    if (currentKeyPressed != KeyCode.UnknownKey)
                    {
                        currentKey = currentKeyPressed.ToString();
                    }
                }



                // ---------------------------------------------------------
                // State Machine
                // ---------------------------------------------------------

                if (globalState != previousState)
                {
                    Console.WriteLine($"State changed from {previousState} to {globalState}");
                    previousState = globalState; // Update previous state
                }



                // ---------------------------------------------------------
                // State: Idle
                // ---------------------------------------------------------

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
                        // Change state to entering matrix
                        globalState = State.EnteringDimensions; // Example for entering matrix A, can be changed based on user selection
                    }
                }
                

                // ---------------------------------------------------------
                // State: EnteringDimensions
                // ---------------------------------------------------------

                if (globalState == State.EnteringDimensions)
                {
                    // Enter Row, then Column dimensions, then move to EnteringData state
                    if (!string.IsNullOrEmpty(currentKey))
                    {
                        // Format the raw KeyCode string
                        string cache = GetValidNum(currentKey); 
                        
                        // Verify it is a valid key (0-9, minus, period, or Backspace)
                        bool isValid = (cache == "Enter" ||
                                        cache == "Backspace" ||
                                        (cache.Length == 1 && char.IsDigit(cache[0])));
                        
                        if (isValid)
                        {
                            Console.WriteLine($"Validated Key: '{cache}'");
                            if (cache == "Backspace")
                            {
                                if (tempData.Length > 0)
                                {
                                    tempData = tempData.Substring(0, tempData.Length - 1); // Remove last character
                                    Console.WriteLine($"Current Input after Backspace: '{tempData}'");
                                }
                            } else if (cache == "Enter")
                            {
                                if (tempData.Length > 0)
                                {
                                    if (tempRows == 0)
                                    {
                                        tempRows = int.Parse(tempData);
                                        Console.WriteLine($"Rows set to: {tempRows}");
                                    }
                                    else if (tempCols == 0)
                                    {
                                        tempCols = int.Parse(tempData);
                                        Console.WriteLine($"Columns set to: {tempCols}");
                                        DrawMatrix(tempRows, tempCols); // This also changes State
                                    }
                                    tempData = ""; // Reset tempData for next input
                                }
                            }
                            else
                            {
                                tempData += cache; // Append valid key to tempData
                                Console.WriteLine($"Current Input: '{tempData}'");
                            }
                        }
                    }
                }
                

                // ---------------------------------------------------------
                // State: EnteringData
                // ---------------------------------------------------------

                if (globalState == State.EnteringData)
                {
                    // // Add Zero Matrix, Reset tempRows and tempCols
                    // if (tempRows > 0 && tempCols > 0)
                    // {
                    //     // tempRows = 0;
                    //     // tempCols = 0;
                    // } else { Console.WriteLine($"ERROR: Invalid matrix dimensions: {tempRows} x {tempCols}"); globalState = State.Idle; }
                    
                    // Populate the Zero Matrix data
                    if (!string.IsNullOrEmpty(currentKey))
                    {
                        // Format the raw KeyCode string
                        string cache = GetValidNum(currentKey);
                        
                        // Verify it is a valid key (0-9, minus, period, or Backspace)
                        bool isValid = (cache == "Enter" ||
                                        cache == "." ||
                                        cache == "-" ||
                                        cache == "Backspace" ||
                                        (cache.Length == 1 && char.IsDigit(cache[0])));
                        
                        if (isValid)
                        {
                            Console.WriteLine($"Validated Key: '{cache}'");
                            if (cache == "Backspace")
                            {
                                if (tempData.Length > 0)
                                {
                                    tempData = tempData.Substring(0, tempData.Length - 1); // Remove last character
                                    Console.WriteLine($"Current Input after Backspace: '{tempData}'");
                                }
                            }
                            else if (cache == "Enter")
                            {
                                if (tempData.Length > 0)
                                {
                                    // Fill that data in Matrix cell (tempRows, tempCols)
                                    Console.WriteLine($"Placing value '{tempData}' in cell ({tempRows}, {tempCols})");
                                    tempMatrix.matrix[tempRows, tempCols] = double.Parse(tempData);
                                    tempData = ""; // Reset tempData for next input
                                    tempCols++; // Increment column
                                    if (tempCols >= tempMatrix.cols)
                                    {
                                        tempCols = 0; // Reset column
                                        tempRows++; // Increment row
                                        if (tempRows >= tempMatrix.rows)
                                        {
                                            Console.WriteLine("Matrix input complete");
                                            globalState = State.Idle; // Move back to idle after filling the matrix
                                        }
                                    }
                                }
                            }
                            else
                            {
                                tempData += cache; // Append valid key to tempData
                                Console.WriteLine($"Current Input: '{tempData}'");
                            }
                        }
                    }
                }

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
            Console.WriteLine($"Drawing matrix with dimensions: {Rows} x {Cols}");
            // // Update temp rows and cols for later use when storing matrix data
            // tempRows = Rows;
            // tempCols = Cols;

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

            // Update State
            globalState = State.EnteringData;
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

        private string GetValidNum(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";

            // Trim "Key" at the end if it exists (e.g. "Num1Key" -> "Num1", "BackspaceKey" -> "Backspace")
            if (input.EndsWith("Key"))
            {
                input = input.Substring(0, input.Length - 3);
            }

            // If it starts with "Num" (e.g. "Num1"), trim "Num" so we just get the number ("1")
            if (input.StartsWith("Num"))
            {
                input = input.Substring(3);
            }
            else if (input.StartsWith("Keypad"))
            {
                input = input.Substring(6); // Trim "Keypad" (e.g. "Keypad1" -> "1")
            }

            // Map special character formats
            if (input == "Minus") return "-";
            if (input == "Period") return ".";
            if (input == "Return") return "Enter";

            return input;
        }
    }
}
