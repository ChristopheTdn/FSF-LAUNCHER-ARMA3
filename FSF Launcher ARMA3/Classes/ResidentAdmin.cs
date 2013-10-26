using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSFLauncherA3
{
    class ResidentAdmin
    {
        static public NotifyIcon trayIcon = new NotifyIcon();
        static public ContextMenu trayMenu = new ContextMenu();
        static public void initialiseTrayIcon()
        {
            
            /*
            FSFLauncherCore.fenetrePrincipale.WindowState = FormWindowState.Minimized;
            FSFLauncherCore.fenetrePrincipale.Visible = false;
            FSFLauncherCore.fenetrePrincipale.ShowInTaskbar = false;
            */
            FSFLauncherCore.fenetrePrincipale.Icon = FSFLauncherA3.Properties.Resources.FSFLauncherA3;
            FSFLauncherCore.fenetrePrincipale.ShowInTaskbar = false;

            //Init trayMenu
            trayMenu.MenuItems.Add("&Ouvrir FSF Launcher", OuvrirFSFLauncher);
            trayMenu.MenuItems.Add("&Rendre résident");

            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("&Quitter");

            //Init trayIcon
            trayIcon.Text = FSFLauncherCore.fenetrePrincipale.Text;
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Icon = FSFLauncherCore.fenetrePrincipale.Icon;
            trayIcon.Text = "FSF Launcher";
            trayIcon.Visible = true;
        }
        static private void OuvrirFSFLauncher(object sender, EventArgs e) 
        {
            trayIcon.BalloonTipTitle = "Rétablir FSF Launcher";
            trayIcon.BalloonTipText = "Vous avez restauré l'interface du FSF Launcher.";
            trayIcon.ShowBalloonTip(500);
            FSFLauncherCore.fenetrePrincipale.WindowState = FormWindowState.Normal;
            FSFLauncherCore.fenetrePrincipale.Activate();
            trayIcon.Icon.Dispose();
        }
    }
}
