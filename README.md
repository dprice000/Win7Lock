# Win7Lock
A C# library for locking setting up a windows kiosk  

This application was created to mimic the third party WinLock library. 
The WinLock library was originally created for Windows XP and is now deprecated.
This library has been created with a mixture of the windows API and the Group Policy registries
to lock down windows. 

#ResetWinExplorer()
A function called ResetWinExplorer() has been created as quick way to restart Windows Explorer. This call is required after
most Win7Lock function calls. Those calls that do not require this call have been noted in the remarks field. 

#AutoHotKeys
To disable Alt+Tab and Windows Keys I made use of an AutoHotKeys script. If anyone finds a good replacement for this feel free to
push out an update.
http://www.autohotkey.com/