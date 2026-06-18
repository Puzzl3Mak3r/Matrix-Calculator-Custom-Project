using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    // Program-wide Matrix Enum
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

        // ---------------------------------------------------------
        // Window and Object Variables

        private Window _window;
        private RenderVisuals _renderVisuals;
        // private MatrixMemento _matrixMemento;
        private MOadd _MOadd = new MOadd();
        private MOsubt _MOsubt = new MOsubt();
        private MOmult _MOmult = new MOmult();
        private MOtran _MOtran = new MOtran();
        private MOinvr _MOinvr = new MOinvr();
        private MOdetr _MOdetr = new MOdetr();
        private Copy _Copy = new Copy();
        private CopyLaTeX _CopyLaTeX = new CopyLaTeX();
        private CopyASCII _CopyASCII = new CopyASCII();

        // ---------------------------------------------------------
        // Temporary Variables

        int tempRows = 0;
        int tempCols = 0;
        string messageText = "Click to add Matrix"; // Initial message
        string tempData = ""; // MultiPurpose string to store user input
        MatrixData tempMatrix;
        string currentKey = ""; // To store the user input
        bool matricesShown = false; // To check if matrices are already diaplyed in State.Idle
        bool shoRidOfMsg = false;
        


        // ---------------------------------------------------------
        // States

        enum State
        {
            Idle,
            ConfirmRidOfMatrix,
            EnteringDimensions,
            EnteringData,
            ExecutingMath,
            CopyPasting
        };
        State globalState = State.Idle;
        State previousState = State.Idle;

        // ---------------------------------------------------------
        // Matrix and Equation Variables

        private MatrixData[] _matrices = new MatrixData[3]; // Stores Matrix A, B, and Result
        int currentCellX = 0;
        int currentCellY = 0;
        string mathUsed = ""; // Tracking math to be use in copy paste
        string copyMethodUsed = ""; // Tracking what copy paste function to use



        // ---------------------------------------------------------
        // Initialization
        // ---------------------------------------------------------

        public static void Main()
        {
            Program program = new Program();
            program.Run();
        }

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

            // Main event loop
            while (!_window.CloseRequested)
            {
                SplashKit.ProcessEvents();
                currentKey = ""; // Reset current key each update

                // ---------------------------------------------------------
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
                    if (globalState != State.Idle && globalState != State.CopyPasting)
                    {
                        // Prompt to only do it when its in idle
                        Console.WriteLine("User attempted to use operation out of State.Idle");
                        _renderVisuals.ClearBoard();
                        matricesShown = false;
                        _renderVisuals.UpdateMessageText($"{messageText}\nComplete the current matrix first");
                    }
                    else
                    {
                        if (_matrices[0].matrix != null)
                        {
                            if (_matrices[1].matrix != null)
                            {
                                // ---------------------------------------------------------
                                // 1. Two Matrix functions

                                if (SplashKit.PointInRectangle(mousePos, _renderVisuals.AddButton))
                                {
                                    // Run Dimension Checker
                                    if (!(_matrices[0].rows == _matrices[1].rows && _matrices[0].cols == _matrices[1].cols))
                                    {
                                        _renderVisuals.ReRenderMatrices(_matrices);
                                        matricesShown = true;
                                        Console.WriteLine("Error: Matrices must have the same dimensions for addition");
                                        _renderVisuals.UpdateMessageText($"{messageText}\nMatrices must have the same dimensions for addition");
                                    }
                                    else
                                    {
                                        // Handle addition: Set strategy to MOadd, execute, and prepare to display result
                                        Console.WriteLine("Button Clicked: Add");
                                        _matrices[2] = _MOadd.ExecuteTwo(_matrices[0], _matrices[1]);
                                        _renderVisuals.ClearBoard(); // Clear UI for result display
                                        matricesShown = false;
                                        globalState = State.ExecutingMath;
                                        Console.WriteLine(_Copy.MatrixToText(_matrices[2]));
                                        mathUsed = "add";
                                    }
                                }
                                else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.SubtractButton))
                                {
                                    // Run Dimension Checker
                                    if (!(_matrices[0].rows == _matrices[1].rows && _matrices[0].cols == _matrices[1].cols))
                                    {
                                        _renderVisuals.ReRenderMatrices(_matrices);
                                        matricesShown = true;
                                        Console.WriteLine("Error: Matrices must have the same dimensions for subtraction");
                                        _renderVisuals.UpdateMessageText($"{messageText}\nMatrices must have the same dimensions for subtraction");
                                    }
                                    else
                                    {
                                        // Handle subtraction: Set strategy to MOsub, execute, and prepare to display result
                                        Console.WriteLine("Button Clicked: Subtract");
                                        _matrices[2] = _MOsubt.ExecuteTwo(_matrices[0], _matrices[1]);
                                        _renderVisuals.ClearBoard(); // Clear UI for result display
                                        matricesShown = false;
                                        globalState = State.ExecutingMath;
                                        Console.WriteLine(_Copy.MatrixToText(_matrices[2]));
                                        mathUsed = "subtract";
                                    }
                                }
                                else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.MultiplyButton))
                                {
                                    // Run Dimension Checker
                                    if (_matrices[0].cols != _matrices[1].rows)
                                    {
                                        _renderVisuals.ClearBoard();
                                        _renderVisuals.DisplayMatrix(_matrices[0], 10, 150, 370, 360);
                                        _renderVisuals.DisplayMatrix(_matrices[1], 360, 150, 370, 360);
                                        Console.WriteLine("Error: Matrices are not compatible for multiplication");
                                        _renderVisuals.UpdateMessageText($"{messageText}\nMatrices are not compatible for multiplication");
                                    }
                                    else
                                    {
                                        // Handle multiplication
                                        Console.WriteLine("Button Clicked: Multiply");
                                        _matrices[2] = _MOmult.ExecuteTwo(_matrices[0], _matrices[1]);
                                        _renderVisuals.ClearBoard(); // Clear UI for result display
                                        matricesShown = false;
                                        globalState = State.ExecutingMath;
                                        Console.WriteLine(_Copy.MatrixToText(_matrices[2]));
                                        mathUsed = "multiply";
                                    }
                                }
                            }

                            // ---------------------------------------------------------
                            // 2. One Matrix Functions

                            if (SplashKit.PointInRectangle(mousePos, _renderVisuals.TransposeButton))
                            {
                                if (_matrices[1].matrix != null)
                                {
                                    _renderVisuals.ReRenderMatrices(_matrices);
                                    matricesShown = true;
                                    Console.WriteLine("Error: There are multiple matrices");
                                    _renderVisuals.UpdateMessageText("There are multiple matrices\nPlease clear matrices, and enter 1 matrix");
                                }
                                else
                                {
                                    // Handle transpose
                                    Console.WriteLine("Button Clicked: Transpose");
                                    _matrices[2] = _MOtran.ExecuteOne(_matrices[0]);
                                    _renderVisuals.ClearBoard();
                                    matricesShown = false;
                                    globalState = State.ExecutingMath;
                                    Console.WriteLine(_Copy.MatrixToText(_matrices[2]));
                                    mathUsed = "transpose";
                                }
                            }
                            else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.InverseButton))
                            {
                                if (_matrices[1].matrix != null) // Checks if a second matrix exists
                                {
                                    _renderVisuals.ReRenderMatrices(_matrices);
                                    matricesShown = true;
                                    Console.WriteLine("Error: There are multiple matrices");
                                    _renderVisuals.UpdateMessageText("There are multiple matrices\nPlease clear matrices, and enter 1 matrix");
                                }
                                else if (_matrices[0].rows != _matrices[0].cols) // Checks if the matrix is a square
                                {
                                    _renderVisuals.ReRenderMatrices(_matrices);
                                    matricesShown = true;
                                    Console.WriteLine("Error: Matrix must be square for determinant");
                                    _renderVisuals.UpdateMessageText($"{messageText}\nMatrix must be square for determinant");
                                }
                                else if (_matrices[0].rows != 2 && _matrices[0].rows != 3) // Checks if it is a 2x2 or 3x3 matrix
                                {
                                    _renderVisuals.ReRenderMatrices(_matrices);
                                    matricesShown = true;
                                    Console.WriteLine("Error: Not a 2x2 or 3x3 matrix");
                                    _renderVisuals.UpdateMessageText($"{messageText}\nInverse only supports 2x2 or 3x3 matrix");
                                }
                                else if (_MOdetr.CalculateDeterminant(_matrices[0]) == 0) // Checks if the matrix is singular
                                {
                                    _renderVisuals.ReRenderMatrices(_matrices);
                                    matricesShown = true;
                                    Console.WriteLine("Error: Matrix is singular (determinant is 0)");
                                    _renderVisuals.UpdateMessageText($"{messageText}\nMatrix is singular (cannot be inverted)");
                                }
                                else
                                {
                                    // Handle inverse
                                    Console.WriteLine("Button Clicked: Inverse");
                                    _matrices[2] = _MOinvr.ExecuteOne(_matrices[0]);
                                    _renderVisuals.ClearBoard();
                                    matricesShown = false;
                                    globalState = State.ExecutingMath;
                                    Console.WriteLine(_Copy.MatrixToText(_matrices[2]));
                                    mathUsed = "inverse";
                                }
                            }
                            else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.DeterminantButton))
                            {
                                // Run Dimension Checker
                                
                                if (_matrices[1].matrix != null)
                                {
                                    _renderVisuals.ReRenderMatrices(_matrices);
                                    matricesShown = true;
                                    Console.WriteLine("Error: There are multiple matrices");
                                    _renderVisuals.UpdateMessageText("There are multiple matrices\nPlease clear matrices, and enter 1 matrix");
                                }
                                else if (_matrices[0].rows != _matrices[0].cols)
                                {
                                    _renderVisuals.ReRenderMatrices(_matrices);
                                    matricesShown = true;
                                    Console.WriteLine("Error: Matrix must be square for determinant");
                                    _renderVisuals.UpdateMessageText($"{messageText}\nMatrix must be square for determinant");
                                }
                                else if (_matrices[0].rows != 2 && _matrices[0].rows != 3)
                                {
                                    _renderVisuals.ReRenderMatrices(_matrices);
                                    matricesShown = true;
                                    Console.WriteLine("Error: Not a 2x2 or 3x3 matrix");
                                    _renderVisuals.UpdateMessageText($"{messageText}\nDeterminant only supports 2x2 or 3x3 matrix");
                                }
                                else
                                {
                                    // Handle determinant
                                    Console.WriteLine("Button Clicked: Determinant");
                                    double det = _MOdetr.CalculateDeterminant(_matrices[0]);
                                    
                                    // Store into a 1x1 matrix
                                    _matrices[2] = new MatrixData();
                                    _matrices[2].rows = 1;
                                    _matrices[2].cols = 1;
                                    _matrices[2].matrix = new double[1, 1];
                                    _matrices[2].matrix[0, 0] = det;
                                    
                                    _renderVisuals.ClearBoard();
                                    matricesShown = false;
                                    globalState = State.ExecutingMath;
                                    Console.WriteLine(_Copy.MatrixToText(_matrices[2]));
                                    mathUsed = "determinant";
                                }
                            }
                        }
                    }
                    
                    // ---------------------------------------------------------
                    // 3. Bottom Buttons - Not exclusively State.Idle

                    // Handle Bottom 1
                    // Copy Equation // Copy/Paste Raw
                    if (SplashKit.PointInRectangle(mousePos, _renderVisuals.Bottom1Button))
                    {
                        // Copying Equation
                        if (globalState == State.Idle)
                        {
                            copyMethodUsed = "copyE";
                            globalState = State.CopyPasting;
                            _renderVisuals.DrawBButtons2();
                        }
                        // Copy/Paste Raw
                        else if (globalState == State.CopyPasting)
                        {
                            FinishCopyAction(_Copy);
                        }
                    }
                    // Handle Bottom 2
                    // Copy LaTeX
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.Bottom2Button))
                    {
                        Console.WriteLine("Button Clicked: Bottom 2");

                        // Copy LaTeX
                        if (globalState == State.CopyPasting)
                        {
                            FinishCopyAction(_CopyLaTeX);
                        }
                    }
                    // Handle Bottom 3
                    // Copy Result // Copy ASCII
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.Bottom3Button))
                    {
                        Console.WriteLine("Button Clicked: Bottom 3");
                        
                        // Copying Result
                        if (globalState == State.Idle)
                        {
                            copyMethodUsed = "copyR";
                            globalState = State.CopyPasting;
                            _renderVisuals.DrawBButtons2();
                        }
                        // Copy ASCII
                        else if (globalState == State.CopyPasting)
                        {
                            FinishCopyAction(_CopyASCII);
                        }
                    }
                    // Handle Bottom 4
                    // Exit
                    else if (SplashKit.PointInRectangle(mousePos, _renderVisuals.Bottom4Button))
                    {
                        Console.WriteLine("Button Clicked: Bottom 4");
                        
                        // Exit
                        if (globalState == State.CopyPasting)
                        {
                            copyMethodUsed = "";
                            globalState = State.Idle;
                            _renderVisuals.DrawBButtons1();
                        }
                    }
                }



                // ---------------------------------------------------------
                // State: Idle
                // ---------------------------------------------------------

                if (globalState == State.Idle )
                {
                    // Draw Matrix(s) if existing
                    if (!matricesShown)
                    {
                        _renderVisuals.ReRenderMatrices(_matrices);
                        matricesShown = true;
                        messageText = "Click to add matrix";
                        _renderVisuals.UpdateMessageText(messageText);
                    }

                    // Matrix OnClicks Actions (Uses the same mousePos)
                    if (SplashKit.MouseClicked(MouseButton.LeftButton))
                    {

                        // Adding Matrices OnClick
                        if (SplashKit.PointInRectangle(mousePos, _renderVisuals.MatrixEntryBox))
                        {
                            if (_matrices[1].matrix != null)
                            {
                                // Confirm to get rid of current matrices
                                globalState = State.ConfirmRidOfMatrix;
                            }
                            else
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
                }



                // ---------------------------------------------------------
                // State: ConfirmRidOfMatrix // To confirm user to get rid of matrices
                // ---------------------------------------------------------

                if (globalState == State.ConfirmRidOfMatrix)
                {
                    if (!shoRidOfMsg)
                    {
                        // Prompt user if they want to erase current 2 matrices
                        shoRidOfMsg = true; // Don't overlap text
                        _renderVisuals.ClearBoard();
                        matricesShown = false;
                        messageText = "Clear current 2 matrices? Yes(Enter) No(Esc)";
                        _renderVisuals.UpdateMessageText(messageText);
                    }
                    
                    if (!string.IsNullOrEmpty(currentKey))
                    {
                        string cache = UserInput.GetValidKey(currentKey);
                        if (cache == "Enter")
                        {
                            // Delete the matrices
                            _matrices[0] = new MatrixData();
                            _matrices[1] = new MatrixData();
                            _matrices[2] = new MatrixData();
                            _renderVisuals.ClearBoard();
                            matricesShown = false;
                            _renderVisuals.UpdateMessageText("Click to add matrix");
                            globalState = State.Idle;
                            shoRidOfMsg = false; // Change for reuse
                            mathUsed = "";
                            Console.WriteLine("Matrices cleared");
                        }
                        else if (cache == "Escape")
                        {
                            _renderVisuals.ClearBoard();
                            matricesShown = false;
                            _renderVisuals.UpdateMessageText("Click to add matrix");
                            globalState = State.Idle;
                        }
                    }
                }



                // ---------------------------------------------------------
                // State: CopyPasting
                // ---------------------------------------------------------

                if (globalState == State.CopyPasting)
                {
                    if (previousState != State.CopyPasting)
                    {
                        Console.WriteLine(copyMethodUsed);
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
                        string cache = UserInput.GetValidKey(currentKey); 
                        
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
                            }
                            else if (cache == "Enter")
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
                        string cache = UserInput.GetValidKey(currentKey);
                        
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
                                                if (_matrices[0].matrix == null)
                                                {
                                                    _matrices[0] = tempMatrix;
                                                }
                                                else
                                                {
                                                    _matrices[1] = tempMatrix;
                                                }
                                                Console.WriteLine("Matrix contents:");
                                                Console.WriteLine(_Copy.MatrixToText(tempMatrix));

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



                // ---------------------------------------------------------
                // State: ExecutingMath
                // ---------------------------------------------------------

                if (globalState == State.ExecutingMath)
                {
                    Console.WriteLine("Executing Math");
                    _renderVisuals.ClearBoard();
                    _renderVisuals.DisplayMatrix(_matrices[2], 185, 150, 370, 360);
                    Console.WriteLine(_Copy.MatrixToText(_matrices[2]));
                    matricesShown = true;
                    Console.WriteLine($"messageText: {messageText}");


                    // Prompt the result and copy
                    if (messageText != "Result:")
                    {
                        messageText = "Result:";
                        _renderVisuals.UpdateMessageText(messageText);
                    }
                    // Return to Idle if clicked
                    if (SplashKit.MouseClicked(MouseButton.LeftButton))
                    {
                        globalState = State.Idle;
                    }
                    Console.WriteLine($"messageText: {messageText}");
                }
                // Update Screen // Remove\Comment out when final project is done to reduce power consumption, only use when debugging
                SplashKit.RefreshScreen();
            }
        }

        private void FinishCopyAction(Copy copyHandler)
        {
            ClipboardManager.HandleCopyAction(copyMethodUsed, mathUsed, _matrices, copyHandler);
            
            _renderVisuals.ReRenderMatrices(_matrices);
            matricesShown = true;
            _renderVisuals.UpdateMessageText($"Copied to clipboard\nClick to continue");

            // Return to Idle after action is completed
            copyMethodUsed = "";
            globalState = State.Idle;
            _renderVisuals.DrawBButtons1();
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

    }
}