using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Net;
using Microsoft.Win32;
using System.Deployment.Application;
using WinSCP;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Collections;
using Infralution.Localization;


namespace FSFLauncherA3
{
    public partial class FenetrePrincipale : Form
    {
        Form splashscreen = new SplashScreen();
        string[] argumentFSFLauncher;       

        public FenetrePrincipale(string [] args)
        {

            argumentFSFLauncher = args;
            InitializeComponent();
        }

        private void FenetrePrincipale_Load(object sender, EventArgs e)
        {        
            Control.CheckForIllegalCrossThreadCalls = false;

           
            // Gestion WINSCP

            if (File.Exists("WinSCP.exe")) //Si le fichier existe 
            {
                button26.Visible = false;
                progressBar1.Visible = false;
                pictureBox17.Visible = false;
                button25.Enabled = true;
                button16.Enabled = true;
            }
            else
            {
                button26.Enabled = true;
                button26.Visible = true;
                pictureBox17.Visible = true;
                button25.Enabled = false;
                button16.Enabled = false;
            }

            // PREPARATION INITIALISATION INTERFACE
            FSFLauncherCore.fenetrePrincipale = this;
   

            FSFLauncherCore.DefinitionConstante();

            
            /*
                   Config repertoire FSF Launcher
            */

            initialiseFichierConfig();
            Interface.initialiseListeProfil();
            initialiseProfilActif();
            configureInstallationMODS();      
            /* 
                Organisation INTERFACE
            */          
            Interface.dessineInterface();
            splashscreen.ShowDialog();
            if (argumentFSFLauncher.Length > 0) ResidentAdmin.initialiseTrayIcon();
        }


        /***********************************
         
                Procedures PERSO
          
         ***********************************/



