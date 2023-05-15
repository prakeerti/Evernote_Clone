using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote_Clone.ViewModel.Commands
{
    public class RegisterUserCommand : ICommand
    {
        //this is to register a new user 
        public LoginVM VM { get; set; }
        public event EventHandler? CanExecuteChanged;

        public RegisterUserCommand(LoginVM vm)
        {
            VM = vm;   
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //TODO: LOGIN FUNCTIONALITY 
        }
    }
}
