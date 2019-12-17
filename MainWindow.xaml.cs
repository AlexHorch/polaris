using System.Windows;

namespace Polaris_Charactergenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            Character character = new Character();
            this.DataContext = character;
        }
    }
}
