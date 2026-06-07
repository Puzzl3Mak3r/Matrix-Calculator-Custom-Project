# System Sequence Diagram

Below is the UML Sequence Diagram demonstrating the behavioral flow of the application when a user performs a mathematical operation(s) (Matrix Addition, Subtraction, Multiplication, etc.) using the Strategy Pattern.

To view this diagram, view this file in a markdown editor that supports Mermaid.js (such as Obsidian, GitHub, or VS Code with a Mermaid extension).

```mermaid
sequenceDiagram
    actor User
    participant GUI as Program (Main Loop)
    participant Strategy as Operation
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