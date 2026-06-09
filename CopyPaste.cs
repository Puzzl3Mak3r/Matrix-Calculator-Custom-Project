using System;

namespace Matrix_Calculator
{
    // Provides cross-platform text interfacing between the application layer and the host operating system's clipboard stack for raw matrix data.
    // Acts as the base class for format-specific clipboard operations.
    public class CopyPaste
    {
        public virtual void CopyToClipboard(string text)
        {
            // TODO: Implement direct OS clipboard integration
            throw new NotImplementedException("Clipboard copy not implemented yet");
        }

        public virtual string PasteFromClipboard()
        {
            // TODO: Implement direct OS clipboard integration
            throw new NotImplementedException("Clipboard paste not implemented yet");
        }
    }

    // Handles formatting matrix data into LaTeX syntax before copying, and parsing LaTeX syntax when pasting.
    public class CopyPasteLaTeX : CopyPaste
    {
        public override void CopyToClipboard(string text)
        {
            // TODO: Format string to LaTeX and copy to clipboard
            base.CopyToClipboard(text);
        }

        public override string PasteFromClipboard()
        {
            // TODO: Paste from clipboard and parse LaTeX to standard string
            return base.PasteFromClipboard();
        }
    }

    // Handles formatting matrix data into an ASCII grid before copying, and parsing ASCII grids when pasting.
    public class CopyPasteASCII : CopyPaste
    {
        public override void CopyToClipboard(string text)
        {
            // TODO: Format string to ASCII grid and copy to clipboard
            base.CopyToClipboard(text);
        }

        public override string PasteFromClipboard()
        {
            // TODO: Paste from clipboard and parse ASCII grid to standard string
            return base.PasteFromClipboard();
        }
    }
}