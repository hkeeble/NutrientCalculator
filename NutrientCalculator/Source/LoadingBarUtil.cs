using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Shell;

namespace Assignment
{
    public class LoadingBar
    {
        public static void Show()
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();
            SystemTray.ProgressIndicator.IsIndeterminate = true;
            SystemTray.ProgressIndicator.IsVisible = true;
        }

        public static void Show(string text)
        {
            Show();
            SystemTray.ProgressIndicator.Text = text;
        }

        public static void Hide()
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();
            SystemTray.ProgressIndicator.IsIndeterminate = true;
            SystemTray.ProgressIndicator.IsVisible = false;
        }

    }
}
