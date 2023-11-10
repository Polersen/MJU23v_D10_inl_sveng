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
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!\nEnter \"help\" to see various commands.\nEnter \"quit\" to stop.");
            do
            {
                string[] argument = Input("> ").Split();
                string command = argument[0];
                if (command == "quit") // NYI: quit does not shut down program
                {
                    Console.WriteLine("Goodbye!");
                    exit = true;
                }
                // TODO: Break out load method
                else if (command == "load")
                {
                    if(argument.Length == 2) //FIXME: Unhandled exception. System.IO.FileNotFoundException
                    {
                        using (StreamReader sr = new StreamReader(argument[1]))
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
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))
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
                }
                // TODO: Break out list method
                else if (command == "list")  // FIXME: Unhandled exception. System.NullReferenceException
                {
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                // TODO: Break out new method
                else if (command == "new") // NYI: Prevent empty strings
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if(argument.Length == 1)
                    {
                        string swe_input = Input("Write word in Swedish: ");
                        string eng_input = Input("Write word in English: ");
                        dictionary.Add(new SweEngGloss(swe_input, eng_input));
                    }
                }
                // TODO: Break out delete method
                else if (command == "delete")
                {
                    if (argument.Length == 3) // FIXME: Unhandled exception. System.ArgumentOutOfRangeException
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1) // FIXME: Unhandled exception. System.ArgumentOutOfRangeException
                    {
                        string swe_input = Input("Write word in Swedish: ");
                        string eng_input = Input("Write word in English: ");
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == swe_input && gloss.word_eng == eng_input)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                // TODO: Break out translate method
                else if (command == "translate") // NYI: If word does not exist
                {
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        string input = Input("Write word to be translated: ");
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == input)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == input)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else if (command == "help")
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
                else if (command == "save")
                {
                    // NYI: Code to save list to file
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
                // TODO: Add save to file method
            }
            while (exit != true);
        }
    }
}