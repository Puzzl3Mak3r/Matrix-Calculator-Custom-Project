using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    // Matrix Enum
    public struct MatrixData
    {
        public int rows; // rows
        public int cols; // columns
        public double[,] matrix; // 2D array
    }

    public class Program
    {
        // ---------------------------------------------------------
        //  Window and Object Variables
        private Window _window;
        private RenderVisuals _renderVisuals;
        private MatrixMemento _matrixMemento;
        private Operation _matrixOperation;

        // ---------------------------------------------------------
        // Temporary Variables
        // They change a lot and handle carry data across the program
        int tempRows = 0;
        int tempCols = 0;
        string messageText = "Click to add Matrix"; // Initial message
        string tempData = ""; // MultiPurpose string to store user input
        MatrixData tempMatrix;
        string currentKey = ""; // To store the user input
        bool matricesShown = false;
        
        
        // ---------------------------------------------------------
        // States
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
        // Matrix
        private MatrixData[] _matrices = new MatrixData[3]; // Stores Matrix A, B, and Result
        int currentCellX = 0;
        int currentCellY = 0;



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
            _renderVisuals      = new RenderVisuals();
        }



        // ---------------------------------------------------------
        // Core Execution Loop
        // ---------------------------------------------------------

        public void Run()
        {
            // Clear screen to prevent ghosting
            SplashKit.ClearScreen(Color.White);
            _renderVisuals.DrawUI();
            messageText = "Click to add matrix";
            _renderVisuals.UpdateMessageText(messageText); // First run

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
                // Buttons Handler
                // ---------------------------------------------------------

                // Vars
                Point2D mousePos = SplashKit.MousePosition();

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    if (SplashKit.PointInRectangle(mousePos, _renderVisuals.AddButton))
                    {
                        // Handle addition
                        Console.WriteLine("Button Clicked: Add");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.SubtractButton))
                    {
                        // Handle subtraction
                        Console.WriteLine("Button Clicked: Subtract");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.MultiplyButton))
                    {
                        // Handle multiplication
                        Console.WriteLine("Button Clicked: Multiply");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.TransposeButton))
                    {
                        // Handle transpose
                        Console.WriteLine("Button Clicked: Transpose");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.InverseButton))
                    {
                        // Handle inverse
                        Console.WriteLine("Button Clicked: Inverse");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.DeterminantButton))
                    {
                        // Handle determinant
                        Console.WriteLine("Button Clicked: Determinant");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.Bottom1Button))
                    {
                        // Handle Bottom 1
                        Console.WriteLine("Button Clicked: Bottom 1");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.Bottom2Button))
                    {
                        // Handle Bottom 2
                        Console.WriteLine("Button Clicked: Bottom 2");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.Bottom3Button))
                    {
                        // Handle Bottom 3
                        Console.WriteLine("Button Clicked: Bottom 3");
                    }
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.Bottom4Button))
                    {
                        // Handle Bottom 4
                        Console.WriteLine("Button Clicked: Bottom 4");
                    }
                }



                // ---------------------------------------------------------
                // State: Idle
                // ---------------------------------------------------------

                if (globalState == State.Idle )
                {
                    // Draw Matrix(s) if existing
                    if (_matrices[1].matrix != null && !matricesShown)
                    {
                        // Display first matrix on left, second matrix on right
                        DisplayMatrix(_matrices[0], 10, 150, 370, 360);
                        DisplayMatrix(_matrices[1], 360, 150, 370, 360);
                        matricesShown = true;
                    } else if (_matrices[0].matrix != null && !matricesShown)
                    {
                        // Display first matrix in center
                        DisplayMatrix(_matrices[0], 185, 150, 370, 360);
                        matricesShown = true;
                    }

                    // Matrix OnClicks Actions (Uses the same mousePos)
                    if (SplashKit.MouseClicked(MouseButton.LeftButton))
                    {

                        // Adding Matrices OnClick
                        if (SplashKit.PointInRectangle(mousePos, _renderVisuals.MatrixEntryBox))
                        {
                            // Change state to entering matrix
                            Reset();
                            matricesShown = false;
                            globalState = State.EnteringDimensions; // Example for entering matrix A, can be changed based on user selection
                            messageText = "How many Rows? (Esc to exit)";
                            _renderVisuals.UpdateMessageText(messageText);
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
                                    _renderVisuals.UpdateMatrixDisplay(globalState == State.EnteringData, messageText, tempData);
                                }
                            } else if (cache == "Escape")
                            {
                                // Exit entering data
                                Reset();
                                messageText = "Click to add matrix";
                                _renderVisuals.UpdateMessageText(messageText);
                                Console.WriteLine("Matrix input canceled");
                            } else if (cache == "Enter")
                            {
                                if (tempData.Length > 0 && int.TryParse(tempData, out int parsedValue))
                                {
                                    if (tempRows == 0)
                                    {
                                        tempRows = parsedValue;
                                        Console.WriteLine($"Rows set to: {tempRows}");
                                        tempData = ""; // Reset tempData for next input
                                        messageText = "How many Columns? (Esc to exit)";
                                        _renderVisuals.UpdateMatrixDisplay(globalState == State.EnteringData, messageText, tempData);
                                    }
                                    else if (tempCols == 0)
                                    {
                                        tempCols = parsedValue;
                                        Console.WriteLine($"Columns set to: {tempCols}");
                                        tempData = ""; // Reset tempData for next input
                                        messageText = "";
                                        _renderVisuals.DrawMatrix(tempRows, tempCols);
                                        tempMatrix = MatrixFactory.CreateZMatrix(tempRows, tempCols);
                                        Console.WriteLine($"Matrix created with dimensions: {tempMatrix.rows} x {tempMatrix.cols}");
                                        globalState = State.EnteringData;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Invalid input: '{tempData}' is not a valid integer.");
                                }
                            }
                            else
                            {
                                tempData += cache; // Append valid key to tempData
                                Console.WriteLine($"Current Input: '{tempData}'");
                                _renderVisuals.UpdateMatrixDisplay(globalState == State.EnteringData, messageText, tempData);
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
                    if (messageText != $"Enter value in cell ({currentCellY+1}, {currentCellX+1}) (Esc to exit)")
                    {
                        messageText = $"Enter value in cell ({currentCellY+1}, {currentCellX+1}) (Esc to exit)";
                        _renderVisuals.UpdateMatrixDisplay(globalState == State.EnteringData, messageText, tempData);
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
                                    _renderVisuals.UpdateMatrixDisplay(globalState == State.EnteringData, messageText, tempData);
                                }
                            } else if (cache == "Escape")
                            {
                                // Exit entering data
                                Reset();
                                messageText = "Click to add matrix";
                                _renderVisuals.UpdateMessageText(messageText);
                                Console.WriteLine("Matrix input canceled");
                            }
                            else if (cache == "Enter")
                            {
                                if (tempData.Length == 0 || tempData == "-" || tempData == ".")
                                {
                                    Console.WriteLine("Invalid input: No valid number entered");
                                }
                                else
                                {
                                    if (tempData.Length > 0 && double.TryParse(tempData, out double parsedValue))
                                    {
                                        // Fill that data in Matrix cell (tempRows, currentCellX)
                                        Console.WriteLine($"Placing value '{parsedValue}' in cell ({currentCellY}, {currentCellX})");
                                        tempMatrix.matrix[currentCellY, currentCellX] = parsedValue;
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
                                                Reset();
                                                messageText = "Click to add matrix";
                                                _renderVisuals.UpdateMessageText(messageText);
                                            }
                                        }
                                        _renderVisuals.UpdateMatrixDisplay(globalState == State.EnteringData, messageText, tempData);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Invalid input: '{tempData}' is not a valid number.");
                                    }
                                }
                            }
                            else
                            {
                                // Guardrails for decimal points and minus signs
                                if (cache == "." && tempData.Contains("."))
                                {
                                    Console.WriteLine("Invalid input: Multiple decimal points");
                                }
                                else if (cache == "-" && tempData.Length > 0)
                                {
                                    Console.WriteLine("Invalid input: Minus sign can only be at the beginning");
                                }
                                else
                                {
                                    tempData += cache; // Append valid key to tempData
                                    Console.WriteLine($"Current Input: '{tempData}'");

                                    // Draw the current input
                                    _renderVisuals.UpdateMatrixDisplay(globalState == State.EnteringData, messageText, tempData);
                                }
                            }
                        }
                    }
                }

                // Update Screen // Remove\Comment out when final project is done to reduce power consumption, only use when debugging
                SplashKit.RefreshScreen();
            }
        }

        // ---------------------------------------------------------
        // State & Board Management
        // ---------------------------------------------------------

        private void Reset()
        {
            _renderVisuals.ClearBoard();
            globalState = State.Idle;
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
            // Store the matrix
            if (_matrices[0].matrix == null)
            {
                _matrices[0] = M;
            }
            else
            {
                _matrices[1] = M;
            }



            // Print the matrix
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

        // Display matrix on matrixEntryBox
        void DisplayMatrix(MatrixData M, int x, int y, int w, int h)
        {
            Console.WriteLine($"Displaying matrix with dimensions: {M.rows} x {M.cols}");

            // Add Brackets
            _renderVisuals.DrawBrackets((double)x, (double)y, (double)w, (double)h);


            // Iterate through the values
            for (int i = 0; i < M.rows; i++)
            {
                for (int j = 0; j < M.cols; j++)
                {
                    string val = M.matrix[i, j].ToString();
                    Font font = SplashKit.GetSystemFont();
                    const int fontSize = 18;

                    // Calculate grid positions based on provided dimensions and top-left corner
                    double cellWidth = (w - 80) / (double)M.cols;
                    double cellHeight = (h - 80) / (double)M.rows;
                    double textX = x + 40 + (j * cellWidth) + (cellWidth / 2) - (SplashKit.TextWidth(val, font, fontSize) / 2);
                    double textY = y + 40 + (i * cellHeight) + (cellHeight / 2) - (SplashKit.TextHeight(val, font, fontSize) / 2);

                    SplashKit.DrawText(val, Color.Black, font, fontSize, textX, textY);
                }
            }
        }
    }
}
