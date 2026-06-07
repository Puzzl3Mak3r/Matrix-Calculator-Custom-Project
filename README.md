# Matrix-Calculator - Custom-Project

Welcome to the custom Matrix Calculator project! Below you will find the architectural design diagrams that define the structure and behavior of the system.

## System UML Class Diagram

This Class Diagram maps out the core Object-Oriented principles, including the Memento, Strategy, and Factory patterns defined in the project schema.

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
```

## System Sequence Diagram

Below is the UML Sequence Diagram demonstrating the behavioral flow of the application when a user performs a mathematical operation (e.g., Matrix Addition) using the Strategy Pattern.

```mermaid
sequenceDiagram
    actor User
    participant GUI as Program (Main Loop)
    participant Strategy as OperationAddition
    participant Factory as MatrixFactory
    participant MatA as Matrix (A)
    participant MatB as Matrix (B)
    participant MatRes as Matrix (Result)

    User->>GUI: Clicks "Add" Button
    GUI->>GUI: PointInRectangle(mousePos, _addButton) checks true
    
    GUI->>Strategy: Execute(MatA, MatB)
    activate Strategy
    
    Strategy->>Strategy: CheckDimensions(MatA, MatB)
    Strategy->>MatA: Get Rows, Cols
    Strategy->>MatB: Get Rows, Cols
    
    Strategy->>Factory: CreateZeroMatrix(Rows, Cols)
    activate Factory
    Factory-->>MatRes: Instantiates Matrix
    Factory-->>Strategy: Returns MatRes
    deactivate Factory
    
    loop For each row and col
        Strategy->>MatA: GetValue(r, c)
        Strategy->>MatB: GetValue(r, c)
        Strategy->>MatRes: SetValue(r, c, val)
    end

    Strategy-->>GUI: Returns MatRes
    deactivate Strategy
    
    GUI->>User: RefreshScreen() / Render Result Matrix
```
