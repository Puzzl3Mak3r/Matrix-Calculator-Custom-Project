# How to store the matrices
Using extendable 2D float arrays
Floats, because they will be numbers, possible decimals.

The format will be something like
```csharp
public double[][] matrices = {
    // Matrices Here
};
```
where the structure is
```csharp
matrices = {
    {
        2, 2, // Size of matrix (rows, columns)
        1, 0, // Row 1
        0, 1  // Row 2
    },
    {
        3, 4, // Size 3 columns, 4 rows
        1, 0, 0, 0, // Row 1
        0, 1, 0, 0, // Row 2
        0, 0, 1, 0  // Row 3
    }
};
```