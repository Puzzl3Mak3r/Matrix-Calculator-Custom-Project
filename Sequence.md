# System Sequence Diagram

Below is the UML Sequence Diagram demonstrating the behavioral flow of the application when a user performs a mathematical operation (e.g., Matrix Addition) using the Strategy Pattern.

To view this diagram, view this file in a markdown editor that supports Mermaid.js (such as Obsidian, GitHub, or VS Code with a Mermaid extension).

```mermaid
sequenceDiagram
    actor User
    participant GUI as Program (Main Loop)
    participant Strategy as OperationAddition
    participant MatA as Matrix A
    participant MatB as Matrix B
    participant MatRes as Matrix Result

    User->>GUI: Clicks "Add" Button
    GUI->>GUI: PointInRectangle(mousePos, _addButton) checks true
    
    GUI->>Strategy: Execute(Matrix A, Matrix B)
    activate Strategy
    
    Strategy->>Strategy: CheckDimensions(Matrix A, Matrix B)
    Strategy->>MatA: Get dimensions and values
    Strategy->>MatB: Get dimensions and values
    
    Strategy->>MatRes: Create new Matrix (Result)
    Strategy-->>GUI: Return Matrix Result
    deactivate Strategy
    
    GUI->>User: RefreshScreen() / Render Result Matrix
```