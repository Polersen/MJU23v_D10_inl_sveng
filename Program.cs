namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static string Input(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        static void Main(string[] args)
        {
            bool exit = false;
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!\nEnter \"help\" to see various commands.\nEnter \"quit\" to stop.");
            do
            {
                string[] argument = Input("> ").Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    exit = true;
                }
                else if (command == "load")
                {
                    if(argument.Length == 2)
                    {
                        Load(argument[1]);
                    }
                    else if(argument.Length == 1)
                    {
                        Load(defaultFile);
                    }
                }
                else if (command == "list")
                {
                    List();
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        New(argument[1], argument[2]);
                    }
                    else if(argument.Length == 1)
                    {
                        string swe_input = Input("Write word in Swedish: ");
                        string eng_input = Input("Write word in English: ");
                        New(swe_input, eng_input);
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        Delete(argument[1], argument[2]);
                    }
                    else if (argument.Length == 1)
                    {
                        string swe_input = Input("Write word in Swedish: ");
                        string eng_input = Input("Write word in English: ");
                        Delete(swe_input, eng_input);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        Translate(argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        string input = Input("Write word to be translated: ");
                        Translate(input);
                    }
                }
                else if (command == "help")
                {
                    Help();
                }
                else if (command == "save")
                {
                    // This is not part of the assignment, just a bit of extra practice for fun
                    // NYI: Code to save list to file
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (exit != true);
        }

        private static void Load(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader($"..\\..\\..\\dict\\{file}"))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                string fileNameOnly = System.IO.Path.GetFileName(ex.FileName);
                Console.WriteLine($"The specified file \"{fileNameOnly}\" could not be found.\nPlease try again!");
            }
        }

        private static void List()
        {
            try
            {
                foreach (SweEngGloss gloss in dictionary)
                {
                    Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}"); // FIXME: Unhandled exception. System.NullReferenceException
                }
            }
            catch (System.NullReferenceException ex)
            {
                Console.WriteLine("No file has been loaded into the program.\nPlease load a file first.");
            }
        }

        private static void New(string swe_input, string eng_input)
        {
            dictionary.Add(new SweEngGloss(swe_input, eng_input)); // NYI: Prevent empty strings
        }

        private static void Delete(string swe_input, string eng_input)
        {
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++) // FIXME: Unhandled exception. System.ArgumentOutOfRangeException
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == swe_input && gloss.word_eng == eng_input)
                    index = i;
            }
            dictionary.RemoveAt(index);
        }

        private static void Translate(string input) // NYI: If word does not exist
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == input)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == input)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }

        private static void Help()
        {
            Console.WriteLine("-help\t\t---\t\tShows various commands.\n" +
                                                "-quit\t\t---\t\tStops the program.\n" +
                                                "-load\t\t---\t\tLoads either default or chosen file into program.\n" +
                                                "-list\t\t---\t\tLists the contents of the list.\n" +
                                                "-new\t\t---\t\tAdds new content to the list.\n" +
                                                "-delete\t\t---\t\tDeletes chosen content from the list.\n" +
                                                "-translate\t---\t\tTranslates the word chosen.\n" +
                                                "-save\t\t---\t\tSaves the list (not yet implemented).");
        }
        // TODO: Add save to file method
    }
}