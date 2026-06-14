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
        // ---------------------------------------------------------
        // Variables & State
        // ---------------------------------------------------------

        // Variables for GUI
        private Window _window;
        private Rectangle _addButton;
        private Rectangle _subtractButton;
        private Rectangle _multiplyButton;
        private Rectangle _transposeButton;
        private Rectangle _inverseButton;
        private Rectangle _determinantButton;
        private Rectangle bottom1Button;
        private Rectangle bottom2Button;
        private Rectangle bottom3Button;
        private Rectangle bottom4Button;

        // Var for entering data into matrix
        private Rectangle _matrixEntryBox;
        int tempRows = 0;
        int tempCols = 0;
        int currentCellX = 0;
        int currentCellY = 0;
        string messageText = "Click to add Matrix";
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

        // ---------------------------------------------------------
        // Initialization
        // ---------------------------------------------------------

        // Main Method
        public static void Main()
        {
            Program program = new Program();
            program.Run();
        }

        // Program
        public Program()
        {
            // Initialize window
            _window             = new Window("Matrix Calculator", 740, 600);

            // Initialize top buttons
            _addButton          = SplashKit.RectangleFrom(20, 20, 100, 60);
            _subtractButton     = SplashKit.RectangleFrom(140, 20, 100, 60);
            _multiplyButton     = SplashKit.RectangleFrom(260, 20, 100, 60);
            _transposeButton    = SplashKit.RectangleFrom(380, 20, 100, 60);
            _inverseButton      = SplashKit.RectangleFrom(500, 20, 100, 60);
            _determinantButton  = SplashKit.RectangleFrom(620, 20, 100, 60);

            // Initialize matrix box
            _matrixEntryBox     = SplashKit.RectangleFrom(20, 100, 700, 400);

            // Initialize bottom buttons
            bottom1Button       = SplashKit.RectangleFrom(20, 520, 160, 60);
            bottom2Button       = SplashKit.RectangleFrom(200, 520, 160, 60);
            bottom3Button       = SplashKit.RectangleFrom(380, 520, 160, 60);
            bottom4Button       = SplashKit.RectangleFrom(560, 520, 160, 60);
        }

        // ---------------------------------------------------------
        // Core Execution Loop
        // ---------------------------------------------------------

        public void Run()
        {
            // Clear screen to prevent ghosting
            SplashKit.ClearScreen(Color.White);
            DrawUI();
            messageText = "Click to add matrix";
            UpdateMessageText(messageText); // First run

            // Main event loop
            while (!_window.CloseRequested)
            {
                SplashKit.ProcessEvents();
                currentKey = ""; // Reset current key each update

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

                if (globalState == State.Idle )
                {
                    // Vars
                    Point2D mousePos = SplashKit.MousePosition();

                    // Matrix OnClicks Actions
                    if (SplashKit.MouseClicked(MouseButton.LeftButton))
                    {
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
                        else if (SplashKit.PointInRectangle(mousePos, _determinantButton))
                        {
                            // Handle determinant
                            Console.WriteLine("Button Clicked: Determinant");
                        }
                        else if (SplashKit.PointInRectangle(mousePos, bottom1Button))
                        {
                            // Handle Bottom 1
                            Console.WriteLine("Button Clicked: Bottom 1");
                        }
                        else if (SplashKit.PointInRectangle(mousePos, bottom2Button))
                        {
                            // Handle Bottom 2
                            Console.WriteLine("Button Clicked: Bottom 2");
                        }
                        else if (SplashKit.PointInRectangle(mousePos, bottom3Button))
                        {
                            // Handle Bottom 3
                            Console.WriteLine("Button Clicked: Bottom 3");
                        }
                        else if (SplashKit.PointInRectangle(mousePos, bottom4Button))
                        {
                            // Handle Bottom 4
                            Console.WriteLine("Button Clicked: Bottom 4");
                        }

                        // Adding Matrices OnClick
                        if (SplashKit.PointInRectangle(mousePos, _matrixEntryBox))
                        {
                            // Change state to entering matrix
                            globalState = State.EnteringDimensions; // Example for entering matrix A, can be changed based on user selection
                            ClearBoard();
                            messageText = "How many Rows?";
                            UpdateMessageText(messageText);
                        }
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
                        string cache = GetValidKey(currentKey); 
                        
                        // Verify it is a valid key (0-9, minus, period, or Backspace)
                        bool isValid = (cache == "Enter" ||
                                        cache == "Backspace" ||
                                        cache == "Escape" ||
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
                                    UpdateMatrixDisplay();
                                }
                            } else if (cache == "Escape")
                            {
                                // Exit entering data
                                ResetVars();
                                ClearBoard();
                                globalState = State.Idle;
                                messageText = "Click to add matrix";
                                UpdateMessageText(messageText);
                                Console.WriteLine("Matrix input canceled");
                            } else if (cache == "Enter")
                            {
                                if (tempData.Length > 0)
                                {
                                    if (tempRows == 0)
                                    {
                                        tempRows = int.Parse(tempData);
                                        Console.WriteLine($"Rows set to: {tempRows}");
                                        tempData = ""; // Reset tempData for next input
                                        messageText = "How many Columns?";
                                        UpdateMatrixDisplay();
                                    }
                                    else if (tempCols == 0)
                                    {
                                        tempCols = int.Parse(tempData);
                                        Console.WriteLine($"Columns set to: {tempCols}");
                                        tempData = ""; // Reset tempData for next input
                                        messageText = "";
                                        DrawMatrix(tempRows, tempCols); // This also changes State
                                    }
                                }
                            }
                            else
                            {
                                tempData += cache; // Append valid key to tempData
                                // Console.WriteLine($"Current Input: '{tempData}'");
                                // UpdateMatrixDisplay();
                            }
                        }
                    }
                }



                // ---------------------------------------------------------
                // State: EnteringData
                // ---------------------------------------------------------

                if (globalState == State.EnteringData)
                {
                    // Prompt to enter values
                    if (messageText != $"Enter value in cell ({currentCellY}, {currentCellX})")
                    {
                        messageText = $"Enter value in cell ({currentCellY}, {currentCellX})";
                        UpdateMatrixDisplay();
                    }

                    // Populate the Zero Matrix data
                    if (!string.IsNullOrEmpty(currentKey))
                    {
                        // Format the raw KeyCode string
                        string cache = GetValidKey(currentKey);
                        
                        // Verify it is a valid key (0-9, minus, period, or Backspace)
                        bool isValid = (cache == "Enter" ||
                                        cache == "." ||
                                        cache == "-" ||
                                        cache == "Backspace" ||
                                        cache == "Escape" ||
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
                                    UpdateMatrixDisplay();
                                }
                            } else if (cache == "Escape")
                            {
                                // Exit entering data
                                ResetVars();
                                ClearBoard();
                                globalState = State.Idle;
                                messageText = "Click to add matrix";
                                UpdateMessageText(messageText);
                                Console.WriteLine("Matrix input canceled");
                            }
                            else if (cache == "Enter")
                            {
                                // Check if multiple decimal points or minus signs are being entered, if so ignore the input
                                if (cache == "." && tempData.Contains("."))
                                {
                                    Console.WriteLine("Invalid input: Multiple decimal points");
                                } else if (cache == "-" && tempData.Contains("-"))
                                {
                                    Console.WriteLine("Invalid input: Multiple minus signs");
                                }
                                else if (cache == "-" && tempData.Length > 0)
                                {
                                    Console.WriteLine("Invalid input: Minus sign can only be at the beginning");
                                }
                                else if (tempData.Length == 0)
                                {
                                    Console.WriteLine("Invalid input: No number entered");
                                }
                                else
                                {
                                    if (tempData.Length > 0)
                                    {
                                        // Fill that data in Matrix cell (tempRows, currentCellX)
                                        Console.WriteLine($"Placing value '{tempData}' in cell ({currentCellY}, {currentCellX})");
                                        tempMatrix.matrix[currentCellY, currentCellX] = double.Parse(tempData);
                                        tempData = ""; // Reset tempData for next input
                                        currentCellX++; // Increment column
                                        if (currentCellX >= tempMatrix.cols)
                                        {
                                            currentCellX = 0; // Reset column
                                            currentCellY++; // Increment row
                                            if (currentCellY >= tempMatrix.rows)
                                            {
                                                // Store the data
                                                Console.WriteLine("Matrix input complete");
                                                StoreMatrix(tempMatrix);

                                                // Go back to Idle and reset
                                                ClearBoard();
                                                globalState = State.Idle; // Move back to idle after filling the matrix
                                                messageText = "Click to add matrix";
                                                UpdateMessageText(messageText);

                                                // Print the filled matrix for debugging

                                                // // Draw the filled matrix on the screen
                                                // ClearBoard();
                                                // for (int i = 0; i < tempMatrix.rows; i++)
                                                // {
                                                //     for (int j = 0; j < tempMatrix.cols; j++)
                                                //     {
                                                //         // Draw here
                                                //     }
                                                // }
                                            }
                                        }
                                        UpdateMatrixDisplay();
                                    }
                                }
                            }
                            else
                            {
                                tempData += cache; // Append valid key to tempData
                                Console.WriteLine($"Current Input: '{tempData}'");

                                // Draw the current input
                                UpdateMatrixDisplay();
                            }
                        }
                    }
                }

                // Update Screen // Remove\Comment out when final project is done to reduce power consumption, only use when debugging
                SplashKit.RefreshScreen();
            }
        }

        // ---------------------------------------------------------
        // UI & Visual Rendering
        // ---------------------------------------------------------

        private void DrawUI()
        {
            // Draw the top buttons
            DrawButton(_addButton, "Add");
            DrawButton(_subtractButton, "Subtract");
            DrawButton(_multiplyButton, "Multiply");
            DrawButton(_transposeButton, "Transpose");
            DrawButton(_inverseButton, "Inverse");
            DrawButton(_determinantButton, "Determinant");
            
            // Draw Matrix entry box
            SplashKit.FillRectangle(Color.White, _matrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, _matrixEntryBox);

            // Draw the bottom buttons
            DrawButton(bottom1Button, "Copy Equation");
            DrawButton(bottom2Button, "Paste Equation");
            DrawButton(bottom3Button, "Copy Result");
            DrawButton(bottom4Button, "Paste Result");
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

        private void UpdateMatrixDisplay()
        {
            // Clear the matrix entry box
            SplashKit.FillRectangle(Color.White, _matrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, _matrixEntryBox);

            if (globalState == State.EnteringData)
            {
                // Draw the Square Brackets
                DrawBrackets();
            }

            // Add optional text
            UpdateMessageText(messageText);


            if (!string.IsNullOrEmpty(tempData))
            {
                Font font = SplashKit.GetSystemFont();
                const int fontSize = 20;
                int textWidth = SplashKit.TextWidth(tempData, font, fontSize);
                int textHeight = SplashKit.TextHeight(tempData, font, fontSize);
                double x = _matrixEntryBox.X + (_matrixEntryBox.Width - textWidth) / 2;
                double y = _matrixEntryBox.Y + (_matrixEntryBox.Height - textHeight) / 2;

                SplashKit.DrawText(tempData, Color.Black, font, fontSize, x, y);
            }
        }

        private void UpdateMessageText(string t)
        {
            // Write Meessage
            Console.WriteLine($"Message Text: '{tempData}'");
            Font font = SplashKit.GetSystemFont();
            const int fontSize = 20;
            int messageTextWidth = SplashKit.TextWidth(t, font, fontSize);
            SplashKit.DrawText(t, Color.Black, font, fontSize, _matrixEntryBox.X + (_matrixEntryBox.Width - messageTextWidth) / 2, _matrixEntryBox.Height / 2);
        }

        private void DrawBrackets()
        {
            int padding = 20;

            // Draw Left Bits
            SplashKit.DrawLine(Color.Black,
                _matrixEntryBox.X + padding,    _matrixEntryBox.Y + padding,
                _matrixEntryBox.X + 3*padding,  _matrixEntryBox.Y + padding);
            SplashKit.DrawLine(Color.Black,
                _matrixEntryBox.X + padding,    _matrixEntryBox.Y + _matrixEntryBox.Height - padding,
                _matrixEntryBox.X + 3*padding,  _matrixEntryBox.Y + _matrixEntryBox.Height - padding);

            // Draw Right Bits
            SplashKit.DrawLine(Color.Black,
                _matrixEntryBox.X + _matrixEntryBox.Width - padding,    _matrixEntryBox.Y + padding,
                _matrixEntryBox.X + _matrixEntryBox.Width - 3*padding,  _matrixEntryBox.Y + padding);
            SplashKit.DrawLine(Color.Black,
                _matrixEntryBox.X + _matrixEntryBox.Width - padding,    _matrixEntryBox.Y + _matrixEntryBox.Height - padding,
                _matrixEntryBox.X + _matrixEntryBox.Width - 3*padding,  _matrixEntryBox.Y + _matrixEntryBox.Height - padding);

            // Draw Sides
            SplashKit.DrawLine(Color.Black, _matrixEntryBox.X + padding, _matrixEntryBox.Y + padding, _matrixEntryBox.X + padding, _matrixEntryBox.Y + _matrixEntryBox.Height - padding);
            SplashKit.DrawLine(Color.Black, _matrixEntryBox.X + _matrixEntryBox.Width - padding, _matrixEntryBox.Y + padding, _matrixEntryBox.X + _matrixEntryBox.Width - padding, _matrixEntryBox.Y + _matrixEntryBox.Height - padding);

        }

        // ---------------------------------------------------------
        // State & Board Management
        // ---------------------------------------------------------

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
            DrawBrackets();

            // Store matrix
            tempMatrix = MatrixFactory.CreateZMatrix(Rows, Cols);
            Console.WriteLine($"Matrix created with dimensions: {tempMatrix.rows} x {tempMatrix.cols}");

            // Update State
            globalState = State.EnteringData;
            // messageText = "Enter values for matrix";
            // UpdateMessageText(messageText);
        }

        private void ClearBoard()
        {
            // Clear the matrix entry box
            SplashKit.FillRectangle(Color.White, _matrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, _matrixEntryBox);
            
            // Reset
            ResetVars();
            Console.WriteLine("Matrix cleared");
        }

        private void ResetVars()
        {
            tempMatrix = new MatrixData();
            tempRows = 0;
            tempCols = 0;
            tempData = "";
            currentCellX = 0;
            currentCellY = 0;
            Console.WriteLine("Variables reset");
        }

        // ---------------------------------------------------------
        // Input & Validation
        // ---------------------------------------------------------

        private string GetValidKey(string input)
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

        // ---------------------------------------------------------
        // Matrix Data Operations & Debugging
        // ---------------------------------------------------------

        // Store matrix
        void StoreMatrix(MatrixData M)
        {
            // // Print rows and cols
            // Console.WriteLine($"Rows: {M.rows}");
            // Console.WriteLine($"Cols: {M.cols}");

            Console.WriteLine("Matrix contents:");

            PrintMatrix(M);
        }

        void PrintMatrix(MatrixData M)
        {
            // Print each value
            for (int i = 0; i < M.rows; i++)
            {
                for (int j = 0; j < M.cols; j++)
                {
                    // Console.WriteLine($"{i}, {j}, {M.matrix[i, j]}");
                    Console.Write($"{M.matrix[i, j]}, ");
                }
                Console.WriteLine();
            }
        }
    }
}
