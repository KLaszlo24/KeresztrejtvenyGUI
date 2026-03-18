using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeresztrejtvenyGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		TextBox[,] mezok;

		public MainWindow()
		{
			InitializeComponent();

			for (int i = 6; i <= 15; i++)
			{
				cbSor.Items.Add(i);
				cbOszlop.Items.Add(i);
			}

			cbSor.SelectedItem = 15;
			cbOszlop.SelectedItem = 15;

			for (int i = 1; i <= 10; i++)
			{
				cbIndex.Items.Add(i);
			}

			cbIndex.SelectedItem = 3;
		}
		private void Letrehozas_Click(object sender, RoutedEventArgs e)
		{
			int sor = (int)cbSor.SelectedItem;
			int oszlop = (int)cbOszlop.SelectedItem;

			gridRacs.Children.Clear();
			gridRacs.RowDefinitions.Clear();
			gridRacs.ColumnDefinitions.Clear();

			mezok = new TextBox[sor, oszlop];

			for (int i = 0; i < sor; i++)
				gridRacs.RowDefinitions.Add(new RowDefinition());//chatgpt segitett

			for (int j = 0; j < oszlop; j++)
				gridRacs.ColumnDefinitions.Add(new ColumnDefinition());//chatgpt segitett

			for (int i = 0; i < sor; i++)
			{
				for (int j = 0; j < oszlop; j++)
				{
					TextBox tb = new TextBox 
					{
						Text = "-",
						Width = 30,
						Height = 30,
						TextAlignment = TextAlignment.Center,
						VerticalContentAlignment = VerticalAlignment.Center
					};
					tb.MaxLength = 1;

					tb.MouseDoubleClick += Mezo_DoubleClick;

					Grid.SetRow(tb, i);
					Grid.SetColumn(tb, j); //itt is kerestem kicsit interneten

					gridRacs.Children.Add(tb);
					mezok[i, j] = tb;
				}
			}
		}
		private void Mezo_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			TextBox tb = sender as TextBox;

			if (tb.Text == "-")
				tb.Text = "#";
			else
				tb.Text = "-";
		}
		private void Mentes_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int index = (int)cbIndex.SelectedItem;
				string fajlNev = $"kr{index}.txt";

				using (StreamWriter sw = new StreamWriter(fajlNev))//StremWriter használatát megnéztem, nem voltam benne biztos
				{
					int sor = mezok.GetLength(0);
					int oszlop = mezok.GetLength(1);

					for (int i = 0; i < sor; i++)
					{
						string sorSzoveg = "";

						for (int j = 0; j < oszlop; j++)
						{
							sorSzoveg += mezok[i, j].Text;
						}

						sw.WriteLine(sorSzoveg);
					}
				}

				MessageBox.Show("Mentés kész");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}