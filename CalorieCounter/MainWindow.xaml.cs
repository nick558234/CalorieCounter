﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalorieCounter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Bereken_Click(object sender, RoutedEventArgs e)
        {
            if (GeslachtMan.IsChecked == true && ZwangerCheckBox.IsChecked == true)
            {
                MessageBox.Show("In de war", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int leeftijd;
            if (!int.TryParse(LeeftijdTextBox.Text, out leeftijd))
            {
                MessageBox.Show("Leeftijd is geen geldig getal.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (GeslachtVrouw.IsChecked == true && ZwangerCheckBox.IsChecked == true && leeftijd <= 18)
            {
                MessageBox.Show("Teen mom");
            }

            double calorieën = BerekenBasisCalorieën();
            calorieën = PasLevensstijlAan(calorieën);
            calorieën = PasLeeftijdAan(calorieën, leeftijd);
            calorieën = PasZwangerschapAan(calorieën, leeftijd);

            ResultTextBlock.Text = $"Dagelijkse calorieën: {calorieën}";
        }

        private double BerekenBasisCalorieën()
        {
            if (GeslachtMan.IsChecked == true || NonBinair.IsChecked == true)
            {
                return 2500;
            }
            else if (GeslachtVrouw.IsChecked == true)
            {
                return 2000;
            }
            return 0;
        }

        private double PasLevensstijlAan(double calorieën)
        {
            if (LevensstijlNietActief.IsChecked == true)
            {
                return calorieën * 0.9;
            }
            if (LevensstijlAfvallen.IsChecked == true)
            {
                return calorieën * 0.8;
            }
            if (LevensstijlBulken.IsChecked == true)
            {
                MessageBox.Show("Vergeet de eiwitten niet");
                return calorieën * 1.2;
            }
            return calorieën;
        }

        private double PasLeeftijdAan(double calorieën, int leeftijd)
        {
            if (leeftijd > 50)
            {
                calorieën -= 200;
            }
            else if (leeftijd < 12)
            {
                calorieën -= 180;
            }
            return calorieën;
        }

        private double PasZwangerschapAan(double calorieën, int leeftijd)
        {
            if (GeslachtVrouw.IsChecked == true && ZwangerCheckBox.IsChecked == true)
            {
                if (leeftijd <= 30)
                {
                    calorieën = 2600;
                }
                else
                {
                    calorieën = 2500;
                }

                // Verlaag calorieën met 180 als de leeftijd jonger is dan 12
                if (leeftijd < 12)
                {
                    calorieën -= 180;
                }
            }
            return calorieën;
        }
    }
}
