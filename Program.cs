using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab_6
{

 
    class Program
    {
        public enum Type { F, US }

        struct Geography
        {

            public string country;
            public string capital;
            public Type type;
            public int population;
            public Geography(string country, string capital, string population, string type)
            {
                this.country = country;
                this.capital = capital;
                if (type.Contains('F'))
                {
                    this.type = Type.F;
                }
                else
                {
                    this.type = Type.US;
                }
                this.population = int.Parse(population);
            }
           
            public void DisplayInfo()
            {
                Console.WriteLine("{0,-20} {1,-15} {2,-30} {3,-20}", country, capital, population, type);
            }
           
               
             
            public string ConvertString()
            {
                string text = country + '\t' + capital + '\t' + population + '\t' + type.ToString();
                return text;
            }
        
        }
        struct Log
        {
            public DateTime time;
            public string operation;
            public string country;

            public void DisplayLog()
            {
                Console.WriteLine("{0,-20} - {1,-15} {2,-30}", time, operation, country);
            }
        }
        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();
            for (int i = 0; i < data.Length; i += 8)
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in data.ToCharArray())
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            return sb.ToString();
        }


        public static void Main(string[] args)
        {
            var table = new List<Geography>();

            string path = @"E:\Учеба\Программач\Лабы\Lab_6\Lab_6.1\Lab_6.1\Table.dat";
            
               
            try
            {
                String data = File.ReadAllText(path);
                data = BinaryToString(data);
                using (StreamReader sr = new StreamReader(path))
                {
                   
                    foreach (string item in data.Trim().Split('\n'))
                    {
                        string[] texts = item.Split('\t');
                        if (texts.Length == 4)
                        {
                            table.Add(new Geography(texts[0], texts[1], texts[2], texts[3]));
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            var logFile = new List<Log>();
            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now;
            TimeSpan interval = time2 - time1;
            bool StopDoing = true;

            do
            {
                Console.WriteLine("1 - View table\n2 - Add entry\n3 - Delete entry\n4 - Update entry\n5 - Search for entries\n6 - View log\n7 - Exit");
                int Num = int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (Num)
                {
                    //CASE 1______________________________________________
                    case 1:
                        for (int i = 0; i < table.Count; i++)
                        { 

                            table[i].DisplayInfo();
                            
                        }

                        Console.WriteLine();

                        break;

                    //CASE 2______________________________________________
                    case 2:
                        Console.WriteLine("Enter your Country");

                        bool errorEmptiness = false;
                        string country;

                        
                        int population = 0;
                        var type = Type.F;

                        do
                        {
                            country = Console.ReadLine();
                            if (country == "")
                            {
                                Console.WriteLine("Please, enter the text");
                                errorEmptiness = true;

                            }
                            else
                            {
                                errorEmptiness = false;
                            }
                        } while (errorEmptiness == true);

                        Console.WriteLine("Enter your Capital");
                        string capital;
                        do
                        {
                            capital = Console.ReadLine();
                            if (capital == "")
                            {
                                Console.WriteLine("Please, enter the text");
                                errorEmptiness = true;

                            }
                            else
                            {
                                errorEmptiness = false;
                            }
                        } while (errorEmptiness == true);

                        Console.WriteLine("Enter your Type (F, US)");
                        bool errorType = false;
                        do
                        {
                            string YuorType = Console.ReadLine();
                            if (YuorType == "F" || YuorType == "Ф")
                            {
                                type = Type.F;
                                errorType = false;
                            }
                            else if (YuorType == "US" || YuorType == "УГ")
                            {
                                type = Type.US;
                                errorType = false;
                            }
                            else
                            {
                                Console.WriteLine("Enter correct your Type (F, US)");
                                errorType = true;
                            }
                        }
                        while (errorType == true);
                        Console.WriteLine("Enter your population");
                        bool errorPopulation = false;
                        do
                        {
                            try
                            {
                                population = int.Parse(Console.ReadLine());
                                if (population >= 1 && population <= 99999999)
                                {
                                    errorPopulation = false;
                                }
                                else
                                {
                                    Console.WriteLine("Enter a number less");
                                    errorPopulation = true;

                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Enter correct numb");
                                errorPopulation = true;
                            }

                        } while (errorPopulation == true);
                      

                        Geography newGeography;
                        newGeography.country = country;
                        newGeography.capital = capital;
                        newGeography.type = type;
                        newGeography.population = population;
                        table.Add(newGeography);

                        Log ADD;
                        ADD.time = DateTime.Now;
                        ADD.operation = "Added post";
                        ADD.country = country;
                        logFile.Add(ADD);

                        time1 = DateTime.Now;
                        TimeSpan interval_2 = time1 - time2;
                        if (interval < interval_2)
                        {
                            interval = interval_2;
                        }
                        time2 = ADD.time;
                        Console.WriteLine();
                        break;

                    //CASE 3______________________________________________
                    case 3:
                        Console.WriteLine("Enter the number of the delete entry");
                        bool errorDelete = false;
                        do
                        {

                            try
                            {
                                int NumbDel = int.Parse(Console.ReadLine());
                                if (NumbDel < table.Count || NumbDel > 0)
                                {

                                    Log DELETE;
                                    DELETE.time = DateTime.Now;
                                    DELETE.operation = "Record deleted";
                                    DELETE.country = table[NumbDel - 1].country;

                                    logFile.Add(DELETE);
                                    table.RemoveAt(NumbDel - 1);
                                    time1 = DateTime.Now;
                                    interval_2 = time1 - time2;

                                    if (interval < interval_2)
                                    {
                                        interval = interval_2;
                                    }
                                    time2 = DELETE.time;
                                }
                                else
                                {
                                    Console.WriteLine("Enter a valid row index");
                                    errorDelete = true;
                                }
                            }

                            catch (Exception)
                            {
                                Console.WriteLine("Enter correct numb");
                            }

                        } while (errorDelete == true);

                        Console.WriteLine();

                        break;

                    //CASE 4______________________________________________
                    case 4:
                        Console.WriteLine("Enter edit entry number");
                        bool errorEdit = false;
                        int NumEdit = 0;
                        do
                        {
                            try
                            {
                                NumEdit = int.Parse(Console.ReadLine());
                                if (NumEdit < table.Count || NumEdit > 0)
                                {
                                    errorEdit = false;
                                }
                                else
                                {
                                    Console.WriteLine("Enter a valid row index");
                                    errorEdit = true;
                                }

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Enter correct numb");
                            }
                        } while (errorEdit == true);

                        Console.WriteLine("Enter your Country");
                        string OldCountry = table[NumEdit - 1].country;
                        country = Console.ReadLine();

                        Console.WriteLine("Enter your Capital");
                        capital = Console.ReadLine();
                        population = 0;
                        type = Type.F;

                        Console.WriteLine("Enter your Type (F, US)");
                        table.RemoveAt(NumEdit - 1);
                        errorType = false;
                        do
                        {
                            string YuorType = Console.ReadLine();
                            if (YuorType == "F" || YuorType == "Ф")
                            {
                                type = Type.F;
                                errorType = false;
                            }
                            else if (YuorType == "US" || YuorType == "УГ")
                            {
                                type = Type.US;
                                errorType = false;
                            }
                            else
                            {
                                Console.WriteLine("Enter correct your Type (F, US)");
                                errorType = true;
                            }
                        }
                        while (errorType == true);

                        Console.WriteLine("Enter your population");
                        errorPopulation = false;
                        do
                        {
                            try
                            {
                                population = int.Parse(Console.ReadLine());
                                if (population >= 1 && population <= 99999999)
                                {
                                    errorPopulation = false;
                                }
                                else
                                {
                                    Console.WriteLine("Enter a number less");
                                    errorPopulation = true;

                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Enter correct numb");
                                errorPopulation = true;
                            }

                        } while (errorPopulation == true);

                       

                        Geography geographyEdit;
                        geographyEdit.country = country;
                        geographyEdit.capital = capital;
                        geographyEdit.type = type;
                        geographyEdit.population = population;

                        Log UPDATE;
                        UPDATE.time = DateTime.Now;
                        UPDATE.operation = "Record updated";
                        UPDATE.country = OldCountry;

                        logFile.Add(UPDATE);
                        time1 = UPDATE.time;
                        interval_2 = time1 - time2;

                        if (interval < interval_2)
                        {
                            interval = interval_2;
                        }
                        time2 = UPDATE.time;
                        table.Insert(NumEdit - 1, geographyEdit);

                        Console.WriteLine();
                        break;

                    //CASE 5______________________________________________
                    case 5:
                        Console.WriteLine("Enter your search selection:");
                        int NumChoise = 0;
                        bool errorChoise = false;
                        Console.WriteLine("1 - Withdraw federation\n2 - Withdraw a unitary state");
                        do
                        {
                            try
                            {
                                NumChoise = int.Parse(Console.ReadLine());

                                if (NumChoise == 1)
                                {
                                    var found = table.FindAll(x => x.type == Type.F);
                                    foreach (var gg in found)
                                    {
                                        gg.DisplayInfo();
                                        gg.ConvertString();
                                    }
                                    errorChoise = false;
                                }
                                else if (NumChoise == 2)
                                {

                                    var found = table.FindAll(x => x.type == Type.US);
                                    foreach (var gg in found)
                                    {
                                        gg.DisplayInfo();
                                        gg.ConvertString();
                                    }
                                    errorChoise = false;
                                }
                                else
                                {
                                    errorChoise = true;
                                }

                            }
                            catch
                            {
                                Console.WriteLine("Enter the correct option");
                                errorChoise = true;
                            }

                        }
                        while (errorChoise);
                        break;

                    //CASE 6______________________________________________
                    case 6:
                        for (int i = 0; i < logFile.Count; i++)
                        {
                            logFile[i].DisplayLog();
                        }
                        Console.WriteLine();
                        Console.WriteLine(interval + " - Longest user inactivity");
                        break;

                    //CASE 7______________________________________________
                    case 7:

                       
                        try
                        {
                            File.WriteAllText(path, string.Empty);
                            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                            {

                                foreach (Geography i in table)
                                {
                                    string text = i.ConvertString();
                                    text = StringToBinary(text);
                                    sw.WriteLine(text);
                                    
                                }

                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        StopDoing = false;
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Enter the correct command");
                        StopDoing = true;
                        break;
                }
            } while (StopDoing);
        }
    }
}
