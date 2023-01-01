//https://github.com/Macro-Deck-org/Macro-Deck/wiki/Plugin-API

using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;


namespace Sephless.WinampControl
{
    public class Main : MacroDeckPlugin
    {
        public static Main Instance; //for debug logging

        // Optional; If your plugin can be configured, set to "true". It'll make the "Configure" button appear in the package manager.
        public override bool CanConfigure => true;

        // Gets called when the plugin is loaded
        public override void Enable()
        {
            Instance ??= this;
            Actions = new List<PluginAction>{
            // add the instances of your actions here
                new Winamp_API_Command(),
                new Winamp_Play(),
                new Winamp_Pause(),
                new Winamp_Stop()
            };
        }
    }

    //Hard coded play plug-in with no config
    public class Winamp_Play : PluginAction
    {
        //Name and description of action
        public override string Name => "Play Winamp";
        public override string Description => "Play the current track in Winamp";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => false;

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            WinampAPI.HWNDSendMessage(WinampAPI.WM_COMMAND, WinampAPI.PlayBTN, "Play Button");
        }

    }

    //Hard coded stop plug-in with no config
    public class Winamp_Stop : PluginAction
    {
        //Name and description of action
        public override string Name => "Stop Winamp";
        public override string Description => "Stop the current track in Winamp";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => false;

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            WinampAPI.HWNDSendMessage(WinampAPI.WM_COMMAND, WinampAPI.StopBTN, "Stop Button");
        }
    }

    //Hard coded pause plug-in with no config
    public class Winamp_Pause : PluginAction
    {
        //Name and description of action
        public override string Name => "Pause/Unpause Winamp";
        public override string Description => "Pause/Unpause the current track in Winamp";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => false;

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            WinampAPI.HWNDSendMessage(WinampAPI.WM_COMMAND, WinampAPI.PauseBTN, "Pause Button");
        }
    }

    //Select from a list of available winamp commands and configure this plugin to that action
    public class Winamp_API_Command : PluginAction
    {
        //Name and description of action
        public override string Name => "Winamp API Command";
        public override string Description => "Pick from the list of available Winamp API commands";

        // Optional; Add if this action can be configured. This will make the ActionConfigurator calling GetActionConfigurator();
        public override bool CanConfigure => true;

        // Gets called when the action is triggered by a button press or an event
        public override void Trigger(string clientId, ActionButton actionButton)
        {
            int index = WinampAPI.getIndexFromString(Configuration.ToString());
            WinampAPI.HWNDSendMessage(WinampAPI.WM_COMMAND, WinampAPI.Winamp_Command_Array[index].command_value, WinampAPI.Winamp_Command_Array[index].text_prompt);
        }

        // Gets called when the action can be configured and the action got selected in the ActionSelector. You need to return a user control with the "ActionConfigControl" class as base class
        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new WinampConfigControl(this, actionConfigurator);
        }

    }

}
