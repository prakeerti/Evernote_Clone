using System;
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
using System.Windows.Shapes;
using System.Speech;
using System.Speech.Recognition;
using System.Threading;
using System.Windows.Controls.Primitives;

namespace Evernote_Clone.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        SpeechRecognitionEngine recognizer;
        public NotesWindow()
        {
            InitializeComponent();
            var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                                 where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                                 select r).FirstOrDefault();
            recognizer = new SpeechRecognitionEngine(currentCulture);
            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendDictation();
            Grammar grammar = new Grammar(builder);
            recognizer.LoadGrammar(grammar);
            recognizer.SetInputToDefaultAudioDevice();
            //now we need an event handler tht can recognise speech 
            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;

            //setting the spource for for family combo box 
            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamily_Combobox.ItemsSource = fontFamilies;

            List<double> fontSizes= new List<double>() { 7,8,9,11,14,6,18,22,24,28,32,36,40,44,52,64,72};
            fontSize_Combobox.ItemsSource = fontSizes;

        }

        public void Recognizer_SpeechRecognized(object sender,SpeechRecognizedEventArgs e)
        {
            //this eventargs recognises the speech that has been captured 
            string recognizedText= e.Result.Text;
            ContentRichTextbox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        bool isRecognizing = false;
        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            //this button initiates the action of listening 
           
            if(!isRecognizing)
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                isRecognizing = true;
            }
            else
            {
                recognizer.RecognizeAsyncStop();
                isRecognizing = false;
            }
        }

        private void richTextbox_TextcChanged(object sender, TextChangedEventArgs e)
        {
            //this function helps in reading the total characters in text box and display the count 
            int totalCharacters= (new TextRange(ContentRichTextbox.Document.ContentStart, ContentRichTextbox.Document.ContentEnd)).Text.Length;

            statusTextBlock.Text = $"Document Length: {totalCharacters} characters";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
            {
                ContentRichTextbox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                ContentRichTextbox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
            {
                ContentRichTextbox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                ContentRichTextbox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
            {
                ContentRichTextbox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                //I define a textdecoration collection here and I try to get the text decoration property as collection
                //in this collection I try to remove the underline value and save it into textDecoration that we can use further to remove the underline property. 
                TextDecorationCollection textDecorations;
                (ContentRichTextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                ContentRichTextbox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void fontFamilyCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(fontFamily_Combobox.SelectedItem !=  null)
            {
                ContentRichTextbox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamily_Combobox.SelectedItem);
            }
        }

        private void fontSizeCombobox_SelectionChanged(object sender, TextChangedEventArgs e)
        {
            ContentRichTextbox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSize_Combobox.Text);
            //here we are not taking the selected value as the user can himself write his own font size and apply it 
        }

        private void richTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = ContentRichTextbox.Selection.GetPropertyValue(FontWeightProperty);
            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));

            var selectedStyle = ContentRichTextbox.Selection.GetPropertyValue(FontStyleProperty);
            italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

            var selectedDecoration = ContentRichTextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) && (selectedDecoration.Equals(TextDecorations.Underline));

            // ContentRichTextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty); this part retreives the value of text property of the selected text 
            //selectedDecoration stores the property value for the selected text 
            //This line of code assigns a boolean value to the IsChecked property of an underlineButton. It checks if the selectedDecoration is not equal to
            //DependencyProperty.UnsetValue (indicating a valid property value) and if the selectedDecoration is equal to
            //TextDecorations.Underline (indicating the underline decoration is applied).
            //In summary, this code is used to determine if the underline button (underlineButton)
            //should be checked or unchecked based on whether the selected text within the ContentRichTextbox has the underline decoration applied.

            //these boxes should reflect what font or what size has been a[pplied to certain selected text
            fontFamily_Combobox.SelectedItem = ContentRichTextbox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSize_Combobox.Text = ContentRichTextbox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
        }

    }
}
    