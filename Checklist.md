# Project Refactoring & Implementation Checklist

### Phase 1: Core Domain Entities & The Memento Pattern
Currently, `MatrixData` is a raw `struct` sitting in `Program.cs`. It must be encapsulated inside a robust `Matrix` class with boundary protection and state-saving capabilities.
* [ ] **Create `Matrix.cs` (Originator):** Build the class to encapsulate `MatrixData`.
* [ ] Implement boundary-protected getters and setters (`GetValue(r, c)` and `SetValue(r, c, val)`).
* [ ] Implement the `ToLatexString()` method.
* [ ] **Create `MatrixMemento.cs` (Memento):** Build the immutable state snapshot class.
* [ ] Implement `CreateMemento()` and `RestoreFromMemento()` inside the `Matrix` class to enable copy/paste/undo functionality.

---

### Phase 2: Factory Pattern Completion
The `MatrixFactory` needs to be upgraded to return new encapsulated objects.
* [ ] Update `CreateZeroMatrix` (currently `CreateZMatrix`) to return a `Matrix` object instead of a raw `MatrixData` struct.
* [ ] Implement `CreateIdentityMatrix(int size)` with safety validations ensuring it only processes square dimensions.

---

### Phase 3: Strategy Pattern (Mathematical Engine)
Abstract mathematical operations out of `Program.cs` entirely so the UI doesn't know *how* math is calculated.
* [ ] **Refactor `Operation.cs`:** Ensure it acts as the clean Strategy Interface with the signature `Matrix Execute(Matrix a, Matrix b)`.
* [ ] Create `OperationAddition.cs` (Verify dimensions, loop addition).
* [ ] Create `OperationSubtraction.cs` (Verify dimensions, loop subtraction).
* [ ] Create `OperationMultiplication.cs` (Verify `a.cols == b.rows`, execute dot-product accumulation).
* [ ] Create `OperationTranspose.cs`.
* [ ] Create `OperationDeterminant.cs`.
* [ ] Create `OperationInvert.cs`.

---

### Phase 4: Encapsulate Input State (`UserInput.cs`)
Migrate the massive `State` machine (`globalState`, `tempData`, `currentCellX`, `currentCellY`) from `Program.cs`.
* [ ] Build out the `UserInput.cs` fields (`_currentBuffer`, `_parsedValues`, `_targetRows`, `_targetCols`).
* [ ] Migrate the keystroke filtering logic (`GetValidKey` and the giant `switch/if` blocks) from `Program.cs` into `UserInput.ProcessKey()`.
* [ ] Implement `Backspace()` and `Confirm()` logic inside `UserInput` to handle navigating cell-by-cell.

---

### Phase 5: GUI & Rendering Delegation
Eliminate the "God Class" behavior of `Program.cs`.
* [ ] Delete raw `SplashKit.Draw...` and `SplashKit.Fill...` methods from `Program.cs`.
* [ ] Delegate all drawing calls to `CalculatorGUI.cs` or `RenderVisuals.cs`.
* [ ] Route bounding-box click detection to use the `Rectangle` properties already set up in `CalculatorGUI.cs`.

---

### Phase 6: OS Clipboard Integration (Commands)
Implement the OS clipboard text routing and conversions.
* [ ] Figure out the System API hook (e.g., `System.Windows.Forms.Clipboard` or `TextCopy`) and implement it in `Copy.cs`.
* [ ] Implement the LaTeX string formatting logic in `CopyLaTeX.cs`.
* [ ] Implement the visual ASCII formatting logic in `CopyASCII.cs`.