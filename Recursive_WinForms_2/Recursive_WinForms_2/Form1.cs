namespace Recursive_WinForms_2;

public sealed partial class Form1 : Form
{
	public Form1()
	{
		InitializeComponent();
		
		BackColor = Color.Black;

		Opgave1();
		// Opgave2();
	}

	private static void Opgave1()
	{
		string modifiableStringIterative = "Hello World";
		modifiableStringIterative = ExtendingString(modifiableStringIterative, 'l', 2);
		modifiableStringIterative = ExtendingString(modifiableStringIterative, 'o', 4);
		modifiableStringIterative = ExtendingString(modifiableStringIterative, 'r', 0);
		Console.WriteLine($"ITERATIVE: {modifiableStringIterative}");
		
		string modifiableStringRecursive = "Hello World";
		modifiableStringRecursive = RecursiveExtendingString(modifiableStringRecursive, 'l', 2);
		modifiableStringRecursive = RecursiveExtendingString(modifiableStringRecursive, 'o', 4);
		modifiableStringRecursive = RecursiveExtendingString(modifiableStringRecursive, 'r', 0);
		Console.WriteLine($"RECURSIVE: {modifiableStringRecursive}");

		string ExtendingString(string input, char extensionChar, int multiplier)
		{
			// Denne funktion tager og tilføjer et antal ekstra kopier af et symbol (char)
			// efter det antal gange som brugeren (jeg) ønsker det, og præcis det symbol som jeg vil have ændret.
			// Det virker kun med chars, ikke hele string-stykker.
			
			int additions = multiplier - 1;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] != extensionChar) continue;

				switch (additions)
				{
					case > 0:
					{
						string insertion = new string(extensionChar, additions);
						input = input.Insert(i, insertion);
						i += additions;
						break;
					}
					case < 0:
						input = input.Remove(i, 1);
						break;
				}
			}
			return input;
		}
		
		string RecursiveExtendingString(string input, char extensionChar, int multiplier)
		{
			// KOMMENTAR:
			// Denne funktion er et forsøg på at løse opgave 1 med recursion i stedet for iteration.
			// Den virker ikke efter hensigt, men har de rigtige dele for at kunne virke.
			// Selv Rider (min editor) siger at det jeg prøver at gøre er bedre gjort med et loop,
			// så jeg er bare gået videre til næsteo opgave (som at-time-of-writing ikke er færdig).
			
			
			int additions = multiplier - 1;
			
			ModifyInputFromIndexRecursively(0);

			return input;

			void ModifyInputFromIndexRecursively(int index)
			{
				if (input[index] != extensionChar) return;
				
				switch (additions)
				{
					case > 0:
					{
						string insertion = new string(extensionChar, additions);
						input = input.Insert(index, insertion);
						index += additions;
						break;
					}
					case < 0:
						input = input.Remove(index, 1);
						break;
				}
				index++;
				ModifyInputFromIndexRecursively(index);
			}
		}
	}

	private static void Opgave2()
	{
		Graphics graphics;
		Pen pen = new Pen(Color.LightGray, 5);
		
		// string inputString = "F+F+F+F";
		// DrawRecursively(inputString, 0);

		void DrawRecursively(string input, int rotationalOffset)
		{
			if (input.Length == 0) return;

			// process one char, then pass down (now without that char)
			char currentChar = input[0];
			switch (currentChar)
			{
				case 'F':
					Point origin;
					
					// graphics.DrawLine(pen, );
					
					
					break;
				case '+':
					rotationalOffset++;
					break;
			}

			input =	input.Remove(0);
			DrawRecursively(input, rotationalOffset);
		}
	}
}