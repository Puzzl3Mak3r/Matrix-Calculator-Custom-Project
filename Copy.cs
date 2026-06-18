using System;
using TextCopy;

namespace Matrix_Calculator
{
    // Copy is the base class for format-specific clipboard functions
    public class Copy
    {
        // Converts a matrix's data into a raw string
        public virtual string MatrixToText(MatrixData M)
        {
            // Start the raw format with a curly brace
            string text = "{ ";
            // Iterate through each row of the matrix
            for (int i = 0; i < M.rows; i++)
            {
                // Iterate through each column in the current row
                for (int j = 0; j < M.cols; j++)
                {
                    // Append the current element's value
                    text += M.matrix[i, j].ToString();
                    // If not the last column, append a comma separator
                    if (j < M.cols - 1)
                    {
                        text += ",";
                    }
                }
                // If not the last row, append a newline character
                if (i < M.rows - 1)
                {
                    text += "\n";
                }
            }
            // Close the raw format with a curly brace
            return text + " }";
        }

        
        // Sends the formatted string directly to the host OS clipboard
        public virtual void CopyToClipboard(string t)
        {
            Console.WriteLine($"Copying to clipboard: {t}");
            try
            {
                // The command that sends it to the OS clipboard
                ClipboardService.SetText(t);
                Console.WriteLine("Successfully copied to clipboard!");
            }
            catch (Exception ex)
            {
                // Always good to catch exceptions in case the OS blocks clipboard access
                Console.WriteLine($"Clipboard error: {ex.Message}");
            }
        }
    }

    // LaTeX
    public class CopyLaTeX : Copy
    {
        // A LaTeX representation of the matrix
        public override string MatrixToText(MatrixData M)
        {
            // Start the LaTeX bmatrix environment
            string text = "\\begin{bmatrix}\n";
            // Iterate through each row of the matrix
            for (int i = 0; i < M.rows; i++)
            {
                // Iterate through each column in the current row
                for (int j = 0; j < M.cols; j++)
                {
                    // Append the current element's value
                    text += M.matrix[i, j].ToString();
                    // If not the last column, append an ampersand separator for LaTeX columns
                    if (j < M.cols - 1)
                    {
                        text += " & ";
                    }
                }
                // If not the last row, append a LaTeX newline sequence
                if (i < M.rows - 1)
                {
                    text += "\\\\\n";
                }
                else
                {
                    // For the last row, just append a standard newline
                    text += "\n";
                }
            }
            // Close the LaTeX bmatrix environment
            text += "\\end{bmatrix}";
            return text;
        }
    }

    // ASCII
    public class CopyASCII : Copy
    {
        // A visually formatted ASCII representation of the matrix
        public override string MatrixToText(MatrixData M)
        {
            // Find max width of each column to align properly
            int[] colWidths = new int[M.cols];
            for (int j = 0; j < M.cols; j++)
            {
                int max = 0;
                // Check every element in the current column to find the longest string
                for (int i = 0; i < M.rows; i++)
                {
                    int len = M.matrix[i, j].ToString().Length;
                    if (len > max)
                    {
                        max = len;
                    }
                }
                // Store the maximum width found for this column
                colWidths[j] = max;
            }

            string text = "";
            // Iterate through each row of the matrix
            for (int i = 0; i < M.rows; i++)
            {
                // Determine the correct starting bracket character based on the row index
                if (M.rows == 1)
                {
                    text += "[ ";
                }
                else if (i == 0)
                {
                    text += "┌ ";
                }
                else if (i == M.rows - 1)
                {
                    text += "└ ";
                }
                else
                {
                    text += "│ ";
                }

                for (int j = 0; j < M.cols; j++)
                {
                    string val = M.matrix[i, j].ToString();
                    // Pad the value with spaces on the right to align with the column's max width
                    text += val.PadRight(colWidths[j]);
                    if (j < M.cols - 1)
                    {
                        text += " ,  ";
                    }
                }

                // Determine the correct ending bracket character based on the row index
                if (M.rows == 1)
                {
                    text += " ]";
                }
                else if (i == 0)
                {
                    text += " ┐\n";
                }
                else if (i == M.rows - 1)
                {
                    text += " ┘";
                }
                else
                {
                    text += " │\n";
                }
            }
            return text;
        }
    }
}