# ConsoleInput Library
The ConsoleInput library provides functionality for reading mouse states and positions within the Windows console window. This library is designed to facilitate mouse and keyboard input handling in console-based applications (.NET Framework 4.8).

## Getting Started
There are a few ways you can use the library.

### Nuget (Nuget.org / Github)
To get started with using the library as a nuget package, just install the package.
The package is both hosted on [Github](https://github.com/krestenlaust/ConsoleInput/pkgs/nuget/ConsoleInput) and on [Nuget.org](https://www.nuget.org/packages/ConsoleInput)

  - `dotnet add package ConsoleInput`

### Cloning / submodule
To get started with using the library as a direct dependency, follow these steps:

 1. Clone the ConsoleInput repository
 2. Open the solution (with Visual Studio 2022)
 3. Build the solution to ensure all dependencies are resolved and the library is compiled successfully
 4. Add a references to the `ConsoleInput`, `ConsoleInput.WinAPI` and `ConsoleInput.Logic` projects in your solution, and then in your project.

### Usage
To use the ConsoleInput library in your own project, follow these steps:

 1. Add the project as dependency using either guide above
 2. Instantiate an input device, e.g. `ConsoleMouse`
 3. Instantiate the input manager, `InputManager`
 4. Add input devices to `InputManager` using `IInputManager.AddDevice(IDevice)`
 5. Poll input using `IInputManager.Update()`
 6. Use the provided methods and properties to read the device state and position

Here's an example of how to use the ConsoleInput library (the following example applies to the new ConsoleInput 2.0):

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
I've been spending some time splitting up the project and focusing each subproject on a single responsibility.

### Core projects
These are the projects defining the functionality of the library.

#### ConsoleInput
The main project of ConsoleInput. It contains most the API to interact with, and the core functionality. This project depends on other non-test projects.

#### ConsoleInput.Logic
Contains the core logic surrounding determining when a an input should move between `Pressed`, `Released` and `Down` state.

#### ConsoleInput.WinAPI
Contains Windows API wrappers and utilities used for interoping with mouse and keyboard.

### Demo projects
These projects showcase how to use the API. Furthermore, they're useful for letting me know immediately if I were to break API compatibility.

#### PaintDotNet
A painting tool (the name isn't technical, it's just a pun) for drawing with your mouse. It primarily focuses on making use of the mouse coordinates.
It makes use of the 2.0 API.

#### PaintDotNetLegacy
The same tool as above, but maintained to use the legacy API (1.3). I don't advocate using it, but it's useful for keeping track of backwards compatibility.

### Test projects
These projects contain unit-tests for the other projects.

#### ConsoleInput.Tests
Contains additional tests for the ConsoleInput library. Only contains mock-classes right now.

#### ConsoleInput.Logic.Tests
Contains unit tests for the logic components of the ConsoleInput library, to make sure it's correctly categorizing input sequences in each state.

## Contributing
ANY contribution to the ConsoleInput library is welcome!

### Analyzer
I'm using StyleCop.Analyzer, but I don't agree with every single pedantic rule. I haven't taken the time to disable all the rules I don't agree with, so you can just try to follow the rules to the best of your ability.