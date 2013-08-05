using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSFLauncherA3
{
    class ProgExterne
    {
        static public void ValideProgExt()
        {
            ValideFRAPS();
            ValideTRACKIR();
            ValideTEAMSPEAK();
        }

        // FRAPS

        static public void ValideFRAPS()
        {
            if (testFrapsExist() != "") FSFLauncherCore.fenetrePrincipale.checkBox11.Enabled = true;
        }
        static public string testFrapsExist()
        {
            string valeur = "";
            try
            {
                //ouvrir le dossier principal                   
                RegistryKey cle = Registry.CurrentUser;
                //ouvrir les sous répertoires du dossier principal, le second argument est un booleen permettant de modifier la clé    
                RegistryKey subcle = cle.OpenSubKey("Software\\Fraps3", false);
                valeur = subcle.GetValue("Directory").ToString();
                //fermer le registre   
                cle.Close();
            }
            catch
            {
            }

            return valeur;
        }
        static public void lancerFraps()
        {
            // lancement FRAPS
            if (FSFLauncherCore.fenetrePrincipale.checkBox11.Enabled && FSFLauncherCore.fenetrePrincipale.checkBox11.Checked)
            {
                if (Process.GetProcessesByName("fraps").Length == 0)
                {
                    Process frapsProcess = new Process();
                    frapsProcess.StartInfo.Verb = "runas";
                    frapsProcess.StartInfo.UseShellExecute = true;
                    frapsProcess.StartInfo.FileName = testFrapsExist() + @"\fraps.exe";
                    frapsProcess.Start();
                }
            }
        }

        // TRACK IR
        static public void ValideTRACKIR()
        {
            if (testTrackirExist() != "") FSFLauncherCore.fenetrePrincipale.checkBox12.Enabled = true;
        }
        static public void lancerTrackIR()
        {
            if (FSFLauncherCore.fenetrePrincipale.checkBox12.Enabled && FSFLauncherCore.fenetrePrincipale.checkBox12.Checked)
            {
                // lancement TRACKIR
                if (Process.GetProcessesByName("TrackIR5").Length == 0)
                {
                    try
                    {
                        Process trackirProcess = new Process();
                        trackirProcess.StartInfo.UseShellExecute = true;
                        trackirProcess.StartInfo.Verb = "runas";
                        trackirProcess.StartInfo.FileName = testTrackirExist() + @"\TrackIR5.exe";
                        trackirProcess.Start();
                    }
                    catch
                    {
                    }
                }
            }
        }
        static public string testTrackirExist()
        {
            string valeur = "";
            try
            {
                //ouvrir le dossier principal                   
                RegistryKey cle = Registry.CurrentUser;
                //ouvrir les sous répertoires du dossier principal, le second argument est un booleen permettant de modifier la clé    
                RegistryKey subcle = cle.OpenSubKey("Software\\NaturalPoint\\NaturalPoint\\NPClient Location", false);
                valeur = subcle.GetValue("Path").ToString();
                //fermer le registre   
                cle.Close();
            }
            catch
            {
            }

            return valeur;
        }
        // TEAMSPEAK
        static public void ValideTEAMSPEAK()
        {
            FSFLauncherCore.fenetrePrincipale.button18.Enabled = false;
            if (testTeamSpeakExist())
            {
                FSFLauncherCore.fenetrePrincipale.button18.Enabled = true;
            }
            else
            {
                if (File.Exists(FSFLauncherCore.cheminARMA3 + @"\@FSF\@CLIENT\TeamSpeak3\3.0.10.1\TeamSpeak3\ts3client_win64.exe")) //Si le fichier existe 
                {
                    try
                    {
                        FSFLauncherCore.CopyDir(FSFLauncherCore.cheminARMA3 + @"\@FSF\@CLIENT\TeamSpeak3\3.0.10.1\TeamSpeak3\", FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\");
                    }
                    catch
                    {

                    }
                }

            }
        }
        static public bool testTeamSpeakExist()
        {
            try
            {
                if (File.Exists(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\TeamSpeak3\ts3client_win64.exe")) //Si le fichier existe 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
            }
            return false;      
        }
        static public void lancerTeamspeak3()
        {
            Process[] ts364bit = Process.GetProcessesByName("ts3client_win64");
            Process[] ts332bit = Process.GetProcessesByName("ts3client_win32");

                if (ts364bit.Length == 0 && ts332bit.Length == 0)
                {
                    //test l'existence d'un process
                    // lance ACRE
                    Process ts3Acre = new Process();
                    // Activation de l'envoi des événements
                    ts3Acre.StartInfo.UseShellExecute = true;
                    ts3Acre.StartInfo.Verb = "runas";
                    ts3Acre.StartInfo.FileName = FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\TeamSpeak3\ts3client_win64.exe";
                    ts3Acre.StartInfo.Arguments = "ts3server://ts3.clan-fsf.fr?password=welcome";
                    //ts3Acre.StartInfo.CreateNoWindow = true;
                    ts3Acre.Start();
                }
                else
                {
                    var infoBox = MessageBox.Show("Impossible de lancer le TS3 dédié à ACRE (code d'erreur #CON HARD 00x276H). Vous semblez avoir TS3 deja lancé sur votre ordinateur.", "Erreur TS3 en cours d'execution", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }
    }
}