        void initialiseFichierConfig()
        {


            if (!System.IO.File.Exists(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\config.ini"))
            {
                //le fichier n'existe pas
                FileStream fs = File.Create(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\config.ini");
                fs.Close();
            }
            if (!System.IO.File.Exists(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\defaut.profil.xml"))
            {
                //le fichier n'existe pas 
                FSFLauncherCore.sauvegardeConfigProfilXML("defaut");

                comboBox4.Text = "defaut";
            }

            string langue = FSFLauncherCore.GetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "langage");
            if (langue == "fr-FR") { radioButton1.Checked = true; };
            if (langue == "en-US") { radioButton2.Checked = true; };
            if (langue == "ru-RU") { radioButton3.Checked = true; };
            if (langue == "de-DE") { radioButton6.Checked = true; };
            if (langue == "el-GR") { radioButton5.Checked = true; };
            if (langue == "es-ES") { radioButton4.Checked = true; };
        }
        
        void initialiseProfilActif()
        {
            comboBox4.SelectedIndex = 0;
            int indexNom = 0;          

            foreach (ComboboxItem nomProfil in comboBox4.Items)
            {
                if (nomProfil.Value.ToString() + ".profil.xml" == System.IO.File.ReadAllText(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\config.ini"))
                {
                    comboBox4.SelectedIndex = indexNom;
                }
                indexNom++;
            }
        }

        void genereTabModsImportServeur()
        {
            // efface les onglets

            checkedListBox7.Items.Clear();
            checkedListBox8.Items.Clear();
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            checkedListBox3.Items.Clear();
            checkedListBox4.Items.Clear();
            pictureBox1.Image = FSFLauncherA3.Properties.Resources.logofsf;
            //comboBox2.Items.Clear();

            // Recupere et genere les tabs pour chaque repertoire

            //ouvre the XmlDocument
            XmlDocument fichierProfilXML = new XmlDocument();
            fichierProfilXML.Load(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\ImportConfigServeurA3.xml");
            string[] listeRepertoire = Directory.GetDirectories(FSFLauncherCore.cheminARMA3, "Addons*", SearchOption.AllDirectories);

            // Genere les Tab Specifiques pour les tenues FSF
            foreach (var ligne in listeRepertoire)
            {               
                if ((ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@TEMPLATE\@FSFUnit_HelmetsST;") != -1)) { radioButton20.Enabled = true; }
                if ((ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@TEMPLATE\@FSFUnit_HelmetsXT;") != -1)) { radioButton21.Enabled = true; }
            }

            // TEMPLATE

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@TEMPLATE") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("TEMPLATE");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                        string repertoireAChercher = eltList[j].InnerXml;                      
                        if  (repertoireAChercher.IndexOf(@"@FSF\@TEMPLATE\@FSFSkin_") != -1)
                        {
                            int indexApparence = 0;
                            foreach (string apparencePossible in comboBox2.Items)
                            {
                                if (@"@FSF\@TEMPLATE\@FSFSkin_" + apparencePossible == repertoireAChercher)
                                {
                                    comboBox2.SelectedIndex = indexApparence;
                                }
                                indexApparence++;
                            }
                        }
                        if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == FSFLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\")
                            {
                                if (repertoireAChercher == @"@FSF\@TEMPLATE\@FSFUnit_HelmetsST") { radioButton20.Checked = true; }
                                if (repertoireAChercher == @"@FSF\@TEMPLATE\@FSFUnit_HelmetsXT") { radioButton21.Checked = true; }
                                elementsProfilChecked = true;
                            }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(FSFLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            if (menuRepertoire.IndexOf("@FSFSkin_") != -1 ||
                                menuRepertoire.IndexOf("@FSFUnit_Helmets") != -1)
                            {
                                //
                            }
                            else
                            {
                                checkedListBox7.Items.Add(menuRepertoire.Replace(FSFLauncherCore.cheminARMA3 + @"\@FSF\@TEMPLATE\", ""), elementsProfilChecked);
                            }

                        }
                    }
                }
            }

            // FRAMEWORK

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@FRAMEWORK") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("FRAMEWORK");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = FSFLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(FSFLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox8.Items.Add(menuRepertoire.Replace(FSFLauncherCore.cheminARMA3 + @"\@FSF\@FRAMEWORK\", ""), elementsProfilChecked);
                        }
                    }
                }
            }


            // ISLANDS

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@ISLANDS") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("ISLANDS");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = FSFLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(FSFLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox1.Items.Add(menuRepertoire.Replace(FSFLauncherCore.cheminARMA3 + @"\@FSF\@ISLANDS\", ""), elementsProfilChecked);
                        }
                    }
                }
            }

            // CLIENT

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@CLIENT") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("CLIENT");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = FSFLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(FSFLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox6.Items.Add(menuRepertoire.Replace(FSFLauncherCore.cheminARMA3 + @"\@FSF\@CLIENT\", ""), elementsProfilChecked);
                        }
                    }
                }
            }

