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
    /// Interaction logic for NotebookDisplay.xaml
    /// </summary>
    public partial class NotebookDisplay : UserControl
    {
        //any display notebook user control will need a notebook that can be used to bound 


        public Notebook Notebook
        {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookDisplay), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotebookDisplay notebookUserControl= d as NotebookDisplay; 
            if(notebookUserControl != null)
            {
                notebookUserControl.DataContext = notebookUserControl.Notebook;
                //the data context for this user control will the NOTEBOOK property 
                //essentially we are updating the data context for these user control evry single time the notebook property changes 
            }

        }

        public NotebookDisplay()
        {
            InitializeComponent();
        }

        public static implicit operator NotebookDisplay?(NoteDisplay? v)
        {
            throw new NotImplementedException();
        }
    }
}
