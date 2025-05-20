# Squid Bubbles - Vulkan Edition (SquidBubbles_vulkan)

A project to recreate the "Squid Bubbles" game using the Vulkan graphics API. This is a learning exercise and an exploration into low-level graphics programming.

## Project Goals

*   Learn the fundamentals and advanced concepts of the Vulkan API.
*   Recreate the core gameplay mechanics of the original Squid Bubbles game.
*   Explore techniques for 2D rendering in Vulkan.
*   (Potentially) Achieve higher performance or gain more control over the rendering pipeline compared to the original Unity project.
*   Document the development process and challenges.

## Tech Stack (Preliminary)

*   **Language:** C++ (likely C++17 or newer)
*   **Graphics API:** Vulkan
*   **Windowing & Input:** GLFW (or SDL2 - to be decided)
*   **Mathematics Library:** GLM
*   **Shader Language:** GLSL (compiled to SPIR-V)
*   **Build System:** CMake (or to be decided)

## Build Environment Setup

*(Placeholder for detailed build instructions)*

This section will include:
*   Required dependencies (Vulkan SDK, C++ compiler, CMake, specific libraries).
*   Steps to clone the repository.
*   CMake configuration and build commands.
*   Instructions for running the compiled application.

## Current Status

*   **Phase:** Initial planning and setup.
*   **Tasks:** Defining project scope, creating initial project structure, setting up basic Vulkan instance.

## Development Roadmap (Conceptual)

*(This section will evolve as the project progresses)*

*   **Milestone 1: Basic Window & Vulkan Instance**
    *   Window creation using GLFW/SDL2.
    *   Vulkan instance initialization and validation layer setup.
    *   Physical and logical device selection.
*   **Milestone 2: Swap Chain & First Triangle**
    *   Swap chain creation and management.
    *   Basic render pass.
    *   Graphics pipeline for rendering a single colored triangle.
    *   Command buffer recording and submission.
    *   Main render loop.
*   **Milestone 3: 2D Rendering Basics**
    *   Shader setup for 2D sprites.
    *   Texture loading and mapping.
    *   Vertex buffers and index buffers for quads.
    *   Orthographic projection.
*   **Milestone 4: Game Mechanics**
    *   Player character (Squid/Shrimp) implementation.
    *   Movement and input handling.
    *   Basic marine life entities.
    *   Collision detection (if applicable).
*   **Milestone 5: UI & Educational Content**
    *   Displaying facts about marine life.
    *   Basic UI elements.

## Contribution Guidelines

*(Placeholder - to be detailed later if the project becomes open to collaboration)*

1.  Fork the repository.
2.  Create a new branch for your feature (`git checkout -b feature/YourFeature`).
3.  Commit your changes (`git commit -m 'Add some YourFeature'`).
4.  Push to your branch (`git push origin feature/YourFeature`).
5.  Open a Pull Request.

---

*This README is for the Vulkan-based rewrite of the original Unity-based Squid Bubbles game. For the original project, see [link_to_original_repo_if_public_or_remove_this].*
