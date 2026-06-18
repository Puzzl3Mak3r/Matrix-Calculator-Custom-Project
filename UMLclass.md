# System UML Class Diagram

Below is the UML Class Diagram for the Matrix Calculator application. It maps out the core Object-Oriented principles, including the Memento, Strategy, and Factory patterns defined in `SCHEMA.md`.

To view this diagram, view this file in a markdown editor that supports Mermaid.js (such as Obsidian, GitHub, or VS Code with a Mermaid extension).

```mermaid
classDiagram
    %% Core GUI and Application
    class Program {
        -Window _window
        -RenderVisuals _renderVisuals
        -MOadd _MOadd
        -MOsubt _MOsubt
        -MOmult _MOmult
        -MOtran _MOtran
        -MOinvr _MOinvr
        -MOdetr _MOdetr
        -Copy _Copy
        -CopyLaTeX _CopyLaTeX
        -CopyASCII _CopyASCII
        -MatrixData[] _matrices
        +Main()
        +Run()
    }

    class RenderVisuals {
        +DrawUI()
        +DrawMatrix(int Rows, int Cols)
        +UpdateMatrixDisplay(...)
    }

    %% Data Structures & Domain

    class MatrixData {
        <<struct>>
        +int rows
        +int cols
        +double[,] matrix
    }

    class MatrixFactory {
        +CreateZMatrix(int rows, int cols) MatrixData
    }

    %% Copy + Paste
    class Copy {
        +MatrixToText(MatrixData M) string
        +CopyToClipboard(string t)
    }

    class CopyLaTeX {
        +MatrixToText(MatrixData M) string
    }

    class CopyASCII {
        +MatrixToText(MatrixData M) string
    }

    %% Strategy Pattern for Operations
    class MO {
        <<abstract>>
        +ExecuteTwo(MatrixData matrixA, MatrixData matrixB) MatrixData
        +ExecuteOne(MatrixData matrixA) MatrixData
    }

    class MOadd {
        +ExecuteTwo(MatrixData a, MatrixData b) MatrixData
    }

    class MOsubt {
        +ExecuteTwo(MatrixData a, MatrixData b) MatrixData
    }

    class MOmult {
        +ExecuteTwo(MatrixData a, MatrixData b) MatrixData
    }

    class MOtran {
        +ExecuteOne(MatrixData a) MatrixData
    }

    class MOinvr {
        +ExecuteOne(MatrixData a) MatrixData
    }

    class OperationDeterminant {
        +CalculateDeterminant(MatrixData a) double
    }

    %% Relationships
    Program --> RenderVisuals : Uses
    Program --> MatrixFactory : Uses
    Program --> Copy : Uses
    Program --> MO : Uses Strategy
    Program --> MOdetr : Uses
    MO <|-- MOadd
    MO <|-- MOsubt
    MO <|-- MOmult
    MO <|-- MOtran
    MO <|-- MOinvr
    MOinvr --> MOdetr : Uses
    Copy <|-- CopyLaTeX
    Copy <|-- CopyASCII
```