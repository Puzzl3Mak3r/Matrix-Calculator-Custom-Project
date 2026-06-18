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
        -int tempRows
        -int tempCols
        -string messageText
        -string tempData
        -MatrixData tempMatrix
        -string currentKey
        -bool matricesShown
        -bool shoRidOfMsg
        -State globalState
        -State previousState
        -MatrixData[] _matrices
        -int currentCellX
        -int currentCellY
        -string mathUsed
        -string copyMethodUsed
        +Main()$
        +Run()
        -FinishCopyAction(Copy copyHandler)
        -Reset()
    }

    class RenderVisuals {
        +Rectangle AddButton
        +Rectangle SubtractButton
        +Rectangle MultiplyButton
        +Rectangle TransposeButton
        +Rectangle InverseButton
        +Rectangle DeterminantButton
        +Rectangle Bottom1Button
        +Rectangle Bottom2Button
        +Rectangle Bottom3Button
        +Rectangle Bottom4Button
        +Rectangle MatrixEntryBox
        +DrawUI()
        +DrawBButtons1()
        +DrawBButtons2()
        +DrawButton(Rectangle rect, string text)
        +UpdateMatrixDisplay(bool isEnteringData, string messageText, string tempData)
        +UpdateMessageText(string t)
        +DrawBrackets(double x, double y, double w, double h)
        +DrawMatrix(int Rows, int Cols)
        +ClearBoard()
        +ReRenderMatrices(MatrixData[] matrices)
        +DisplayMatrix(MatrixData M, int x, int y, int w, int h)
    }

    class UserInput {
        +GetValidKey(string input)$ string
    }

    class ClipboardManager {
        +HandleCopyAction(string copyMethodUsed, string mathUsed, MatrixData[] matrices, Copy copyHandler)$ void
    }

    %% Data Structures & Domain

    class MatrixData {
        <<struct>>
        +int rows
        +int cols
        +double[,] matrix
    }

    class MatrixFactory {
        +CreateZMatrix(int rows, int cols)$ MatrixData
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
        +ExecuteTwo(MatrixData matrixA, MatrixData matrixB) MatrixData
    }

    class MOsubt {
        +ExecuteTwo(MatrixData matrixA, MatrixData matrixB) MatrixData
    }

    class MOmult {
        +ExecuteTwo(MatrixData matrixA, MatrixData matrixB) MatrixData
    }

    class MOtran {
        +ExecuteOne(MatrixData matrixA) MatrixData
    }

    class MOinvr {
        -MOdetr _detr
        +ExecuteOne(MatrixData matrixA) MatrixData
    }

    class MOdetr {
        +CalculateDeterminant(MatrixData matrixA) double
    }

    %% Relationships
    Program --> RenderVisuals : Uses
    Program --> MatrixFactory : Uses
    Program --> Copy : Uses
    Program --> UserInput : Uses
    Program --> ClipboardManager : Uses
    ClipboardManager --> Copy : Uses
    Program --> MO : Uses Strategy
    Program --> MOdetr : Uses
    MatrixFactory --> MatrixData : Uses
    MO <|-- MOadd
    MO <|-- MOsubt
    MO <|-- MOmult
    MO <|-- MOtran
    MO <|-- MOinvr
    MO --> MatrixFactory : Uses
    MOinvr --> MOdetr : Uses
    Copy <|-- CopyLaTeX
    Copy <|-- CopyASCII
```