using Evernote_Clone.Model;
using Evernote_Clone.ViewModel.Commands;
using Evernote_Clone.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Evernote_Clone.ViewModel
{
    public class NotesVM:INotifyPropertyChanged
    {
		
        public ObservableCollection<Notebook> Notebooks { get; set; }

        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public RenameCommand RenameCommand { get; set; }

        public EndEditingCommand EndEditingCommand { get; set; }

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

		private Note selectedNote;

		public Note SelectedNote
		{
			get { return selectedNote; }
			set 
			{ 
				selectedNote = value;
				OnPropertyChanged("SelectedNote"); //we have to bind this property to the list view of the note
				SelectedNoteChanged?.Invoke(this, new EventArgs()); //we will react to this event from the notes window xaml.cs file 
			}
		}


		public event PropertyChangedEventHandler? PropertyChanged;
		//we have to view content inside a note and we can do it using the setter of selected note, however we donot have access to the content rich text box through view model. 
		public event EventHandler SelectedNoteChanged; //we juts have to invoke this event from the setter of the selected note 


        public NotesVM()
        {

			NewNotebookCommand = new NewNotebookCommand(this);
			NewNoteCommand = new NewNoteCommand(this);
			RenameCommand= new RenameCommand(this);
			EndEditingCommand = new EndEditingCommand(this);
			

			Notebooks= new ObservableCollection<Notebook>();
			Notes= new ObservableCollection<Note>();
			//do do this in constructor because we donot want to initialise a new collection every time only once when this constructor runs 

			//setting visibility of the rename menu item 
			IsVisible= Visibility.Collapsed; //initially the text box will be collapsed 

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


		//we will prop full this visibility property as this will trigger onpropertychange event everty time there is a change in selection of the rename command 

		private Visibility isVisible;

		public Visibility IsVisible
		{
			get { return isVisible; }
			set 
			{ 
				isVisible = value;
				OnPropertyChanged("IsVisible");
			}
		}

		public void StartEditing()
		{
			//when a menu item is clicked a property change will become true, which should display the text box 
			//as is the text box remains collapsed but the moment someone click on the rename property it should be visible 
			//we have to define one more property "visibility" to handl the visibility of the text box 
			IsVisible = Visibility.Visible;
			//now we just have to bind it to the text box we want to show this property 
		}
		//this stop editing takes a parameter notebook which should be present to stop the editing function
		public void StopEditing(Notebook notebook)
		{
			IsVisible = Visibility.Collapsed;
			DatabaseHelper.Update(notebook); //to update in db the changed name of the notebook 
			//once the stop editing is done it should again display the collection of lists of notebooks 
			GetNotebooks();
		}
    }
}
