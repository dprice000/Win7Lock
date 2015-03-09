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
        const String explorerPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";
        const String systemPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        static Microsoft.Win32.RegistryKey explorerKey = Registry.CurrentUser.CreateSubKey(explorerPath);

        /// <summary>
        /// Re-enables all of the icons and desktop settings.
        /// </summary>
        public static void EnableDesktop()
        {
            explorerKey.SetValue("NoDesktop", 0x0, RegistryValueKind.DWord);
        }


        /// <summary>
        /// Hides all icons and prevents any interaction with the desktop.
        /// </summary>
        public static void DisableDesktop()
        {
            explorerKey.SetValue("NoDesktop", 0x1, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Disables the system clock.
        /// </summary>
        public static void HideClock()
        {
            explorerKey.SetValue("HideClock", 0x1, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Re-enables the system clock for this user.
        /// </summary>
        /// <remarks>
        /// The clock may not reappeare after being re-enabled. Sometimes a setting in Windows needs to be updated.
        /// </remarks>
        public static void ShowClock()
        {
            explorerKey.SetValue("HideClock", 0x0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Programatically closes and reopens Windows Explorer. Restarting 
        /// Windows Explorer will help group policy take affect.
        /// </summary>
        /// <param name="delay">Number of senconds between when win explorer is closed and re-opened</param>
        public static void ResetWinExplorer(int delay)
        {
        }
    }
}
