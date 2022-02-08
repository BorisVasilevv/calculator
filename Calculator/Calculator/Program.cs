using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    class MenuItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsChoosen { get; set; }
    }





    class Program
    {
        const int RomanNumberSystemIndex = 0;
        const int NumberSystemsIndex = 1;
        const int MathOperationsIndex = 2;
        const int ExitIndex = 3;

        const int AmountOfMenuItems = 4;

        const int ArabicToRomeIdIndex = 0;
        const int RomeToArabicIdIndex = 1;


        static void Main(string[] args)
        {
            int chooseNumberId = 0;
            do
            {
                Console.CursorVisible = false;
                ClearConsole();
                CreateMenu(chooseNumberId);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        chooseNumberId = ChangeElementToUp(chooseNumberId);
                        break;
                    case ConsoleKey.DownArrow:
                        chooseNumberId = ChangeElementToDown(chooseNumberId);
                        break;
                    case ConsoleKey.Enter:
                        if (chooseNumberId == ExitIndex) return;
                        DoAction(chooseNumberId);
                        break;
                    default:
                        CreateMenu(chooseNumberId);
                        break;
                }
            } while (true);
        }

        static void CreateMenu(int chooseNumber)
        {
            List<MenuItem> list = new List<MenuItem>();
            list.Add(new MenuItem() { Id = RomanNumberSystemIndex, Name = "Римская система" });
            list.Add(new MenuItem() { Id = NumberSystemsIndex, Name = "Перевод из одной системы счисления в другую" });
            list.Add(new MenuItem() { Id = MathOperationsIndex, Name = "Математические операции в различных сиситемах счисления" });
            list.Add(new MenuItem() { Id = ExitIndex, Name = "Выйти" });

            foreach (MenuItem item in list)
                if (item.Id == chooseNumber)
                    item.IsChoosen = true;
            DrawMenu(list);
        }

        static void DrawMenu(List<MenuItem> menuItems)
        {
            Console.WriteLine("Главное меню");
            foreach (MenuItem item in menuItems)
            {
                if (item.IsChoosen) Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(item.Name);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        static int ChangeElementToUp(int oldChange)
        {
            int newChange = oldChange - 1;
            if (newChange < 0)
                newChange += AmountOfMenuItems;
            return newChange;
        }

        static int ChangeElementToDown(int oldChange)
        {
            int newChange = oldChange + 1;
            if (newChange >= AmountOfMenuItems)
                newChange -= AmountOfMenuItems;
            return newChange;
        }

        static void ClearConsole()
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, i);
            }
            Console.SetCursorPosition(0, 0);
        }

        static void DoAction(int numberOfAction)
        {
            switch (numberOfAction)
            {
                case RomanNumberSystemIndex:
                    RomeAndArabicNumberSystem();
                    break;
                case NumberSystemsIndex:
                    FromOneToAnotherNumberSystem();
                    break;
                case MathOperationsIndex:
                    MathOperations();
                    break;

            }
            // ReturnToMenuOrExit(numberOfAction);
        }

        //Возврат в главное меню
        public static void ReturnToMenu(int idOfAction)
        {
            Console.WriteLine("\nНажмите 1 чтобы перевести ещё одно число или нажмите любую другую клавишу для возврата в главное меню");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.KeyChar == '1')
            {
                if (idOfAction == ArabicToRomeIdIndex) FromArabicNumberSystemToRome();
                if (idOfAction == RomeToArabicIdIndex) FromRomeNumberSystemToArabic();
            }
            else return;
        }

        //Возврат в главное меню (2 вариант)
        /*static void ReturnToMenuOrExit(int choice)
        {
            Console.CursorVisible = true;
            Console.WriteLine("\nХотите продолжить?");
            Console.WriteLine("Введите да/yes, чтобы вернуться в главное меню");
            Console.WriteLine("Введите нет/no, чтобы завершить работу программы");
            bool answerIsCorrect = false;
            do
            {
                string answer = Console.ReadLine();
                answer = answer.ToLower();
                if ((answer == "да") || (answer == "yes"))
                {
                    Console.Clear();
                    CreateMenu(choice);
                    answerIsCorrect = true;
                }
                else if ((answer == "нет") || (answer == "no"))
                {
                    Environment.Exit(0);
                }
                else
                    Console.WriteLine("Введённый ответ не распознан");
            } while (!answerIsCorrect);
        }*/

        public static int NumberFromUser()
        {
            bool testPass;
            string answer;
            int number = 0;
            do
            {
                testPass = true;
                answer = Console.ReadLine();
                try
                {
                    number = Convert.ToInt32(answer);
                }
                catch
                {
                    Console.WriteLine("Введённое число некоректно, введите число ещё раз");
                    testPass = false;
                }
            } while (!testPass);
            return number;
        }

        static void MathOperations()
        {
            Console.Clear();
            Console.WriteLine("Введите систему счисления в которой будут производиться вычисления");
            int numberSystem = NumberOfSystemNumberFromUser();
            Dictionary<char, int> dictionary = CreateDictionaryToNumberSystems();
            WriteInstructionToDifferntSystemsNumber();
            Console.WriteLine("\nВведите первое число");
            string firstNumber = CorrectStringFromUserInNumberSystem(numberSystem, dictionary);
            Console.WriteLine("\nВведите второе число");
            string secondNumber = CorrectStringFromUserInNumberSystem(numberSystem, dictionary);
            Console.WriteLine("Нажмите +, чтобы сложить. Нажмите -, чтобы вычесть из первого второе. Нажмите *, чтобы перемножить числа");
            bool inputOK;
            do
            {
                inputOK = true;
                char operation = Console.ReadKey(true).KeyChar;
                if (operation == '+')
                {
                    FoldNumbers(numberSystem, firstNumber, secondNumber);
                }
                else if (operation == '-')
                {
                    SubtractNumbers(numberSystem, firstNumber, secondNumber);
                }
                else if (operation == '*')
                {
                    MultiplyNumbers(numberSystem, firstNumber, secondNumber);
                }
                else
                {
                    Console.WriteLine("Вы нажаль что-то не то, попробуйте ещё раз");
                    inputOK = false;
                }
            } while (!inputOK);
            Console.WriteLine("Нажмите любую клавишу для выхода в меню");
            Console.ReadKey(true);
        }

        static string FoldNumbers(int numberSystem, string firstNumber, string secondNumber)
        {
            while (firstNumber.Length != secondNumber.Length)
            {
                if (firstNumber.Length > secondNumber.Length)
                    secondNumber = "0" + secondNumber;
                else
                    firstNumber = "0" + firstNumber;
            }
            Dictionary<char, int> dictionary = CreateDictionaryToNumberSystems();
            char[] digits = CreateArrayToNumberSystems();
            string result = "";
            bool carringOver = false;
            for (int i = firstNumber.Length - 1; i >= 0; i--)
            {
                int digit = dictionary[firstNumber[i]] + dictionary[secondNumber[i]];
                if (carringOver)
                {
                    Console.WriteLine("Не забываем про перенос разряда");
                    Console.Write("1 + ");
                    digit++;
                    carringOver = false;
                }
                Console.Write($"{firstNumber[i]} + {secondNumber[i]} = {digit} = {digits[digit]} \n");

                if (digit >= numberSystem)
                {
                    Console.WriteLine($"Но цифра {digits[digit]} не может быть использована в этой системе счисления.\nПроизойдёт перенос");
                    Console.Write($"Вместо {digits[digit]} = {digit} на этом разряде будет {digit}-{numberSystem}. ");
                    carringOver = true;

                    digit -= numberSystem;
                    Console.WriteLine($"То есть {digits[digit]}");

                }
                result = digits[digit] + result;
                if (carringOver && i == 0)
                    result = '1' + result;

            }
            Console.WriteLine("Записываем цифры, идущие после \"=\" в обратном порядке");
            Console.WriteLine($"В итоге получим {result}");
            return result;
        }


        //result = differenceFrom - difference
        static void SubtractNumbers(int numberSystem, string firstNumber, string secondNumber)
        {

            while (firstNumber.Length != secondNumber.Length)
            {
                if (firstNumber.Length > secondNumber.Length)
                    secondNumber = "0" + secondNumber;
                else
                    firstNumber = "0" + firstNumber;
            }

            bool writeMinuc = false;
            string differenceFrom = firstNumber;
            string difference = secondNumber;
            if (ConversionToTenNumberSystem(numberSystem, firstNumber) < ConversionToTenNumberSystem(numberSystem, secondNumber))
            {
                writeMinuc = true;
                differenceFrom = secondNumber;
                difference = firstNumber;
                Console.WriteLine("Так как модуль второго числа больше модуля первого, " +
                    $"\nвычтем из модуля втрого модуль первого и напишем - перед числом: -({differenceFrom} - {difference})");
            }

            Dictionary<char, int> dictionary = CreateDictionaryToNumberSystems();
            char[] digits = CreateArrayToNumberSystems();

            string result = "";
            bool carringLess = false;
            for (int i = differenceFrom.Length - 1; i >= 0; i--)
            {

                int digit = dictionary[differenceFrom[i]] - dictionary[difference[i]];
                if (carringLess)
                {
                    Console.WriteLine("Не забываем про заём разряда");
                    Console.Write("-1 + ");
                    digit--;
                    carringLess = false;
                }
                if (digit < 0)
                {
                    Console.Write($"{differenceFrom[i]} - {difference[i]} = {digit} \n");
                    Console.WriteLine($"Но цифра получается меньше нуля. Произойдёт заём");
                    carringLess = true;
                    if (digit + numberSystem < 10)
                        Console.WriteLine($"{digit} + {numberSystem} = {digits[digit + numberSystem]}");
                    else
                        Console.WriteLine($"{digit} + {numberSystem} = {digit + numberSystem} = {digits[digit + numberSystem]}");
                    digit += numberSystem;
                }
                else
                {
                    if (digit < 10)
                        Console.WriteLine($"{differenceFrom[i]} - {difference[i]} = {digit}");
                    else
                        Console.WriteLine($"{differenceFrom[i]} - {difference[i]} = {digit} = {digits[digit]}");
                }
                Console.WriteLine();
                result = digits[digit] + result;
            }
            while ((result[0] == '0') && result.Length != 1)
                result = result.Substring(1);
            if (writeMinuc)
                result = "-" + result;
            Console.WriteLine("Записываем цифры, идущие после \"=\" в обратном порядке");
            Console.WriteLine($"В итоге получим {result}");
        }


        static void MultiplyNumbers(int numberSystem, string firstNumber, string secondNumber)
        {
            Console.Clear();
            WriteTableToDifferntSystemsNumber();
            Console.WriteLine();


            string shortNumber;
            string longNumber;

            if (firstNumber.Length >= secondNumber.Length)
            {
                shortNumber = secondNumber;
                longNumber = firstNumber;
            }
            else
            {
                shortNumber = firstNumber;
                longNumber = secondNumber;
            }

            List<string> numbers = new List<string>();
            Dictionary<char, int> dictionary = CreateDictionaryToNumberSystems();
            char[] digits = CreateArrayToNumberSystems();
            for (int i = shortNumber.Length - 1; i >= 0; i--)
            {
                int carringOverCounter = 0;
                string intermediateResult = "";
                Console.WriteLine($"Умножаем {longNumber} на {shortNumber[i]}");
                for (int j = longNumber.Length - 1; j >= 0; j--)
                {
                    int twoDigitProduct = dictionary[shortNumber[i]] * dictionary[longNumber[j]];
                    if (carringOverCounter > 0)
                    {
                        Console.WriteLine("Не забывем про переносы");
                        Console.Write($"{carringOverCounter} + ");
                        twoDigitProduct += carringOverCounter;
                        carringOverCounter = 0;
                    }
                    Console.WriteLine($"{longNumber[j]} * {shortNumber[i]} = {twoDigitProduct}");
                    if (twoDigitProduct >= numberSystem)
                    {
                        Console.Write($"{twoDigitProduct} не может использовться в этой системе отчёта. ");
                    }
                    string onlyForWrite = twoDigitProduct.ToString();
                    while (twoDigitProduct >= numberSystem)
                    {

                        twoDigitProduct -= numberSystem;
                        carringOverCounter++;
                    }
                    if (carringOverCounter != 0)
                    {
                        Console.WriteLine($"Произойдут переносы в количестве {carringOverCounter} штук");
                        Console.Write($"За каждый перенос вычитаем основание системы счисления, " +
                            $"получим в итоге {onlyForWrite}-{numberSystem}*{carringOverCounter} = ");
                        if (twoDigitProduct < 10)
                            Console.WriteLine($"{twoDigitProduct}\n");
                        else
                            Console.WriteLine($"{twoDigitProduct} = {digits[twoDigitProduct]}\n");
                    }
                    intermediateResult = digits[twoDigitProduct] + intermediateResult;
                    if ((j == 0) && (carringOverCounter > 0))
                    {
                        Console.WriteLine("Не забываем дописать переносы в начало числа (" + carringOverCounter.ToString() + ")");
                        intermediateResult = carringOverCounter.ToString() + intermediateResult;
                    }
                }
                if (intermediateResult != "0")
                    for (int k = 0; k < shortNumber.Length - 1 - i; k++)
                        intermediateResult += "0";
                Console.WriteLine("Записываем цифры, идущие после \"=\" в обратном порядке");
                Console.WriteLine("Итого: " + intermediateResult, Console.ForegroundColor = ConsoleColor.DarkGreen);
                Console.ForegroundColor = ConsoleColor.White;
                numbers.Add(intermediateResult);
            }
            Summ(numbers, numberSystem);
        }

        static void Summ(List<string> numbers, int numberSystem)
        {
            Console.WriteLine("Осталось только сложить полученные числа");
            string result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                Console.WriteLine($"Складываем {result} и {numbers[i]}");
                result = FoldNumbers(numberSystem, result, numbers[i]);
            }
            Console.WriteLine($"\nКонечный результат: {result}\n");
        }



        //Методы перевода из одной системы счисления в другую
        static void FromOneToAnotherNumberSystem()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Перевод из одной в другую систему счисления! Системы счисления не более 50 и не менее 2");
            Console.WriteLine("Введите из какой системы счисления переводим");
            int numberSystemFrom = NumberOfSystemNumberFromUser();
            //Console.Clear();
            Console.WriteLine("Система счисления сохранена");
            Dictionary<char, int> dictionary = CreateDictionaryToNumberSystems();
            WriteInstructionToDifferntSystemsNumber();
            Console.WriteLine("Введите число, которое хотите перевести");
            string strNumber = CorrectStringFromUserInNumberSystem(numberSystemFrom, dictionary);
            Console.WriteLine("Число сохранено");
            Console.WriteLine("Введите систему счисления в которую осуществляется перевод");
            int numberSystemTo = NumberOfSystemNumberFromUser();
            int numberInTenSystem;
            if (numberSystemFrom == 10)
            {
                Console.WriteLine("Число изначално находиться в 10-ичной системе счсления переходим ко 2 этапу");
                numberInTenSystem = Convert.ToInt32(strNumber);
            }
            else
            {
                numberInTenSystem = ConversionToTenNumberSystem(numberSystemFrom, strNumber, dictionary);
            }
            string result;
            if (numberSystemTo == 10)
            {
                Console.WriteLine("Если система счисления, в которую нам необходимо перевести десятичной,\nто полученный результат будет являться конечным резльтатом");
                result = numberInTenSystem.ToString();
            }
            else
            {
                result = ConversionFromTenNumberSystem(numberInTenSystem, numberSystemTo);
            }
            Console.WriteLine($"\n{strNumber} в {numberSystemFrom} системе счисления равно {result} в {numberSystemTo} системе счисления");
            Console.WriteLine("\nНажмите любую клавишу для возврвта в главное меню");
            Console.ReadKey();
        }

        static int ConversionToTenNumberSystem(int numberSystem, string number, Dictionary<char, int> dictionary)
        {
            Console.WriteLine("Сначала переводим в десятичную систему счисления");
            int degree = 0;
            int result = 0;
            Console.WriteLine("Для этого необходимо найти сумму следующих чисел");
            for (int i = number.Length - 1; i >= 0; i--)
            {
                Console.WriteLine($"Берём цифру {number[i]}, ей соответствует число {dictionary[number[i]]}");
                if (i == number.Length - 1)
                    Console.WriteLine($"После чего умножаем это число на основание системы ({numberSystem}) в степени, зависящей от разряда цифры\n" +
                    $"для разряда едениц - 0, для разряда десятков - 1, для разряда сотен - 2,\nи так далее добавляем 1 для каждого последующего разряда");
                Console.WriteLine($"Получаем {dictionary[number[i]]} * {numberSystem}^{degree}");
                Console.WriteLine("Прибавляем полученное число к итоговому результату");
                result += dictionary[number[i]] * NumberInDegree(numberSystem, degree);

                degree++;
            }
            Console.WriteLine($"Итого получим {result}\n");
            return result;
        }

        static int ConversionToTenNumberSystem(int numberSystem, string number)
        {
            Dictionary<char, int> dictionary = CreateDictionaryToNumberSystems();
            int degree = 0;
            int result = 0;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                result += dictionary[number[i]] * NumberInDegree(numberSystem, degree);
                degree++;
            }
            return result;
        }

        static int NumberInDegree(int number, int degree)
        {
            if (degree == 0) return 1;
            int constNumber = number;
            for (int i = 0; i < degree - 1; i++)
                number *= constNumber;
            return number;
        }

        static string ConversionFromTenNumberSystem(int number, int newNumberSystem)
        {
            char[] digit = CreateArrayToNumberSystems();
            string invertedAnswer = "";
            Console.WriteLine("Для перевода из десятичной системы счисления в любую другую, существует следующий алгоритм");
            while (number != 0)
            {
                int numberOfChar = number % newNumberSystem;
                char partOfAnswer = digit[numberOfChar];
                Console.WriteLine($"Делим {number} на основание системы счисления ({newNumberSystem}) в которую мы хотим перевести");
                number = number / newNumberSystem;
                invertedAnswer += partOfAnswer.ToString();
                Console.WriteLine($"Получим целую часть - {number} и остаток - {numberOfChar}");
            }

            string answer = "";
            for (int i = invertedAnswer.Length - 1; i >= 0; i--)
            {
                answer += invertedAnswer[i].ToString();
            }
            Console.WriteLine($"Записывем все полученные остатки в обратном порядке. Получим - {answer}");
            return answer;
        }


        static int NumberOfSystemNumberFromUser()
        {
            int numberSystem;
            do
            {
                numberSystem = NumberFromUser();
                if (numberSystem > 50)
                    Console.WriteLine("Система счисления должна быть не более 50");
                if (numberSystem < 2)
                    Console.WriteLine("Система счисления должна быть не менее 2");
                if (numberSystem < 2 || numberSystem > 50)
                    Console.WriteLine("Введите систему счисления ещё раз");

            } while (numberSystem < 2 || numberSystem > 50);
            return numberSystem;
        }

        static void WriteInstructionToDifferntSystemsNumber()
        {
            Console.WriteLine("Используйте символы, представленные ниже");
            Console.WriteLine("Помните о том, что например в 19-ричной системе счисления нельзя использовать симовол J и все последующие");
            WriteTableToDifferntSystemsNumber();
        }

        static void WriteTableToDifferntSystemsNumber()
        {
            Dictionary<char, int> dictionary = CreateDictionaryToNumberSystems();
            foreach (char key in dictionary.Keys)
            {
                Console.Write($" {key} - {dictionary[key]} ");
                if (key == '9' || key == 'J' || key == 'T' || key == 'Ё' || key == 'Ю')
                    Console.WriteLine();
            }
        }

        static string CorrectStringFromUserInNumberSystem(int numberSystem, Dictionary<char, int> dictionary)
        {
            string strNumber;
            bool testPass;
            do
            {
                testPass = true;
                strNumber = Console.ReadLine().ToUpper();
                foreach (char ch in strNumber)
                {
                    bool charIsOk = true;
                    try
                    {
                        int x = dictionary[ch];
                    }
                    catch
                    {
                        charIsOk = false;
                        testPass = false;
                        Console.WriteLine("Ошибка! Введённая строка некорректна");
                    }
                    if ((charIsOk) && (dictionary[ch] >= numberSystem))
                    {
                        testPass = false;
                        Console.WriteLine("Ошибка! Хотя бы один из символов не может использоваться в этой системе счисления");
                    }
                    if (!testPass) break;
                }
                if (!testPass) Console.WriteLine("Введите число ещё раз");
            } while (!testPass);
            return strNumber;
        }

        static Dictionary<char, int> CreateDictionaryToNumberSystems()
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            for (int i = 0; i < 10; i++)
                dictionary.Add(i.ToString()[0], i);
            dictionary.Add('A', 10);
            dictionary.Add('B', 11);
            dictionary.Add('C', 12);
            dictionary.Add('D', 13);
            dictionary.Add('E', 14);
            dictionary.Add('F', 15);
            dictionary.Add('G', 16);
            dictionary.Add('H', 17);
            dictionary.Add('I', 18);
            dictionary.Add('J', 19);
            dictionary.Add('K', 20);
            dictionary.Add('L', 21);
            dictionary.Add('M', 22);
            dictionary.Add('N', 23);
            dictionary.Add('O', 24);
            dictionary.Add('P', 25);
            dictionary.Add('Q', 26);
            dictionary.Add('R', 27);
            dictionary.Add('S', 28);
            dictionary.Add('T', 29);
            dictionary.Add('U', 30);
            dictionary.Add('V', 31);
            dictionary.Add('W', 32);
            dictionary.Add('X', 33);
            dictionary.Add('Y', 34);
            dictionary.Add('Z', 35);
            dictionary.Add('Б', 36);
            dictionary.Add('Г', 37);
            dictionary.Add('Д', 38);
            dictionary.Add('Ё', 39);
            dictionary.Add('Ж', 40);
            dictionary.Add('З', 41);
            dictionary.Add('И', 42);
            dictionary.Add('Й', 43);
            dictionary.Add('Л', 44);
            dictionary.Add('П', 45);
            dictionary.Add('Ф', 46);
            dictionary.Add('Ц', 47);
            dictionary.Add('Ч', 48);
            dictionary.Add('Ю', 49);
            return dictionary;
        }

        static char[] CreateArrayToNumberSystems()
        {
            return new char[]
            {
                '0','1','2','3','4','5','6','7','8','9',
                'A','B','C','D','E','F','G','H','I','J',
                'K','L','M','N','O','P','Q','R','S','T',
                'U','V','W','X','Y','Z','Б','Г','Д','Ё',
                'Ж','З','И','Й','Л','П','Ф','Ц','Ч','Ю'
            };
        }

        //Блок работа с римскими цифрами
        public static void RomeAndArabicNumberSystem()
        {
            Console.Clear();
            Console.WriteLine("Нажмите 1, чтобы перевести из римского числа в арабское");
            Console.WriteLine("Нажмите 2, чтобы перевести из арабского числа в римское");
            Console.WriteLine("Нажмите любую другую клавишу, чтобы вернуться в главное меню");
            char key = Console.ReadKey(true).KeyChar;
            if (key == '1') FromRomeNumberSystemToArabic();
            else if (key == '2') FromArabicNumberSystemToRome();
            else return;

        }

        public static void FromRomeNumberSystemToArabic()
        {
            Console.Clear();
            Console.CursorVisible = true;
            WriteInstructionRomeDigits();
            Console.WriteLine("Введите Римское число, которое хотите перевести");
            bool correctChars;
            bool correctEntry = false;
            string romeNumber;
            do
            {
                romeNumber = Console.ReadLine();
                correctChars = CorrectCharsInNumberTest(romeNumber);
                if (!correctChars) Console.WriteLine("Ошибка! Используйте только те буквы, которые указаны выше");
                if (correctChars) correctEntry = TestOnCorrectNumber(romeNumber);
                if (!correctEntry) Console.WriteLine("Ошибка! Введённое число не является римской цифрой.Введите число ещё раз");
            } while ((!correctChars) || (!correctEntry));

            int result = 0;
            int[] number = new int[romeNumber.Length];
            if (romeNumber.Length == 1) result = TranslateFromRomeDigit(romeNumber[0]);
            else
            {
                for (int i = 0; i < number.Length; i++)
                    number[i] = TranslateFromRomeDigit(romeNumber[i]);

                for (int i = 0; i < number.Length; i++)
                {
                    if ((i + 1 < number.Length) && (number[i] < number[i + 1]))
                    {
                        result = result + number[i + 1] - number[i];
                        i++;
                    }
                    else
                    {
                        result += number[i];
                    }
                }
            }
            Console.WriteLine(result);
            ReturnToMenu(RomeToArabicIdIndex);
        }

        static bool CorrectCharsInNumberTest(string romeNumber)
        {
            if (romeNumber == "") return false;
            romeNumber = romeNumber.ToLower();
            foreach (char ch in romeNumber)
            {
                int number = TranslateFromRomeDigit(ch);
                if (number == 0) return false;
            }
            return true;
        }

        static bool TestOnCorrectNumber(string romeNumber)
        {
            romeNumber = romeNumber.ToLower();
            //В римской записи числа не может быть два раза подряд меньшей цифры перед большой цифрой. Например чисел DXXL, IXC не может быть
            for (int i = 0; i < romeNumber.Length - 2; i++)
            {
                if (TranslateFromRomeDigit(romeNumber[i]) <= TranslateFromRomeDigit(romeNumber[i + 1])
                    && TranslateFromRomeDigit(romeNumber[i + 1]) < TranslateFromRomeDigit(romeNumber[i + 2]))
                {
                    return false;
                }
            }
            //4 цифры не могут идти подряд
            for (int i = 0; i < romeNumber.Length - 3; i++)
            {
                if (romeNumber[i] == romeNumber[i + 1] && romeNumber[i + 2] == romeNumber[i + 3] && romeNumber[i] == romeNumber[i + 2])
                    return false;
            }
            for (int i = 0; i < romeNumber.Length - 1; i++)
            {
                //Не может быть двух подряд идущих V, C и других символов, которые равны 5*10^n
                int number = (TranslateFromRomeDigit(romeNumber[i]));
                number = number % 10;
                if (number == 5 && romeNumber[i] == romeNumber[i + 1])
                    return false;
                //У римлян можно вычитать только числа в 5 или в 10 раз меньшие  
                int number1 = TranslateFromRomeDigit(romeNumber[i]);
                int number2 = TranslateFromRomeDigit(romeNumber[i + 1]);
                if (number2 > number1)
                {
                    int helpNumber = number2;
                    while (helpNumber > 10)
                        helpNumber = helpNumber / 10;                        //нельзя из 500 вычесть 50
                    if (!(number2 / 5 == number1 || (number2 / 10 == number1 && helpNumber == 10)))
                        return false;
                }

            }
            //Нельзя число в виде LCL
            for (int i = 0; i < romeNumber.Length - 2; i++)
                if (romeNumber[i] == romeNumber[i + 2]
                    && TranslateFromRomeDigit(romeNumber[i + 1]) > TranslateFromRomeDigit(romeNumber[i]))
                    return false;
            return true;
        }

        public static void FromArabicNumberSystemToRome()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Введите число от 1 до 10000 для перевода в римскую систему счисления");

            int number;
            do
            {
                number = NumberFromUser();
                if (number > 10000)
                    Console.WriteLine("Число не может быть более 10000");
            } while (number > 10000);


            if (number == 0)
                Console.WriteLine("У римлян не было 0");
            if (number == 10000)
                Console.WriteLine("K");


            StringBuilder sb = new StringBuilder();
            //Считаем по десяткам
            int numberOfLetter = 0;
            string stringNumber = number.ToString();
            for (int degreeOfTen = stringNumber.Length - 1; degreeOfTen >= 0; degreeOfTen--)
            {
                string partOfRomeNumber = TranslateToRomeDigit(Int32.Parse(stringNumber[numberOfLetter].ToString()), degreeOfTen);
                sb.Append(partOfRomeNumber);
                numberOfLetter++;
            }
            Console.WriteLine(sb.ToString());
            WriteInstructionRomeDigits();
            ReturnToMenu(ArabicToRomeIdIndex);
        }

        static string TranslateToRomeDigit(int digit, int degreOfTen)
        {
            digit = digit % 10;
            char[] romeDigit = new char[3];
            if (degreOfTen == 0)
            {
                romeDigit[0] = 'I';
                romeDigit[1] = 'V';
                romeDigit[2] = 'X';
            }
            if (degreOfTen == 1)
            {
                romeDigit[0] = 'X';
                romeDigit[1] = 'L';
                romeDigit[2] = 'C';
            }
            if (degreOfTen == 2)
            {
                romeDigit[0] = 'C';
                romeDigit[1] = 'D';
                romeDigit[2] = 'M';
            }
            if (degreOfTen == 3)
            {
                romeDigit[0] = 'M';
                romeDigit[1] = 'S';
                romeDigit[2] = 'K';
            }
            switch (digit)
            {
                case 1: return romeDigit[0].ToString();
                case 2: return new string(romeDigit[0], 2);
                case 3: return new string(romeDigit[0], 3);
                case 4: return romeDigit[0].ToString() + romeDigit[1].ToString();
                case 5: return romeDigit[1].ToString();
                case 6: return romeDigit[1].ToString() + romeDigit[0].ToString();
                case 7: return romeDigit[1].ToString() + new string(romeDigit[0], 2);
                case 8: return romeDigit[1].ToString() + new string(romeDigit[0], 3);
                case 9: return romeDigit[0].ToString() + romeDigit[2].ToString();
                default: return ""; //если 0
            }
        }

        static void WriteInstructionRomeDigits()
        {
            Console.WriteLine("Правила написания риских цифр можно найти по ссылке: https://www.latinpro.info/latin_cifrae.php");
            Console.WriteLine("Используемые символы");
            Console.WriteLine($"I-{TranslateFromRomeDigit('i')}  " +
                $"V-{TranslateFromRomeDigit('v')}  " +
                $"X-{TranslateFromRomeDigit('x')}  " +
                $"L-{TranslateFromRomeDigit('l')}\n" +
                $"C-{TranslateFromRomeDigit('c')}  " +
                $"D-{TranslateFromRomeDigit('d')}  " +
                $"M-{TranslateFromRomeDigit('m')}\n" +
                $"S-{TranslateFromRomeDigit('s')} соответствует древнеримской V с чертой\n" +
                $"K-{TranslateFromRomeDigit('k')} соответствует древнеримской X с чертой\n");

        }

        static int TranslateFromRomeDigit(char romeDigit)
        {
            switch (romeDigit.ToString().ToLower()[0])
            {
                case 'i': return 1;
                case 'v': return 5;
                case 'x': return 10;
                case 'l': return 50;
                case 'c': return 100;
                case 'd': return 500;
                case 'm': return 1000;
                case 's': return 5000;
                case 'k': return 10000;
            }
            return 0;

        }
    }
}
