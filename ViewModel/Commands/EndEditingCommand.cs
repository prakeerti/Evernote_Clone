using Evernote_Clone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote_Clone.ViewModel.Commands
{
    public class EndEditingCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public NotesVM VM { get; set; }
        public EndEditingCommand(NotesVM vm)
        {
            VM = vm;
        }
        public bool CanExecute(object? parameter)
        {
            return true; 
        }

        public void Execute(object? parameter)
        {
            Notebook notebook= parameter as Notebook;
            if(notebook != null)
                VM.StopEditing(notebook); //this function will take in a parameter which is a notebook 
        }
    }
}
