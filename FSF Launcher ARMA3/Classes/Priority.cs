using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSFLauncherA3
{
    class Priority
    {

        static public void actualisePrioriteMods()
        {
           // recupere tous les Mods coché dans une liste
           // Compare Liste Mods avec Liste Tab prioritaire
           

           // Efface ceux qui ne sont plus selectionné
           // Ajoute ceux qui manque en fin de liste

            // Affiche la liste par priorité dans la listeBox
           FSFLauncherCore.ListModsrealUrl.Clear();
            
            foreach (string ligne in  compareListeModsValidesEtListePrioritaire(ListeModsValide(), ListeModsPrioritaire()))
            {
                FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Add(ligne);
                FSFLauncherCore.ListModsrealUrl.Add(ligne);                
            }
            
        }
        static private List<string> compareListeModsValidesEtListePrioritaire(List<string> listModsValide, List<string> listModsPrioritaire)
        {
            
            // efface de la liste prioritaire les mods non valide
            List<string> Intersection = listModsPrioritaire.Intersect(listModsValide).ToList();
            List<string> Ajout = listModsValide.Except(listModsPrioritaire).ToList();


            // efface le listCheckBox priorité
            FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Clear();
            return Intersection.Union(Ajout).ToList();
        }
        static private List<string> ListeModsPrioritaire()
        {
            List<string> listeModsPrioritaire = new List<string>();
            int compteur = 0;
            foreach (string lignes in FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items)
            {
                listeModsPrioritaire.Add(lignes);
                compteur++;
            }
            return listeModsPrioritaire;
        }
        static private List<string> ListeModsValide()
        {
            List<string> listeModsValide = new List<string>();
            // recupere tous les Mods coché dans une seule liste
            // Template
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox7, @"@FSF\@TEMPLATE\"))
            {
                listeModsValide.Add(ligne);
                if (ligne==@"@FSF\@TEMPLATE\@FSFUnits_Cfg")
                {
                    if (FSFLauncherCore.fenetrePrincipale.comboBox2.Text != "")
                    {
                        listeModsValide.Add(@"@FSF\@TEMPLATE\@FSFSkin_" + FSFLauncherCore.fenetrePrincipale.comboBox2.Text);
                    }
                }
            }
            // Casque Perso
            if (FSFLauncherCore.fenetrePrincipale.radioButton20.Checked) {listeModsValide.Add(@"@FSF\@TEMPLATE\@FSFUnit_HelmetsST");};
            if (FSFLauncherCore.fenetrePrincipale.radioButton21.Checked) { listeModsValide.Add(@"@FSF\@TEMPLATE\@FSFUnit_HelmetsXT"); };
            // FRAMEWORK
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox8, @"@FSF\@FRAMEWORK\"))
            {
                listeModsValide.Add(ligne);
            }
            // Islands
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox1, @"@FSF\@ISLANDS\"))
            {
                listeModsValide.Add(ligne);
            }
            // Units
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox2, @"@FSF\@UNITS\"))
            {
                listeModsValide.Add(ligne);
            }
            // Materiel
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox3, @"@FSF\@MATERIEL\"))
            {
                listeModsValide.Add(ligne);
            }
            // Client
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox6, @"@FSF\@CLIENT\"))
            {
                listeModsValide.Add(ligne);
            }
            // test
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox4, @"@FSF\@TEST\"))
            {
                listeModsValide.Add(ligne);
            }
            // INTERCLAN
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox11, @"@FSF\@INTERCLAN\"))
            {
                listeModsValide.Add(ligne);
            }
            // Autres MODS
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox5, ""))
            {
                listeModsValide.Add(ligne);
            }
            // ARMA 3 DOC
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox9, ""))
            {
                listeModsValide.Add((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3\") + ligne);
            }
            // ARMA 3 DOC OTHER PROFILE
            foreach (string ligne in ExtractionListeModsValides(FSFLauncherCore.fenetrePrincipale.checkedListBox10, ""))
            {
                listeModsValide.Add((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\Arma 3 - Other Profiles\") + ligne);
            }
            return listeModsValide;
        }
        static private List<string> ExtractionListeModsValides(CheckedListBox ListBox,string cheminModsFSF)
        {
            List<string> listeModsValide= new List<string>();
            int compteur=0;
            foreach (string lignes in ListBox.Items)
            {
                if (ListBox.GetItemChecked(compteur)) { listeModsValide.Add(cheminModsFSF + lignes); }
                compteur++;
            }
            return listeModsValide;

        }


        /*
         *  CONTROL BOUTONS Du FORM
         */
        static public void topPrioriteMod()
        {
            if (FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                int index;
                string valeur;
                while (FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex > 0)
                {
                    valeur = FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    index = FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index - 1, valeur);
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index - 1, true);
                }
            }
        }
        static public void downPrioriteMod()
        {
            if (FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                while (FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex < FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Count - 1)
                {
                    string valeur = FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    int index = FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index + 1, valeur);
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index + 1, true);
                }
            }
        }
        static public void augmentePrioriteMod()
        {
            if (FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                string valeur;
                int index;
                if (FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex > 0)
                {
                    valeur = FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    index = FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index - 1, valeur);
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index - 1, true);
                }
            }
        }
        static public void diminuePrioriteMod()
        {
            if (FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                if (FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex < FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Count - 1)
                {
                    string valeur = FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    int index = FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index + 1, valeur);
                    FSFLauncherCore.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index + 1, true);
                }
            }
        }
    }
}
