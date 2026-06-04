# Copy/Paste Schema

## Overview
This module handles the Copying and Pasting of Matrices to and from the system clipboard.

## Implementation Details
- **Mechanism:** Implementation details to be investigated.
- **Potential API:** May utilize `System.Windows.Forms.Clipboard` for system-level clipboard interactions.

## Supported Formats
The implementation will include checks and parsers for the following text formats:

1. **Direct:** Raw numerical/data representation, straight from the matrices 2D array
```
{
    3, 4, // Size 3 columns, 4 rows
    1, 4, 0, -0.4, // Row 1
    -1, 1, 0, -30, // Row 2
    0, 6.03, 1, 5  // Row 3
}
```
2. **LaTeX:** Mathematical markup format 
```
\begin{bmatrix}
1 & 2 & 3\\
a & b & c
\end{bmatrix}
```
3. **Formatted ASCII:** Visually formatted plain text representation of the matrix
Seperated by commas, on the left side of the commas is whitespaces to line up the numbers. Same with the last character for end of row
```
┌ 1     ,  2   ,  3  ┐
│ -0.04 ,  -3  ,  9  │
│ 6     ,  0   ,  -1 │
│ 0.3   ,  4.4 ,  0  │
└ 4     ,  33  ,  0  ┘
```