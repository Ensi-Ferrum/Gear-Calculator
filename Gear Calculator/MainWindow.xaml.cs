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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Gear_Calculator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String CarsCfg = "cars.cfg";
        String finalDriveCfg = @"\FinalDrive.cfg";
        String gearRatiosCfg = @"\GearRatios.cfg";

        List<string> lCars = new List<string>();
        List<string> lFinalDrive = new List<string>();
        List<string> lGearRatio = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
            FillList(lCars, CarsCfg);
            FillComboBox(cbCars, lCars);
        }

        private static void FillList (List<string> list, string data)
        {
            try
            {
                StreamReader sr = new StreamReader(data);
                string line = sr.ReadLine();

                while (line != null)
                {
                    list.Add(line);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void FillComboBox(ComboBox comboBox, List<string> list)
        {
            try
            {
                foreach (var item in list)
                {
                    comboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void cbCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String car = @".\" + cbCars.SelectedItem.ToString();

            FillList(lFinalDrive, car + finalDriveCfg);
            FillComboBox(cbFinalDrive, lFinalDrive);

            FillList(lGearRatio, car + gearRatiosCfg);
            FillComboBox(cbFirstGear, lGearRatio);
            FillComboBox(cbSecondGear, lGearRatio);
            FillComboBox(cbThirdGear, lGearRatio);
            FillComboBox(cbFourthGear, lGearRatio);
            FillComboBox(cbFifthGear, lGearRatio);
            FillComboBox(cbSixthGear, lGearRatio);
            FillComboBox(cbSeventhGear, lGearRatio);
            FillComboBox(cbEighthGear, lGearRatio);

        }
    }
}
