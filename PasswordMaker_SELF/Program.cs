using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PasswordMaker_SELF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char state = 'Y';
            while (state == 'Y')
            {
                int lineNumber = 0;
                foreach (string line in File.ReadAllLines(@"D:\C# Projects\SELF Codes\Pw.txt"))
                {
                    lineNumber++;
                }
                Process[] fileName = Process.GetProcessesByName("notepad");
                foreach (Process f in fileName)
                {
                    f.Kill();
                }
                Console.Write("Name (/help to show the possible commands): ");
                string name = Console.ReadLine();

                int quantity = 0;
                bool m = true;
                string[] namesUsed = new string[lineNumber];

                foreach (string line in File.ReadLines(@"D:\C# Projects\SELF Codes\Pw.txt"))
                {
                    quantity++;
                    int firstAppearance = line.IndexOf("-") + 2;
                    int lastAppearance = line.LastIndexOf("-") - firstAppearance - 1;
                    string checkName = line.Substring(firstAppearance, lastAppearance);
                    namesUsed[quantity - 1] = checkName;
                }

                while (namesUsed.Contains(name))
                {
                    Console.WriteLine($"The name {name} already exists");
                    Console.Write("Name (/show to show the possible commands): ");
                    name = Console.ReadLine();
                }

                if(name == "/help")
                {
                    Console.WriteLine("\n/clear - clears the entire file\n/show - opens the file\n/delete - erase a specif line already in the file\n/color - change the foreground and background colors of the terminal\n");
                    continue;
                }

                else if (name == "/clear")
                {
                    Console.WriteLine("File Cleared");
                    File.WriteAllText(@"D:\C# Projects\SELF Codes\Pw.txt", string.Empty);
                    continue;
                }

                else if (name == "/show")
                {
                    Process.Start(@"D:\C# Projects\SELF Codes\Pw.txt");
                    Console.ReadKey();
                    continue;
                }

                else if (name == "/delete")
                {
                    Console.WriteLine("Specify which line has to be deleted: ");
                    int dl = int.Parse(Console.ReadLine())-1;
                    List<string> allContent = File.ReadAllLines(@"D:\C# Projects\SELF Codes\Pw.txt").ToList();
                    if (dl > allContent.Count())
                    {
                        Console.WriteLine("The number typed is greater than the size of the file");
                        continue;
                    }
                    Console.WriteLine("{0} was removed\n", allContent[dl]);
                    allContent.RemoveAt(dl);
                    for(int u = 0; u < allContent.Count; u++)
                    {
                        string lineC = allContent[u].ToString();
                        lineC = lineC.Remove(0, allContent[u].IndexOf("-")+2);
                        Console.WriteLine($"LineC = {lineC}");
                        allContent[u] = $"({u+1}) - {lineC}";
                    }
                    File.WriteAllLines(@"D:\C# Projects\SELF Codes\Pw.txt", allContent);
                    foreach (string content in allContent)
                    {
                        Console.WriteLine(content);
                    }
                    continue;
                }

                else if (name == "/color")
                {

                    Console.Write("Chose a foreground color (type /help to show the list): ");
                    var fc = Console.ReadLine();

                    if (fc == "/help")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("0 - Black\n1 - DarkBlue\n2 - DarkGreen\n3 - DarkCyan\n4 - DarkRed\n5 - DarkMagenta\n6 - DarkYellow\n7 - Gray\n8 - DarkGray\n9 - Blue\n10 - Green\n11 - Cyan\n12 - Red\n13 - Magenta\n14 - Yellow\n15 - White");
                        Console.Write("Foreground color: ");
                        fc = Console.ReadLine();
                    }


                    Console.Write("Chose a background color (type /list to show the list of colors used previously): ");
                    var bc = Console.ReadLine();

                    if (bc == "/list")
                    {
                        Process.Start(@"D:\C# Projects\SELF Codes\colorlist.txt");
                        Console.Write("Chose a background color: ");
                        bc = Console.ReadLine();
                        foreach (Process p in Process.GetProcessesByName("notepad"))
                        {
                            p.Kill();
                        }
                    }

                    Console.WriteLine($"The colors were changed to: ");
                    if (Enum.TryParse(fc, out ConsoleColor foreground))
                    {
                        Console.ForegroundColor = foreground;
                        Console.WriteLine($"Foreground: {fc}");
                    }
                    if (Enum.TryParse(bc, out ConsoleColor background))
                    {
                        Console.BackgroundColor = background;
                        Console.WriteLine($"Background: {bc}", Convert.GetTypeCode(bc));
                    }
                    else
                    {
                        Console.WriteLine("The numbers typed were not viable colors.");
                    }
                    File.AppendAllText(@"D:\C# Projects\SELF Codes\colorlist.txt", $"\nF = {fc} / B = {bc}");
                    continue;
                }
                else
                {
                    if (lineNumber == 0)
                    {
                        File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", $"({lineNumber+1}) - {name} - ");
                    }
                    else
                    {
                        File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", $"\n({lineNumber+1}) - {name} - ");
                    }
                    Console.Write("Number of characters: ");
                    int size = int.Parse(Console.ReadLine());
                    int[] n = new int[size];
                    Random p = new Random();
                    #region
                    Console.Write("\nInclude special operations? (Y/N) ");
                    char special = char.ToUpper(char.Parse(Console.ReadLine()));
                    if (special == 'Y')
                    {
                        Console.Write("\nInclude spaces? ");
                        char s1 = char.ToUpper(char.Parse(Console.ReadLine()));
                        Console.Write(@"Include special characters? ");
                        char s2 = char.ToUpper(char.Parse(Console.ReadLine()));
                        Console.Write("Include capital letters? ");
                        char s3 = char.ToUpper(char.Parse(Console.ReadLine()));

                        if (s1 != 'Y' && s2 != 'Y' && s3 != 'Y') //none are true
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int o = p.Next(33, 123);
                                if (n.Contains(o) || o >= 58 && o <= 96 || o >= 33 && o <= 47 || o >= 91 && o <= 96 || o == 45)
                                {

                                    i--;
                                    continue;
                                }
                                else
                                {
                                    n[i] = o;
                                    Console.Write((char)o);
                                    File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                                }
                            }
                        }
                        else if (s1 == 'Y' && s2 != 'Y' && s3 != 'Y') //1 is true
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int o = p.Next(32, 123);
                                if (n.Contains(o) || o >= 58 && o <= 96 || o >= 33 && o <= 47 || o >= 91 && o <= 96 || o == 45)
                                {
                                    i--;
                                    continue;
                                }
                                else
                                {
                                    n[i] = o;
                                    Console.Write((char)o);
                                    File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                                }
                            }
                        }
                        else if (s1 != 'Y' && s2 == 'Y' && s3 != 'Y') //2 is true
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int o = p.Next(33, 127);
                                if (n.Contains(o) || o >= 65 && o <= 90 || o == 45)
                                {
                                    i--;
                                    continue;
                                }
                                else
                                {
                                    n[i] = o;
                                    Console.Write((char)o);
                                    File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                                }
                            }
                        }
                        else if (s1 != 'Y' && s2 != 'Y' && s3 == 'Y') //3 is true
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int o = p.Next(48, 123);
                                if (n.Contains(o) || o >= 58 && o <= 64 || o >= 91 && o <= 96 || o == 45)
                                {
                                    i--;
                                    continue;
                                }
                                else
                                {
                                    n[i] = o;
                                    Console.Write((char)o);
                                    File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                                }
                            }
                        }
                        else if (s1 == 'Y' && s2 == 'Y' && s3 != 'Y')//1 and 2 are true
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int o = p.Next(32, 127);
                                if (n.Contains(o) || o >= 65 && o <= 90 || o == 45)
                                {
                                    i--;
                                    continue;
                                }
                                else
                                {
                                    n[i] = o;
                                    Console.Write((char)o);
                                    File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                                }
                            }
                        }
                        else if (s1 != 'Y' && s2 == 'Y' && s3 == 'Y')  //2 and 3 are true
                        {
                            for (int i = 3; i < size; i++)
                            {
                                int o = p.Next(33, 127);
                                if (n.Contains(o) || o == 45)
                                {
                                    i--;
                                    continue;
                                }
                                else
                                {
                                    n[i] = o;
                                    Console.Write((char)o);
                                    File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                                }
                            }
                        }
                        else if (s1 == 'Y' && s2 != 'Y' && s3 == 'Y') //1 and 3 are true
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int o = p.Next(32, 127);
                                if (n.Contains(o) || o >= 33 && o <= 47 || o >= 58 && o <= 64 || o >= 91 && o <= 96 || o == 45)
                                {
                                    i--;
                                    continue;
                                }
                                else
                                {
                                    n[i] = o;
                                    Console.Write((char)o);
                                    File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                                }
                            }
                        }
                        else if (s1 == 'Y' && s2 == 'Y' && s3 == 'Y')
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int o = p.Next(32, 127);
                                if (n.Contains(o) || o == 45)
                                {
                                    i--;
                                    continue;
                                }
                                else
                                {
                                    n[i] = o;
                                    Console.Write((char)o);
                                    File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                                }
                            }
                        }//all are true
                    }
                    else if (special != 'Y')
                    {
                        Console.Write($"{name} - ");
                        for (int i = 0; i < size; i++)
                        {
                            int o = p.Next(33, 127);
                            if (n.Contains(o) == true || o == 45)
                            { 
                                i--;
                                continue;
                            }
                            else
                            {
                                n[i] = o;
                                Console.Write((char)o);
                                File.AppendAllText(@"D:\C# Projects\SELF Codes\Pw.txt", Convert.ToString((char)o));
                            }
                        }
                    }
                }
                #endregion
                Console.Write("\nContinue procedure: ");
                state = char.ToUpper(char.Parse(Console.ReadLine()));
            }
            Console.Write("Section Terminated ");
            Console.ReadKey();
        }
    }
}