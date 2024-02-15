using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для EditZombie.xaml
    /// </summary>
    public partial class EditZombie : Window, INotifyPropertyChanged
    {
        private ZombieDTO selectedZombie;
        private ZombieTypeDTO selectedZombieType;

        public List<ZombieTypeDTO> ZombieTypes { get; set; }
        public ZombieTypeDTO SelectedZombieType { 
            get => selectedZombieType;
            set
            {
                selectedZombieType = value;
                Signal();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public ZombieDTO SelectedZombie 
        { 
            get => selectedZombie;
            set 
            { 
                selectedZombie = value; 

            } 
        }
        public EditZombie(ZombieDTO zombie)
        {
            InitializeComponent();
            
            SelectedZombie = zombie;
            LoadZTypes();
            DataContext = this;
        }

        private async Task LoadZTypes()
        {
            var client = new Client();
            ZombieTypes = await client.GetZTypes();
            SelectedZombieType = ZombieTypes.FirstOrDefault(s => s.Id == SelectedZombie.ZombieTypeId);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ZombieTypes)));
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (SelectedZombie == null) {
                MessageBox.Show("!!");
                return;
            }
            SelectedZombie.ZombieTypeId = SelectedZombieType.Id;
            SelectedZombie.ZombieType = SelectedZombieType.Name;
            Client.Instance.EditZombie(SelectedZombie, SelectedZombie.Id) ;
            Close();
        }
    }
}
