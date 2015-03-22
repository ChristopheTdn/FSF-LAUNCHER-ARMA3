using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FSFLauncherA3
{
    class interclan
    {
        static public string url_organisateur;
        static public void init()
        {
            try
            {
                if (System.IO.File.Exists(FSFLauncherCore.cheminARMA3 + @"\@FSF\@INTERCLAN\interclan.xml"))
                {
                    string MODS = "-MOD=";
                    XmlTextReader fichierProfilXML = new XmlTextReader(FSFLauncherCore.cheminARMA3 + @"\@FSF\@INTERCLAN\interclan.xml");
                    while (fichierProfilXML.Read())
                    {
                        // nom evenement
                        fichierProfilXML.ReadToFollowing("name");
                        string name = fichierProfilXML.ReadString();
                        if (name != "") { FSFLauncherCore.fenetrePrincipale.textBox13.Text = name;};
                        // date
                        fichierProfilXML.ReadToFollowing("date");
                        string date = fichierProfilXML.ReadString();
                        if (date != "") { FSFLauncherCore.fenetrePrincipale.textBox14.Text = date; };
                        // organisateur
                        fichierProfilXML.ReadToFollowing("organisateur");
                        string organisateur = fichierProfilXML.ReadString();
                        if (organisateur != "") { FSFLauncherCore.fenetrePrincipale.linkLabel4.Text = organisateur; };
                        fichierProfilXML.ReadToFollowing("url_organisateur");
                        string organisateur_link = fichierProfilXML.ReadString();
                        if (organisateur_link != "") { url_organisateur = organisateur_link; }
                        // Teamspeak
                        fichierProfilXML.ReadToFollowing("teamspeak");
                        string teamspeak = fichierProfilXML.ReadString();
                        if (teamspeak != "") { FSFLauncherCore.fenetrePrincipale.textBox15.Text = teamspeak; };
                        fichierProfilXML.ReadToFollowing("teamspeak_pass");
                        string teamspeak_pass = fichierProfilXML.ReadString();
                        if (teamspeak_pass != "") { FSFLauncherCore.fenetrePrincipale.textBox16.Text = teamspeak_pass; }
                        // Serveur
                        fichierProfilXML.ReadToFollowing("serveur_ip");
                        string serveur_ip = fichierProfilXML.ReadString();
                        if (serveur_ip != "") { FSFLauncherCore.fenetrePrincipale.textBox10.Text = serveur_ip; };
                        fichierProfilXML.ReadToFollowing("serveur_port");
                        string serveur_port = fichierProfilXML.ReadString();
                        if (serveur_port != "") { FSFLauncherCore.fenetrePrincipale.textBox17.Text = serveur_port; }
                        fichierProfilXML.ReadToFollowing("serveur_pass");
                        string serveur_pass = fichierProfilXML.ReadString();
                        if (serveur_pass != "") { FSFLauncherCore.fenetrePrincipale.textBox12.Text = serveur_pass; }
                        // Description
                        fichierProfilXML.ReadToFollowing("description");
                        string description = fichierProfilXML.ReadString();
                        if (description != "") { FSFLauncherCore.fenetrePrincipale.textBox9.Text = description; };
                        // MODS
                        fichierProfilXML.ReadToFollowing("MODS");
                        string Mods_item = fichierProfilXML.ReadString();
                        if (Mods_item != "") { MODS += Mods_item + " ";  };
                    }
                    FSFLauncherCore.fenetrePrincipale.textBox18.Text = MODS;
                    fichierProfilXML.Close();

                }

                
            }
            catch { }
            

        }
    }
}
