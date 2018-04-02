﻿using System.Windows;

namespace Contratos
{
    /// <summary>
    /// Lógica de interacción para AltaGrano.xaml
    /// </summary>
    public partial class AltaGrano : Window
    {
        public AltaGrano()
        {
            InitializeComponent();
        }

        private void GuardarGrano_Click(object sender, RoutedEventArgs e)
        {
            ControladorGrano ctrl = new ControladorGrano();
            if (tipoGrano.Text != "" && ctrl.IngresarGrano(tipoGrano.Text))
                MessageBox.Show("Operación exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else
                MessageBox.Show("Error en la operación. Compruebe los datos o contacte con el administrador.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CancelarGrano_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GuardarSalirGrano_Click(object sender, RoutedEventArgs e)
        {
            ControladorGrano ctrl = new ControladorGrano();
            if (tipoGrano.Text != "" && ctrl.IngresarGrano(tipoGrano.Text))
            {
                MessageBox.Show("Operación exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Close();
            }
            else
                MessageBox.Show("Error en la operación. Compruebe los datos o contacte con el administrador.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}