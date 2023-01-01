using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.Logging;
using System;
using System.Runtime.InteropServices;

namespace Sephless.WinampControl
{
    //Class for storing winamp api commands
    public class Winamp_Command
    {
        public string text_prompt;
        public int command_value;

        public Winamp_Command(string Button_Text, int Command)
        {
            text_prompt = Button_Text;
            command_value = Command;
        }
    }

    public static class WinampAPI
    {
        //Winamp API Command List
        //with associated WM_COMMAND values
        public static Winamp_Command[] Winamp_Command_Array = new Winamp_Command[]
        {
            new Winamp_Command("Previous track button",40044),
            new Winamp_Command("Next track button",40048),
            new Winamp_Command("Play button",40045),
            new Winamp_Command("Pause/Unpause button",40046),
            new Winamp_Command("Stop button",40047),
            new Winamp_Command("Fadeout and stop",40147),
            new Winamp_Command("Stop after current track",40157),
            new Winamp_Command("Fast-forward 5 seconds",40148),
            new Winamp_Command("Fast-rewind 5 seconds",40144),
            new Winamp_Command("Start of playlist",40154),
            new Winamp_Command("Go to end of playlist", 40158),
            new Winamp_Command("Open file dialog", 40029),
            new Winamp_Command("Open URL dialog", 40155),
            new Winamp_Command("Open file info box", 40188),
            new Winamp_Command("Set time display mode to elapsed", 40037),
            new Winamp_Command("Set time display mode to remaining", 40038),
            new Winamp_Command("Toggle preferences screen", 40012),
            new Winamp_Command("Open visualization options", 40190),
            new Winamp_Command("Open visualization plug-in options", 40191),
            new Winamp_Command("Execute current visualization plug-in", 40192),
            new Winamp_Command("Toggle about box", 40041),
            new Winamp_Command("Toggle title Autoscrolling", 40189),
            new Winamp_Command("Toggle always on top", 40019),
            new Winamp_Command("Toggle Windowshade", 40064),
            new Winamp_Command("Toggle Playlist Windowshade", 40266),
            new Winamp_Command("Toggle doublesize mode", 40165),
            new Winamp_Command("Toggle EQ", 40036),
            new Winamp_Command("Toggle playlist editor", 40040),
            new Winamp_Command("Toggle main window visible", 40258),
            new Winamp_Command("Toggle minibrowser", 40298),
            new Winamp_Command("Toggle easymove", 40186),
            new Winamp_Command("Raise volume by 1%", 40058),
            new Winamp_Command("Lower volume by 1%", 40059),
            new Winamp_Command("Toggle repeat", 40022),
            new Winamp_Command("Toggle shuffle", 40023),
            new Winamp_Command("Open jump to time dialog", 40193),
            new Winamp_Command("Open jump to file dialog", 40194),
            new Winamp_Command("Open skin selector", 40219),
            new Winamp_Command("Configure current visualization plug-in", 40221),
            new Winamp_Command("Reload the current skin", 40291),
            new Winamp_Command("Close Winamp", 40001),
            new Winamp_Command("Moves back 10 tracks in playlist", 40197),
            new Winamp_Command("Show the edit bookmarks", 40320),
            new Winamp_Command("Adds current track as a bookmark", 40321),
            new Winamp_Command("Play audio CD", 40323),
            new Winamp_Command("Load a preset from EQ", 40253),
            new Winamp_Command("Save a preset to EQF", 40254),
            new Winamp_Command("Opens load presets dialog", 40172),
            new Winamp_Command("Opens auto-load presets dialog", 40173),
            new Winamp_Command("Load default preset", 40174),
            new Winamp_Command("Opens save preset dialog", 40175),
            new Winamp_Command("Opens auto-load save preset", 40176),
            new Winamp_Command("Opens delete preset dialog", 40178),
            new Winamp_Command("Opens delete an auto load preset dialog", 40180)
        };

        //Winamp API constants
        public const string lpClassName = "Winamp v1.x";
        public const int WM_COMMAND = 0x111;

        //For the hardcoded functions
        public const int PlayBTN = 40045;
        public const int PauseBTN = 40046;
        public const int StopBTN = 40047;

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public static void HWNDSendMessage(int Command_Type, int Command_Value, string debug_log_prefix)
        {
            //find the window handle
            IntPtr intptr_hwnd = (IntPtr)FindWindow(WinampAPI.lpClassName, null);

            //send the message
            int int_hwnd_result = SendMessage(intptr_hwnd, Command_Type, new IntPtr(Command_Value), new IntPtr(0));

            //Log the result
            string logger_msg = debug_log_prefix + " >> HWND: " + intptr_hwnd.ToString() + ", SendMessage result: " + int_hwnd_result.ToString();
            MacroDeckLogger.Info(Main.Instance, logger_msg);
        }

        //returns global plugin index value (array and dropdown) for SelectedOption property
        public static int getPluginArrayIndex()
        {
            string WinampConfig = SuchByte.MacroDeck.Plugins.PluginConfiguration.GetValue(Main.Instance, "WinampAction");
            string result = JObject.Parse(WinampConfig)["SelectedOption"].ToString();
            int selectedOption = int.Parse(result);
            MacroDeckLogger.Info(Main.Instance, "winamp choice " + result + " from saved config: " + WinampConfig.ToString());
            return selectedOption;
        }

        //Return the index value (array and dropdown) for the configuration value stored for this action button instance
        public static int getIndexFromString(string str_json)
        {
            int selectedOption = 1;
            try
            {
                JObject configurationObject = JObject.Parse(str_json);
                string result = JObject.Parse(configurationObject.ToString())["SelectedOption"].ToString();
                selectedOption = int.Parse(result);
            }
            catch { }
            return selectedOption;
        }

    }


}
