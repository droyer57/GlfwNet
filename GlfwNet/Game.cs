using System;
using GlfwNet.Core;
using ImGuiNET;

namespace GlfwNet
{
    using static Glfw;
    using static Gl;

    public class Game : IDisposable
    {
        private IntPtr _window;
        private ImGuiController _imGuiController;
        private bool _showDemo;

        public void Run()
        {
            if (glfwInit() == 0)
                throw new Exception("glfwInit");

            glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 4);
            glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 6);
            glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

            _window = glfwCreateWindow(640, 480, "Hello World", IntPtr.Zero, IntPtr.Zero);
            if (_window == IntPtr.Zero)
            {
                glfwTerminate();
                throw new Exception("glfwCreateWindow");
            }

            glfwMakeContextCurrent(_window);
            LoadEntryPoints();

            _imGuiController = new ImGuiController(_window);
            _imGuiController.Init();

            while (glfwWindowShouldClose(_window) == 0)
            {
                glfwPollEvents();

                if (glfwGetKey(_window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
                    glfwSetWindowShouldClose(_window, GLFW_TRUE);

                _imGuiController.Update();

                ImGui.Begin("Info");
                ImGui.Text(glGetString(GL_VERSION));
                ImGui.Text($"FPS : {ImGui.GetIO().Framerate.ToString("0")}");
                if (ImGui.Button("Show demo"))
                    _showDemo = true;

                ImGui.End();

                if (_showDemo)
                    ImGui.ShowDemoWindow(ref _showDemo);

                glClear(GL_COLOR_BUFFER_BIT);
                _imGuiController.Render();

                glfwSwapBuffers(_window);
            }
        }

        public void Dispose()
        {
            _imGuiController.Dispose();
            glfwTerminate();
        }
    }
}