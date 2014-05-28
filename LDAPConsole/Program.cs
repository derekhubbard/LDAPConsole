using System;
using Novell.Directory.Ldap;

namespace LDAPConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Connecting to LDAP...");

			var ldapService = new LDAPDirectoryService () {
				Host = "host"
			};	

			ldapService.Login ("un", "pwd");
		}
	}

	public class LDAPDirectoryService 
	{
		public string Host { get; set; }


		public int Port { get; set; }

		LdapConnection conn = null;

		public LDAPDirectoryService()
		{
			Host = string.Empty;
			Port = 389;
		}

		public void Login(string username, string password)
		{
			this.conn = new LdapConnection ();
			conn.Connect (Host, Port);

			conn.Bind (username, password);

			Console.WriteLine ("Connected");
		}

		public void CheckPassword(string testUsername, string testPassword)
		{

			var attr = new LdapAttribute("userPassword", testPassword);
			bool correct = conn.Compare(testUsername, attr);

			System.Console.Out.WriteLine(correct?"The password is correct.":"The password is incorrect.\n");

			// disconnect with the server
			conn.Disconnect();
		}
	}
}