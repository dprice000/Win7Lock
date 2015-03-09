using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Win7Lock
{
    public static class Winlock
    {
        const String mExplorerPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";
        const String mSystemPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        static Microsoft.Win32.RegistryKey mExplorerKey = Registry.CurrentUser.CreateSubKey(mExplorerPath);
        static Microsoft.Win32.RegistryKey mSystemKey = Registry.CurrentUser.CreateSubKey(mSystemPath);
        const int SW_HIDE = 0;
        const int SW_SHOW = 1;

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr FindWindowExW(IntPtr parentHWnd, IntPtr childAfterHWnd, IntPtr className, string windowTitle);

        /// <summary>
        /// Uses the Windows API to re-enable the taskbar
        /// </summary>
        /// <remarks>Calling ResetWinExplorer() will reset this setting</remarks>
        public static void ShowTaskBar()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", ""), SW_SHOW);
        }

        /// <summary>
        /// Uses the Windows API to hide the taskbar
        /// </summary>
        /// <remarks>Calling ResetWinExplorer() will reset this setting</remarks>
        public static void HideTaskBar()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", ""), SW_HIDE);
        }

        /// <summary>
        /// Starts a third party process that disables Winkeys.
        /// </summary>
        /// <remarks>
        /// Does not require ResetWinExplorer()
        /// </remarks>
        public static void DisableWinKeys()
        {
            System.Diagnostics.Process.Start("NoWinKey.exe");
        }

        /// <summary>
        /// Kills the third party process that disables WinKeys
        /// </summary>
        /// <remarks>
        /// Does not require ResetWinExplorer()
        /// </remarks>
        public static void EnableWinKeys()
        {
            System.Diagnostics.Process[] noAltTabProc = System.Diagnostics.Process.GetProcessesByName("NoWinKey");

            foreach (System.Diagnostics.Process proc in noAltTabProc)
            {
                proc.Kill();
            }
        }

        /// <summary>
        /// Kills the third party process that disables Alt+Tab
        /// </summary>
        /// <remarks>
        /// Does not require ResetWinExplorer()
        /// </remarks>
        public static void EnableAltTab()
        {
            System.Diagnostics.Process.Start("NoAltTab.exe");
        }

        /// <summary>
        /// Starts a third party process that disables Alt+Tab
        /// </summary>
        /// <remarks>
        /// Does not require ResetWinExplorer()
        /// </remarks>
        public static void DisableAltTab()
        {
            System.Diagnostics.Process[] noAltTabProc = System.Diagnostics.Process.GetProcessesByName("NoAltTab");

            foreach (System.Diagnostics.Process proc in noAltTabProc)
            {
                proc.Kill();
            }
        }

        /// <summary>
        /// Re-enables all of the icons and desktop settings.
        /// </summary>
        public static void EnableDesktop()
        {
            mExplorerKey.SetValue("NoDesktop", 0x0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Hides all icons and prevents any interaction with the desktop.
        /// </summary>
        public static void DisableDesktop()
        {
            mExplorerKey.SetValue("NoDesktop", 0x1, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Prevents user from accessing task manager
        /// </summary>
        public static void DisableTaskMgr()
        {
            mSystemKey.SetValue("DisableTaskMgr", 0x1, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Enables the task manager
        /// </summary>
        public static void EnableTaskMgr()
        {
            mSystemKey.SetValue("DisableTaskMgr", 0x0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Disables the system clock.
        /// </summary>
        public static void HideClock()
        {
            mExplorerKey.SetValue("HideClock", 0x1, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Re-enables the system clock for this user.
        /// </summary>
        /// <remarks>
        /// The clock may not reappeare after being re-enabled. Sometimes a setting in Windows needs to be updated.
        /// </remarks>
        public static void ShowClock()
        {
            mExplorerKey.SetValue("HideClock", 0x0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Programatically closes and reopens Windows Explorer. Restarting 
        /// Windows Explorer will help group policy take affect.
        /// </summary>
        /// <param name="delay">Number of senconds between when win explorer is closed and re-opened</param>
        public static void ResetWinExplorer(int delay)
        {
            var explorerProcs = System.Diagnostics.Process.GetProcessesByName("explorer");

            foreach(var process in explorerProcs)
            {
                process.Kill();
            }

            System.Threading.Thread.Sleep(delay * 1000);

            System.Diagnostics.Process.Start("explorer.exe");
        }
    }
}
