﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Contratos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<GridItem> Items;

        public MainWindow()
        {
            InitializeComponent();
            PopulateGrid();
        }

        public class GridItem
        {
            public int Numero { get; set; }
            public string CuitCuil { get; set; }
            public string RazonSocial { get; set; }
            public int Cantidad { get; set; }
            public string Tipo { get; set; }
            public float Precio { get; set; }
            public string FechaLabra { get; set; }
            public string FechaLimite { get; set; }
            public string Estado { get; set; }

            public GridItem(int Num, string CC, string razSoc, int Cant, string tipo, float Prec, string FLabra, string FLimite, string Fin)
            {
                Numero = Num;
                CuitCuil = CC;
                RazonSocial = razSoc;
                Tipo = tipo;
                Cantidad = Cant;
                Precio = Prec;
                FechaLabra = FLabra;
                FechaLimite = FLimite;
                Estado = Fin;
            }
        }

        public void PopulateGrid()
        {
            ControladorContrato ctrl = new ControladorContrato();
            List<Contrato> contratos = ctrl.VerTodos();
            Items = new ObservableCollection<GridItem>();
            if (contratos.Count != 0)
            {
                var lista = new List<GridItem>();
                contratos.ForEach(x => {
                    string estado;
                    if (x.Cerrado)
                        estado = "Cerrado";
                    else
                        estado = "Abierto";
                    GridItem item = new GridItem(x.Numero, x.Proveedor.CuitCuil, x.Proveedor.RazonSocial, x.Cantidad, x.TipoContrato, x.Precio, x.FechaLabra.ToString("dd MMM yyyy"), x.FechaLimite.ToString("dd MMM yyyy"), estado);
                    lista.Add(item);
                });
                lista = lista.OrderBy( x => x.Numero).ToList();
                lista.ForEach( x => Items.Add(x));
            }
            grillaContratos.ItemsSource = Items;
        }

        private void MenuContrato_Click(object sender, RoutedEventArgs e)
        {
            AltaContrato altaContratoVentana = new AltaContrato
            { Owner = this };
            altaContratoVentana.ShowDialog();
            PopulateGrid();
        }

        private void MenuCondicion_Click(object sender, RoutedEventArgs e)
        {
            AltaCondicion altaCondicionVentana = new AltaCondicion()
            { Owner = this };
            altaCondicionVentana.ShowDialog();
        }

        private void MenuGrano_Click(object sender, RoutedEventArgs e)
        {
            AltaGrano altaGranoVentana = new AltaGrano()
            { Owner = this };
            altaGranoVentana.ShowDialog();
        }

        private void MenuProveedor_Click(object sender, RoutedEventArgs e)
        {
            AltaProveedor altaProveedorVentana = new AltaProveedor()
            { Owner = this };
            altaProveedorVentana.ShowDialog();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBoxBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(textboxBuscar.Text, out int result))
            {
                int cantCarac = textboxBuscar.Text.Length;
                if (Items != null && Items.Count != 0)
                {
                    var subList = Items.Where( x => x.Numero.ToString().Length >= cantCarac).ToList();
                    subList = subList.Where( x => x.Numero.ToString().Substring(0, cantCarac) == textboxBuscar.Text).ToList();
                    grillaContratos.ItemsSource = subList;
                }
            }
            else
            {
                grillaContratos.ItemsSource = Items;
            }
        }

        private void MenuSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void VerContrato(int numero)
        {
            VerEditarContrato verEditarContratoVentana = new VerEditarContrato(numero)
            { Owner = this };
            verEditarContratoVentana.ShowDialog();
            PopulateGrid();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCellInfo cell = grillaContratos.SelectedCells[0];
            VerContrato(Int32.Parse(((TextBlock)cell.Column.GetCellContent(cell.Item)).Text));
        }

        private void MenuVerProveedores_Click(object sender, RoutedEventArgs e)
        {
            VerProveedores verProveedoresVentana = new VerProveedores()
            { Owner = this };
            verProveedoresVentana.ShowDialog();
        }
    }
}