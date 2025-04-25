using App.domain;
using App.sevice;



using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
namespace App
{
    public partial class OficiuLoggedIN : Form
    {
        private PersoanaOficiu _utilizatorLogat;
        private PersoanaOficiuService _persoanaOficiuService;
        private InscriereService _inscriereService;
        private NumeProbaService _numeProbaService;
        private CategorieService _categorieService;
        private ParticipantiService _participantiService;
        private TextBox searchBox;
        private Button searchBtn;
        private TextBox nameTextBox;
        private TextBox cnpTextBox;
        private ListBox probaListBox;
        private TextBox categorieTextBox;
        private Button inscriereBtn;
        private TextBox varstaTextBox;
        private ListBox categorieListBox;


        public OficiuLoggedIN(CategorieService categorieService, InscriereService inscriereService, NumeProbaService numeProbaService, ParticipantiService participantiService, PersoanaOficiuService persoanaOficiuService, PersoanaOficiu persoanaOficiu)
        {
            InitializeComponent();

            _utilizatorLogat = persoanaOficiu;
            _persoanaOficiuService = persoanaOficiuService;
            _inscriereService = inscriereService;
            _numeProbaService = numeProbaService;
            _categorieService = categorieService;
            _participantiService = participantiService;

            // Evenimente
            searchBtn.Click += SearchBtn_Click;
            inscriereBtn.Click += InscriereBtn_Click;
            logOutBtn.Click += logOutBtn_Click;

            // Populare datagrid
            LoadTableData();
            LoadProbaListBox();

        }

        private void InitializeSearchControls()
        {
            searchBox = new TextBox { Location = new Point(12, 330), Width = 200 };
            searchBtn = new Button { Location = new Point(220, 330), Text = "Căutare", Width = 80 };

            searchBtn.Click += SearchBtn_Click;

            this.Controls.Add(searchBox);
            this.Controls.Add(searchBtn);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string probaText = searchBox.Text;

            // Verificăm dacă inputul nu este gol și dacă poate fi convertit într-un ID valid
            if (string.IsNullOrWhiteSpace(probaText))
            {
                MessageBox.Show("Introduceți un ID valid pentru proba.");
                return;
            }

            int proba_id;
            if (int.TryParse(probaText, out proba_id))
            {
                var participants = _participantiService.GetParticipantByProba(proba_id);

                // Verifică dacă sunt participanți pentru proba respectivă
                if (participants != null && participants.Any())
                {
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Nume Participant");
                    dataTable.Columns.Add("Vârsta");

                    foreach (var participant in participants)
                    {
                        dataTable.Rows.Add(participant.Nume, participant.Varsta);
                    }

                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    MessageBox.Show("Nu au fost găsiți participanți pentru proba specificată.");
                }
            }
            else
            {
                MessageBox.Show("ID-ul probei nu este valid.");
            }
        }


        private void logOutBtn_Click(object sender, EventArgs e)
        {
            // Închide aplicația curentă (formularele sau aplicația principală)
            this.Close(); // Aceasta va închide formularul curent
        }
        
        private void InitializeInscriereForm()
        {
            nameTextBox = new TextBox { Location = new Point(12, 360), Width = 200, PlaceholderText = "Numele copilului" };
            cnpTextBox = new TextBox { Location = new Point(12, 390), Width = 200, PlaceholderText = "CNP" };

            probaListBox = new ListBox { Location = new Point(12, 420), Width = 200, Height = 100, SelectionMode = SelectionMode.MultiSimple };

            inscriereBtn = new Button { Location = new Point(220, 430), Text = "Înscriere" };
            inscriereBtn.Click += InscriereBtn_Click;

            this.Controls.Add(nameTextBox);
            this.Controls.Add(cnpTextBox);
            this.Controls.Add(probaListBox);
            this.Controls.Add(inscriereBtn);
        }

        private void InscriereBtn_Click(object sender, EventArgs e)
        {
            var nume = nameTextBox.Text;
            var cnp = cnpTextBox.Text;
            var varstas = varstaTextBox.Text;

            if (string.IsNullOrEmpty(nume) || string.IsNullOrEmpty(cnp) || probaListBox.SelectedItems.Count == 0 || string.IsNullOrEmpty(varstas))
            {
                MessageBox.Show("Completați toate câmpurile!");
                return;
            }

            if (!int.TryParse(varstas, out int varsta))
            {
                MessageBox.Show("Introduceți o vârstă validă!");
                return;
            }

            Console.WriteLine(probaListBox.SelectedItems[0].ToString());

            Participant participant = null;
            participant = _participantiService.GetParticipantByCNP(cnp);
            //MessageBox.Show(participant.ToString());

            //Console.WriteLine(participant.ToString());

            if (participant == null)
            {
                Participant participantNou = new Participant(0, nume, varsta, cnp, _utilizatorLogat.Id);
               // _participantiService.SaveEntity(participantNou);
                bool saved = false;
                int maxRetries = 5;
                int retries = 0;

                while (!saved && retries < maxRetries)
                {
                    try
                    {
                        _participantiService.SaveEntity(participantNou);
                        saved = true;
                    }
                    catch (Exception ex) when (ex.Message.Contains("database is locked"))
                    {
                        retries++;
                        Thread.Sleep(100); // așteaptă 100ms și reîncearcă
                    }
                }

                if (!saved)
                {
                    MessageBox.Show("Eroare: baza de date este blocată. Încearcă din nou.");
                }

                
                

                // Refacem căutarea pentru a obține ID-ul real al participantului salvat
                participant = _participantiService.GetParticipantByCNP(cnp);
                //Console.WriteLine(participant.ToString());
                //MessageBox.Show(participant.ToString());


            }
            

            if (participant == null)
            {
                MessageBox.Show("Eroare la salvarea participantului!");
                return;
            }



            var proba = probaListBox.SelectedItems[0].ToString();
            //var proba_id = Int32.Parse(proba);
            if (!int.TryParse(proba, out int proba_id))
            {
                MessageBox.Show("ID-ul probei trebuie să fie un număr valid!");
                return;
            }

            /*
            var categorie = categorieTextBox.Text;
            //var categorieId = Int32.Parse(categorieTextBox.Text);
            if (!int.TryParse(categorie, out int categorie_id))
            {
                MessageBox.Show("ID-ul categoriei trebuie să fie un număr valid!");
               // return;
            }
            */
            
            if (!int.TryParse(categorieListBox.SelectedItem.ToString(), out int categorie_id))
            {
                MessageBox.Show("ID-ul categoriei trebuie să fie un număr valid!");
                return;
            }

            Inscriere inscriere = new Inscriere(participant.Id, proba_id, categorie_id);
            _inscriereService.SaveEntity(inscriere);
            


            LoadTableData();
            MessageBox.Show("Participantul a fost înscris cu succes!");
        }

        private void LoadTableData()
        {
            var competitions = _inscriereService.GetCompetitionsWithParticipants();

            var dataTable = new DataTable();
            dataTable.Columns.Add("Proba");
            dataTable.Columns.Add("Categorie");
            dataTable.Columns.Add("Numar Inscrisi");

            foreach (var proba in competitions)
            {
                foreach (var categorie in proba.Value)
                {
                    dataTable.Rows.Add(proba.Key, categorie.Key, categorie.Value);
                }
            }

            dataGridView1.DataSource = dataTable;  // Populate the existing DataGridView with competitions data
        }
    }
}

