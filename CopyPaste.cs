using System;
using TextCopy;

namespace Matrix_Calculator
{
    // Provides cross-platform text interfacing between the application layer and the host operating system's clipboard stack for raw matrix data.
    // Acts as the base class for format-specific clipboard operations.
    public class CopyPaste
    {
        public string MatrixToText(MatrixData M)
        {
            string text = "{ ";
            for (int i = 0; i < M.rows; i++)
            {
                for (int j = 0; j < M.cols; j++)
                {
                    text += M.matrix[i, j].ToString();
                    if (j < M.cols - 1)
                    {
                        text += ",";
                    }
                }
                if (i < M.rows - 1)
                {
                    text += "\n";
                }
            }
            return text + " }";
        }
        public virtual void CopyToClipboard(string t)
        {
            // TODO: Implement direct OS clipboard integration
            Console.WriteLine($"Copying to clipboard: {t}");
            try
            {
                // 2. The command that sends it to the OS clipboard
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

    // Handles formatting matrix data into LaTeX syntax before copying
    public class CopyPasteLaTeX : CopyPaste
    {
        public override void CopyToClipboard(string text)
        {
            // TODO: Format string to LaTeX and copy to clipboard
            base.CopyToClipboard(text);
        }
    }

    // Handles formatting matrix data into an ASCII grid before copying
    public class CopyPasteASCII : CopyPaste
    {
        public override void CopyToClipboard(string text)
        {
            // TODO: Format string to ASCII grid and copy to clipboard
            base.CopyToClipboard(text);
        }
    }
}