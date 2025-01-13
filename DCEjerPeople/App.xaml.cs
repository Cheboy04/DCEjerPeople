namespace DCEjerPeople
{
    public partial class App : Application
    {
        public static DCPersonRepository PersonRepo { get; private set; }
        public App(DCPersonRepository repo)
        {
            InitializeComponent();

            PersonRepo = repo;
            MainPage = new AppShell();
        }
    }
}
