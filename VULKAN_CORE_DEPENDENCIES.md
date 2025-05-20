# Core Dependencies for a Basic C++ Vulkan Application

This document lists the typical core dependencies required or highly recommended for developing a basic Vulkan application using C++.

1.  **Vulkan SDK (Software Development Kit)**
    *   **Purpose:** This is the foundational dependency. It provides the Vulkan loader (which acts as an intermediary between your application and installed Vulkan drivers), the Vulkan API headers (`vulkan/vulkan.h`), validation layers for debugging, and various essential tools. Without the SDK, you cannot link against or call Vulkan API functions.
    *   **Key Components:**
        *   **Vulkan Loader:** Discovers and loads Vulkan drivers.
        *   **Vulkan Headers:** Allows your C++ code to recognize Vulkan types and functions.
        *   **Validation Layers:** Help catch API misuse and provide debugging information.
        *   **SPIR-V Tools:** Tools for working with SPIR-V, the shader intermediate representation used by Vulkan (e.g., `glslangValidator`).

2.  **Windowing and Input Library**
    *   **Purpose:** Vulkan itself is a graphics and compute API; it does not handle window creation, managing window events (like resizing or closing), or processing user input (keyboard, mouse). A dedicated library is needed for these platform-specific tasks and to create a `VkSurfaceKHR` object that Vulkan can render to.
    *   **Popular Choices:**
        *   **GLFW:** A lightweight, cross-platform library specifically designed for OpenGL, OpenGL ES, and Vulkan development. It handles window creation, input, and context/surface management.
        *   **SDL2 (Simple DirectMedia Layer):** A more comprehensive cross-platform development library that provides low-level access to audio, keyboard, mouse, joystick, and graphics hardware via OpenGL, Direct3D, and Vulkan. It's suitable if you need more than just windowing and input.

3.  **C++ Mathematics Library**
    *   **Purpose:** Graphics programming involves extensive use of mathematical operations, particularly linear algebra (vectors, matrices, quaternions). A good math library provides these structures and functions, optimized for graphics, and helps keep the application code clean and focused on graphics logic rather than reinventing math routines. It's crucial for transformations (model, view, projection), lighting calculations, and more.
    *   **Popular Choice:**
        *   **GLM (OpenGL Mathematics):** A header-only C++ mathematics library based on the GLSL (OpenGL Shading Language) specification. It's designed to work well with graphics APIs like Vulkan and provides types and functions that mirror GLSL's, making it intuitive for graphics developers.

4.  **Shader Compilation Tools/Libraries (Optional but Recommended)**
    *   **Purpose:** Vulkan shaders must be provided in the SPIR-V (Standard Portable Intermediate Representation-V) binary format. You typically write shaders in a high-level language like GLSL (OpenGL Shading Language) or HLSL (High-Level Shading Language) and then compile them to SPIR-V. This compilation can be done offline as part of the build process or at runtime.
    *   **Tools/Libraries:**
        *   **`glslangValidator`:** A command-line tool (part of the Vulkan SDK) that can compile GLSL shaders to SPIR-V. Often used in build scripts.
        *   **`shaderc`:** A library from Google that wraps `glslang` and `SPIRV-Tools`. It allows you to compile GLSL/HLSL to SPIR-V programmatically within your C++ application, which can be useful for runtime shader compilation or more integrated build systems.
        *   **DirectXShaderCompiler (DXC):** For compiling HLSL to SPIR-V.

These dependencies form the core toolkit for getting started with Vulkan development in C++. Additional libraries for tasks like image loading, model loading, or GUI rendering might be added as the project grows in complexity.
