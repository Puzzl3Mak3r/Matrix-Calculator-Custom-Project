# System Sequence Diagrams

There are multiple ways the data flows in the program, so I included multiple diagrams.


## 1 Matrix Operations (Transpose, Inverse, Determinant), Copying LaTeX Equation
```mermaid
sequenceDiagram
    actor User
    participant GUI as Program (Main Loop)
    participant Strategy as MOtran / MOinvr
    participant Det as MOdetr
    participant CopyStrat as CopyLaTeX
    participant OS as OS Clipboard

    User->>GUI: Clicks Transpose / Inverse / Determinant Button
    GUI->>GUI: PointInRectangle checks true
    
    alt Transpose or Inverse
        GUI->>Strategy: ExecuteOne(MatA)
        activate Strategy
        opt If Inverse
            Strategy->>Det: CalculateDeterminant(MatA)
            activate Det
            Det-->>Strategy: Returns det
            deactivate Det
        end
        Strategy->>Strategy: Instantiates & calculates MatrixData Result
        Strategy-->>GUI: Returns Result MatrixData
        deactivate Strategy
    else Determinant
        GUI->>Det: CalculateDeterminant(MatA)
        activate Det
        Det-->>GUI: Returns det (double)
        deactivate Det
        GUI->>GUI: Wraps det in 1x1 Result MatrixData
    end
    
    GUI->>User: RefreshScreen() / Render Result Matrix
    
    User->>GUI: Clicks "Copy Equation" Button
    GUI->>GUI: PointInRectangle checks true
    GUI->>User: Displays Copy options, RefreshScreen()
    
    User->>GUI: Clicks "Copy LaTeX" Button
    GUI->>GUI: PointInRectangle checks true
    
    GUI->>CopyStrat: MatrixToText(MatA) & MatrixToText(ResultMat)
    activate CopyStrat
    CopyStrat->>CopyStrat: Formats elements into \begin{bmatrix}... syntax
    CopyStrat-->>GUI: Returns formatted LaTeX strings
    
    GUI->>GUI: Formats full equation string
    GUI->>CopyStrat: CopyToClipboard(equationString)
    CopyStrat->>OS: ClipboardService.SetText(equationString)
    deactivate CopyStrat
    
    GUI->>User: UpdateMessageText("Copied to clipboard")
```

## 2 Martix Operations (Addition, Subtraction, Multiplication), Copying RAW Result
Handles operations requiring both Matrix A and Matrix B

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