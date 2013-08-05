using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FSFLauncherA3
{
    class ProfilServer
    {
        static public void SauvegardeProfilServer()
        {
            //
            ProfilServer.GenerefichierServer();
            //
            FormSerializer formSerializer = new FormSerializer();
            formSerializer.serialize(FSFLauncherCore.fenetrePrincipale.ServeurZoneA3, FSFLauncherCore.cheminARMA3 + @"\@FSFServer\" + (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + @"\profilServer.LauncherFSF.bin");
            // sauvegarde profil general
            FSFLauncherCore.sauvegardeConfigProfilXML("");
        }
        static public void ChargeProfilServer()
        {
            DefautConfig();

            string repertoireDeTravail = FSFLauncherCore.cheminARMA3 + @"\@FSFServer\" + (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString();
            // ANNULE si PROFIL n EXISTE PAS
            if (!Directory.Exists(repertoireDeTravail + @"\profile"))
            {
                return;
            };
            FormSerializer formSerializer = new FormSerializer();
            formSerializer.unSerialize(FSFLauncherCore.fenetrePrincipale,FSFLauncherCore.cheminARMA3 + @"\@FSFServer\" + (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + @"\profilServer.LauncherFSF.bin");
        }
        static public void DefautConfig()
        {
            // Onglet General
            FSFLauncherCore.fenetrePrincipale.textBox18.Text = FSFLauncherCore.cheminARMA3;

            // onglet Serveur
            FSFLauncherCore.fenetrePrincipale.textBox12.Text = "";
            FSFLauncherCore.fenetrePrincipale.textBox13.Text = "";
            FSFLauncherCore.fenetrePrincipale.textBox14.Text = "";
            FSFLauncherCore.fenetrePrincipale.numericUpDown1.Value = 10;
            FSFLauncherCore.fenetrePrincipale.textBox15.Text = "2302";
            FSFLauncherCore.fenetrePrincipale.textBox16.Text = "8766";
            FSFLauncherCore.fenetrePrincipale.textBox17.Text = "27016";
            FSFLauncherCore.fenetrePrincipale.checkBox25.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox26.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox27.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox20.Checked = false;
            FSFLauncherCore.fenetrePrincipale.comboBox1.SelectedItem = 0;

            // onglet regles

            FSFLauncherCore.fenetrePrincipale.checkBox16.Checked = false;
            FSFLauncherCore.fenetrePrincipale.numericUpDown3.Value = 1;
            FSFLauncherCore.fenetrePrincipale.numericUpDown4.Value = 30;
            FSFLauncherCore.fenetrePrincipale.checkBox13.Checked = false;
            FSFLauncherCore.fenetrePrincipale.numericUpDown2.Value = 10;
            FSFLauncherCore.fenetrePrincipale.checkBox14.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox15.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox17.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox18.Checked = false;
            FSFLauncherCore.fenetrePrincipale.textBox21.Text = "";
            FSFLauncherCore.fenetrePrincipale.numericUpDown2.Value = 10;

            // onglet difficultés            

            FSFLauncherCore.fenetrePrincipale.radioButton7.Checked = true;

            // recruit
            FSFLauncherCore.fenetrePrincipale.checkBox28.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox29.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox30.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox31.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox49.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox34.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox35.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox36.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox37.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox38.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox39.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox40.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox41.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox42.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox43.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox44.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox45.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox46.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox47.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox48.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox32.Checked = true;
            FSFLauncherCore.fenetrePrincipale.numericUpDown6.Value = 0.65M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown7.Value = 0.65M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown8.Value = 0.40M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown9.Value = 0.40M;

            // regular

            FSFLauncherCore.fenetrePrincipale.checkBox71.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox70.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox69.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox66.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox50.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox68.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox63.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox64.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox65.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox62.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox61.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox60.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox59.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox58.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox57.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox56.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox55.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox54.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox53.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox52.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox51.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox67.Checked = true;
            FSFLauncherCore.fenetrePrincipale.numericUpDown15.Value = 0.75M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown14.Value = 0.75M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown13.Value = 0.60M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown5.Value = 0.60M;

            // Veteran

            FSFLauncherCore.fenetrePrincipale.checkBox115.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox114.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox113.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox110.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox94.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox112.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox107.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox108.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox109.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox106.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox105.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox104.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox103.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox102.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox101.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox100.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox99.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox98.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox97.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox96.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox95.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox111.Checked = true;
            FSFLauncherCore.fenetrePrincipale.numericUpDown23.Value = 0.85M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown22.Value = 0.85M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown21.Value = 0.85M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown20.Value = 0.85M;

            // ELITE

            FSFLauncherCore.fenetrePrincipale.checkBox93.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox92.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox91.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox88.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox72.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox90.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox85.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox86.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox87.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox84.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox83.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox82.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox81.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox80.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox79.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox78.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox77.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox76.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox75.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox74.Checked = true;
            FSFLauncherCore.fenetrePrincipale.checkBox73.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox89.Checked = true;
            FSFLauncherCore.fenetrePrincipale.numericUpDown19.Value = 1.00M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown18.Value = 1.00M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown17.Value = 0.85M;
            FSFLauncherCore.fenetrePrincipale.numericUpDown16.Value = 0.85M;

        }
        static public void GenerefichierServer()
        {
            FileStream fs;
            string text;

            string repertoireDeTravail = FSFLauncherCore.cheminARMA3 + @"\@FSFServer\" + (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString();
            // creation repertoire
            if (!Directory.Exists(repertoireDeTravail + @"\profile"))
            {
                // repertoire n'existe pas
                Directory.CreateDirectory(repertoireDeTravail + @"\profile\Users\server");
            };

            // creation server.cfg

            fs = File.Create(repertoireDeTravail + @"\server.cfg");
            fs.Close();
            text = "";
            text += "// ******************" + Environment.NewLine;
            text += "//     server.cfg" + Environment.NewLine;
            text += "// ******************" + Environment.NewLine;
            text += "// généré avec le FSF Launcher A3 edition" + Environment.NewLine;
            text += "// www.clan-fsf.fr" + Environment.NewLine;
            text += Environment.NewLine;
            text += "//server Info" + Environment.NewLine;
            text += @"hostname = """ + FSFLauncherCore.fenetrePrincipale.textBox12.Text + @""";" + Environment.NewLine;
            text += @"password = """ + FSFLauncherCore.fenetrePrincipale.textBox13.Text + @""";" + Environment.NewLine;
            text += @"passwordAdmin = """ + FSFLauncherCore.fenetrePrincipale.textBox14.Text + @""";" + Environment.NewLine;
            if (FSFLauncherCore.fenetrePrincipale.checkBox25.Checked) { text += @"logFile = ""Console.log""" + Environment.NewLine; };
            text += "//STEAM Info Port" + Environment.NewLine;
            if (FSFLauncherCore.fenetrePrincipale.textBox16.Text != "") { text += @"steamport = " + FSFLauncherCore.fenetrePrincipale.textBox16.Text + ";" + Environment.NewLine; };
            if (FSFLauncherCore.fenetrePrincipale.textBox17.Text != "") { text += @"steamqueryport = " + FSFLauncherCore.fenetrePrincipale.textBox17.Text + ";" + Environment.NewLine; };
            if (FSFLauncherCore.fenetrePrincipale.textBox21.Text != "")
            {
                text += "//Message of the Day" + Environment.NewLine;
                text += "motd[]=" + Environment.NewLine;
                text += "{" + Environment.NewLine;
                {
                    foreach (string line in FSFLauncherCore.fenetrePrincipale.textBox21.Lines)
                    {
                        text += @"""" + line + @"""," + Environment.NewLine;
                    }
                    text += @"""""};" + Environment.NewLine;
                }
                text += "motdInterval = " + FSFLauncherCore.fenetrePrincipale.numericUpDown10.Value.ToString() + @";" + Environment.NewLine;
            }
            text += "// Server Param" + Environment.NewLine;
            text += "maxPlayers = " + FSFLauncherCore.fenetrePrincipale.numericUpDown1.Value.ToString() + @";" + Environment.NewLine;
            if (FSFLauncherCore.fenetrePrincipale.checkBox18.Checked) { text += "kickDuplicate = 1;" + Environment.NewLine; };
            if (FSFLauncherCore.fenetrePrincipale.checkBox17.Checked) { text += "verifySignatures = 2;" + Environment.NewLine; };
            if (FSFLauncherCore.fenetrePrincipale.checkBox14.Checked) { text += "persistent = 1;" + Environment.NewLine; };
            if (FSFLauncherCore.fenetrePrincipale.checkBox13.Checked)
            {
                text += "// VON" + Environment.NewLine;
                text += "disableVoN = 0;" + Environment.NewLine;
                text += "vonCodecQuality = " + FSFLauncherCore.fenetrePrincipale.numericUpDown2.Value.ToString() + @";" + Environment.NewLine;
            }
            else
            {
                text += "// VON" + Environment.NewLine;
                text += "disableVoN = 1;" + Environment.NewLine;
            };
            if (FSFLauncherCore.fenetrePrincipale.checkBox16.Checked)
            {
                text += "// voteMissionPlayers" + Environment.NewLine;
                text += "voteMissionPlayers = " + FSFLauncherCore.fenetrePrincipale.numericUpDown3.Value.ToString() + @";" + Environment.NewLine;
                text += "voteThreshold = " + (FSFLauncherCore.fenetrePrincipale.numericUpDown4.Value / 100).ToString().Replace(",", ".") + @";" + Environment.NewLine;
            }
            System.IO.File.WriteAllText(repertoireDeTravail + @"\server.cfg", text);
            // creation basic.cfg
            fs = File.Create(repertoireDeTravail + @"\basic.cfg");
            fs.Close();
            text = "";
            text += "// fichier basic.cfg" + Environment.NewLine;
            System.IO.File.WriteAllText(repertoireDeTravail + @"\basic.cfg", text);
            //creation .Arma3Profile
            fs = File.Create(repertoireDeTravail + @"\profile\Users\server\server.Arma3Profile");
            fs.Close();
            text = "";
            text += "// *************************" + Environment.NewLine;
            text += "// fichier Arma3AlphaProfile" + Environment.NewLine;
            text += "// *************************" + Environment.NewLine;
            text += "" + Environment.NewLine;
            text += "class Difficulties" + Environment.NewLine;
            text += " {" + Environment.NewLine;
            text += "      class Recruit" + Environment.NewLine;
            text += "       {" + Environment.NewLine;
            text += "       	class Flags" + Environment.NewLine;
            text += "             {" + Environment.NewLine;
            text += "              Armor=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox28.Checked) + ";" + Environment.NewLine;
            text += "              FriendlyTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox29.Checked) + ";" + Environment.NewLine;
            text += "              EnemyTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox30.Checked) + ";" + Environment.NewLine;
            text += "              MineTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox31.Checked) + ";" + Environment.NewLine;
            text += "              HUD=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox49.Checked) + ";" + Environment.NewLine;
            text += "              HUDPerm=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox33.Checked) + ";" + Environment.NewLine;
            text += "              HUDWp=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox34.Checked) + ";" + Environment.NewLine;
            text += "              HUDWpPerm=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox35.Checked) + ";" + Environment.NewLine;
            text += "              HUDGroupInfo=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox36.Checked) + ";" + Environment.NewLine;
            text += "              AutoSpot=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox37.Checked) + ";" + Environment.NewLine;
            text += "              Map=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox38.Checked) + ";" + Environment.NewLine;
            text += "              WeaponCursor=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox39.Checked) + ";" + Environment.NewLine;
            text += "              AutoGuideAT=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox40.Checked) + ";" + Environment.NewLine;
            text += "              ClockIndicator=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox41.Checked) + ";" + Environment.NewLine;
            text += "              3rdPersonView=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox42.Checked) + ";" + Environment.NewLine;
            text += "              UltraAI=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox43.Checked) + ";" + Environment.NewLine;
            text += "              CameraShake=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox44.Checked) + ";" + Environment.NewLine;
            text += "              UnlimitedSaves=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox45.Checked) + ";" + Environment.NewLine;
            text += "              DeathMessages=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox46.Checked) + ";" + Environment.NewLine;
            text += "              NetStats=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox47.Checked) + ";" + Environment.NewLine;
            text += "              VonID=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox48.Checked) + ";" + Environment.NewLine;
            text += "              ExtendetInfoType=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox32.Checked) + ";" + Environment.NewLine;
            text += "             };" + Environment.NewLine;
            text += "   		  skillFriendly=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown6.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionFriendly=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown7.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  skillEnemy=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown8.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionEnemy=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown9.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "       };" + Environment.NewLine;

            text += "      class Regular" + Environment.NewLine;
            text += "       {" + Environment.NewLine;
            text += "       	class Flags" + Environment.NewLine;
            text += "             {" + Environment.NewLine;
            text += "              Armor=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox71.Checked) + ";" + Environment.NewLine;
            text += "              FriendlyTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox70.Checked) + ";" + Environment.NewLine;
            text += "              EnemyTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox69.Checked) + ";" + Environment.NewLine;
            text += "              MineTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox66.Checked) + ";" + Environment.NewLine;
            text += "              HUD=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox50.Checked) + ";" + Environment.NewLine;
            text += "              HUDPerm=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox68.Checked) + ";" + Environment.NewLine;
            text += "              HUDWp=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox63.Checked) + ";" + Environment.NewLine;
            text += "              HUDWpPerm=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox64.Checked) + ";" + Environment.NewLine;
            text += "              HUDGroupInfo=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox65.Checked) + ";" + Environment.NewLine;
            text += "              AutoSpot=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox62.Checked) + ";" + Environment.NewLine;
            text += "              Map=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox61.Checked) + ";" + Environment.NewLine;
            text += "              WeaponCursor=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox60.Checked) + ";" + Environment.NewLine;
            text += "              AutoGuideAT=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox59.Checked) + ";" + Environment.NewLine;
            text += "              ClockIndicator=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox58.Checked) + ";" + Environment.NewLine;
            text += "              3rdPersonView=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox57.Checked) + ";" + Environment.NewLine;
            text += "              UltraAI=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox56.Checked) + ";" + Environment.NewLine;
            text += "              CameraShake=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox55.Checked) + ";" + Environment.NewLine;
            text += "              UnlimitedSaves=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox54.Checked) + ";" + Environment.NewLine;
            text += "              DeathMessages=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox53.Checked) + ";" + Environment.NewLine;
            text += "              NetStats=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox52.Checked) + ";" + Environment.NewLine;
            text += "              VonID=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox51.Checked) + ";" + Environment.NewLine;
            text += "              ExtendetInfoType=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox67.Checked) + ";" + Environment.NewLine;
            text += "             };" + Environment.NewLine;
            text += "   		  skillFriendly=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown15.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionFriendly=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown14.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  skillEnemy=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown13.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionEnemy=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown5.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "       };" + Environment.NewLine;
            text += "      class Veteran" + Environment.NewLine;
            text += "       {" + Environment.NewLine;
            text += "       	class Flags" + Environment.NewLine;
            text += "             {" + Environment.NewLine;
            text += "              Armor=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox115.Checked) + ";" + Environment.NewLine;
            text += "              FriendlyTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox114.Checked) + ";" + Environment.NewLine;
            text += "              EnemyTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox113.Checked) + ";" + Environment.NewLine;
            text += "              MineTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox110.Checked) + ";" + Environment.NewLine;
            text += "              HUD=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox94.Checked) + ";" + Environment.NewLine;
            text += "              HUDPerm=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox112.Checked) + ";" + Environment.NewLine;
            text += "              HUDWp=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox107.Checked) + ";" + Environment.NewLine;
            text += "              HUDWpPerm=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox108.Checked) + ";" + Environment.NewLine;
            text += "              HUDGroupInfo=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox109.Checked) + ";" + Environment.NewLine;
            text += "              AutoSpot=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox106.Checked) + ";" + Environment.NewLine;
            text += "              Map=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox105.Checked) + ";" + Environment.NewLine;
            text += "              WeaponCursor=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox104.Checked) + ";" + Environment.NewLine;
            text += "              AutoGuideAT=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox103.Checked) + ";" + Environment.NewLine;
            text += "              ClockIndicator=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox102.Checked) + ";" + Environment.NewLine;
            text += "              3rdPersonView=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox101.Checked) + ";" + Environment.NewLine;
            text += "              UltraAI=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox100.Checked) + ";" + Environment.NewLine;
            text += "              CameraShake=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox99.Checked) + ";" + Environment.NewLine;
            text += "              UnlimitedSaves=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox98.Checked) + ";" + Environment.NewLine;
            text += "              DeathMessages=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox97.Checked) + ";" + Environment.NewLine;
            text += "              NetStats=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox96.Checked) + ";" + Environment.NewLine;
            text += "              VonID=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox95.Checked) + ";" + Environment.NewLine;
            text += "              ExtendetInfoType=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox67.Checked) + ";" + Environment.NewLine;
            text += "             };" + Environment.NewLine;
            text += "   		  skillFriendly=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown23.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionFriendly=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown22.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  skillEnemy=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown21.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionEnemy=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown20.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "       };" + Environment.NewLine;

            text += "      class Expert" + Environment.NewLine;
            text += "       {" + Environment.NewLine;
            text += "       	class Flags" + Environment.NewLine;
            text += "             {" + Environment.NewLine;
            text += "              Armor=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox93.Checked) + ";" + Environment.NewLine;
            text += "              FriendlyTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox92.Checked) + ";" + Environment.NewLine;
            text += "              EnemyTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox91.Checked) + ";" + Environment.NewLine;
            text += "              MineTag=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox88.Checked) + ";" + Environment.NewLine;
            text += "              HUD=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox72.Checked) + ";" + Environment.NewLine;
            text += "              HUDPerm=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox90.Checked) + ";" + Environment.NewLine;
            text += "              HUDWp=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox85.Checked) + ";" + Environment.NewLine;
            text += "              HUDWpPerm=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox86.Checked) + ";" + Environment.NewLine;
            text += "              HUDGroupInfo=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox87.Checked) + ";" + Environment.NewLine;
            text += "              AutoSpot=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox84.Checked) + ";" + Environment.NewLine;
            text += "              Map=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox83.Checked) + ";" + Environment.NewLine;
            text += "              WeaponCursor=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox82.Checked) + ";" + Environment.NewLine;
            text += "              AutoGuideAT=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox81.Checked) + ";" + Environment.NewLine;
            text += "              ClockIndicator=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox80.Checked) + ";" + Environment.NewLine;
            text += "              3rdPersonView=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox79.Checked) + ";" + Environment.NewLine;
            text += "              UltraAI=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox78.Checked) + ";" + Environment.NewLine;
            text += "              CameraShake=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox77.Checked) + ";" + Environment.NewLine;
            text += "              UnlimitedSaves=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox76.Checked) + ";" + Environment.NewLine;
            text += "              DeathMessages=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox75.Checked) + ";" + Environment.NewLine;
            text += "              NetStats=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox74.Checked) + ";" + Environment.NewLine;
            text += "              VonID=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox73.Checked) + ";" + Environment.NewLine;
            text += "              ExtendetInfoType=" + Convert.ToInt32(FSFLauncherCore.fenetrePrincipale.checkBox89.Checked) + ";" + Environment.NewLine;
            text += "             };" + Environment.NewLine;
            text += "   		  skillFriendly=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown19.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionFriendly=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown18.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  skillEnemy=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown17.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionEnemy=" + (FSFLauncherCore.fenetrePrincipale.numericUpDown16.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "       };" + Environment.NewLine;
            text += " };" + Environment.NewLine;
            System.IO.File.WriteAllText(repertoireDeTravail + @"\profile\Users\server\server.Arma3Profile", text);

        }
  

    }
}

