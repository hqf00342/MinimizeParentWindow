using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MinimizeParentWindow
{
    internal static class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MINIMIZE = 6;

        private static void Main(string[] args)
        {
            try
            {
                //Get the parent PID.
                var pc = new PerformanceCounter("Process", "Creating Process ID", Process.GetCurrentProcess().ProcessName);
                var ppid = (int)pc.NextValue();
                if (ppid <= 0)
                    throw new Exception("Cannot get parent process ID.");

                //get window handle of the parent process.
                var hwnd = Process.GetProcessById(ppid).MainWindowHandle;
                if (hwnd == IntPtr.Zero)
                    throw new Exception("Cannot get parent window handle.");

                //Minimize the parent window
                ShowWindow(hwnd, SW_MINIMIZE);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name} : {e.Message}");
            }
        }
    }
}