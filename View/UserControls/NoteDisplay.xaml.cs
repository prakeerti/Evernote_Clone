using Evernote_Clone.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Evernote_Clone.View.UserControls
{
    /// <summary>
    /// Interaction logic for NoteDisplay.xaml
    /// </summary>
    public partial class NoteDisplay : UserControl
    {


        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(NoteDisplay), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NoteDisplay noteUserControl = d as NoteDisplay;

            if (noteUserControl != null)
            {
                noteUserControl.DataContext = noteUserControl.Note;
                //the data context for this user control will the NOTEBOOK property 
                //essentially we are updating the data context for these user control evry single time the notebook property changes 
            }

        }
        public NoteDisplay()
        {
            InitializeComponent();
        }
    }
}
