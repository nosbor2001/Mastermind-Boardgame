using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace Mastermind
{
    class Program
    {
        public static class Globals
        {
            //Globals used throughout the functions to track progress
            public static bool codeSetup = false;
            public static int turns = 1;
            public static Dictionary<int, string> codeDict = new Dictionary<int, string>();
            public static Dictionary<int, string> guessDict = new Dictionary<int, string>();
            public static Dictionary<int, string> resultsDict = new Dictionary<int, string>();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                //Mastermind
                //Row of code pegs on the decoding board XXXX
                //Codemaker provides feedback
                //  Black - Correct, colour and position
                //  White - Correct, colour
                //  None - Incorrect

                //Pick marbles to be decoded
                //12 Chances to guess (loop till 12)

                //Defines list of acceptable colours
                List<string> codeList = new List<string>() { "blue", "green", "red", "white", "orange", "purple" };

                displaySplash();

                //Options list for the main loop
                if (Globals.codeSetup == false)
                {
                    Console.WriteLine("1. Setup code");
                }
                else
                {
                    Console.WriteLine("1. Setup code - View");
                }
                    

                if (Globals.codeSetup == true)
                {
                    Console.WriteLine("2. Decipher code - Ready");
                }
                else
                    Console.WriteLine("2. Decipher code - Code not setup!");

                Console.WriteLine("3. Reset Game");
                Console.WriteLine("4. Help");
                Console.WriteLine("5. Quit");
                Console.Write("Enter option below: ");

                int opt = int.Parse(Console.ReadLine());

                //Options that determine the function to be used
                if (opt == 1 && Globals.codeSetup == false)
                {
                    displaySplash();
                    Console.Write("Random (1) or User generated (2): ");
                    string genOpt = Console.ReadLine();

                    //Sets the key to a random array from the pre-defined colours
                    if (genOpt == "1")
                    {
                        Random rnd = new Random();
                        for (int x = 1; x < 5; x++)
                        {
                            int randomNum = rnd.Next(1, 6);
                            Globals.codeDict.Add(x, codeList[randomNum]);
                        }

                        Globals.codeSetup = true;

                        //Debug
                        //Console.Write("Code: ");
                        //foreach (KeyValuePair<int, string> kvp in Globals.codeDict)
                        //{
                        //    Console.Write("{0}, ", kvp.Value);
                        //}
                        //Console.ReadKey();
                    }

                    if (genOpt == "2")
                        //Goes to the user-generation key function
                        genCode(codeList);
                }
                //Checks if the code is setup and also displays it
                else if (opt == 1 && Globals.codeSetup == true)
                {
                    displaySplash();
                    Console.Write("Code: ");
                    foreach (KeyValuePair<int, string> kvp in Globals.codeDict)
                    {
                        Console.Write("{0}, ", kvp.Value);
                    }
                    Console.ReadLine();
                }

                //Triggers the actual start of the guessing function
                else if (opt == 2 && Globals.codeSetup == true)
                {
                    displaySplash();
                    //Turn limit for game
                    while (Globals.turns < 13)
                    {
                        gamePlay();

                        //Adds to turns in the Global class
                        Globals.turns++;
                    }
                }

                else if (opt == 3)
                {
                    //Long winded way of resetting the program through exiting and relaunching
                    Process.Start(System.IO.Directory.GetCurrentDirectory() + "\\" + System.AppDomain.CurrentDomain.FriendlyName);
                    Environment.Exit(0);
                }

                else if (opt == 4)
                {
                    //Displays the help information
                    displaySplash();
                    string help = @"
Row of code pegs on the decoding board XXXX
Codemaker provides feedback
White - Correct, colour and position
Black - Correct, colour
Red - Incorrect

Pick marbles to be decoded
12 Chances to guess (loop till 12)";

                    Console.WriteLine(help);
                    Console.ReadLine();
                }

                else if (opt == 5)
                {
                    //Exits the program
                    Environment.Exit(0);
                }
            }
        }

        //Function to display the splash screen
        static string displaySplash()
        {
            Console.Clear();
            centerText(" __  __           _                      _           _ ");
            centerText(@"|  \/  |         | |                    (_)         | |");
            centerText(@"| \  / | __ _ ___| |_ ___ _ __ _ __ ___  _ _ __   __| |");
            centerText(@"| |\/| |/ _` / __| __/ _ \ '__| '_ ` _ \| | '_ \ / _` |");
            centerText(@"| |  | | (_| \__ \ ||  __/ |  | | | | | | | | | | (_| |");
            centerText(@"|_|  |_|\__,_|___/\__\___|_|  |_| |_| |_|_|_| |_|\__,_|");

            return "";
        }

        //Useless function that passes data, could be removed
        static string gamePlay()
        {
            decodeAttempt(Globals.guessDict);

            return "";
        }

        //Does the work of 'decoding' the given guess dictionary
        static string decodeAttempt(Dictionary<int, string> guessDict)
        {
            Globals.guessDict.Clear();
            Globals.resultsDict.Clear();
            for (int x = 1; x < 5; x++)
            {
                //Console.WriteLine(x);
                Console.Write("Enter colour guess " + x + ": ");
                //Guess validation
                Globals.guessDict.Add(x, Console.ReadLine().ToLower());

                //Console.WriteLine(Globals.guessDict[1]);
                
            }

            //Sends data to be displayed for the user
            decodeResults(Globals.guessDict, Globals.codeDict);

            return "";
        }

        //Case (switch) to print the correct console color for a given marble
        static string marbleColor(string guess)
        {
            switch (guess)
            {
                case ("blue"):
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("X");
                        break;
                    }
                case ("green"):
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("X");
                        break;
                    }
                case ("white"):
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("X");
                        break;
                    }
                case ("orange"):
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("X");
                        break;
                    }
                case ("red"):
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X");
                        break;
                    }
                case ("purple"):
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("X");
                        break;
                    }
                case ("black"):
                    {
                        //Uses special white background so it can be seen as black
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("X");
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    }
            }

            return "";
        }

        //Uses the two dictionaries to generate how correct a guess was
        static string decodeResults(Dictionary<int, string> guessDict, Dictionary<int, string> codeDict)
        {
            for (int x = 1; x < 5; x++)
            {
                if (guessDict[x] == codeDict[x])
                {
                    Globals.resultsDict.Add(x, "white");
                }
                else if (guessDict.ContainsValue(codeDict[x])  && !(guessDict[x] == codeDict[x]))
                {
                    Globals.resultsDict.Add(x, "black");
                }
                else
                    Globals.resultsDict.Add(x, "red");
            }

            //Debug
            //foreach (KeyValuePair<int, string> kvp in Globals.resultsDict)
            //    {
            //        Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            //    }

            //Guess - some fancy output
            Console.WriteLine("//=========[]=========\\");
            Console.Write("|| ");
            Console.Write(@"" + marbleColor(guessDict[1])
                + " " + marbleColor(guessDict[2])
                + " " + marbleColor(guessDict[3])
                + " " + marbleColor(guessDict[4]));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" || ");

            //Result - some fancy output
            Console.Write(@"" + marbleColor(Globals.resultsDict[1])
                + " " + marbleColor(Globals.resultsDict[2])
                + " " + marbleColor(Globals.resultsDict[3])
                + " " + marbleColor(Globals.resultsDict[4]));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ||\n");
            Console.WriteLine("\\=========[] =========//");

            //For loop (count controlled) to determine if the values in both dictionaries are the same
            int sameDict = 0;

            for (int x = 1; x < Globals.resultsDict.Count()+1; x++)
            {
                if (Globals.guessDict[x] == Globals.codeDict[x])
                {
                    sameDict++;
                }
                
            }

            //If statement (selection) to check if the player has won 
            if (sameDict == 4)
            {
                Console.WriteLine("You have won in " + Globals.turns + " turns");
                Console.ReadLine();
                Environment.Exit(0);
            }

            return "";
        }

        //Function to center any text given within a console window
        static string centerText(string text)
        {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
            return "";
        }

        //Function to generate the users code list
        static bool genCode(List<string> codeList)
        {
            displaySplash();
            Console.WriteLine("\nEnter your decoding values seperately from the colours: ");
            Console.WriteLine(String.Join(", ", codeList));
            Console.WriteLine();

            //For loop to create said dictionary
            for (int x = 1; x < 5; x++)
            {
                Console.Write("Enter marble " + x + ": ");
                Globals.codeDict.Add(x, Console.ReadLine().ToLower());
                checkInput(Globals.codeDict[x], codeList);
            }
            
            return Globals.codeSetup = true;
        }

        //Checks the input of the code list and converts it to lower case for checking later
        static string checkInput(string input, List<string> codeList)
        {

            if (codeList.Contains(input.ToLower()))
            {
                //Debug Case Check
                //Console.WriteLine("Valid");
            }

            else
            {
                Console.WriteLine("Invalid Input!");
            }

            return "";
        }
    }
}