# System UML Class Diagram

Below is the UML Class Diagram for the Matrix Calculator application. It maps out the core Object-Oriented principles, including the Memento, Strategy, and Factory patterns defined in `SCHEMA.md`.

To view this diagram, view this file in a markdown editor that supports Mermaid.js (such as Obsidian, GitHub, or VS Code with a Mermaid extension).

```mermaid
classDiagram
    %% Core GUI and Application
    class Program {
        -Window _window
        +MatrixData[] matrices
        +Main()
        +Run()
        -DrawUI()
        -DrawMatrix(int Rows, int Cols)
    }

    class UserInput {
        -string _currentBuffer
        -List~double~ _parsedValues
        -int _targetRows
        -int _targetCols
        +ProcessKey(KeyCode key)
        +Backspace()
        +Confirm()
        +IsComplete() bool
    }

    %% Data Structures & Domain

    class Matrix {
        -MatrixData[] matrices
        -int _rows
        -int _cols
        +GetValue(int r, int c) double
        +SetValue(int r, int c, double val)
        +CreateMemento() MatrixMemento
        +RestoreFromMemento(MatrixMemento memento)
        +ToLatexString() string
    }

    class MatrixMemento {
        -double[,] _stateSnapshot
        -DateTime _timestamp
        +MatrixMemento(double[,] gridData)
    }

    class MatrixFactory {
        +CreateZeroMatrix(int rows, int cols) Matrix
    }
    class MatrixData {
        <<struct>>
        +int rows
        +int cols
        +double[,] matrix
    }

    %% Copy + Paste
    class CopyPaste {
        +CopyToClipboard(string text)
        +PasteFromClipboard() string
    }

    class CopyPasteLaTeX {
        +CopyToClipboard(string text)
        +PasteFromClipboard() string
    }

    class CopyPasteASCII {
        +CopyToClipboard(string text)
        +PasteFromClipboard() string
    }

    %% Strategy Pattern for Operations
    class Operation {
        <<abstract / interface>>
        +Execute(Matrix a, Matrix b) Matrix
        +CheckDimensions(Matrix a, Matrix b) bool
    }

    class OperationAddition {
        +AddMatrices(Matrix a, Matrix b) Matrix
    }

    class OperationSubtraction {
        +SubtractMatrices(Matrix a, Matrix b) Matrix
    }

    class OperationMultiplication {
        +MultiplyMatrices(Matrix a, Matrix b) Matrix
    }

    class OperationTranspose {
        +TransposeMatrix(Matrix a) Matrix
    }

    class OperationInvert {
        +InvertMatrix(Matrix a) Matrix
    }

    class OperationDeterminant {
        -double _determinant
        +CalculateDeterminant(Matrix a) double
    }

    %% Relationships
    Program --> Matrix : Manages
    Program --> UserInput : Uses
    Program --> Operation : Uses Strategy
    Program --> MatrixFactory : Uses
    Program --> CopyPaste : Uses
    Matrix --> MatrixMemento : Creates / Restores
    MatrixFactory --> Matrix : Creates
    Matrix --> MatrixData : Uses
    Operation <|-- OperationAddition
    Operation <|-- OperationSubtraction
    Operation <|-- OperationMultiplication
    Operation <|-- OperationTranspose
    OperationDeterminant <|-- OperationInvert
    Operation <|-- OperationInvert
    Operation <|-- OperationDeterminant
    CopyPaste <|-- CopyPasteLaTeX
    CopyPaste <|-- CopyPasteASCII
```