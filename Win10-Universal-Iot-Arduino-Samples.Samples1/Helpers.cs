using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win10_Universal_Iot_Arduino_Samples.Samples1
{
    public static class Helpers
    {
        public static Windows.UI.Xaml.Controls.TextBox OutputTextBox { get; set; }
        public static async void ShowMessage(string msg)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
               () =>
               {
                   OutputTextBox.Text = DateTime.Now.ToLocalTime().ToString() + msg + Environment.NewLine + OutputTextBox.Text;
               }
            );

        }
    }
}
