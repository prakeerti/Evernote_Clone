using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote_Clone.ViewModel.Commands
{
    public class RenameCommand : ICommand
    {
        public NotesVM VM { get; set; }
        public event EventHandler? CanExecuteChanged;

        //we have to create a constructor to recieve the instance of notesVM 
        public RenameCommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.StartEditing();
        }
    }
}
