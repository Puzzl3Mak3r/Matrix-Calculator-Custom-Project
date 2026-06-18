using System;

namespace Matrix_Calculator
{
    public class UserInput
    {
        public static string GetValidKey(string input)
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