# System UML Class Diagram

Below is the UML Class Diagram for the Matrix Calculator application. It maps out the core Object-Oriented principles, including the Memento, Strategy, and Factory patterns defined in `SCHEMA.md`.

To view this diagram, view this file in a markdown editor that supports Mermaid.js (such as Obsidian, GitHub, or VS Code with a Mermaid extension).

```mermaid
classDiagram
    %% Core GUI and Application
    class Program {
        -Matrix _matrixA
        -Matrix _matrixB
        -Matrix _matrixResult
        -Operation _activeStrategy
        -MatrixMemento _internalClipboard
        +Main()
        +Run()
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

    class MatrixData {
        <<struct>>
        +int rows
        +int cols
        +double[,] matrix
    }

    class Matrix {
        -MatrixData[] matrices
        -int _rows
        -int _cols
        +int Rows
        +int Cols
        +GetValue(int r, int c) double
        +SetValue(int r, int c, double val)
        +CreateMemento() MatrixMemento
        +RestoreFromMemento(MatrixMemento memento)
        +ToLatexString() string
    }

    class MatrixMemento {
        -MatrixData[] _stateSnapshot
        -DateTime _timestamp
        +MatrixData[] StateSnapshot
        +MatrixMemento(MatrixData[] gridData)
    }

    class MatrixFactory {
        +CreateZeroMatrix(int rows, int cols) Matrix
        +CreateIdentityMatrix(int size) Matrix
    }

    %% Copy + Paste
    class Copy {
        +CopyToClipboard(string text)
        +PasteFromClipboard() string
    }

    class CopyLaTeX {
        +CopyToClipboard(string text)
        +PasteFromClipboard() string
    }

    class CopyASCII {
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
        +Execute(Matrix a, Matrix b) Matrix
    }

    class OperationSubtraction {
        +Execute(Matrix a, Matrix b) Matrix
    }

    class OperationMultiplication {
        +Execute(Matrix a, Matrix b) Matrix
    }

    class OperationTranspose {
        +Execute(Matrix a, Matrix b) Matrix
    }

    class OperationInvert {
        +Execute(Matrix a, Matrix b) Matrix
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
    Program --> Copy : Uses
    Matrix --> MatrixMemento : Creates / Restores
    MatrixFactory --> Matrix : Creates
    Matrix --> MatrixData : Uses
    Operation <|-- OperationAddition
    Operation <|-- OperationSubtraction
    Operation <|-- OperationMultiplication
    Operation <|-- OperationTranspose
    Operation <|-- OperationInvert
    Operation <|-- OperationDeterminant
    Copy <|-- CopyLaTeX
    Copy <|-- CopyASCII
```