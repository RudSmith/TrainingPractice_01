using System;

namespace RIM_Task_06
{
    class RIM_Task_06
    {
        // Функция для вывода меню
        static void _outputMenu()
        {
            Console.Clear();
            Console.WriteLine("Меню: ");
            Console.WriteLine("1. Добавить досье.");
            Console.WriteLine("2. Удалить досье.");
            Console.WriteLine("3. Просмотреть все досье.");
            Console.WriteLine("4. Найти досье по фамилии.");
            Console.WriteLine("5. Выход.");
        }

        // Функция для ввода выбора пользователя и проверки правильности этого ввода
        static int _inputChoice()
        {
            Console.WriteLine("Введите выбранный пункт: ");
            if (Int32.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 5)
            {
                return choice;
            }
            return 0;
        }

        // Функция для определения, какое действие необходимо совершить
        static void _getActionByChoice(int choice, ref string [] names, ref int namesSize, 
            ref string[] positions, ref int positionsSize, ref bool isExitPressed)
        {
            // В блоке switch-case определяем, что нужно сделать
            switch(choice)
            {
                case 1:
                    _addDossier(ref names, ref namesSize, ref positions, ref positionsSize);
                    break;
                case 2:
                    _deleteDossier(ref names, ref namesSize, ref positions, ref positionsSize);
                    break;
                case 3:
                    _showAllDossiers(ref names, ref namesSize, ref positions, ref positionsSize);
                    break;
                case 4:
                    _findDossierByFirstName(ref names, ref namesSize, ref positions, ref positionsSize);
                    break;
                case 5:
                    isExitPressed = true;
                    break;
                // Случай неверного ввода обрабатывается
                default:
                    Console.WriteLine("Неверный ввод.");
                    System.Threading.Thread.Sleep(2000);
                    break;

            }
        }

        // Функция для добавления досье
        static void _addDossier(ref string[] names, ref int namesSize, ref string[] positions, ref int positionsSize)
        {
            // Реаллоцируем память для массива имён (делаем его размер на единицу больше)
            string[] newNames = new string[namesSize + 1];

            for (int iter = 0; iter < namesSize; ++iter)
                newNames[iter] = names[iter];

            names = new string[namesSize + 1];
            names = newNames;
            ++namesSize;

            // Вводим ФИО (я всё кладу в одну и ту же строку с помощью конкатенации)
            Console.WriteLine("Введите фамилию:");
            names[namesSize - 1] = Console.ReadLine() + " ";
            Console.WriteLine("Введите имя:");
            names[namesSize - 1] += Console.ReadLine() + " ";
            Console.WriteLine("Введите отчество:");
            names[namesSize - 1] += Console.ReadLine() + " ";

            // Ниже реаллоцируем память для массива должностей
            string[] newPositions = new string[positionsSize + 1];

            for (int iter = 0; iter < positionsSize; ++iter)
                newPositions[iter] = positions[iter];

            positions = new string[positionsSize + 1];
            positions = newPositions;
            ++positionsSize;

            // Вводим должность
            Console.WriteLine("Введите должность:");
            positions[positionsSize - 1] = Console.ReadLine();
        }

        // Функция для удаления досье
        static void _deleteDossier(ref string[] names, ref int namesSize, ref string[] positions, ref int positionsSize)
        {
            // Если размер массивов досье и имён равен нулю, ничего не делаем
            if(namesSize == 0 || positionsSize == 0)
                return;

            // Делаем те же трюки, что и в функции с добавлением досье
            // Реаллоцируем память, копируем старый массив в новый и приравниваем старый к новому
            string[] newNames = new string[namesSize - 1];

            for (int iter = 0; iter < namesSize - 1; ++iter)
                newNames[iter] = names[iter];

            names = new string[namesSize - 1];
            names = newNames;
            --namesSize;

            string[] newPositions = new string[positionsSize - 1];

            for (int iter = 0; iter < positionsSize - 1; ++iter)
                newPositions[iter] = positions[iter];

            positions = new string[positionsSize - 1];
            positions = newPositions;
            --positionsSize;
        }

        static void _showAllDossiers(ref string[] names, ref int namesSize, ref string[] positions, ref int positionsSize)
        {
            // Если список досье пуст - сообщаем об этом пользователю
            if(namesSize == 0)
                Console.WriteLine("Список досье пуст.");
            // Иначе - выводим все досье
            else
                for(int iter = 0; iter < namesSize; ++iter)
                    Console.WriteLine($"{ iter + 1 }. { names[iter] } - { positions[iter] }");

            Console.WriteLine("Для продолжения нажмите любую кнопку.");
            Console.ReadKey(true);
        }

        static void _findDossierByFirstName(ref string[] names, ref int namesSize, ref string[] positions, ref int positionsSize)
        {
            if (namesSize == 0)
                Console.WriteLine("Список досье пуст.");
            else
            {
                // Вводим фамилию, по которой производим поиск
                Console.WriteLine("Введите фамилию, по которой будет произведён поиск: ");
                string firstName = Console.ReadLine();
                // Объявляем булеву переменную, которая отслеживает, найдено ли хотя бы одно досье
                bool isFinded = false;

                for (int iter = 0; iter < namesSize; ++iter)
                {
                    // С помощью поиска подстроки в строке, находим досье, которые относятся к человеку с фамилией firstName
                    if (names[iter].Contains(firstName))
                    {
                        Console.WriteLine("Найдено досье: ");
                        Console.WriteLine($"{ iter + 1 }. { names[iter] } - { positions[iter] }");
                        isFinded = true;
                    }
                }

                // Если ничего не найдено, сообщаем об этом пользователю
                if (!isFinded)
                    Console.WriteLine("Досье не найдено.");
            }

            Console.WriteLine("Для продолжения нажмите любую кнопку.");
            Console.ReadKey(true);
        }

        static void Main(string[] args)
        {
            // Создаём два массива - имена и должности, пока что они будут нулевого размера
            int namesSize = 0;
            int positionsSize = 0;
            string[] names = new string[namesSize];
            string[] positions = new string[positionsSize];

            // Создаём булеву переменную для отслеживания того, нужно ли завершить программу
            bool isExitPressed = false;

            // Пока нам не нужно завершать программу, вводим действие с клавиатуры и вызываем функцию, в которой это дествие обрабатывается
            while(!isExitPressed)
            {
                _outputMenu();
                _getActionByChoice(_inputChoice(), ref names, ref namesSize, ref positions, ref positionsSize, ref isExitPressed);
            }
        }
    }
}
