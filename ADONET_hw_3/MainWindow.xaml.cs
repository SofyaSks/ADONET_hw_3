using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ADONET_hw_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\.;Initial Catalog=music2;Integrated Security=True");
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter = null;
        public MainWindow()
        {
            InitializeComponent();

            connection.Open();

            cb_table.Items.Add("songs");
            cb_table.Items.Add("artists");
            cb_table.Items.Add("song_artists");
        }

        private void cb_table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            adapter = new SqlDataAdapter($"select * from {cb_table.SelectedValue}", connection);
            new SqlCommandBuilder(adapter);

            dataSet.Clear();
            dataSet.Tables.Clear();
            adapter.Fill(dataSet);

            DataContext = dataSet.Tables[0].DefaultView;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            adapter.Update(dataSet);
            dataSet.Clear();
            adapter.Fill(dataSet);

        }
    }
}
