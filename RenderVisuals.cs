using System;
using SplashKitSDK;

namespace Matrix_Calculator
{
    public class RenderVisuals
    {
        // Vars
        int padding = 20;

        // Buttons and Entry box
        public Rectangle AddButton { get; private set; }
        public Rectangle SubtractButton { get; private set; }
        public Rectangle MultiplyButton { get; private set; }
        public Rectangle TransposeButton { get; private set; }
        public Rectangle InverseButton { get; private set; }
        public Rectangle DeterminantButton { get; private set; }
        public Rectangle Bottom1Button { get; private set; }
        public Rectangle Bottom2Button { get; private set; }
        public Rectangle Bottom3Button { get; private set; }
        public Rectangle Bottom4Button { get; private set; }
        public Rectangle MatrixEntryBox { get; private set; }

        // Functions
        public RenderVisuals()
        {
            // Initialize top buttons
            AddButton          = SplashKit.RectangleFrom(20, 20, 100, 60);
            SubtractButton     = SplashKit.RectangleFrom(140, 20, 100, 60);
            MultiplyButton     = SplashKit.RectangleFrom(260, 20, 100, 60);
            TransposeButton    = SplashKit.RectangleFrom(380, 20, 100, 60);
            InverseButton      = SplashKit.RectangleFrom(500, 20, 100, 60);
            DeterminantButton  = SplashKit.RectangleFrom(620, 20, 100, 60);

            // Initialize matrix box
            MatrixEntryBox     = SplashKit.RectangleFrom(20, 100, 700, 400);

            // Initialize bottom buttons
            Bottom1Button       = SplashKit.RectangleFrom(20, 520, 160, 60);
            Bottom2Button       = SplashKit.RectangleFrom(200, 520, 160, 60);
            Bottom3Button       = SplashKit.RectangleFrom(380, 520, 160, 60);
            Bottom4Button       = SplashKit.RectangleFrom(560, 520, 160, 60);
        }

        public void DrawUI()
        {
            // Draw the top buttons
            DrawButton(AddButton, "Add");
            DrawButton(SubtractButton, "Subtract");
            DrawButton(MultiplyButton, "Multiply");
            DrawButton(TransposeButton, "Transpose");
            DrawButton(InverseButton, "Inverse");
            DrawButton(DeterminantButton, "Determinant");
            
            // Draw Matrix entry box
            SplashKit.FillRectangle(Color.White, MatrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, MatrixEntryBox);

            // Draw the bottom buttons
            DrawButton(Bottom1Button, "Copy Equation");
            DrawButton(Bottom2Button, "Paste Equation");
            DrawButton(Bottom3Button, "Copy Result");
            DrawButton(Bottom4Button, "Paste Result");
        }

        public void DrawButton(Rectangle rect, string text)
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

        public void UpdateMatrixDisplay(bool isEnteringData, string messageText, string tempData)
        {
            // Clear the matrix entry box
            SplashKit.FillRectangle(Color.White, MatrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, MatrixEntryBox);

            if (isEnteringData)
            {
                // Draw the Square Brackets
                DrawBrackets(MatrixEntryBox.X, MatrixEntryBox.Y, MatrixEntryBox.Width, MatrixEntryBox.Height);
            }

            // Add optional text
            UpdateMessageText(messageText);

            if (!string.IsNullOrEmpty(tempData))
            {
                Font font = SplashKit.GetSystemFont();
                const int fontSize = 20;
                int textWidth = SplashKit.TextWidth(tempData, font, fontSize);
                int textHeight = SplashKit.TextHeight(tempData, font, fontSize);
                double x = MatrixEntryBox.X + (MatrixEntryBox.Width - textWidth) / 2;
                double y = MatrixEntryBox.Y + (MatrixEntryBox.Height - textHeight) / 2;
                SplashKit.DrawText(tempData, Color.Black, font, fontSize, x, y);
            }
        }

        public void UpdateMessageText(string t)
        {
            // Write Message
            Console.WriteLine($"Message Text: '{t}'");
            Font font = SplashKit.GetSystemFont();
            const int fontSize = 20;
            int messageTextWidth = SplashKit.TextWidth(t, font, fontSize);
            SplashKit.DrawText(t, Color.Black, font, fontSize, MatrixEntryBox.X + (MatrixEntryBox.Width - messageTextWidth) / 2, (MatrixEntryBox.Height / 2) - 80); // Got to make it higher to be out of the way of matrices
        }

        public void DrawBrackets(double x, double y, double w, double h)
        {
            // Draw Left Bits
            SplashKit.DrawLine(Color.Black,
                x + padding,    y + padding,
                x + 3*padding,  y + padding);
            SplashKit.DrawLine(Color.Black,
                x + padding,    y + h - padding,
                x + 3*padding,  y + h - padding);

            // Draw Right Bits
            SplashKit.DrawLine(Color.Black,
                x + w - padding,    y + padding,
                x + w - 3*padding,  y + padding);
            SplashKit.DrawLine(Color.Black,
                x + w - padding,    y + h - padding,
                x + w - 3*padding,  y + h - padding);

            // Draw Sides
            SplashKit.DrawLine(Color.Black, x + padding, y + padding, x + padding, y + h - padding);
            SplashKit.DrawLine(Color.Black, x + w - padding, y + padding, x + w - padding, y + h - padding);
        }

        public void DrawMatrix(int Rows, int Cols)
        {
            Console.WriteLine($"Drawing matrix with dimensions: {Rows} x {Cols}");
            
            // Draw the matrix entry box and the grid for entering matrix values
            SplashKit.FillRectangle(Color.White, MatrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, MatrixEntryBox);

            // Draw the Square Brackets
            DrawBrackets(MatrixEntryBox.X, MatrixEntryBox.Y, MatrixEntryBox.Width, MatrixEntryBox.Height);
        }

        public void ClearBoard()
        {
            // Clear the matrix entry box
            SplashKit.FillRectangle(Color.White, MatrixEntryBox);
            SplashKit.DrawRectangle(Color.Black, MatrixEntryBox);
            
            Console.WriteLine("Board cleared");
        }
    }
}