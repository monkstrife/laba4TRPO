using System;
using System.Reflection;
using Lab4.Solvers;

namespace Lab4.UI
{
    class Program
    {
        static public string[] files = new string[0];
        static public int SizeArrFiles = 0;

        static public char[] delimiters = new char[0];
        static public int SizeArrDel = 0;

        static public int N = 3;
        static public int K = 2;

        static void Main(string[] args)
        {
            mainMenu();
        }

        static void mainMenu()
        {
            string menu_text = "Главное меню: \r\n1 – Настройка обработки \r\n2 – Самое часто встречающееся слово по всем файлам \r\n3 – Самое часто встречающееся слово по всем файлам (Linq) \r\n4 – N часто встречающихся общеупотребимых слов, которые встречаются в каждом файле хотя бы K раз \r\n5 – N часто встречающихся общеупотребимых слов, которые встречаются в каждом файле хотя бы K раз (Linq) \r\n6 – Файл с наибольшим количеством уникальных слов (слова, которые не повторяются в рамках одного файла) и число таких слов \r\n7 – Файл с наибольшим количеством уникальных слов (слова, которые не повторяются в рамках одного файла) и число таких слов (Linq) \r\n8 – Распределение количества слов по длине (по возрастанию) \r\n9 – Распределение количества слов по длине (по возрастанию) (Linq) \r\n0 - Выход из программы";
            string key;


            /*Создаем меню*/
            Console.Clear();
            Console.WriteLine(menu_text);

            LinqSolver linqSolver = new LinqSolver();
            NoLinqSolver noLinqSolver = new NoLinqSolver();
            IList<string> l;
            IDictionary<int, int> dic = new Dictionary<int, int>();
            string result;

            while (true)
            {
                /*читаем первый символ и запускаем соответствующую подпрограмму*/
                key = Console.ReadLine() ?? string.Empty;
                if (key == string.Empty)
                {
                    continue;
                }

                /*Запускаем выбранную задачу*/
                switch (key[0])
                {
                    case '1':
                        Сustomization();
                        Console.WriteLine(menu_text);
                        break;
                    case '2':
                        Console.WriteLine(noLinqSolver.GetTheMostPopularWord(files, delimiters));
                        File.WriteAllText("NoLinqSolverResults/GetTheMostPopularWord.txt", noLinqSolver.GetTheMostPopularWord(files, delimiters));
                        break;
                    case '3':
                        Console.WriteLine(linqSolver.GetTheMostPopularWord(files, delimiters));
                        File.WriteAllText("LinqSolverResults/GetTheMostPopularWord.txt", linqSolver.GetTheMostPopularWord(files, delimiters));
                        break;
                    case '4':
                        l = noLinqSolver.GetCommonWords(files, delimiters, N, K);
                        result = "";
                        foreach (string word in l)
                        {
                            result += word + " ";
                        }
                        Console.WriteLine(result);
                        File.WriteAllText("NoLinqSolverResults/GetCommonWords.txt", result);
                        break;
                    case '5':
                        l = linqSolver.GetCommonWords(files, delimiters, N, K);
                        result = "";
                        foreach (string word in l)
                        {
                            result += word + " ";
                        }
                        Console.WriteLine(result);
                        File.WriteAllText("LinqSolverResults/GetCommonWords.txt", result);
                        break;
                    case '6':
                        Console.WriteLine(noLinqSolver.GetFileWithManyUniqueWords(files, delimiters));
                        File.WriteAllText("NoLinqSolverResults/GetFileWithManyUniqueWords.txt", noLinqSolver.GetFileWithManyUniqueWords(files, delimiters).Item1 + " " + noLinqSolver.GetFileWithManyUniqueWords(files, delimiters).Item2);
                        break;
                    case '7':
                        Console.WriteLine(linqSolver.GetFileWithManyUniqueWords(files, delimiters));
                        File.WriteAllText("LinqSolverResults/GetFileWithManyUniqueWords.txt", linqSolver.GetFileWithManyUniqueWords(files, delimiters).Item1 + " " + linqSolver.GetFileWithManyUniqueWords(files, delimiters).Item2);
                        break;
                    case '8':
                        dic = noLinqSolver.GetWordDistributionByLength(files, delimiters);
                        result = "";
                        foreach (var item in dic)
                        {
                            result += item.Key + ": к-во " + item.Value + "\n";
                        }
                        Console.WriteLine(result);
                        File.WriteAllText("NoLinqSolverResults/GetWordDistributionByLength.txt", result);
                        break;
                    case '9':
                        dic = linqSolver.GetWordDistributionByLength(files, delimiters);
                        result = "";
                        foreach (var item in dic)
                        {
                            result += item.Key + ": к-во " + item.Value + "\n";
                        }
                        Console.WriteLine(result);
                        File.WriteAllText("LinqSolverResults/GetWordDistributionByLength.txt", result);

                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Введите нужный символ (1, 2, 3, 4, 5, 6, 7, 8, 9 или 0)");
                        break;
                }
            }
        }


        /*
         *
         *  === 1 ===
         *
         */
        /*Настройка обработки*/
        static void Сustomization()
        {
            Console.Clear();

            string text = "Настройка обработки:\r\n1 – добавить новый файл (указать Абс.пусть) (по умолчанию массив пуст)\r\n2 – добавить новый разделитель (по умолчанию массив пуст)\r\n3 – указать число N (по умолчанию N = 3)\r\n4 – указать число K (по умолчанию K = 2)\r\n5 – вернуть значения по умолчанию\r\n0 – Выход в главное меню";
            string key;
            string[] copy;
            char[] copyChar;

            Console.WriteLine(text);

            while (true)
            {
                /*читаем первый символ и запускаем соответствующую подпрограмму*/
                key = Console.ReadLine() ?? string.Empty;
                if (key == string.Empty)
                {
                    continue;
                }

                /*Запускаем выбранную задачу*/
                switch (key[0])
                {
                    case '1':
                        key = Console.ReadLine() ?? string.Empty;
                        copy = new string[SizeArrFiles++];
                        files.CopyTo(copy, 0);
                        files = new string[SizeArrFiles];
                        copy.CopyTo(files, 0);
                        files[SizeArrFiles - 1] = @"" + key;

                        break;
                    case '2':
                        key = Console.ReadLine() ?? string.Empty;
                        copyChar = new char[SizeArrDel++];
                        delimiters.CopyTo(copyChar, 0);
                        delimiters = new char[SizeArrDel];
                        copyChar.CopyTo(delimiters, 0);
                        delimiters[SizeArrDel - 1] = key[0];

                        break;
                    case '3':
                        key = Console.ReadLine() ?? string.Empty;
                        N = Convert.ToInt32(key);

                        break;
                    case '4':
                        key = Console.ReadLine() ?? string.Empty;
                        K = Convert.ToInt32(key);

                        break;
                    case '5':
                        N = 3;
                        K = 2;
                        files = new string[0];
                        SizeArrFiles = 0;

                        delimiters = new char[0];
                        SizeArrDel = 0;

                        break;
                    case '0':
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Введите нужный символ (1, 2, 3, 4, 5, или 0)");
                        break;
                }
            }
        }
    }
}