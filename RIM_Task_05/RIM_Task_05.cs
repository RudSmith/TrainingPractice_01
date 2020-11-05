using System;
using System.IO;

namespace RIM_Task_05
{
    // Класс, описывающий сущность, будь то игрок или враг
    class Entity
    {
        // В конструкторе устанавливаем позицию по умолчанию и задаём указанный символ "модельке" игрока
        public Entity(char symbol)
        {
            m_position = new Tuple<int, int>(0, 0);
            m_symbol = symbol;
        }

        // Геттер символа
        public char GetSymbol()
        {
            return m_symbol;
        }

        // Геттер позиции
        public Tuple<int, int> GetPosition()
        {
            return m_position;
        }

        // Сеттер позиции
        public void SetPosition(Tuple<int, int> newPos)
        {
            m_position = newPos;
        }

        // Переместиться вверх
        public void MoveUp()
        {
            m_position.Deconstruct(out int oldRow, out int oldColumn);
            m_position = new Tuple<int, int>(oldRow - 1, oldColumn);
        }

        // Переместиться вниз
        public void MoveDown()
        {
            m_position.Deconstruct(out int oldRow, out int oldColumn);
            m_position = new Tuple<int, int>(oldRow + 1, oldColumn);
        }

        // Переместиться влево
        public void MoveLeft()
        {
            m_position.Deconstruct(out int oldRow, out int oldColumn);
            m_position = new Tuple<int, int>(oldRow, oldColumn - 1);
        }

        // Переместиться вправо
        public void MoveRight()
        {
            m_position.Deconstruct(out int oldRow, out int oldColumn);
            m_position = new Tuple<int, int>(oldRow, oldColumn + 1);
        }

        // Приватные поля(символ модельки и позиция на карте)
        private char m_symbol;
        private Tuple <int, int> m_position; 
    }

    // Класс, описывающий игру
    class Labirynth
    {
        // Конструктор, который инициализирует карту, игрока и его врагов
        public Labirynth()
        {
            // Рисуем карту (из файла считать у меня не вышло, были проблемы со спец. символами)
            m_map = new string[m_mapSize] { "------------",
                                            "|           ",
                                            "| |--------|",
                                            "| |        |",
                                            "| |        |",
                                            "|          |",
                                            "|          |",
                                            "|------- --|",
                                            "|          |",
                                            "|  --------|",
                                            "|          ",
                                            "------------" };

            // Создаём игрока
            m_player = new Entity('x');
            m_player.SetPosition(new Tuple<int, int>(1, 10));
            m_playerHealth = 10;

            // Игрок выбирает сложность, в зависимости от неё выбирается кол-во врагов
            Console.WriteLine("Вам предлагается выбрать сложность игры (1 - 3).");
            Console.WriteLine("1 - три врага на карте.");
            Console.WriteLine("2 - пять врагов на карте.");
            Console.WriteLine("3 - семь врагов на карте.");

            if(uint.TryParse(Console.ReadLine(), out uint difficulty) && difficulty > 0 && difficulty <= 3)
            {
                switch(difficulty)
                {
                    case 1:
                        m_enemiesCount = 3;
                        break;
                    case 2:
                        m_enemiesCount = 5;
                        break;
                    case 3:
                        m_enemiesCount = 7;
                        break;
                }
            }
            else
            {
                m_enemiesCount = 11;
                Console.WriteLine("За неправильный ввод вам полагается сложность 'супергерой'");
                System.Threading.Thread.Sleep(1500);
            }

            Console.Clear();

            // Создаём массив врагов
            enemies = new Entity[m_enemiesCount];

            rnd = new Random();

            // Рандомно располагаем врагов на карте (враги обозначены символами 'е')
            for(int i = 0; i < m_enemiesCount; ++i)
            {
                enemies[i] = new Entity('e');
                
                while(m_map[enemies[i].GetPosition().Item1][enemies[i].GetPosition().Item2] != ' ')
                    enemies[i].SetPosition(new Tuple<int, int>(rnd.Next(1, m_mapSize - 3), rnd.Next(1, m_mapSize - 1)));
            }
        }

        // Выводим ХП игрока
        public void PrintBar()
        {
            Console.WriteLine("Здоровье:");
            switch(m_playerHealth)
            {
                case 10:
                    Console.WriteLine("[##########]");
                    break;
                case 9:
                    Console.WriteLine("[#########_]");
                    break;
                case 8:
                    Console.WriteLine("[########__]");
                    break;
                case 7:
                    Console.WriteLine("[#######___]");
                    break;
                case 6:
                    Console.WriteLine("[######____]");
                    break;
                case 5:
                    Console.WriteLine("[#####_____]");
                    break;
                case 4:
                    Console.WriteLine("[####______]");
                    break;
                case 3:
                    Console.WriteLine("[###_______]");
                    break;
                case 2:
                    Console.WriteLine("[##________]");
                    break;
                case 1:
                    Console.WriteLine("[#_________]");
                    break;
                case 0:
                    Console.WriteLine("[__________]");
                    break;
            }
        }

