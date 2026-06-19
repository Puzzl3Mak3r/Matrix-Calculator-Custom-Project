# Matrix-Calculator

Welcome to the custom Matrix Calculator project! Below you will find the architectural design diagrams that define the structure and behavior of the system.

## System UML Class Diagram

This Class Diagram maps out the core Object-Oriented principles, including the Memento, Strategy, and Factory patterns defined in the project schema.
This sequence diagram shows 2 Martix Operations (Addition, Subtraction, Multiplication), Copying RAW Result

```mermaid
sequenceDiagram
    actor User
    participant GUI as Program (Main Loop)
    participant Strategy as MOadd / MOsubt / MOmult
    participant CopyStrat as Copy
    participant OS as OS Clipboard

    User->>GUI: Clicks Add / Sub / Mult Button
    GUI->>GUI: PointInRectangle checks true
    
    GUI->>Strategy: ExecuteTwo(MatA, MatB)
    activate Strategy
    
    Strategy->>Strategy: Instantiates MatrixData Result
    
    loop For each row and col
        Strategy->>Strategy: Calculate MatA & MatB elements
    end

    Strategy-->>GUI: Returns Result MatrixData
    deactivate Strategy
    
    GUI->>User: RefreshScreen() / Render Result Matrix
    
    User->>GUI: Clicks "Copy Result" Button
    GUI->>GUI: PointInRectangle checks true
    GUI->>User: Displays Copy options, RefreshScreen()
    
    User->>GUI: Clicks "Copy RAW" Button
    GUI->>GUI: PointInRectangle checks true

    GUI->>CopyStrat: MatrixToText(MatA), (MatB), (ResultMat)
    activate CopyStrat
    CopyStrat->>CopyStrat: Formats elements into raw string syntax
    CopyStrat-->>GUI: Returns formatted strings
    
    GUI->>CopyStrat: CopyToClipboard(string)
    CopyStrat->>OS: ClipboardService.SetText(string)
    deactivate CopyStrat
    
    GUI->>User: UpdateMessageText("Copied to clipboard")
```
