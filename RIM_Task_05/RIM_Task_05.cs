using System;
using System.IO;

namespace RIM_Task_05
{
    class Entity
    {
        public Entity(char symbol)
        {
            m_position = new Tuple<int, int>(-1, -1);
            m_symbol = symbol;
        }

        public char GetSymbol()
        {
            return m_symbol;
        }

        public Tuple<int, int> GetPosition()
        {
            return m_position;
        }

        public void SetPosition(Tuple<int, int> newPos)
        {
            m_position = newPos;
        }

        public void MoveUp()
        {
            m_position.Deconstruct(out int oldRow, out int oldColumn);
            m_position = new Tuple<int, int>(oldRow - 1, oldColumn);
        }
        public void MoveDown()
        {
            m_position.Deconstruct(out int oldRow, out int oldColumn);
            m_position = new Tuple<int, int>(oldRow + 1, oldColumn);
        }
        public void MoveLeft()
        {
            m_position.Deconstruct(out int oldRow, out int oldColumn);
            m_position = new Tuple<int, int>(oldRow, oldColumn - 1);
        }
        public void MoveRight()
        {
            m_position.Deconstruct(out int oldRow, out int oldColumn);
            m_position = new Tuple<int, int>(oldRow, oldColumn + 1);
        }

        private char m_symbol;
        private Tuple <int, int> m_position; 
    }

    class Labirynth
    {
        public Labirynth()
        {
            m_mapFileReader = new StreamReader(m_mapFilePath);
            m_map = new char[m_mapSize, m_mapSize];

            m_player = new Entity('P');

            for (int i = 0; i < m_mapSize; ++i)
                for(int j = 0; j < m_mapSize; ++j)
                    m_map[i, j] = Convert.ToChar(m_mapFileReader.Read());

            m_player.SetPosition(new Tuple<int, int>(1, 10));

            m_isRightPathPrinted = false;
            m_mapFileReader.Close();
        }

        public void Print()
        {
            Console.Clear();

            for (int i = 0; i < m_mapSize; ++i)
                for (int j = 0; j < m_mapSize; ++j)
                {
                    if (m_map[i, j] == '.')
                    {
                        if (m_isRightPathPrinted)
                            Console.Write(m_map[i, j]);
                        else
                            Console.Write(' ');
                    }
                    else
                        Console.Write(m_map[i, j]);
                }

            Console.SetCursorPosition(m_player.GetPosition().Item2, m_player.GetPosition().Item1);
            Console.Write(m_player.GetSymbol());
        }

        public void AskForHelp()
        {
            m_isRightPathPrinted = true;
        }

        public void StartGame()
        {
            while(true)
            {
                this.Print();
                char keyPressed = Console.ReadKey(true).KeyChar;
                
                switch(keyPressed)
                {
                case 'a':
                    m_player.MoveLeft();
                    break;
                case 'w':
                    m_player.MoveUp();
                    break;
                case 'd':
                    m_player.MoveRight();
                    break;
                case 's':
                    m_player.MoveDown();
                    break;
                }
            }
        }


        private StreamReader m_mapFileReader;
        private const string m_mapFilePath = "map.txt";
        private char [,] m_map;
        private const int m_mapSize = 31;

        private bool m_isRightPathPrinted;

        private Entity m_player;
    }

    class RIM_Task_05
    {
        static void Main(string[] args)
        {
            Labirynth labirynth = new Labirynth();
            labirynth.StartGame();

            Console.ReadKey();
        }
    }
}