            // UNITS


            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@UNITS") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("UNITS");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = FSFLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(FSFLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox2.Items.Add(menuRepertoire.Replace(FSFLauncherCore.cheminARMA3 + @"\@FSF\@UNITS\", ""), elementsProfilChecked);
                        }
                    }
                }
            }

            // MATERIEL

            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@MATERIEL") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("MATERIEL");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = FSFLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(FSFLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox3.Items.Add(menuRepertoire.Replace(FSFLauncherCore.cheminARMA3 + @"\@FSF\@MATERIEL\", ""), elementsProfilChecked);
                        }
                    }
                }
            }

            // TEST
            foreach (var ligne in listeRepertoire)
            {
                if (ligne.IndexOf(FSFLauncherCore.cheminARMA3 + @"\@FSF\@TEST") != -1)
                {
                    bool elementsProfilChecked = false;
                    // Read the XmlDocument (Directory Node)
                    XmlNodeList elemList = fichierProfilXML.GetElementsByTagName("TEST");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNodeList eltList = elemList[i].ChildNodes;
                        for (int j = 0; j < eltList.Count; j++)
                        {
                            string repertoireAChercher = FSFLauncherCore.cheminARMA3 + @"\" + eltList[j].InnerXml + @"\";
                            if ((System.IO.Directory.GetParent(ligne).ToString() + @"\") == repertoireAChercher) { elementsProfilChecked = true; }
                        }
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        if (menuRepertoire.Replace(FSFLauncherCore.cheminARMA3, "").IndexOf("@") != -1)
                        {
                            checkedListBox4.Items.Add(menuRepertoire.Replace(FSFLauncherCore.cheminARMA3 + @"\@FSF\@TEST\", ""), elementsProfilChecked);
                        }
                    }
                }
            }
        }
        static bool validationNomFichier(string nomFichier)
        {
            char[] InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char InvalidFileNameChar in InvalidFileNameChars)
                if (nomFichier.Contains(InvalidFileNameChar.ToString()))
                    return false;
            return true;
        }
        static string ConversionNomFichierValide(string FileName, char RemplaceChar)
        {
            char[] InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char InvalidFileNameChar in InvalidFileNameChars)
                if (FileName.Contains(InvalidFileNameChar.ToString()))
                    FileName = FileName.Replace(InvalidFileNameChar, RemplaceChar);
            return FileName;
        }



        void DownloadConfigServeur(string nomFichier, string repertoireFTP, string repertoireLocal)
        {
            // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
            WebClient request = new WebClient();
            request.Credentials = new NetworkCredential(FSFLauncherCore.constLoginFTP, FSFLauncherCore.constMdpFTP);
            byte[] fileData = request.DownloadData(repertoireFTP + "/" + nomFichier);
            FileStream file = File.Create(repertoireLocal + "\\" + nomFichier);
            file.Write(fileData, 0, fileData.Length);
            file.Close();
        }
        void UploadConfigServeur(string nomFichier, string repertoireFTP)
        {
            // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(repertoireFTP);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(FSFLauncherCore.constLoginFTP, FSFLauncherCore.constMdpFTP);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            FileStream stream = File.OpenRead(nomFichier);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }
        void sauvegardeInfoServeur(string typeServeur)
        {
            if (typeServeur == "LOCAL")
            {
                XmlTextWriter FichierProfilXML = new XmlTextWriter(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\ImportConfigServeurA3.xml", System.Text.Encoding.UTF8);
                FichierProfilXML.Formatting = Formatting.Indented;
                FichierProfilXML.WriteStartDocument();
                FichierProfilXML.WriteComment("Creation Du profil FSF LAUNCHER " + typeServeur); // commentaire
                FichierProfilXML.WriteStartElement("PROFIL");
                FichierProfilXML.WriteStartElement("MODS_FSF");

                //FRAMEWORK
                FichierProfilXML.WriteStartElement("FRAMEWORK");
                if (checkedListBox8.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox8.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@FSF\@FRAMEWORK\" + checkedListBox8.CheckedItems[x].ToString());
                    }
                }
                FichierProfilXML.WriteEndElement();

                //ISLANDS
                FichierProfilXML.WriteStartElement("ISLANDS");
                if (checkedListBox1.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox1.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@FSF\@ISLANDS\" + checkedListBox1.CheckedItems[x].ToString());
                    }
                }
                FichierProfilXML.WriteEndElement();

                //UNITS
                FichierProfilXML.WriteStartElement("UNITS");
                if (checkedListBox2.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox2.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@FSF\@UNITS\" + checkedListBox2.CheckedItems[x].ToString());

                    }
                }
                FichierProfilXML.WriteEndElement();

                //MATERIEL
                FichierProfilXML.WriteStartElement("MATERIEL");
                if (checkedListBox3.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox3.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@FSF\@MATERIEL\" + checkedListBox3.CheckedItems[x].ToString());
                    }
                }
                FichierProfilXML.WriteEndElement();

                //TEST
                FichierProfilXML.WriteStartElement("TEST");
                if (checkedListBox4.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox4.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEST\" + checkedListBox4.CheckedItems[x].ToString());
                    }
                }
                FichierProfilXML.WriteEndElement();


                //TEMPLATE
                FichierProfilXML.WriteStartElement("TEMPLATE");
                if (checkedListBox7.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox7.CheckedItems.Count - 1; x++)
                    {
                        FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEMPLATE\" + checkedListBox7.CheckedItems[x].ToString());
                    }
                // ecrire skin
                if (comboBox2.Text != "") { FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEMPLATE\@FSFSkin_"+comboBox2.Text);
                // ecrire casque
                if (radioButton20.Checked == true) { FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEMPLATE\@FSFUnit_HelmetsST"); }
                if (radioButton21.Checked == true) { FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEMPLATE\@FSFUnit_HelmetsXT"); }
                }
                FichierProfilXML.WriteEndElement();
                FichierProfilXML.WriteEndElement();
                FichierProfilXML.WriteEndElement();

                FichierProfilXML.Flush(); //vide le buffer
                FichierProfilXML.Close(); // ferme le document
                UploadConfigServeur(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\ImportConfigServeurA3.xml", @"ftp://ftp1.clan-fsf.fr/system/config/ImportConfigServeurA3.xml");
            }
          }
        }

 
        string testTailleFTP(string nomFichier, string ftpDistant, string login, string motDePasse)
        {
            string resultat = "";
            try
            {
                // test taille fichier FTP

                var req = (FtpWebRequest)WebRequest.Create("ftp://" + ftpDistant + "/" + nomFichier);
                req.Proxy = null;
                req.Credentials = new NetworkCredential(login, motDePasse);

                req.Method = WebRequestMethods.Ftp.GetFileSize;
                using (WebResponse resp = req.GetResponse())
                    resultat = resp.ContentLength.ToString();
            }
            catch
            {
            }
            return resultat;
        }
        void CopyDir(string sourceDir, string destDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            if (dir.Exists)
            {
                string realDestDir;
                if (dir.Root.Name != dir.Name)
                {
                    realDestDir = Path.Combine(destDir, dir.Name);
                    if (!Directory.Exists(realDestDir))
                        Directory.CreateDirectory(realDestDir);
                }
                else realDestDir = destDir;
                foreach (string d in Directory.GetDirectories(sourceDir))
                    CopyDir(d, realDestDir);
                foreach (string file in Directory.GetFiles(sourceDir))
                {

                    string fileNameDest = Path.Combine(realDestDir, Path.GetFileName(file));
                    //if (!File.Exists(fileNameDest))

                    File.Copy(file, fileNameDest, true);
                }
            }
        }



        bool downloadnouvelleVersion(string nomFichier, string repertoireFTP, string username, string password, string destinationRepertoire)
        {
            // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
            try
            {

                WebClient request = new WebClient();
                request.Credentials = new NetworkCredential(username, password);
                byte[] fileData = request.DownloadData("ftp://" + repertoireFTP + "/" + nomFichier);
                FileStream file = File.Create(destinationRepertoire + nomFichier);
                file.Write(fileData, 0, fileData.Length);
                file.Close();
                return true;
            }
            catch
            {
                //MessageBox.Show("Impossible de réaliser la mise à jour automatique du programme. Nouvel essai...\n\n"+e,"Erreur Critique",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
        }
        void configureInstallationMODS()
        {
            string filename;
          
            // Test Acre
            filename = FSFLauncherCore.cheminARMA3 + @"\userconfig\acre\acre_clientside_config.hpp";
            if (!File.Exists(filename)) //Si le fichier n existe pas
            {
                try
                {
                    //Déplacement du dossier 
                    CopyDir(FSFLauncherCore.cheminARMA3 + @"\@FSF\@TEMPLATE\@FSF_TOOLS\ATTENTION (copier le contenu de ce repertoire a la racine du jeu)\userconfig\acre", FSFLauncherCore.cheminARMA3 + @"\userconfig");
                }
                catch
                {
                }
            }

        }
        string testTailleLocal(string cheminFichier)
        {
            FileInfo f = new FileInfo(cheminFichier);
            return f.Length.ToString();
        }
        void telechargeFichier(string cheminFichier, string nomFichier)
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            // Starts the download
            client.DownloadFileAsync(new Uri(cheminFichier + nomFichier), FSFLauncherCore.repertoireCourant + @"/" + nomFichier);
            button26.Text = "Téléchargement...";
            button26.Enabled = false;
        }
        void client_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            button26.Visible = false;
            progressBar1.Visible = false;
            pictureBox17.Visible = false;
            button25.Enabled = true;
            button16.Enabled = true;
        }

        /*
         * 
         * 
         *   LISTE ACTION CONTROL 
         * 
         * 
         * 
         */


        private void button29_Click(object sender, EventArgs e)
        {
            Priority.augmentePrioriteMod();
        }
        private void button30_Click(object sender, EventArgs e)
        {
            Priority.diminuePrioriteMod();           
        }
        private void button33_Click(object sender, EventArgs e)
        {
            Priority.actualisePrioriteMods();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.lancerJeu("");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox1);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox2);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox3);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox4);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox6);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox2);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox3);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox4);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox6);
        }
        private void button12_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox7);
        }
        private void button13_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox7);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox5);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox5);
        }


        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
            label4.Visible = true;
            FSFLauncherCore.sauvegardeProfil();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Visible = false;
            label5.Visible = false;
            timer1.Stop();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null || (FSFLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString() == "" || (FSFLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString() == "defaut" || (FSFLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Text.ToString() == (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Text.ToString())
            {
                var infoBox = MessageBox.Show("Impossible d'effacer ce profil si il est celui par defaut ou celui actif.", "Erreur Suppression profil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    File.Delete(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\" + listBox1.SelectedItem.ToString() + ".profil.xml");
                    File.Delete(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\" + listBox1.SelectedItem.ToString() + ".profilServeur.xml");
                }
                catch
                {
                }
                string profilactif = (FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Text.ToString();
                Interface.initialiseListeProfil();
                int compteur = 0;
                foreach (ComboboxItem profil in FSFLauncherCore.fenetrePrincipale.comboBox4.Items)
                {
                    if (profil.Text.ToString() == profilactif) { FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedIndex = compteur; };
                    compteur++;
                }
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string nomProfil = ConversionNomFichierValide(textBox1.Text, '_');
            nomProfil = nomProfil.TrimStart();
            string[] listeProfil = Directory.GetFiles(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\", "*.profil.xml", SearchOption.TopDirectoryOnly);
            Boolean nomProfilValid = true;
            foreach (var ligne in listeProfil)
            {
                string textCombo = ligne.Replace(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\", "");
                string nomProfilATester = textCombo.Replace(".profil.xml", "");
                if (nomProfil == nomProfilATester) { nomProfilValid = false; }

            }

            if (nomProfil == "") { nomProfilValid = false; }
            if (nomProfilValid)
            {
                Interface.effaceTousItemsOnglets();
                Interface.effaceTousparamsOnglet();
                Directory.CreateDirectory(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3");
                FSFLauncherCore.sauvegardeConfigProfilXML(nomProfil);
                Interface.initialiseListeProfil();
                int compteur = 0;
                foreach (ComboboxItem profil in FSFLauncherCore.fenetrePrincipale.comboBox4.Items)
                {
                    if (profil.Text.ToString() == nomProfil) { FSFLauncherCore.fenetrePrincipale.comboBox4.SelectedIndex = compteur; };
                    compteur++;
                }
            }
            else
            {
                var infoBox = MessageBox.Show("Votre nom de profil n'est pas valide.", "Erreur création profil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Interface.genereTab();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            timer1.Start();
            label5.Visible = true;
            if (listBox1.SelectedItem != null)
            {
                string profilChoisis = (FSFLauncherCore.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString();
                string text = profilChoisis + ".profil.xml";
                System.IO.File.WriteAllText(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\config.ini", text);

            }

        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) { trackBar1.Enabled = true; textBox5.Enabled = true; textBox5.Text = trackBar1.Value.ToString(); } else { trackBar1.Enabled = false; textBox5.Enabled = false; }

        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) { trackBar2.Enabled = true; textBox6.Enabled = true; textBox6.Text = trackBar2.Value.ToString(); } else { trackBar2.Enabled = false; textBox6.Enabled = false; }

        }

        private void button18_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Remove(MODs);
        }
        private void button19_Click(object sender, EventArgs e)
        {
            tabControl2.TabPages.Add(MODs);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox5.Text = trackBar1.Value.ToString();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox6.Text = trackBar2.Value.ToString();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.clan-fsf.fr");
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.arma2.com/beta-patch.php");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.clan-fsf.fr");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Form dialogue = new Dial_LanceSynchroBeta();
            dialogue.ShowDialog();
            if (FSFLauncherCore.dialogueReponse)
            {
                FSFLauncherCore.SetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "Synchro", "beta");
                FSFLauncherCore.synchro("beta");
                Interface.initialiseListeProfil();
                initialiseProfilActif();
                configureInstallationMODS();
            }
            else
            {
                textBox11.Text = "Break...";
            }

        }
        private void button26_Click(object sender, EventArgs e)
        {
            telechargeFichier("http://www.clan-gign.net/FSFLauncherA3/", "WinSCP.exe");
        }


        private void Priorité_Enter(object sender, EventArgs e)
        {
            Priority.actualisePrioriteMods();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Priority.topPrioriteMod();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Priority.downPrioriteMod();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = FSFLauncherA3.Properties.Resources.logofsf;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\ImportConfigServeurA3.xml"))
            {
                //le fichier n'existe pas
                FileStream fs = File.Create(FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\ImportConfigServeurA3.xml");
                fs.Close();
            }
            DownloadConfigServeur("ImportConfigServeurA3.xml", "ftp://ftp1.clan-fsf.fr/system/config", FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\");
            genereTabModsImportServeur();
            MessageBox.Show("Liste des MODS importée.", "Importation Liste MODs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            sauvegardeInfoServeur("LOCAL");
            MessageBox.Show("Liste des MODS exportée", "Exportation Liste MODs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button18_Click_4(object sender, EventArgs e)
        {
            ProgExterne.lancerTeamspeak3();
        }

        private void radioButton1_CheckedChanged_2(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Interface.ChangeLangage("fr-FR");
            };
        }

        private void radioButton2_CheckedChanged_2(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                Interface.ChangeLangage("en-US");
            };
        }
        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                Interface.ChangeLangage("ru-RU");
            };
        }


           private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void progExterne_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            Interface.UnlockFSFLauncher();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ChristopheTdn/FSF-LAUNCHER-ARMA3/issues");
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                Interface.ChangeLangage("de-DE");
            };
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox20.Text = trackBar3.Value.ToString();
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked) { trackBar3.Enabled = true; textBox20.Enabled = true; textBox20.Text = trackBar3.Value.ToString(); } else { trackBar3.Enabled = false; textBox20.Enabled = false; }

        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                comboBox3.Enabled = true;
                if (comboBox3.SelectedIndex == -1)
                   { comboBox3.SelectedIndex = 0; };
            }
            else
            {
                comboBox3.Enabled = false;
                comboBox3.SelectedIndex = -1;
                pictureBox26.Image = FSFLauncherA3.Properties.Resources.delete;
                label34.Enabled = false;
                pictureBox28.Image = FSFLauncherA3.Properties.Resources.delete;
                label35.Enabled = false;
                pictureBox30.Image = FSFLauncherA3.Properties.Resources.delete;
                label36.Enabled = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.Text)
            {
                case "0":
                    pictureBox26.Image = FSFLauncherA3.Properties.Resources.delete;
                    label34.Enabled = false;
                    pictureBox28.Image = FSFLauncherA3.Properties.Resources.delete;
                    label35.Enabled = false;
                    pictureBox30.Image = FSFLauncherA3.Properties.Resources.delete;
                    label36.Enabled = false;
                    break;

                case "1":
                    pictureBox26.Image = FSFLauncherA3.Properties.Resources.valide;
                    label34.Enabled = true;
                    pictureBox28.Image = FSFLauncherA3.Properties.Resources.delete;
                    label35.Enabled = false;
                    pictureBox30.Image = FSFLauncherA3.Properties.Resources.delete;
                    label36.Enabled = false;
                    break;
                case "3":
                    pictureBox26.Image = FSFLauncherA3.Properties.Resources.valide;
                    label34.Enabled = true;
                    pictureBox28.Image = FSFLauncherA3.Properties.Resources.valide;
                    label35.Enabled = true;
                    pictureBox30.Image = FSFLauncherA3.Properties.Resources.delete;
                    label36.Enabled = false;
                    break;
                case "5":
                    pictureBox26.Image = FSFLauncherA3.Properties.Resources.valide;
                    label34.Enabled = true;
                    pictureBox28.Image = FSFLauncherA3.Properties.Resources.delete;
                    label35.Enabled = false;
                    pictureBox30.Image = FSFLauncherA3.Properties.Resources.valide;
                    label36.Enabled = true;
                    break;
                case "7":
                    pictureBox26.Image = FSFLauncherA3.Properties.Resources.valide;
                    label34.Enabled = true;
                    pictureBox28.Image = FSFLauncherA3.Properties.Resources.valide;
                    label35.Enabled = true;
                    pictureBox30.Image = FSFLauncherA3.Properties.Resources.valide;
                    label36.Enabled = true;
                    break;

            }
        }

        private void radioButton5_CheckedChanged_1(object sender, EventArgs e)
        {
            
             if (radioButton5.Checked)
            {
                Interface.ChangeLangage("el-GR");
            };

        }

        private void groupBox12_Enter(object sender, EventArgs e)
        {

        }

        private void button19_Click_1(object sender, EventArgs e)
        {
        }



        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        private void button37_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog CheminA3Server = new FolderBrowserDialog();
            CheminA3Server.SelectedPath = FSFLauncherCore.cheminARMA3;
            CheminA3Server.ShowDialog();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ChristopheTdn/FSF-LAUNCHER-ARMA3");
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                Interface.ChangeLangage("es-ES");
            };
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            splashscreen.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form dialogue = new Dial_LanceSynchroOfficielle();
            dialogue.ShowDialog();
            if (FSFLauncherCore.dialogueReponse)
            {
                FSFLauncherCore.SetKeyValue(@"Software\Clan FSF\FSF Launcher A3\", "Synchro", "officielle");
                FSFLauncherCore.synchro("officielle");
                Interface.initialiseListeProfil();
                initialiseProfilActif();
                configureInstallationMODS();
            }
            else
            {
                textBox11.Text = "Break...";
            }
        }

        private void button19_Click_2(object sender, EventArgs e)
        {
            ProgExterne.ReinstallTS3();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

            FSFLauncherCore.SelectionneTousTAB(checkedListBox8);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox8);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Interface.genereTab();
        }

        private void checkBox_HeadlessClient_CheckedChanged(object sender, EventArgs e)
        {
            FSFLauncherCore.fenetrePrincipale.textBox2.Enabled = FSFLauncherCore.fenetrePrincipale.checkBox_HeadlessClient.Checked;
            FSFLauncherCore.fenetrePrincipale.textBox3.Enabled = FSFLauncherCore.fenetrePrincipale.checkBox_HeadlessClient.Checked;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox9);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox9);
        }

        private void button33_Click_1(object sender, EventArgs e)
        {
            FSFLauncherCore.SelectionneTousTAB(checkedListBox10);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.InverseTousTAB(checkedListBox10);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Interface.genereTab();
        }



        private void button23_Click(object sender, EventArgs e)
        {
            Interface.genereTab();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            ProgExterne.lancerTeamspeak3TaskForce();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            ProgExterne.ReinstallTS3TaskForce();
        }

        private void button37_Click_1(object sender, EventArgs e)
        {
            FSFLauncherCore.lancerJeu("server1");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.lancerJeu("mapping");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Enabled = checkBox3.Checked;
            textBox8.Enabled = checkBox3.Checked;
        }

    }
}
    


    

