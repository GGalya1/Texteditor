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

            object temp1 = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty); //"Style" entspricht "italic"
            btnBold.IsChecked = (temp1 != DependencyProperty.UnsetValue) && temp.Equals(FontWeights.Bold);

            object temp2 = rtbEditor.Selection.GetPropertyValue(Inline.FontDecorationProperty); //"Style" entspricht "bold"
            btnBold.IsChecked = (temp2 != DependencyProperty.UnsetValue) && temp.Equals(FontWeights.Bold);
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
    }
}
