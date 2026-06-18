using System;

namespace Matrix_Calculator
{
    public static class ClipboardManager
    {
        public static void HandleCopyAction(string copyMethodUsed, string mathUsed, MatrixData[] matrices, Copy copyHandler)
        {
            if (copyMethodUsed == "copyR") // Handle copying just the result matrix
            {
                if (matrices[2].matrix != null) // Ensure there is a result to copy
                {
                    string clipboardText = copyHandler.MatrixToText(matrices[2]);
                    Console.WriteLine(clipboardText);
                    copyHandler.CopyToClipboard(clipboardText);
                }
            }
            else if (copyMethodUsed == "copyE") // Handle copying the full equation
            {
                if (matrices[0].matrix != null && !string.IsNullOrEmpty(mathUsed))
                {
                    string clipboardText;
                    // Format for unary operations like transpose, inverse, determinant
                    if (mathUsed == "transpose" || mathUsed == "inverse" || mathUsed == "determinant")
                    {
                        clipboardText = $"{mathUsed}\n{copyHandler.MatrixToText(matrices[0])}\n=\n{copyHandler.MatrixToText(matrices[2])}";
                    }
                    else // Format for binary operations like addition, subtraction, multiplication
                    {
                        clipboardText = $"{copyHandler.MatrixToText(matrices[0])}\n{mathUsed}\n{copyHandler.MatrixToText(matrices[1])}\n=\n{copyHandler.MatrixToText(matrices[2])}";
                    }
                    Console.WriteLine(clipboardText);
                    copyHandler.CopyToClipboard(clipboardText);
                }
            }
        }
    }
}