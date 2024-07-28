# Final Project Assembler

This project is a Windows Forms application written in C# that simulates an assembler's operations.

## Overview

The application performs the following key functions:

1. **Assembler Conversion**: 
   - Converts assembler instructions to machine code when the "Run" button is clicked.
   - The converted instructions are stored in memory at specific addresses.

2. **Instruction Execution**:
   - Initializes the Instruction Register (IR) with the first 4-bit value.
   - Executes the instructions line by line when the "Debug" button is clicked.
   - Performs necessary operations on registers (R0-R7, PC, N, Z, P) based on the instructions.
   - Updates the Program Counter (PC) accordingly.

## Features

- **User-friendly Interface**: The Windows Forms application provides an intuitive interface for users to input assembler instructions and execute them.
- **Step-by-step Execution**: Allows users to debug and observe the execution of instructions step by step.
- **Register Operations**: Simulates operations on various registers and memory.

## How to Use

1. **Running the Assembler**:
   - Input your assembler instructions in the provided text area.
   - Click the "Run" button to convert the instructions to machine code and store them in memory.

2. **Debugging Instructions**:
   - Click the "Debug" button to initialize the Instruction Register with the first instruction.
   - Observe the step-by-step execution of the instructions and the corresponding changes in the registers.

## Requirements

- .NET Framework (version required for your project)
- Windows operating system

## Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/your-repo-name.git
