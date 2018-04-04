using System;
using System.Data;
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

        int nSpeedAtRedline = 0;
        int nWheelRPMAtRedline = 0;
        int nShiftRPM = 0;
        int nRedlineRPM = 0;
        int nMaxPowerRPM = 0;
        int nPowerbandStartRPM = 0;
        int nMinRaceSpeed = 0;
        int nMaxRaceSpeed = 0;

        List<string> lCars = new List<string>();
        List<string> lFinalDrive = new List<string>();
        List<string> lGearRatio = new List<string>();

        List<decimal> lCarGearRatios = new List<decimal>(9) { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        List<decimal> lWheelRatio = new List<decimal>(8) { 0, 0, 0, 0, 0, 0, 0, 0 };

        List<int> lShiftSpeed = new List<int>(8) { 0, 0, 0, 0, 0, 0, 0, 0 };
        List<int> lRedlineSpeed = new List<int>(8) { 0, 0, 0, 0, 0, 0, 0, 0 };
        List<int> lSpeedRange = new List<int>(8) { 0, 0, 0, 0, 0, 0, 0, 0 };
        List<int> lMinRPM = new List<int>(8) { 0, 0, 0, 0, 0, 0, 0, 0 };


        DataTable table;

        public MainWindow()
        {
            InitializeComponent();

            dgData.ItemsSource = LoadCollectionData();

            FillList(lCars, CarsCfg);
            FillComboBox(cbCars, lCars);
        }

        private List<WheelRatio> LoadCollectionData()
        {
            List<WheelRatio> wheelRatio = new List<WheelRatio>();

            wheelRatio.Add(new WheelRatio() { LWheelRatio = lWheelRatio });

            return wheelRatio;
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

        private void CalculateWheelRatio (List<decimal> ListSource, List<decimal> ListDest )
        {
            for (int n = 0; n < ListDest.Count; n++)
            {
                ListDest.RemoveAt(n);
                ListDest.Insert(n, (ListSource[0] * ListSource[n + 1]));
            }
            dgData.ItemsSource = LoadCollectionData();

        }

        private void tbSpeedAtRedline_TextChanged(object sender, TextChangedEventArgs e)
        {
            nSpeedAtRedline = Convert.ToInt32(tbSpeedAtRedline.Text);
        }

        private void tbShiftRPM_TextChanged(object sender, TextChangedEventArgs e)
        {
            nShiftRPM = Convert.ToInt32(tbShiftRPM.Text);
        }

        private void tbRedlineRPM_TextChanged(object sender, TextChangedEventArgs e)
        {
            nRedlineRPM = Convert.ToInt32(tbRedlineRPM.Text);
        }

        private void tbMaxPowerRPM_TextChanged(object sender, TextChangedEventArgs e)
        {
            nMaxPowerRPM = Convert.ToInt32(tbMaxPowerRPM.Text);
        }

        private void tbPowerbandStartRPM_TextChanged(object sender, TextChangedEventArgs e)
        {
            nPowerbandStartRPM = Convert.ToInt32(tbPowerbandStartRPM.Text);
        }

        private void tbMinRaceSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            nMinRaceSpeed = Convert.ToInt32(tbMinRaceSpeed.Text);
        }

        private void tbMaxRaceSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            nMaxPowerRPM = Convert.ToInt32(tbMaxRaceSpeed.Text);
        }

        private void cbFinalDrive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(0);
            lCarGearRatios.Insert(0, Convert.ToDecimal(cbFinalDrive.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }

        private void cbFirstGear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(1);
            lCarGearRatios.Insert(1, Convert.ToDecimal(cbFirstGear.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }

        private void cbSecondGear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(2);
            lCarGearRatios.Insert(2, Convert.ToDecimal(cbSecondGear.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }

        private void cbThirdGear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(3);
            lCarGearRatios.Insert(3, Convert.ToDecimal(cbThirdGear.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }

        private void cbFourthGear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(4);
            lCarGearRatios.Insert(4, Convert.ToDecimal(cbFourthGear.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }

        private void cbFifthGear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(5);
            lCarGearRatios.Insert(5, Convert.ToDecimal(cbFifthGear.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }

        private void cbSixthGear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(6);
            lCarGearRatios.Insert(6, Convert.ToDecimal(cbSixthGear.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }

        private void cbSeventhGear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(7);
            lCarGearRatios.Insert(7, Convert.ToDecimal(cbSeventhGear.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }

        private void cbEighthGear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lCarGearRatios.RemoveAt(8);
            lCarGearRatios.Insert(8, Convert.ToDecimal(cbEighthGear.SelectedValue));

            CalculateWheelRatio(lCarGearRatios, lWheelRatio);
        }
    }
}
