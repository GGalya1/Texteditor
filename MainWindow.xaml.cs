using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Texteditor
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source); //hier geben wir Combobox die Einträge
            cmbFontsize.ItemsSource = new List<double>() { 8,9,10,11,12,14,16,18,20,22,24,26,28,36,48,72}; //geben die default Größen ein
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*rtf|Alle Dateien (*.*)|*.*"; //wir filtrieren nach Format
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd); //wie viel aus Datei gelesen werden soll
                range.Load(fs, DataFormats.Rtf);
            }
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty); //"Weight" entspricht "bold"
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(FontWeights.Bold); //brauchen das, damit ein Programm erkennt, ob in Selection etw bold usw ist

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty); 
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(FontStyles.Italic); //"Style" entspricht "italic"

            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty); //"Style" entspricht "bold"
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && temp.Equals(TextDecorations.Underline);


            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty); //Schrift aendern
            cmbFontFamily.SelectedItem = temp; //setzen ausgewaehlte Wert in Zeile
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*rtf|Alle Dateien (*.*)|*.*"; //wir filtrieren nach Format
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Create); //erstellen ein Datei
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd); //wie viel aus Datei gelesen werden soll
                range.Save(fs, DataFormats.Rtf);
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e) //die Methode für Schriften
        {
            if (cmbFontFamily.SelectedItem != null) //wenn etwas ausgewählt ist 
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void cmbFontsize_TextChanged(object sender, SelectionChangedEventArgs e) //die Methode für Groesse
        {
            //if (cmbFontsize.SelectedItem != null) //wenn etwas ausgewählt ist 
            //{
            //    rtbEditor.Selection.ApplyPropertyValue(Inline.FontsizeProperty, cmbFontFamily.SelectedItem);
            //}
        }
        
    }
}
