using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZombiWpf.API;
using ZombiWpf.DTO;

namespace ZombiWpf
{
    /// <summary>
    /// Логика взаимодействия для AddZombie.xaml
    /// </summary>
    public partial class AddZombie : Window, INotifyPropertyChanged
    {
        public int Health { get; set; }
        public string ZTitle { get; set; }
        public string Description { get; set; }
        public List<ZombieTypeDTO> ZombieTypes { get; set; }
        public ZombieTypeDTO SelectedZombieType { get; set; }
        public AddZombie()
        {
            InitializeComponent();
            LoadZombieTypes();
            DataContext = this;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private async void LoadZombieTypes()
        {
            var client = new Client();
            ZombieTypes = await client.GetZTypes();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ZombieTypes)));
        }

        private async void Save(object sender, RoutedEventArgs e)
        {
            await Client.Instance.AddZombieAsync(new ZombieDTO
            {
                Name = ZTitle,
                Description = Description,
                Health = Health,
                ZombieTypeId = SelectedZombieType.Id,
                 ZombieType = SelectedZombieType.Name
            });
            Close();
        }
    }
}
