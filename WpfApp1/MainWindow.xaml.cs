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
using System.Data;

namespace WpfApp1
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

        public Task<DataTable> ImportFromCSVFileAsync(string filePath)
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Distance");
                dt.Columns.Add("Angle");
                dt.Columns.Add("Width");
                dt.Columns.Add("Hegth");
                dt.Columns.Add("IsDefect");

                // splitting the values using Split() command 
                foreach (var srLine in File.ReadAllLines(filePath))
                {
                    dt.Rows.Add(srLine.Split(';'));
                }
                return dt;
            });
        }

        private void selectedRowsButton_Click(object sender, RoutedEventArgs e)
        {
            //Int32 selectedRowCount =
            //    Datagridview1.Row.GetRowCount(DataGridViewElementStates.Selected);
            //if (selectedRowCount > 0)
            //{
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //    for (int i = 0; i < selectedRowCount; i++)
            //    {
            //        sb.Append("Row: ");
            //        sb.Append(Datagridview1.SelectedRows[i].Index.ToString());
            //        sb.Append(Environment.NewLine);
            //    }

            //    sb.Append("Total: " + selectedRowCount.ToString());
            //    MessageBox.Show(sb.ToString(), "Selected Rows");
            //}

            //Datagridview1.Select

        }

        private async void btnImport_CSV_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DataTable dt = await ImportFromCSVFileAsync(@"C:\objects.csv");
                if (dt.Rows.Count > 0)
                {
                    Datagridview1.DataContext = null;    // To clear the previous data before adding the new ones
                    Datagridview1.DataContext = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }


        }

        private void Datagridview1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView? row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                name_txt.Text = row_selected["Name"].ToString();
                distance_txt.Text = row_selected["Distance"].ToString();
                angle_txt.Text = row_selected["Angle"].ToString();
                width_txt.Text = row_selected["Width"].ToString();
                hegth_txt.Text = row_selected["Hegth"].ToString();
                isDefect_txt.Text = row_selected["IsDefect"].ToString();

            }

        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //File.WriteAllLines(FileName, NodeName.ToList());
            //File.WriteAllLines(@"C:\new_objects.csv");
        }
    }
}
