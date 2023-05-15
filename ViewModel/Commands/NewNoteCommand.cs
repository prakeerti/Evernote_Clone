using Evernote_Clone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote_Clone.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {

        public NotesVM VM { get; set; }
        public NewNoteCommand(NotesVM vm)
        {
            VM = vm;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void CommandManager_RequerySuggested(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool CanExecute(object? parameter)
        {
            Notebook selectedNotebook= parameter as Notebook;
            if (selectedNotebook != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            //get the id of the notebook(parameter) which is selected to open a new note 
            Notebook selectedNotebook = parameter as Notebook;
            //to get a new notebook based on (notebook id) make sure that get newnote method is defined in the VM 
            VM.CreateNote(selectedNotebook.Id);
        }
    }
}