        // Выводим карту 
        public void Print()
        {
            Console.Clear();

            for(int i = 0; i < m_mapSize; ++i)
            {
                Console.Write(m_map[i]);
                Console.WriteLine();
            }

            // Здесь же печатаем ХП игрока
            this.PrintBar();

            // Выводим врагов на экран
            for(int i = 0; i < m_enemiesCount; ++i)
            {
                Console.SetCursorPosition(enemies[i].GetPosition().Item2, enemies[i].GetPosition().Item1);
                Console.Write(enemies[i].GetSymbol());
            }

            // Выводим игрока на экран
            Console.SetCursorPosition(m_player.GetPosition().Item2, m_player.GetPosition().Item1);
            Console.Write(m_player.GetSymbol());
        } 

        // Метод проверки правильности хода
        private bool _isMoveCorrect(int x, int y)
        {
            if (x >= m_mapSize || y >= m_mapSize || x <= 0 || y <= 0 || m_map[x][y] != ' ')
                return false;
            else
                return true;

        }

        // Метод с главным циклом программы
        public void StartGame()
        {
            // Пока игрок жив и не находится в конце лабиринта
            while(!m_isGameEnded)
            {
                // Выводим карту на экран
                this.Print();

                char keyPressed = Console.ReadKey(true).KeyChar;
                // Считываем нажатие кнопки и определяем, корректен ли ход, после чего перемещаем модельку игрока
                switch (keyPressed)
                {
                case 'a':
                    if (_isMoveCorrect(m_player.GetPosition().Item1, m_player.GetPosition().Item2 - 1)) 
                        m_player.MoveLeft();
                    break;
                case 'w':
                    if (_isMoveCorrect(m_player.GetPosition().Item1 - 1, m_player.GetPosition().Item2)) 
                        m_player.MoveUp();
                    break;
                case 'd':
                    if (_isMoveCorrect(m_player.GetPosition().Item1, m_player.GetPosition().Item2 + 1)) 
                        m_player.MoveRight();
                    break;
                case 's':
                    if (_isMoveCorrect(m_player.GetPosition().Item1 + 1, m_player.GetPosition().Item2))
                        m_player.MoveDown();
                    break;
                }

                // В данном цикле ходит каждый враг
                for (int i = 0; i < m_enemiesCount; ++i)
                {
                    int variantOfEnemyMove = rnd.Next(0, 4);

                    switch(variantOfEnemyMove)
                    {
                        case 0:
                            if (_isMoveCorrect(enemies[i].GetPosition().Item1, enemies[i].GetPosition().Item2 - 1))
                                enemies[i].MoveLeft();
                            break;
                        case 1:
                            if (_isMoveCorrect(enemies[i].GetPosition().Item1 - 1, enemies[i].GetPosition().Item2))
                                enemies[i].MoveUp();
                            break;
                        case 2:
                            if (_isMoveCorrect(enemies[i].GetPosition().Item1, enemies[i].GetPosition().Item2 + 1))
                                enemies[i].MoveRight();
                            break;
                        case 3:
                            if (_isMoveCorrect(enemies[i].GetPosition().Item1 + 1, enemies[i].GetPosition().Item2))
                                enemies[i].MoveDown();
                            break;
                    }

                    
                }

                // Если в конце хода игрок находится в клетке с врагом, у него отнимается одна единица здоровья
                for(int i = 0; i < m_enemiesCount; ++i)
                {
                    if (enemies[i].GetPosition().Equals(m_player.GetPosition()))
                        --m_playerHealth;
                }

                // Если игрок дошёл до конца лабиринта, он выигрывает
                if (m_player.GetPosition().Equals(m_mazeEndPoint))
                {
                    Console.Clear();
                    Console.WriteLine("Поздравляем, вы победили!");
                    m_isGameEnded = true;
                }
                // Если игрок умер, игра заканчивается
                else if(m_playerHealth == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Вы мертвы. Игра окончена.");
                    m_isGameEnded = true;
                }
                
            }
        }

        // Карта игры
        private string [] m_map;
        // Размер карты
        private const int m_mapSize = 12;
        // Точка выхода из карты
        private Tuple<int, int> m_mazeEndPoint = new Tuple<int, int>(10, 10);

        // Булева переменная, которая отслеживает, закончена ли игра
        private bool m_isGameEnded;

        // Обьект класса Random для генерации случайных чисел
        private Random rnd; 

        // Обьект игрока
        private Entity m_player;
        // Здоровье игрока
        private int m_playerHealth;
        // Массив врагов
        private Entity[] enemies;
        // Количество врагов
        public int m_enemiesCount { get; set; }
    }

    class RIM_Task_05
    {
        static void Main(string[] args)
        {
            // Игрок получает информацию об игре
            Console.WriteLine("Вы попали в подземелье, полное врагов. \nВам нужно сбежать отсюда. \nВыход справа внизу.");
            Console.WriteLine("Вы обозначены крестиком, ваши враги - буквами 'e'.");
            Console.WriteLine("Если в конце хода вы встанете в одной клетке с врагом, вы получите урон.");
            Console.WriteLine("Для управления используйте кнопки w, a, s, d.");
            Console.WriteLine("Да прибудет с вами сила! Для начала игры нажмите любую кнопку.");
            Console.ReadKey(true);
            Console.Clear();

            // Игра начинается
            Labirynth labirynth = new Labirynth();
            labirynth.StartGame();

            Console.ReadKey(true);
        }
    }
}
