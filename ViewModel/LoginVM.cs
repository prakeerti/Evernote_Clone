using Evernote_Clone.Model;
using Evernote_Clone.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote_Clone.ViewModel
{
    public class LoginVM
    {
		private User user;

		public User User
		{
			get { return user; }
			set { user = value; }
		}

		public RegisterUserCommand RegisterUserCommand { get; set; }
		public LoginCommand LoginCommand { get; set; }

		public LoginVM()
		{
			RegisterUserCommand = new RegisterUserCommand(this);
			//this command take a vm as parameter so this 
			//the register command has a property of type loginVM, using that we can give the functionallity to login 
			// so when login VM runs its ctor will run, and here we will specify that to run a new  register user whenever the login happens 
			// the login window will be binded using the loginVm instance hence for login loginVM is the mai functionality to run. 

			LoginCommand = new LoginCommand(this);

			//both login and register are present in the login window, so both these actions will use the instance of loginVM method. 
		}
	}
}
