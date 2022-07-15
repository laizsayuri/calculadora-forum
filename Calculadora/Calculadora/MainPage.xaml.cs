using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Calculadora
{
	/// <summary>
	/// Classe principal da calculadora
	/// </summary>
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			results = new List<History>();
		}

		private decimal firstNumber;
		private decimal secondNumber;
		private string operatorName;

		private bool isOperatorClicked;
		private bool isOperationFinished;

		private List<History> results;

		/// <summary>
		/// Função para abrir modal de histórico
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void HistoryButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new HistoryPage(results), true);
		}

		/// <summary>
		/// Método para capturar resultados
		/// </summary>
		private void CaptureResult()
		{
			var result = new History($"{secondLabel.Text} = {firstLabel.Text}");
			results.Insert(0, result);
		}

		/// <summary>
		/// Função que processa botões numéricos
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCommon_Clicked(object sender, EventArgs e)
		{
			var button = sender as Button;

			if ((firstLabel.Text == "0" && button.Text != ",") || isOperatorClicked || isOperationFinished)
			{
				isOperatorClicked = false;
				firstLabel.Text = button.Text;

				if (isOperationFinished)
				{
					isOperationFinished = false;
					secondLabel.Text = "0";
				}
			}
			else if (button.Text != "," || !firstLabel.Text.Contains(","))
				firstLabel.Text += button.Text;
		}


		/// <summary>
		/// Função que processa a tecla Clear
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClear_Clicked(object sender, EventArgs e)
		{
			firstLabel.Text = "0";
			secondLabel.Text = "0";

			isOperatorClicked = false;
			isOperationFinished = false;

			firstNumber = 0;
		}

		/// <summary>
		/// Função que apaga números do visor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnDelete_Clicked(object sender, EventArgs e)
		{
			string number = firstLabel.Text;
			if (number != "0")
			{
				number = number.Remove(number.Length - 1, 1);

				if (string.IsNullOrEmpty(number))
					firstLabel.Text = "0";
				else
					firstLabel.Text = number;
			}
		}

		/// <summary>
		/// Função que define o tipo de operação
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCommonOperation_Clicked(object sender, EventArgs e)
		{
			var button = sender as Button;

			isOperatorClicked = true;
			isOperationFinished = false;
			operatorName = button.Text;

			firstNumber = Convert.ToDecimal(firstLabel.Text);
			secondLabel.Text = $"{firstNumber} {operatorName}";
		}

		/// <summary>
		/// Função que transforma o número em porcentagem
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtnPercentage_Clicked(object sender, EventArgs e)
		{
			try
			{
				string number = firstLabel.Text;
				if (number != "0")
				{
					decimal percentValue = Convert.ToDecimal(number);
					string result = (percentValue / 100).ToString("0.##");
					firstLabel.Text = result;
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "Ok");
			}
		}

		/// <summary>
		/// Função disparada após selecionar igual
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnEqual_Clicked(object sender, EventArgs e)
		{
			try
			{
				if (isOperationFinished)
					firstNumber = Convert.ToDecimal(firstLabel.Text);
				else
					secondNumber = Convert.ToDecimal(firstLabel.Text);

				string finalResult = Calculate(firstNumber, secondNumber).ToString("0.##");

				isOperationFinished = true;

				firstLabel.Text = finalResult;
				secondLabel.Text = $"{firstNumber} {operatorName} {secondNumber}";
				CaptureResult();
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", ex.Message, "Ok");
			}
		}

		/// <summary>
		/// Função que executa o cálculo
		/// </summary>
		/// <param name="firstNumber"></param>
		/// <param name="secondNumber"></param>
		/// <returns></returns>
		private decimal Calculate(decimal firstNumber, decimal secondNumber)
		{
			decimal result = 0;

			switch (operatorName)
			{
				case "+":
					result = firstNumber + secondNumber;
					break;
				case "-":
					result = firstNumber - secondNumber;
					break;
				case "×":
					result = firstNumber * secondNumber;
					break;
				case "÷":
					result = firstNumber / secondNumber;
					break;
			};

			return result;
		}

	}
}
