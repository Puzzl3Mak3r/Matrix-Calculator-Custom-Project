# Agent Specification File: Object-Oriented Matrix Calculator (C#/.NET)

## 1. Executive Project Overview
This document serves as the complete, unambiguous technical specification for an autonomous developer agent to build a custom Matrix Calculator in C# using the .NET framework and the SplashKit graphics library. 

### Academic & Assessment Context
**Target Tier:** Option 4 (Challenging Custom Program), targeting up to 30 marks[cite: 23, 136].
**Core Requirement:** The codebase must explicitly demonstrate the four core pillars of Object-Oriented Programming: Abstraction, Encapsulation, Inheritance, and Polymorphism[cite: 12, 201].
**Design Patterns:** The application must cleanly implement a minimum of three distinct software design patterns to achieve loose coupling and high cohesion[cite: 140].
**Architectural Expectation:** Code must be modular, maintainable, and easily upgradable[cite: 206]. Classes must be isolated rather than compacted into 7–8 massive files to prevent long-term maintenance decay[cite: 207]. The expected scope is approximately 10 to 30 classes[cite: 205].
**Timeline Constraint:** Final code delivery is firmed for the End of Week 13[cite: 4, 37]. It must pass a rigorous Week 14 panel interview where code quality and modifiability will be verified by live modifications[cite: 38, 54, 230].

---

## 2. Functional Requirements ("How It Should Work")

### Mathematical Engine
* **Matrix Creation:** The system must allow users to define dynamic dimensions (Rows $\times$ Columns) for two distinct input matrices: Matrix A and Matrix B.
* **Basic Operations:** The calculator must execute Matrix Addition, Matrix Subtraction, and Matrix Multiplication.
* **Unary Operations:** The system must calculate the Transpose of a matrix.
* **Validation Guardrails:** The program must validate mathematical compatibility before attempting operations. For addition and subtraction, dimensions must be identical. For multiplication, Matrix A columns must equal Matrix B rows. If validations fail, the user must receive a clean, non-crashing visual error message.

### User Interface & Input Grid (SplashKit GUI)
* **Canvas Rendering:** The GUI must be drawn onto a SplashKit window.
* **Custom Component Logic:** Because SplashKit does not natively feature text boxes or text-entry elements, the system must manually implement a responsive UI grid. 
* **Input Intercepts:** The system must detect mouse clicks inside a drawn bounding box (cell) to target it, intercept keyboard numerical data character-by-character, and render the text inside the cell.
* **Layout:** The window should show Matrix A (left panel), Matrix B (right panel), an Operation Selector panel (center), and a Result Matrix panel (bottom).

### LaTeX Export & Clipboard Services
* **Conversion Script:** The system must convert any selected matrix's numerical state into a valid LaTeX array block format.
* **Formatting Syntax:** A $2 \times 2$ identity matrix must string-serialize precisely as:
  `\begin{bmatrix} 1 & 0 \\ 0 & 1 \end{bmatrix}`
* **System Clipboard Integration:** Users must be able to click a "Copy LaTeX" button to send this string directly to the OS clipboard, enabling seamless pasting into documentation editors or markdown files.
* **State Restoration (Paste):** A "Paste" function must parse clipboard matrix text back into the system's grid values.

---

## 3. System Architecture & Design Patterns

To secure the highest grading criteria and maintain loose coupling, the architecture is strictly split into domain logic, operational strategies, creational factories, and state-mementos.


```
   +---------------------------------------+
   |             CalculatorGUI             |
   +------------------+--------------------+
                      |
    +-----------------+-----------------+
    |                                   |
    v                                   v


+---------------+                   +---------------+
| MatrixFactory |                   |   Clipboard   |
+---------------+                   +---------------+
|                                   |
v                                   v
+---------------+  creates memento  +---------------+
|    Matrix     +------------------>| MatrixMemento |
+-------+-------+                   +---------------+
|
v requires execution
+-------+-------+
| IMathOperation| <--- (Strategy Pattern Interface)
+-------+-------+
|
+-------> MatrixAddition
|
+-------> MatrixSubtraction
|
+-------> MatrixMultiplication

```

### Pattern 1: Strategy Pattern (Behavioral)
* **Intent:** Encapsulate mathematical calculations into independent modules so operations can be swapped dynamically at runtime without editing the UI logic.
* **Implementation:** `IMathOperation` acts as the strategy interface. Concrete math implementations execute the calculations independently.

### Pattern 2: Factory Method Pattern (Creational)
* **Intent:** Centralize matrix creation tasks. This abstracts instantiating multi-dimensional arrays from the UI, letting users spawn complex data presets instantly.
* **Implementation:** `MatrixFactory` intercepts requests for specific presets (e.g., Identity Matrices or Zero Matrices) and handles their population automatically.

### Pattern 3: Memento Pattern (Behavioral)
* **Intent:** Capture and externalize a matrix's internal state without exposing its grid structure, facilitating the snapshot history needed for copy/paste buffers.
* **Implementation:** `Matrix` acts as the Originator. It dumps its numerical state array into an immutable wrapper called `MatrixMemento`. The UI Caretaker manages these snapshots.

---

## 4. Class & Interface Blueprints

The developer agent must construct the codebase using the following precise class breakdowns:

### Core Domain Entities

#### `Matrix` (Originator)
* **Description:** Models and encapsulates a single 2D mathematical array.
* **Fields:**
  * `private double[,] _grid` - The underlying storage matrix data structure.
  * `private int _rows` - The row count boundary.
  * `private int _cols` - The column count boundary.
* **Properties:**
  * `public int Rows { get; }`
  * `public int Cols { get; }`
