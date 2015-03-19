using Infralution.Localization;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FSFLauncherA3
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

       public override string ToString()
        {
            return Text;
        }      
    }

    class Interface
    {
        static public void dessineInterface()
        {


            // Affiche version 

            FSFLauncherCore.fenetrePrincipale.label31.Text = AfficheVersionProgramme();
            
            // Determine Serveur


            ProgExterne.ValideProgExt();

            // isFSF
            if (!FSFLauncherCore.isFSFValid())
            {
                FSFLauncherCore.fenetrePrincipale.tabControl2.TabPages.Remove(FSFLauncherCore.fenetrePrincipale.ModsFSF);
                FSFLauncherCore.fenetrePrincipale.tabControl2.TabPages.Remove(FSFLauncherCore.fenetrePrincipale.SynchroZONE);
                FSFLauncherCore.fenetrePrincipale.pictureBox6.Visible = false;
                FSFLauncherCore.fenetrePrincipale.pictureBox36.Visible = false;
                FSFLauncherCore.fenetrePrincipale.button39.Visible = false;
                FSFLauncherCore.fenetrePrincipale.button38.Visible = false;
                FSFLauncherCore.fenetrePrincipale.button37.Visible = false;
                FSFLauncherCore.fenetrePrincipale.button35.Visible = false;
                FSFLauncherCore.fenetrePrincipale.button36.Visible = false; 

                // TS3 version 3.0.14
                FSFLauncherCore.fenetrePrincipale.button18.Visible = false;
                FSFLauncherCore.fenetrePrincipale.button19.Visible = false;
                FSFLauncherCore.fenetrePrincipale.pictureBox26.Visible = false;
                FSFLauncherCore.fenetrePrincipale.button17.Visible = true;
                FSFLauncherCore.fenetrePrincipale.groupBox3.Visible = false;
            }
            else
            {
                AlerteVersionArma3();
                testToutesTaillesSynchroEnLigne();
            }
            AfficheSynchroActive();
            AfficheServeurActif();

        }
        static public void genereTab()
        {
            effaceTousItemsOnglets();

            // @FSF 
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox7, "@TEMPLATE", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox8, "@FRAMEWORK", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox1, "@ISLANDS", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox2, "@UNITS", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox3, "@MATERIEL", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox6, "@CLIENT", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox4, "@TEST", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox11, "@INTERCLAN", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            // @Autre
            // Root
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox5, "AUTRES_MODS", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            //Arma3 profile            
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox9, "DOC_ARMA3", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            //Arma3 other profile
            FSFLauncherCore.ListeTab(FSFLauncherCore.fenetrePrincipale.checkedListBox10, "DOC_OTHERPROFILE", (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            //genereTabMods();
            genereTabParam();
            genereTabPriorite();
        }
        static public void effaceTousItemsOnglets()
        {
            FSFLauncherCore.fenetrePrincipale.comboBox2.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.comboBox2.Items.Add("");
            FSFLauncherCore.fenetrePrincipale.radioButton20.Enabled = false;
            FSFLauncherCore.fenetrePrincipale.radioButton20.Checked = false;
            FSFLauncherCore.fenetrePrincipale.radioButton21.Enabled = false;
            FSFLauncherCore.fenetrePrincipale.radioButton21.Checked = false;
            FSFLauncherCore.fenetrePrincipale.pictureBox1.Image = FSFLauncherA3.Properties.Resources.logofsf;
            FSFLauncherCore.fenetrePrincipale.checkedListBox8.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox7.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox1.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox2.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox3.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox6.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox4.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox5.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox9.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.checkedListBox10.Items.Clear();
        }
        static public void effaceTousparamsOnglet()
        {
            FSFLauncherCore.fenetrePrincipale.checkBox1.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox2.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox3.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox4.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox5.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox6.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox7.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox8.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox9.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox11.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox12.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox13.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox19.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox22.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox23.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox21.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox10.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox24.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox_HeadlessClient.Checked = false;
            FSFLauncherCore.fenetrePrincipale.checkBox_EnableHT.Checked = false;
            FSFLauncherCore.fenetrePrincipale.textBox2.Text = "";
            FSFLauncherCore.fenetrePrincipale.textBox3.Text = "";
            FSFLauncherCore.fenetrePrincipale.textBox4.Text = "";
            FSFLauncherCore.fenetrePrincipale.textBox7.Text = "";
            FSFLauncherCore.fenetrePrincipale.textBox8.Text = "";

        }
        static public void genereTabParam()
        {
            effaceTousparamsOnglet();
            ProgExterne.ValideProgExt();
            XmlTextReader fichierProfilXML = new XmlTextReader(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\" + (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + ".profil.xml");
            while (fichierProfilXML.Read())
            {

                fichierProfilXML.ReadToFollowing("winXP");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox9.Checked = true; }
                fichierProfilXML.ReadToFollowing("showScriptErrors");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox5.Checked = true; }
                fichierProfilXML.ReadToFollowing("worldEmpty");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox4.Checked = true; }
                fichierProfilXML.ReadToFollowing("noPause");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox2.Checked = true; }
                fichierProfilXML.ReadToFollowing("nosplash");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox1.Checked = true; }
                fichierProfilXML.ReadToFollowing("window");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox3.Checked = true; }
                fichierProfilXML.ReadToFollowing("maxMem");
                string maxmem = fichierProfilXML.ReadString();
                if (maxmem != "") { FSFLauncherCore.fenetrePrincipale.checkBox6.Checked = true; FSFLauncherCore.fenetrePrincipale.trackBar1.Value = int.Parse(maxmem); FSFLauncherCore.fenetrePrincipale.textBox5.Text = maxmem; }
                fichierProfilXML.ReadToFollowing("cpuCount");
                string cpucount = fichierProfilXML.ReadString();
                if (cpucount != "") { FSFLauncherCore.fenetrePrincipale.checkBox7.Checked = true; FSFLauncherCore.fenetrePrincipale.trackBar2.Value = int.Parse(cpucount); FSFLauncherCore.fenetrePrincipale.textBox6.Text = cpucount; }
                fichierProfilXML.ReadToFollowing("fraps");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox11.Checked = true; }
                fichierProfilXML.ReadToFollowing("trackir");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox12.Checked = true; }
                fichierProfilXML.ReadToFollowing("noCB");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox8.Checked = true; }
                fichierProfilXML.ReadToFollowing("minimize");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox19.Checked = true; }
                fichierProfilXML.ReadToFollowing("noFilePatching");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox23.Checked = true; }
                fichierProfilXML.ReadToFollowing("VideomaxMem");
                string Videomaxmem = fichierProfilXML.ReadString();
                if (Videomaxmem != "") { FSFLauncherCore.fenetrePrincipale.checkBox22.Checked = true; FSFLauncherCore.fenetrePrincipale.trackBar3.Value = int.Parse(Videomaxmem); FSFLauncherCore.fenetrePrincipale.textBox20.Text = Videomaxmem; }
                fichierProfilXML.ReadToFollowing("threadMax");
                string threadMax = fichierProfilXML.ReadString();
                if (threadMax != "") { FSFLauncherCore.fenetrePrincipale.checkBox21.Checked = true; FSFLauncherCore.fenetrePrincipale.comboBox3.SelectedIndex = int.Parse(threadMax); }
                fichierProfilXML.ReadToFollowing("adminmode");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox24.Checked = true; }
                fichierProfilXML.ReadToFollowing("nologs");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox10.Checked = true; }
                fichierProfilXML.ReadToFollowing("HC");
                if (fichierProfilXML.ReadString() == "true")
                {
                    FSFLauncherCore.fenetrePrincipale.checkBox_HeadlessClient.Checked = true;
                    fichierProfilXML.ReadToFollowing("HCPort");
                    FSFLauncherCore.fenetrePrincipale.textBox2.Text = fichierProfilXML.ReadString();
                    fichierProfilXML.ReadToFollowing("HCPassWord");
                    FSFLauncherCore.fenetrePrincipale.textBox3.Text = fichierProfilXML.ReadString();
                }
                fichierProfilXML.ReadToFollowing("other");
                string otherCMD = fichierProfilXML.ReadString();
                if (otherCMD != "") { FSFLauncherCore.fenetrePrincipale.checkBox13.Checked = true; FSFLauncherCore.fenetrePrincipale.textBox4.Text = otherCMD; }
                if (FSFLauncherCore.fenetrePrincipale.checkBox3.Checked)
                {
                    fichierProfilXML.ReadToFollowing("windowX");
                    string windowX = fichierProfilXML.ReadString();
                    if (windowX != "") { FSFLauncherCore.fenetrePrincipale.textBox7.Text = windowX; }
                    fichierProfilXML.ReadToFollowing("windowY");
                    string windowY = fichierProfilXML.ReadString();
                    if (windowY != "") { FSFLauncherCore.fenetrePrincipale.textBox8.Text = windowY; }
                }
                fichierProfilXML.ReadToFollowing("enableHT");
                if (fichierProfilXML.ReadString() == "true") { FSFLauncherCore.fenetrePrincipale.checkBox_EnableHT.Checked = true; }

            }
            fichierProfilXML.Close();
        }
        static public void genereTabPriorite()
        {
            FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Clear();
            if (System.IO.File.Exists(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\" + (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + ".profilPriorite.xml"))
            {
                XmlTextReader fichierProfilPrioriteXML = new XmlTextReader(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\" + (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + ".profilPriorite.xml");
                while (fichierProfilPrioriteXML.Read())
                {
                    fichierProfilPrioriteXML.ReadToFollowing("MODS");
                    string ligne = fichierProfilPrioriteXML.ReadString();
                    if (ligne != "")
                    {
                        FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Add(ligne);
                    }
                }
                fichierProfilPrioriteXML.Close();
            }
            else
            {
                Priority.actualisePrioriteMods();
                FSFLauncherCore.sauvegardeConfigProfilXML("");
            };

        }
        static private string AfficheVersionProgramme()
        {
            string versionProg = "";
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                versionProg = "v. " + version.Major + "." + version.Minor + "." + version.Build + " (Rev. " + version.Revision + ")";
            }
            return versionProg;
        }
        static private string VersionArma3()
        {
            try
            {
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(FSFLauncherCore.cheminARMA3 + @"/arma3.exe");
                //ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return version.ProductVersion;
            }
            catch
            {
                return "";
            }
        }
        static public void testToutesTaillesSynchroEnLigne()
            
        {
            FSFLauncherCore.synchroRsyncTaille("", FSFLauncherCore.fenetrePrincipale.button16, null, null, FSFLauncherCore.fenetrePrincipale.label8, null);
            FSFLauncherCore.synchroRsyncTaille("@TEMPLATE", FSFLauncherCore.fenetrePrincipale.button25, null, null, FSFLauncherCore.fenetrePrincipale.label45, null);
            FSFLauncherCore.synchroRsyncTaille("@ISLANDS", FSFLauncherCore.fenetrePrincipale.button26, null, null, FSFLauncherCore.fenetrePrincipale.label46, null);
            FSFLauncherCore.synchroRsyncTaille("@MATERIEL", FSFLauncherCore.fenetrePrincipale.button41, null, null, FSFLauncherCore.fenetrePrincipale.label47, null);
            FSFLauncherCore.synchroRsyncTaille("@UNITS", FSFLauncherCore.fenetrePrincipale.button42, null, null, FSFLauncherCore.fenetrePrincipale.label48, null);
            FSFLauncherCore.synchroRsyncTaille("@CLIENT", FSFLauncherCore.fenetrePrincipale.button43, null, null, FSFLauncherCore.fenetrePrincipale.label49, null);
            FSFLauncherCore.synchroRsyncTaille("@TEST", FSFLauncherCore.fenetrePrincipale.button_TESTBoutonSynchro, null, null, FSFLauncherCore.fenetrePrincipale.label_TESTTailleSynchro, null);
            FSFLauncherCore.synchroRsyncTaille("@FRAMEWORK", FSFLauncherCore.fenetrePrincipale.button45, null, null, FSFLauncherCore.fenetrePrincipale.label51, null);
            FSFLauncherCore.synchroRsyncTaille("@INTERCLAN", FSFLauncherCore.fenetrePrincipale.button_INTERCLANBoutonSynchro, null, null, FSFLauncherCore.fenetrePrincipale.label_INTERCLANTailleSynchro, null);
        }
        static public void AlerteVersionArma3()
        {
            try
            {
                FSFLauncherA3.FenetrePrincipale.DownloadConfigServeur("versionserveurdistant.xml", "ftp://37.59.36.179", FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\");
                XmlTextReader fichierInfoServer = new XmlTextReader(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\versionserveurdistant.xml");
                fichierInfoServer.ReadToFollowing("VERSION");
                string VersionServeur = fichierInfoServer.ReadString();
                fichierInfoServer.Close();
                if (VersionServeur == VersionArma3())
                {
                    FSFLauncherCore.fenetrePrincipale.label7.Text = VersionArma3();
                    FSFLauncherCore.fenetrePrincipale.toolTip1.SetToolTip(FSFLauncherCore.fenetrePrincipale.pictureBox24, "Version (FSF server) : " + VersionServeur + Environment.NewLine);
                    FSFLauncherCore.fenetrePrincipale.pictureBox24.Image = FSFLauncherA3.Properties.Resources.valide;
                }
                else 
                {
                    FSFLauncherCore.fenetrePrincipale.label7.Text = VersionArma3();
                    FSFLauncherCore.fenetrePrincipale.toolTip1.SetToolTip(FSFLauncherCore.fenetrePrincipale.pictureBox24, "Version (FSF server) : " + VersionServeur );
                    FSFLauncherCore.fenetrePrincipale.pictureBox24.Image = FSFLauncherA3.Properties.Resources.delete;
                    FSFLauncherCore.fenetrePrincipale.label7.ForeColor = System.Drawing.Color.Red;
                }               
            }
            catch
            {
                
            }

        }
        static public void AfficheSynchroActive()
        {
             switch (FSFLauncherCore.GetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "synchro"))
            {
                case "beta" :
                    FSFLauncherCore.fenetrePrincipale.checkBox_SyncBETA.Checked = true;
                    break;
               case "officielle" :
                    FSFLauncherCore.fenetrePrincipale.checkBox_SyncBETA.Checked = false;
                    break;
            }
             switch (FSFLauncherCore.GetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "synchro_INTERCLAN"))
             {
                 case "true":
                     FSFLauncherCore.fenetrePrincipale.checkBox_SyncINTERCLAN.Checked = true;
                     break;
                 case "false":
                     FSFLauncherCore.fenetrePrincipale.checkBox_SyncINTERCLAN.Checked = false;
                     break;
             }
        }
        static public void AfficheServeurActif()
        {
            switch (FSFLauncherCore.GetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "serveur"))
            {
                case "SERVEUR1":
                    FSFLauncherCore.fenetrePrincipale.radioButton7.Checked = true;
                    break;
                case "SERVEUR2":
                    FSFLauncherCore.fenetrePrincipale.radioButton8.Checked = true;
                    break;
                default:
                    FSFLauncherCore.fenetrePrincipale.radioButton8.Checked = true ;
                    break;
            }
        }

        

        /*
         *    LANGAGE
         */

        #region LANGAGE
        static public void ChangeLangage(string langue)
        {
            int i = FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedIndex;
            CultureManager.ApplicationUICulture = new CultureInfo(langue);
            SauvegardeLangage(langue);
            dessineInterface();
            Interface.initialiseListeProfil();
            FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedIndex = i;
        }
        static private void SauvegardeLangage(string langue)
        {
            FSFLauncherCore.SetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "langage", langue);
        }
        #endregion

        /*
         *    Gestion des profils
         */
        #region GESTION PROFIL
        static public void AjouteComboNomProfil(int index, string nomProfil)
        {
            ComboboxItem item = new ComboboxItem();
            string textAffiche = nomProfil;
            if (nomProfil == "defaut")
            {
                string langue = FSFLauncherCore.GetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "langage");
                switch (langue)
                {
                    case "en-US":
                        textAffiche = "default";
                        break;
                    case "ru-RU":
                        textAffiche = "умолчание";
                        break;
                    case "de-DE":
                        textAffiche = "Vorgabe";
                        break;
                    case "el-GR":
                        textAffiche = "Προεπιλογή";
                        break;
                    case "es-ES":
                        textAffiche = "Por defecto";
                        break;
                    default:
                        textAffiche = "defaut";
                        break;
                }

            }

            item.Text = textAffiche;
            item.Value = nomProfil;
            FSFLauncherCore.fenetrePrincipale.comboBox4.Items.Add(item);

        }
        static public void AjouteListeBoxNomProfil(int index, string nomProfil)
        {
            ComboboxItem item = new ComboboxItem();
            string textAffiche = nomProfil;
            if (nomProfil == "defaut")
            {
                string langue = FSFLauncherCore.GetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "langage");
                switch (langue)
                {
                    case "en-US":
                        textAffiche = "default";
                        break;
                    case "ru-RU":
                        textAffiche = "умолчание";
                        break;
                    case "de-DE":
                        textAffiche = "Vorgabe";
                        break;
                    case "el-GR":
                        textAffiche = "Προεπιλογή";
                        break;
                    case "es-ES":
                        textAffiche = "Por defecto";
                        break;

                    default:
                        textAffiche = "defaut";
                        break;
                }

            }

            item.Text = textAffiche;
            item.Value = nomProfil;
            FSFLauncherCore.fenetrePrincipale.listBox1.Items.Add(item);
        }
        public static void initialiseListeProfil()
        {
            string[] listeProfil = Directory.GetFiles(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\", "*.profil.xml", SearchOption.TopDirectoryOnly);
            string textMenuProfil = FSFLauncherCore.fenetrePrincipale.comboBox4.Text;
            if (textMenuProfil == "") { textMenuProfil = "defaut"; }
            FSFLauncherCore.fenetrePrincipale.listBox1.Items.Clear();
            FSFLauncherCore.fenetrePrincipale.comboBox4.Items.Clear();
            int compteur = 0;
            foreach (var ligne in listeProfil)
            {
                string textCombo = ligne.Replace(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\", "");                            
                Interface.AjouteListeBoxNomProfil(compteur, textCombo.Replace(".profil.xml", ""));
                Interface.AjouteComboNomProfil(compteur, textCombo.Replace(".profil.xml", ""));
                compteur++;
            }
        }
        #endregion


        /*
         *   UNLOCK SYSTEM
         */

        #region UNLOCK
        static public void UnlockFSFLauncher()
        {
            Form dialogue = new Dial_Unlock();
            dialogue.ShowDialog();
            Application.Restart();
        }
        #endregion

        /*
         *   CHANGELOG
         */

        #region CHANGELOG
        static public void AfficheChangelog()
        {
            Form dialogue = new Dial_ChangeLog();
            dialogue.ShowDialog();
        }
        #endregion
    }
}