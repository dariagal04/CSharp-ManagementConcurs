using System.Configuration;
using System.Reflection;
using App.domain;
using App.repository;
using App.sevice;
using log4net;
using log4net.Config;

namespace App;

internal class Program
{


    private static readonly ILog log = LogManager.GetLogger(typeof(Program));

    private static void Main()
    {

        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("C:/Users/user/RiderProjects/mpp-proiect-csharp-dariagal04/concurs_csharpWF/log4net.config"));
        log.Info($"Starting up {Assembly.GetEntryAssembly()?.GetName().Version}");
        log.Info("Application started");
        Console.WriteLine("Configuration Settings for triathlonCompetition {0}",GetConnectionStringByName("Concurs"));
        IDictionary<string, string> props = new SortedList<string, string>();
        props.Add("ConnectionString", GetConnectionStringByName("Concurs"));
        
        var participant1 = new Participant(0, "Ion Popescu2", 12, "1234567890124", 2);
        var inscriere1 = new Inscriere(1, 2, 3);
        
        var categorieDbRepo = new CategorieDBRepo(props);
        var inscriereDbRepo = new InscriereDBRepo(props);
        var numeProbaDbRepo = new NumeProbaDBRepo(props);
        var participantiDbRepo = new ParticipantiDBRepo(props);
        var persoanaOficiuDbRepo = new PersoanaOficiuDBRepo(props);
        
        //participantiDbRepo.SaveEntity(participant1);

        /*
        Console.WriteLine("=== Participanți ===");
        var participanti = participantiDbRepo.GetAll();
        if (!participanti.Any())
        {
            Console.WriteLine("Nu există participanți în baza de date.");
        }
        else
        {
            foreach (var participant in participanti)
            {
                Console.WriteLine(participant);
            }
        }*/
        
        
        
        //participantiDbRepo.SaveEntity(participant);
        //inscriereDbRepo.SaveEntity(inscriere);

        Console.WriteLine(participantiDbRepo.GetParticipantByCNP("1231231231231"));
        
        var categorieService=new CategorieService(categorieDbRepo);
        var inscriereService=new InscriereService(inscriereDbRepo);
        var numeProbaService=new NumeProbaService(numeProbaDbRepo);
        var participantiService=new ParticipantiService(participantiDbRepo);
        var persoanaOficiuService=new PersoanaOficiuService(persoanaOficiuDbRepo);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Login(categorieService, inscriereService, numeProbaService,participantiService,persoanaOficiuService));
        
        
        string log4netConfigPath = "C:/Users/user/RiderProjects/mpp-proiect-csharp-dariagal04/concurs_csharpWF/log4net.config";
        if (!File.Exists(log4netConfigPath))
        {
            Console.WriteLine($"[ERROR] log4net.config file not found at: {log4netConfigPath}");
            return;
        }else{Console.WriteLine($"[INFO] log4net.config file found at: {log4netConfigPath}");}
        
        log4netConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");


        var logRepositoryy = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo(log4netConfigPath));

        if (log.IsInfoEnabled)
        {
            log.Info("Log4Net initialized successfully!");
        }
        else
        {
            Console.WriteLine("[ERROR] Log4Net is NOT initialized!");
        }

    }

    private static string GetConnectionStringByName(string name)
    {
        // Assume failure.
        string returnValue = null;

        // Look for the name in the connectionStrings section.
        ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

        // If found, return the connection string.
        if (settings != null)
            returnValue = settings.ConnectionString;

        return returnValue;
    }
}

