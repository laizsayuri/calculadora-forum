using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculadora
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public List<History> OriginalResults;
		public ObservableCollection<History> Results;
		public bool AnyResult;

		public HistoryPage(List<History> results)
		{
			InitializeComponent();

			OriginalResults = results;
			Results = new ObservableCollection<History>(results);
			ResultList.ItemsSource = Results;

			if (!results.Any())
				EmptyLabel.IsVisible = true;
		}

		/// <summary>
		/// Função que limpa os log de histórico
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearHistoriesButtonClicked(object sender, EventArgs e)
		{
			OriginalResults.Clear();
			Results.Clear();
			ResultList.ItemsSource = Results;
			EmptyLabel.IsVisible = true;
		}
	}

	/// <summary>
	/// Classe responsável por armazenar os resultados
	/// </summary>
	public class History
	{
		public History(string result)
		{
			Result = result;
		}

		public string Result { get; set; }
	}
}