using App.sevice;

namespace App;

public partial class Login : Form
{
    private readonly CategorieService _categorieService;
    private readonly InscriereService _inscriereService;
    private readonly NumeProbaService _numeProbaService;
    private readonly ParticipantiService _participantiService;
    private readonly PersoanaOficiuService _persoanaOficiuService;
    public Login(CategorieService categorieService, InscriereService inscriereService,NumeProbaService numeProbaService,ParticipantiService participantiService,PersoanaOficiuService persoanaOficiuService)
    {
        _categorieService = categorieService;
        _inscriereService = inscriereService;
        _numeProbaService = numeProbaService;
        _participantiService = participantiService;
        _persoanaOficiuService = persoanaOficiuService;
        InitializeComponent();
    }
    
    private void BtnLogin_Click(object sender, EventArgs e)
    {
        var username=usernameTxt.Text;
        var password=passwordTxt.Text;
        usernameTxt.Clear();
        passwordTxt.Clear();

        try
        {
            var persoanaOficiu=_persoanaOficiuService.authenticate(username, password);
            var refereeForm = new OficiuLoggedIN(_categorieService, _inscriereService, _numeProbaService, _participantiService, _persoanaOficiuService, persoanaOficiu);
            refereeForm.Show();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
    
}