using System;
using System.Collections.Generic;
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

namespace Petopia
{
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Window
    {
        private PetopiaLinkerDataContext _petDB;

        public Logs()
        {
            InitializeComponent();
            _petDB = new PetopiaLinkerDataContext(Properties.Settings.Default.Petopia_UpdatedConnectionString);
        }
    }
}


 