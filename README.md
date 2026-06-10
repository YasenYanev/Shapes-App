# Shapes Application

A Windows Forms desktop application built with C# and .NET 8.0 that allows users to create, edit, and manage geometric shapes on a canvas.

**Coursework Project**: TU Sofia - Object-Oriented Programming (OOP) Course

## Features

- **Shape Management**: Create, edit, delete, and select geometric shapes
- **Supported Shapes**: 
  - Circle
  - Triangle
  - Rectangle
  - Square
- **Customization**: Adjust shape properties including position, size, and colors
- **Color Customization**: Set both inner and border colors for shapes
- **Shape Information**: View detailed information about all created shapes
- **Canvas Rendering**: Double-buffered panel for smooth drawing and interaction

## Requirements

- .NET 8.0 or higher
- Windows OS with .NET Windows Forms support
- Visual Studio 2022 or later (recommended for development)

## Building

1. Clone the repository
2. Open `Coursework.sln` in Visual Studio
3. Build the solution (Ctrl+Shift+B)
4. Run the application (F5)

## Dependencies

- **Newtonsoft.Json** (v13.0.1) - JSON serialization support

## Usage

1. **Launch the Application**: Run the compiled executable
2. **Add Shapes**: Use the "Add Shape" button/menu to create new shapes on the canvas
3. **Edit Shapes**: Select a shape and modify its properties (size, position, colors)
4. **Delete Shapes**: Select a shape and choose delete to remove it
5. **View Information**: Access the shapes info panel to see details about all created shapes

## Architecture

The application follows several design patterns:

- **Factory Pattern**: Used for shape creation (`ShapeFactory`), form creation (`FormFactory`), and operation creation (`OperationFactory`)
- **Strategy Pattern**: Operations are treated as interchangeable strategies for handling user actions
- **MVC Principles**: Separation between model (shapes), view (forms), and control (operations)

## Development Notes

- The application uses double buffering for smooth rendering
- Shapes are stored in a list and managed through operations
- The event binder handles interaction between UI components and operations
- Each shape maintains its own properties including position, size, and color information
