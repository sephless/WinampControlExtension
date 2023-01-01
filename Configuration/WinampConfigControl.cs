using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;

namespace Sephless.WinampControl
{

    public partial class WinampConfigControl : ActionConfigControl
    {
        // Add a variable for the instance of your action to get access to the Configuration etc.
        private readonly PluginAction _macroDeckAction;

        public WinampConfigControl(PluginAction macroDeckAction, ActionConfigurator actionConfigurator)
        {
            _macroDeckAction = macroDeckAction;
            InitializeComponent();
            populateComboBox();
        }

        public override bool OnActionSave()
        {
            if (string.IsNullOrWhiteSpace(WinampChoice_ComboBox.Text))
            {
                return false; // Return false if the user has not filled out the text box
            }
            try
            {
                JObject configurationObject = JObject.FromObject(new
                {
                    SelectedOption = WinampChoice_ComboBox.SelectedIndex,
                });
                //save action specific value
                _macroDeckAction.Configuration = configurationObject.ToString();
                _macroDeckAction.ConfigurationSummary = WinampChoice_ComboBox.Text;

                //save global value
                //SuchByte.MacroDeck.Plugins.PluginConfiguration.SetValue(Main.Instance, "WinampAction", configurationObject.ToString());
            }
            catch { }
            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }


    }

}


