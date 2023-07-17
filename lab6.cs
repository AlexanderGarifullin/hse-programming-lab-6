using System;
using System.Text.RegularExpressions;
namespace l6
{
    class Program
    {
        static int MAXLENGHT = 10;
        static int MINLENGHT = 1;
        static int MAXVALUE = 10;
        static int MINVALUE = -10;
        static int MINLINELENGHT = 2;
        static int MAXLINELENGHT = 500;
        static Random rnd = new Random();
        const string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZабвгдеёжзийклмнопрстуфхцчшщъыьэюя" +
            "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyz ,;:.!?";
        #region Печать меню
        static void PrintMainMenu()
        {
            Console.WriteLine("1. Работа с матрицей" +
                "\n2. Работа со строками" +
                "\n3. Выход");
        }
        static void PrintCreateMenu()
        {
            Console.WriteLine("1. Cоздать матрицу вручную" +
                "\n2. Создать матрицу с помощью ДСЧ");
        }
        static void PrintMatrMenu()
        {
            Console.WriteLine("1. Создать матрицу" +
                "\n2. Напечатать матрицу" + "\n3. Удалить первый столбец с минимальным элементом" + "\n4. Назад");
        }
        static void PrintLineMenu()
        {
            Console.WriteLine("1. Создать строку" + "\n2. Напечатать строку" + "\n3. Добавть символы в уже существующую строку" +
                "\n4. Поменять местами первое и последнее слово в строке" + "\n5. Назад");
        }
        static void PrintCreateLine()
        {
            Console.WriteLine("1. Ввести строку вручную" +
                "\n2. Создать строку с помощью ДСЧ");
        }
        #endregion
        #region Ввод чисел
        /// <summary>
        /// Ввод целого числа из заданного диапазона
        /// </summary>
        /// <param name="min">Левая граница диапазона</param>
        /// <param name="max">Правая граница диапазона</param>
        /// <param name="msg">Приглашение для ввода</param>
        /// <returns></returns>
        static int GetInt(int min, int max, string msg = "")
        {
            int number;
            bool isRead;
            do
            {
                Console.WriteLine(msg);
                isRead = int.TryParse(Console.ReadLine(), out number);
                if (!isRead)
                {
                    Console.WriteLine("Ошибка! Не правильно введено целое число!");
                }
                else
                {
                    if (number < min || number > max)
                    {
                        Console.WriteLine("Ошибка! Число не попадает в заданный диапазон!");
                        isRead = false;
                    }
                }
            } while (!isRead);
            return number;
        }
        #endregion
        #region Матрица
        /// <summary>
        /// Ввод количества строк
        /// </summary>
        /// <returns></returns>
        static int GiveNumberStr()
        {
            int chooseWayFilling = GetInt(1, 2, $"Выберите, сколько будет строк (от {MINLENGHT} до {MAXLENGHT}):" +
                $"\n1. Ввести число самому" + "\n2. Случайное число"); ;
            if (chooseWayFilling == 1)
                return GetInt(MINLENGHT, MAXLENGHT, $"Введите количество строк (от {MINLENGHT} до {MAXLENGHT})");
            return rnd.Next(MINLENGHT, MAXLENGHT + 1);
        }
        /// <summary>
        /// Ввод количества столбцов в матрице
        /// </summary>
        /// <returns></returns>
        static int GiveNumberCol()
        {
            int chooseWayFilling = GetInt(1, 2, $"Выберите, сколько будет столбцов в матрице:" +
               $"\n1. Ввести число самому (от {MINLENGHT} до {MAXVALUE})" + "\n2. Случайное число"); ;
            if (chooseWayFilling == 1)
                return GetInt(MINLENGHT, MAXLENGHT, $"Введите количество столбцов в матрице (от {MINLENGHT} до {MAXLENGHT})");
            return rnd.Next(MINLENGHT, MAXLENGHT + 1);
        }
        /// <summary>
        /// Создание матрицы с помощью ДСЧ
        /// </summary>
        /// <returns></returns>
        static int[,] CreateRandomMatr()
        {
            int str = GiveNumberStr();
            int col = GiveNumberCol();
            int[,] matr = new int[str, col];
            for (int i = 0; i < str; i++)
                for (int j = 0; j < col; j++)
                    matr[i, j] = rnd.Next(MINVALUE, MAXVALUE + 1);
            return matr;
        }
        /// <summary>
        /// Создание матрицы вручную
        /// </summary>
        /// <returns></returns>
        static int[,] CreateMatr()
        {
            int str = GiveNumberStr();
            int col = GiveNumberCol();
            int[,] matr = new int[str, col];
            for (int i = 0; i < str; i++)
                for (int j = 0; j < col; j++)
                    matr[i, j] = GetInt(MINVALUE, MAXVALUE, $"Введите значение массива в {i + 1} строке, " +
                        $"в {j + 1} столбце (от {MINVALUE} до {MAXVALUE})");
            return matr;
        }
        /// <summary>
        /// Проверка матрицы на пустоту
        /// </summary>
        /// <param name="matr">Матрица, которую надо проверить на пустоту</param>
        /// <returns></returns>
        static bool IsEmpty(int[,] matr)
        {
            if (matr == null || matr.Length == 0)
                return true;
            return false;
        }
        /// Вывод двумерного массива
        /// </summary>
        /// <param name="matr">Выводимая матрица</param>
        /// <param name="msg">Сообщение при выводе</param>
        static void PrintMatr(int[,] matr, string msg = "")
        {
            Console.WriteLine(msg);
            if (IsEmpty(matr))
            {
                Console.WriteLine("Матрица пустая");
                return;
            }
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    Console.Write($"{matr[i, j],4}");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Поиск минимального элемента в матрице
        /// </summary>
        /// <param name="matr">Матрица, где ищем минимальный элемент</param>
        /// <param name="colMinNumber">Первый столбец с минимальным элементов матрицы</param>
        /// <returns></returns>
        static int SearchMinNumber(int[,] matr, ref int colMinNumber)
        {
            int minNuber = matr[0, 0];
            for (int i = 0; i < matr.GetLength(0); i++)
                for (int j = 0; j < matr.GetLength(1); j++)
                    if (matr[i, j] <= minNuber)
                    {
                        if (matr[i, j] < minNuber) colMinNumber = j + 1;
                        else if (j + 1 < colMinNumber) colMinNumber = j + 1;
                        minNuber = matr[i, j];
                    }
            return minNuber;
        }
        /// <summary>
        /// Удаление первого столбца с минимальным элементом из матрицы
        /// </summary>
        /// <param name="matr">Матрица, где удаляем первый столбец с минимальным элементом</param>
        static void DeleteFirstColMinMatr(ref int[,] matr)
        {
            if (matr.GetLength(1) == 1)
            {
                Console.WriteLine("Матрица стала пустой");
                matr = null;
                return;
            }
            int colMinNumber = 1;
            int minNuber = SearchMinNumber(matr, ref colMinNumber);
            Console.WriteLine("Минимальный элемент матрицы: " + minNuber + "\nПервый столбец с минимальным элементом: " + colMinNumber);
            int[,] temp = new int[matr.GetLength(0), matr.GetLength(1) - 1];
            for (int i = 0; i < matr.GetLength(0); i++)
                for (int j = 0; j < colMinNumber - 1; j++)
                {
                    temp[i, j] = matr[i, j];
                }
            if (colMinNumber != matr.GetLength(1))
                for (int i = 0; i < matr.GetLength(0); i++)
                    for (int j = colMinNumber - 1; j < matr.GetLength(1) - 1; j++)
                        temp[i, j] = matr[i, j + 1];
            matr = temp;
        }
        #endregion
        #region Работа со строкой
        /// <summary>
        /// Задать значение длины строки
        /// </summary>
        /// <returns></returns>
        static int GetSizeLine()
        {
            int choiceSize = GetInt(1, 2, "Хотите сами ввести длину строки самостоятельно или нет?\n1.Самостоятельно\n2.Случайное число");
            if (choiceSize == 1) return GetInt(MINLINELENGHT, MAXLINELENGHT, $"Введите число от {MINLINELENGHT} до {MAXLINELENGHT}");
            else return rnd.Next(MINLINELENGHT, MAXLINELENGHT + 1);
        }
        /// <summary>
        /// Создание строки с помощью ДСЧ
        /// </summary>
        /// <returns></returns>
        static string CreateRandomLine()
        {
            int size = GetSizeLine();
            string s = "";
            if (size == 2)
            {
                s += alphabet[rnd.Next(0, 128)];
                s += alphabet[rnd.Next(132, alphabet.Length)];
                return s;
            }
            s += alphabet[rnd.Next(0, 128)];
            string del = @"[.!?,;: ]";
            for (int i = 1; i < size - 2; i++)
            {
                if (Regex.IsMatch(s[i - 1].ToString(), del))
                {
                    if (s[i - 1] == ' ') s += alphabet[rnd.Next(0, 128)];
                    else s += ' ';
                }
                else s += alphabet[rnd.Next(0, alphabet.Length)];
            }
            s += alphabet[rnd.Next(0, 128)];
            s += alphabet[rnd.Next(132, alphabet.Length)];
            return s;

        }
        /// <summary>
        /// Проверка строки на правильность ввода
        /// </summary>
        /// <param name="s">Вводимая пользователем строка</param>
        /// <returns></returns>
        static bool IsCorrectLine(string s)
        {
            if (!(s.EndsWith(".") || s.EndsWith("!") || s.EndsWith("?")))
                return false;
            string del = @"[.!?,;:]";
            if (Regex.IsMatch(s[0].ToString(), del)) return false;
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (Regex.IsMatch(s[i].ToString(), del))
                {
                    if (s[i] == s[i + 1]) return false;
                    else if (s[i + 1] == ' ')
                    {
                        for (int j = i + 2; j < s.Length; j++)
                        {
                            if (Regex.IsMatch(s[j].ToString(), del)) return false;
                            else if (s[j] == ' ') continue;
                            else break;
                        }
                    }
                    else if (Regex.IsMatch(s[i + 1].ToString(), del)) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Проверить, если в массиве слов пустые слова после split 
        /// </summary>
        /// <param name="words">Массив слов</param>
        /// <returns></returns>
        static string[] DeleteEmptyWords(string[] words)
        {
            int countEmptyWords = 0;
            for (int i = 0; i < words.Length; i++) if (words[i].Length == 0) countEmptyWords++;
            if (countEmptyWords == 0) return words;
            int indexNewWords = 0;
            string[] newWords = new string[words.Length - countEmptyWords];
            for (int i = 0; i < words.Length; i++)
                if (words[i].Length != 0)
                {
                    newWords[indexNewWords] = words[i];
                    indexNewWords++;
                }
            return newWords;
        }
        /// <summary>
        /// Создать массив слов
        /// </summary>
        /// <param name = "s" > Обрабатываемая строка</param>
        /// <returns></returns>
        static string[] CreateArrayWords(string s)
        {
            s = s.Trim();
            string pattern = @"[ ,;:.!?]+";
            string[] arrayWords = Regex.Split(s, pattern);
            arrayWords = DeleteEmptyWords(arrayWords);
            return arrayWords;
        }
        /// <summary>
        /// Добавить символы в уже существующую строку 
        /// </summary>
        /// <param name="s">Существующая строка</param>
        static void AddSymbols(ref string s)
        {
            string newLine;
            PrintCreateLine();
            int choiceCreate = GetInt(1, 2, "Выберите пункт:");
            if (choiceCreate == 2)
            {
                newLine = CreateRandomLine();
                Console.WriteLine($"Вы добавили  {newLine} в конец строки");
                s += " " + newLine;
            }
            else
            {
                do
                {
                    Console.WriteLine("Введите символы, которые хотите добавить в конец строки:");
                    newLine = Console.ReadLine();
                    newLine = newLine.Trim();
                    if (newLine.Length == 0)
                    {
                        Console.WriteLine("Вы ничего не добавили");
                        return;
                    }
                    else if (IsCorrectLine(newLine))
                    {
                        Console.WriteLine($"Вы добавили  {newLine} в конец строки");
                        s += " " + newLine;
                        return;
                    }
                    else Console.WriteLine("Ваша строка неправильная, введите ее заново." +
                              "(Строка должна заканчивать одним из символов: \".!?\"." +
                              "Слова разделяются пробелами (пробелов может быть несколько) и знаками препинания \",;:\".)");
                } while (true);
            }
        }
        /// <summary>
        /// Поменять местами первое и последнее слово в массиве слов
        /// </summary>
        /// <param name="words">Массив слов</param>
        static void ChangeWord(ref string[] words)
        {
            string temp = words[^1];
            words[^1] = words[0];
            words[0] = temp;

        }
        /// <summary>
        /// Поменять местами первое и последнее слово в строке
        /// </summary>
        /// <param name="s">Изменяемая строка</param>
        /// <param name="words">Массив слов строки</param>
        static void ChangeLine(ref string s, string[] words)
        {
            int indexLastWord = s.LastIndexOf(words[^1]);
            int indextFirstWord = s.IndexOf(words[0]);
            s = s.Remove(indexLastWord, words[^1].Length);
            s = s.Insert(indexLastWord, words[0]);
            s = s.Remove(indextFirstWord, words[0].Length);
            s = s.Insert(indextFirstWord, words[^1]);
        }
        /// <summary>
        /// Составление новой строки
        /// </summary>
        /// <param name="s">изменяемая строка</param>
        static void CreateNewLine(ref string s)
        {
            string[] words = CreateArrayWords(s);
            if (words.Length != 1)
            {
                ChangeLine(ref s, words);
                Console.WriteLine($"В строке поменяли местами первое и последнее слово ({words[0]} и {words[^1]})");
                ChangeWord(ref words);
            }
            else Console.WriteLine("В вашей строке лишь одно слово");
        }
        #endregion
        static void Main(string[] args)
        {
            #region
            Boolean isCreateMatr = false, isCreateLine = false;
            int choiceCreate, choiceMenu, matrMenu, lineMenu;
            int[,] matr = null;
            string s = "";
            do
            {
                PrintMainMenu();
                choiceMenu = GetInt(1, 3, "Выберите пункт меню:");
                switch (choiceMenu)
                {
                    case 1://матрица
                        {
                            do
                            {
                                PrintMatrMenu();
                                matrMenu = GetInt(1, 4, "Выберите пункт:");
                                switch (matrMenu)
                                {
                                    case 1://создание массива
                                        {
                                            PrintCreateMenu();
                                            choiceCreate = GetInt(1, 2, "Выберите пункт:");
                                            if (choiceCreate == 1) matr = CreateMatr();
                                            else matr = CreateRandomMatr();
                                            isCreateMatr = true;
                                            Console.WriteLine("Вы сформировали матрицу целых чисел");
                                            break;
                                        }
                                    case 2://печать матрицы
                                        {
                                            if (!isCreateMatr) Console.WriteLine("Вы не создали матрицу");
                                            else PrintMatr(matr);
                                            break;
                                        }
                                    case 3://удаление первого столбца с минимальным элементом
                                        {

                                            if (isCreateMatr)
                                            {
                                                if (!IsEmpty(matr))
                                                {
                                                    DeleteFirstColMinMatr(ref matr);
                                                    Console.WriteLine("Из матрицы удален первый столбец с минимальным элементом.");
                                                }
                                                else Console.WriteLine("Матрица пуста, нельзя удалить столбец.");
                                            }
                                            else Console.WriteLine("Вы не создали матрицу");
                                            break;
                                        }
                                    case 4://выход 
                                        {
                                            break;
                                        }
                                }
                            } while (matrMenu != 4);
                            break;
                        }
                    case 2://строки
                        {
                            do
                            {
                                PrintLineMenu();
                                lineMenu = GetInt(1, 5, "Выберите пункт:");
                                switch (lineMenu)
                                {
                                    case 1://создание строки
                                        {
                                            PrintCreateLine();
                                            choiceCreate = GetInt(1, 2, "Выберите пункт:");
                                            if (choiceCreate == 1)
                                            {
                                                do
                                                {
                                                    isCreateLine = false;
                                                    Console.WriteLine("Введите строку:");
                                                    s = Console.ReadLine();
                                                    s = s.Trim();
                                                    if (s.Length != 0)
                                                    {
                                                        if (IsCorrectLine(s)) isCreateLine = true;
                                                        else Console.WriteLine("Ваша строка неправильная, введите ее заново." +
                                                            "(Строка должна заканчивать одним из символов: \".!?\"." +
                                                            "Слова разделяются пробелами (пробелов может быть несколько) и знаками препинания \",;:\".)");
                                                    }
                                                    else Console.WriteLine("Строка пустая, следует создать новую строку.");

                                                } while (!isCreateLine);
                                            }
                                            else
                                            {
                                                s = CreateRandomLine();
                                                isCreateLine = true;
                                            }
                                            break;
                                        }
                                    case 2://печать строки
                                        {
                                            if (isCreateLine) Console.WriteLine(s);
                                            else Console.WriteLine("Вы не создали строку");
                                            break;
                                        }
                                    case 3://добавить символы в уже существующую строчку
                                        {
                                            if (isCreateLine) AddSymbols(ref s);
                                            else Console.WriteLine("Вы не создали строку");
                                            break;
                                        }
                                    case 4://поменять местами первое и последнее слово в строке 
                                        {
                                            if (isCreateLine) CreateNewLine(ref s);
                                            else Console.WriteLine("Вы не создали строку");
                                            break;
                                        }
                                    case 5://выход 
                                        {
                                            break;
                                        }
                                }
                            } while (lineMenu != 5);
                            break;
                        }
                    case 3://выход
                        {
                            break;
                        }
                }
            } while (choiceMenu != 3);
            #endregion
        }
    }
}
