using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Reg2InfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void modifyCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (modifyCheck.IsChecked == true)
            {
                INF_writing.IsReadOnly = false;
            }
            else
            {
                INF_writing.IsReadOnly = true;
            }
        }

        private void btnSaveINF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog INFsaveDialog = new SaveFileDialog();
            INFsaveDialog.Filter = "Setup Information (*.inf)|*.inf|Text file (*.inf)|*.txt";
            if (INFsaveDialog.ShowDialog() == true)
            {
                File.WriteAllText(INFsaveDialog.FileName, INF_writing.Text);
            }
        }
    }
}