* **Methods:**
  * `public double GetValue(int r, int c)` - Returns cell value with index boundary protection.
  * `public void SetValue(int r, int c, double val)` - Validates input and updates cell data.
  * `public MatrixMemento CreateMemento()` - Deep-copies `_grid` and instantiates a state snapshot wrapper.
  * `public void RestoreFromMemento(MatrixMemento memento)` - Replaces current `_grid` configuration using snapshot state.
  * `public string ToLatexString()` - Iterates through `_grid` to format and return a LaTeX-compliant string structure.

#### `MatrixMemento` (Memento)
* **Description:** An immutable storage object containing a standalone snapshot of a matrix state.
* **Fields:**
  * `private readonly double[,] _stateSnapshot`
  * `private readonly DateTime _timestamp`
* **Constructor:**
  * `public MatrixMemento(double[,] gridData)` - Must deep-copy values to ensure state cannot be manipulated after instantiation.
* **Properties:**
  * `public double[,] StateSnapshot { get; }`

---

### Strategy Implementations (Mathematical Engine)

#### `IMathOperation` (Strategy Interface)
* **Method:**
  * `Matrix Execute(Matrix a, Matrix b)` - Common contract for compiling calculations. Returns a newly allocated `Matrix` result object.

#### `MatrixAddition` (Concrete Strategy)
* **Method:**
  * `Matrix Execute(Matrix a, Matrix b)` - Verifies `a.Rows == b.Rows && a.Cols == b.Cols`. Performs matrix addition element-by-element. Throws an `InvalidOperationException` upon sizing mismatches.

#### `MatrixSubtraction` (Concrete Strategy)
* **Method:**
  * `Matrix Execute(Matrix a, Matrix b)` - Verifies matching dimensions. Subtracts `b` values from `a` values element-by-element.

#### `MatrixMultiplication` (Concrete Strategy)
* **Method:**
  * `Matrix Execute(Matrix a, Matrix b)` - Verifies dot-product eligibility: `a.Cols == b.Rows`. Instantiates a new matrix sizing `a.Rows` $\times$ `b.Cols`. Runs iterative dot product accumulation loops.

---

### System Services & Creational Handlers

#### `MatrixFactory` (Factory Method)
* **Description:** Central structural dispatcher for instantly provisioning uniform pre-populated configurations.
* **Methods:**
  * `public static Matrix CreateZeroMatrix(int rows, int cols)` - Allocates a standard blank matrix initialized to 0.0.
  * `public static Matrix CreateIdentityMatrix(int size)` - Generates a square matrix where index `(i, j)` is set to 1.0 if `i == j`, and 0.0 otherwise.

#### `ClipboardService` (System Wrapper)
* **Description:** Provides cross-platform text interfacing between the application layer and the host operating system's clipboard stack.
* **Methods:**
  * `public static void CopyText(string text)` - Binds text directly to the active OS clipboard environment.
  * `public static string PasteText()` - Safely captures text stream components out of the system clipboard environment.

#### `CalculatorGUI` (User Interface Caretaker)
**Description:** The orchestrator module running the SplashKit window event loop[cite: 5]. Intercepts mouse coordinates to identify active matrix cells and routes operational requests to strategies.
* **Fields:**
  * `private Matrix _matrixA`
  * `private Matrix _matrixB`
  * `private Matrix _matrixResult`
  * `private IMathOperation _activeStrategy`
  * `private MatrixMemento _internalClipboard`

---

## 5. Implementation Execution Workflow

The agent must approach construction sequentially, testing boundaries at each step to prevent structural corruption:

### Phase 1: Engine Core Setup
1. Scaffold clean, standalone domain files: `Matrix.cs` and `MatrixMemento.cs`.
2. Implement validation exceptions within `Matrix.cs` getter/setter blocks to catch out-of-bounds attempts early.
3. Formulate the `ToLatexString()` method, parsing standard matrices into text streams.

### Phase 2: Strategy Architecture Injection
1. Author `IMathOperation.cs`.
2. Build `MatrixAddition.cs`, `MatrixSubtraction.cs`, and `MatrixMultiplication.cs`.
3. Write unit-level safety guardrails within each strategy file to guarantee dim-checks fail safely before computation loops execute.

### Phase 3: Factory Deployment
1. Standardize initialization within `MatrixFactory.cs`.
2. Implement safety validations within the Identity Matrix method to throw explicit exceptions if users try to pass a non-square length configuration.

### Phase 4: GUI Assembly (SplashKit Loop)
1. Initialize the SplashKit drawing loop canvas framework[cite: 13, 59].
2. Create mouse bounding-box collision detection areas over cell coordinates.
3. Setup key interception hooks to route character inputs into the currently active target cell string buffer.
4. Bind operation selection inputs directly to the Strategy reference assignment handlers.

---

## 6. Code Style, Quality, and Commenting Guidelines

The generated codebase must strictly reflect industry-grade engineering and academic excellence. The autonomous agent must adhere to the following coding specifications:

### Commenting Matrix & Documentation Blueprint
Every class, public interface, and mathematical operation block must feature structured summary documentation.

```csharp
/// Executes a matrix multiplication strategy following linear algebra principles.
/// Requires the column dimensions of Matrix A to match the row dimensions of Matrix B.
/// <param name="a">The left-hand multiplicand matrix.</param>
/// <param name="b">The right-hand multiplier matrix.</param>
/// <returns>A new Matrix object containing the computed dot product results.</returns>
/// <exception cref="InvalidOperationException">Thrown if matrix dimension constraints are violated.</exception>
/// ```