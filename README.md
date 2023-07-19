# ConsoleInput Library

The ConsoleInput library provides functionality for reading mouse states and positions within the Windows console window. This library is designed to facilitate mouse input handling in console-based applications.

## Getting Started

To get started with the ConsoleInput library, follow these steps:

1. Clone or download the ConsoleInput repository from the designated location.
2. Open the solution file in your preferred IDE (Integrated Development Environment).
3. Build the solution to ensure all dependencies are resolved and the library is compiled successfully.


### Usage

To use the ConsoleInput library in your own project, follow these steps:

1. Add a reference to the ConsoleInput.Logic project in your project.
2. Import the necessary namespaces from the ConsoleInput.Logic namespace.
3. Instantiate a `ConsoleMouseInput` object to start capturing mouse input.
4. Use the provided methods and properties to read the mouse state and position.

Here's an example of how to use the ConsoleInput library (the following example applies to ConsoleInput 2.0):

```csharp
using ConsoleInput;

// Instantiate the input devices you are going to use.
var mouse = new ConsoleMouse();
var keyboard = new ConsoleKeyboard();

// Instantiate an input manager.
var inputManager = new InputManager();

// Add devices (IDevice) to the input manager.
inputManager.AddDevice(mouse);
inputManager.AddDevice(keyboard);

while (...) {
	// polls input and updates states.
	inputManager.Update();

    // Changes console cursor position to the cell the mouse is hovering over.
    Console.SetCursorPosition(mouse.X, mouse.Y); // The ICursorDevice provides `short X` and `short Y` properties.

    // The initial frame the key is pressed.
	if (mouse.IsButtonPressed(MouseButton.Left))
    {
        Console.WriteLine("Pressed");
    }

    // The frame after the key has been released.
    if (mouse.IsButtonReleased(MouseButton.Left))
    {
        Console.WriteLine("Released");
    }

    // All frames during the press.
    if (mouse.IsButtonDown(MouseButton.Left))
    {
        Console.Write("x");
    }

    // Both devices implement the same interface (IButtonDevice), but with different enums (button options). This keeps the API simple.
    if (keyboard.IsButtonPressed(KeyboardButton.Space)) {
        Console.WriteLine("SPACE!");
    }
}
```

## Project overview

The ConsoleInput project consists of the following folders:

1. ConsoleInput.Logic.Tests: Contains unit tests for the logic components of the ConsoleInput library.
2. ConsoleInput.Logic: Contains the core logic and functionality of the ConsoleInput library.
3. ConsoleInput.Tests: Contains additional tests for the ConsoleInput library.
4. ConsoleInput.WinAPI: Provides Windows API wrappers and utilities for interacting with the console input.
5. ConsoleInput: Contains the main project files and configuration for the ConsoleInput library.
6. PaintDotNet: A folder for the PaintDotNet project.
7. PaintDotNetLegacy: A folder for the PaintDotNetLegacy project.


## Contributing

ANY contribution to the ConsoleInput library is welcome!