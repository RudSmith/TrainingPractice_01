using System;

namespace RIM_Task_07
{
    class RIM_Task_07
    {
        // Метод для перемешивания массива
        public static void _shuffle(int[] array, int size)
        {
            // Создаём экземпляр класса Random для генерации случайных чисел
            Random rnd = new Random();

            // В цикле генерируем два индекса, элементы под которыми будут обменяны значениями
            for(int iter = 0; iter < size; ++iter)
            {
                int swapIndex1 = rnd.Next(0, size - 1);
                int swapIndex2 = rnd.Next(0, size - 1);

                int temp = array[swapIndex1];
                array[swapIndex1] = array[swapIndex2];
                array[swapIndex2] = temp;
            }
        }

        static void Main(string[] args)
        {
            // Считываем значение длины массива
            Console.WriteLine("Enter the size of the array: ");
            Int32.TryParse(Console.ReadLine(), out int size);
            
            // Выделяем память под массив
            int[] array = new int[size];

            // Создаём экземпляр класса Random для генерации случайных чисел
            Random rnd = new Random();

            // Заполняем массив случайными числами и выводим его на экран
            Console.Write("Before shuffle: ");
            for(int iter = 0; iter < size; ++iter)
            {
                array[iter] = rnd.Next(0, 100);
                Console.Write(array[iter] + " ");
            }

            Console.WriteLine();

            // Перемешиваем массив
            _shuffle(array, size);

            // Выводим результат на экран
            Console.Write("After shuffle: ");
            for (int iter = 0; iter < size; ++iter)
                Console.Write(array[iter] + " ");

            Console.ReadKey();
        }
    }
}
