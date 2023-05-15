using Evernote_Clone.Model;
using Evernote_Clone.ViewModel.Commands;
using Evernote_Clone.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote_Clone.ViewModel
{
    public class NotesVM:INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook 
		{
			get { return selectedNotebook; }
			set 
			{
				selectedNotebook = value;
				OnPropertyChanged("SelectedNotebook");
				GetNotes();
			}
		}

        public event PropertyChangedEventHandler? PropertyChanged;

        public NotesVM()
        {
			NewNotebookCommand = new NewNotebookCommand(this);
			NewNoteCommand = new NewNoteCommand(this);

			Notebooks= new ObservableCollection<Notebook>();
			Notes= new ObservableCollection<Note>();
			//do do this in constructor because we donot want to initialise a new collection every time only once when this constructor runs 

			GetNotebooks(); //bcoz we want the view to show the list of notebooks when the view is called immediately 
        }

		//create a Note function that take notebook id parameter yo create a new note in that paerticular notebook. 
		public void CreateNote(int notebookId)
		{
			Note newNote = new Note()
			{
				NotebookId = notebookId,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				Title = $"Note for {DateTime.Now.ToString()}"
			};
			//this new note is now to be inserted in the database 

			DatabaseHelper.Insert(newNote);
			GetNotes();

		}
		public void CreateNotebook()
		{
			Notebook newNotebook = new Notebook()
			{

				Name= "New Notebook"
			};

			//insert this notebook in db 

			DatabaseHelper.Insert(newNotebook);

			GetNotebooks();
		}

		//to display the notbook names on the list view 
		public void GetNotebooks()
		{
			var notebooks = DatabaseHelper.Read<Notebook>();
			Notebooks.Clear();
			foreach (var notebook in notebooks)
			{
				Notebooks.Add(notebook);
			}
		}

		public void GetNotes()
		{
			if (SelectedNotebook != null)
			{
				//the list of notes we get should belong the notebook that we have selected, so we have to filter the list 
				var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == selectedNotebook.Id).ToList();
				Notes.Clear();
				foreach (var note in notes)
				{
					Notes.Add(note);
				}
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			//this method evaluates if there is a property change  and if does it inoked the property change event 
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

    }
}
