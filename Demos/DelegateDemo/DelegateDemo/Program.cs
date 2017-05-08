using System;

namespace DelegateDemo
{

	class MainClass
	{
		// Declaration of a delegate type 
		// We are giving this delegate type the name Calculate and 
		// declaring it's signature (paramater types and return type)
		public delegate int Calculate (int n1, int n2);


		// Definition for a method that we will assign to a delegate
		static public int add(int num1, int num2)
		{
			return num1 + num2;
		}

		
		public static void Main (string[] args)
		{
			// Creating an instance of the Calculate delegate
			// and assigning the add method to it.
			Calculate addDel = new Calculate(add);

			// Creating an instance of the Calculate delegate
			// and assigning an anonamous method to it
			Calculate subDel = delegate(int n1, int n2) {return n1 - n2;};

			// Creating an instance of the Calculate delegate
			// and assigning an anonamous method to it using a lambda experssion
			Calculate multDel = (nm1, nm2) => {return nm1 * nm2;};

			Console.WriteLine ("Calling add via a delegate: " + addDel(2,3));
			Console.WriteLine ("Calling sub via a delegate: " + subDel(2,3));
			Console.WriteLine ("Calling mult via a delegate: " + multDel(2,3));

			// Implicit typing
			var result = add (3,7);
			Console.WriteLine ("int stored in an implicitly typed variable: " + result);
		}
	}
}







