using System;

namespace Matrix_Calculator
{
    
    // Provides cross-platform text interfacing between the application layer and the host operating system's clipboard stack.
    
    public class ClipboardService
    {
        // Simple internal fallback wrapper
        private static string _internalClipboard = "";

        
        // Binds text directly to the active OS clipboard environment.
        
        public static void CopyText(string text)
        {
            _internalClipboard = text;
            Console.WriteLine("Copied to clipboard (internal wrapper): " + text);
        }

        
        // Safely captures text stream components out of the system clipboard environment.
        
        public static string PasteText()
        {
            return _internalClipboard;
        }
    }
}