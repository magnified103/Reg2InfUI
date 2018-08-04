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
                INF_writingBox.IsReadOnly = false;
            }
            else
            {
                INF_writingBox.IsReadOnly = true;
            }
        }	//check if "enable modification" checkbox was checked

        private void btnSaveINF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog INFsaveDialog = new SaveFileDialog();
            INFsaveDialog.Filter = "Setup Information (*.inf)|*.inf|Text file (*.inf)|*.txt";
            if (INFsaveDialog.ShowDialog() == true)
            {
                File.WriteAllText(INFsaveDialog.FileName, INF_writingBox.Text);
            }
        }	//save inf file

		private void reg_Enter(object sender, RoutedEventArgs e)
		{
			reg_location_input.IsReadOnly = true;
		}	//load registry input section

		private void reg_Modify(object sender, RoutedEventArgs e)
		{
			reg_location_input.IsReadOnly = false;
		}	//modify registry input section

		private void driver_Enter(object sender, RoutedEventArgs e)
		{
			driver_name_input.IsReadOnly = true;
		}	//load driver input section

		private void driver_Modify(object sender, RoutedEventArgs e)
		{
			driver_name_input.IsReadOnly = false;
		}	//modify driver input section

		private void section_Background(Brush color)
		{
			border_reg_location.Background = color;
			reg_location_input.Background = color;
			reg_input_Enter.Background = color;
			reg_input_Modify.Background = color;

			border_driver_name.Background = color;
			driver_name_input.Background = color;
			driver_input_Enter.Background = color;
			driver_input_Modify.Background = color;
		}

		private void section_backgroundDefault()
		{
			border_reg_location.Background = Brushes.White;
			reg_location_input.Background = Brushes.White;
			reg_input_Enter.Background = Brushes.LightGray;
			reg_input_Modify.Background = Brushes.LightGray;

			border_driver_name.Background = Brushes.White;
			driver_name_input.Background = Brushes.White;
			driver_input_Enter.Background = Brushes.LightGray;
			driver_input_Modify.Background = Brushes.LightGray;
		}

		private void cancel_Click(object sender, RoutedEventArgs e)
		{
			Reg2InfUI.INF_writing.Variables.ifCancelled = true;
			section_backgroundDefault();
		}

		private void next_Click(object sender, RoutedEventArgs e)
		{
			section_Background(Brushes.Gray);
			if (reg_location_input.Text == string.Empty || driver_name_input.Text == string.Empty)
			{
				section_backgroundDefault();
				outputBox.Text += "Check your input and try again!\n";
				return;
			}
			Reg2InfUI.INF_writing.Variables.ifCancelled = false;
			INF_writing();
			section_backgroundDefault();
		}

		private void mainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (mainTab.SelectedIndex == 3)
			{
				MessageBoxResult exitResult = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.OKCancel);
				switch (exitResult)
				{
					case MessageBoxResult.OK:
						this.Close();
						break;
					case MessageBoxResult.Cancel:
						mainTab.SelectedIndex = 0;
						return;

				}
			}
		}

		private void INF_writing()
		{
			INF_writingBox.IsReadOnly = true;
			modifyCheck.IsChecked = false;
			var_load();
			progress(0, "Generating text");
			Reg2InfUI.INF_writing.INF_start.generate_text();
			progress(5, "[Version]");
			Reg2InfUI.INF_writing.version_Section.start();
			progress(10, "end test");

			var_unload();	//testing
		}

		private void var_load()
		{
			Reg2InfUI.INF_writing.Variables.INF_content = INF_writingBox.Text;
			Reg2InfUI.INF_writing.Variables.systemHive_location = reg_location_input.Text;
			Reg2InfUI.INF_writing.Variables.driver_name = driver_name_input.Text;
		}

		private void var_unload()
		{
			INF_writingBox.Text = Reg2InfUI.INF_writing.Variables.INF_content;
			
		}

		private void progress(int value, string name)
		{
			progressBar.Value = value;
			sectionName.Text = name;
		}
	}
}
