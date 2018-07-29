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
        }	//check if "enable modification" checkbox was checked

        private void btnSaveINF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog INFsaveDialog = new SaveFileDialog();
            INFsaveDialog.Filter = "Setup Information (*.inf)|*.inf|Text file (*.inf)|*.txt";
            if (INFsaveDialog.ShowDialog() == true)
            {
                File.WriteAllText(INFsaveDialog.FileName, INF_writing.Text);
            }
        }	//save inf file

		private void reg_Enter(object sender, RoutedEventArgs e)
		{
			reg_location_input.IsReadOnly = true;
			second_section_Visiblity(Visibility.Visible);
			first_section_Background(Brushes.Gray);
			second_section_backgroundDefault();
		}	//load registry input section

		private void reg_Modify(object sender, RoutedEventArgs e)
		{
			reg_location_input.IsReadOnly = false;
			second_section_Visiblity(Visibility.Hidden);
			first_section_backgroundDefault();
		}	//modify registry input section

		private void driver_Enter(object sender, RoutedEventArgs e)
		{
			reg_location_input.IsReadOnly = true;
			driver_name_input.IsReadOnly = true;
			first_section_Background(Brushes.Gray);
			second_section_Background(Brushes.Gray);
		}	//load driver input section

		private void driver_Modify(object sender, RoutedEventArgs e)
		{
			reg_location_input.IsReadOnly = true;
			driver_name_input.IsReadOnly = false;
			first_section_Background(Brushes.Gray);
			second_section_backgroundDefault();
		}	//modify driver input section

		private void second_section_Visiblity(Visibility mode)
		{
			border_driver_name.Visibility = mode;
		}	//the visiblity of second section

		private void first_section_Background(Brush color)
		{
			border_reg_location.Background = color;
			reg_location_input.Background = color;
			reg_input_Enter.Background = color;
			reg_input_Modify.Background = color;
		}

		private void first_section_backgroundDefault()
		{
			border_reg_location.Background = Brushes.White;
			reg_location_input.Background = Brushes.White;
			reg_input_Enter.Background = Brushes.LightGray;
			reg_input_Modify.Background = Brushes.LightGray;
		}

		private void second_section_Background(Brush color)
		{
			border_driver_name.Background = color;
			driver_name_input.Background = color;
			driver_input_Enter.Background = color;
			driver_input_Modify.Background = color;
		}

		private void second_section_backgroundDefault()
		{
			border_driver_name.Background = Brushes.White;
			driver_name_input.Background = Brushes.White;
			driver_input_Enter.Background = Brushes.LightGray;
			driver_input_Modify.Background = Brushes.LightGray;
		}

	}
}
