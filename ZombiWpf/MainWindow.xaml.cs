using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZombiWpf.API;
using ZombiWpf.DTO;

namespace ZombiWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public List<ZombieTypeDTO> ZombieTypes { get; set; }
        public ObservableCollection<ZombieDTO> Zombies { get; set; }
        public ZombieDTO SelectedZombie { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadZombies();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task LoadZombies()
        {
            Client client = new Client();
            await LoadZombies(client);
        }

        private async Task LoadZombies(Client client)
        {
            Zombies = new ObservableCollection<ZombieDTO>(await client.GetZombies());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Zombies)));
        }

        private async void Edit(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            SelectedZombie = b.Tag as ZombieDTO;
            new EditZombie(SelectedZombie).ShowDialog();
            LoadZombies();

        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            SelectedZombie = b.Tag as ZombieDTO;
            await Client.Instance.DeleteZombie(SelectedZombie.Id);
            await LoadZombies();
        }

        private async void Add(object sender, RoutedEventArgs e)
        {
            new AddZombie().ShowDialog();
            await LoadZombies();
        }
    }
}
