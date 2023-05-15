using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote_Clone.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {
        public NotesVM VM { get; set; }
        public NewNotebookCommand(NotesVM vm)
        {
            VM = vm;
        }
        public event EventHandler? CanExecuteChanged 
        { 
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value;}
        
        }

        private void CommandManager_RequerySuggested(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.CreateNotebook();
        }
    }
}
