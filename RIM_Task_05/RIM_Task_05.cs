using System;
using System.IO;

namespace RIM_Task_05
{
    class Entity
    {
        public Entity(char symbol)
        {
            m_position = new Tuple<int, int>(0, 0);
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

            m_player = new Entity('x');
            m_player.SetPosition(new Tuple<int, int>(1, 10));
        }

        public void Print()
        {
            Console.Clear();

            for(int i = 0; i < m_mapSize; ++i)
            {
                Console.Write(m_map[i]);
                Console.WriteLine();
            }

            Console.SetCursorPosition(m_player.GetPosition().Item2, m_player.GetPosition().Item1);
            Console.Write(m_player.GetSymbol());
        }

        private bool _isMoveCorrect(int x, int y)
        {
            if (x >= m_mapSize || y >= m_mapSize || x <= 0 || y <= 0 || m_map[x][y] != ' ')
                return false;
            else
                return true;

        }

        public void StartGame()
        {
            while(!m_isGameEnded)
            {
                this.Print();

                char keyPressed = Console.ReadKey(true).KeyChar;
                

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


                if (m_player.GetPosition().Equals(m_mazeEndPoint))
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! You won!");
                    m_isGameEnded = true;
                }
            }
        }

        private string [] m_map;
        private const int m_mapSize = 12;

        private Tuple<int, int> m_mazeEndPoint = new Tuple<int, int>(10, 10);

        private bool m_isGameEnded;

        private Entity m_player;
    }

    class RIM_Task_05
    {
        static void Main(string[] args)
        {
            Labirynth labirynth = new Labirynth();
            labirynth.StartGame();

            Console.ReadKey(true);
        }
    }
}
