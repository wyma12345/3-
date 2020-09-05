using System;

namespace _1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            double[] a = new double[3];// массив 3 коэф.
            int t = 0;//своб. переменная
            Console.WriteLine("Олейников Илья ИУ5-32Б");

            #region Ввод и его проверка

            for (int i = 0; i < args.Length && t<3; i++)//если есть параметры ком. строки(берутся первые 3)
            {
                if (!double.TryParse(args[i], out a[t])||a[0]==0)//если параметр-число, он записывается в след коэф.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введенный параметр неверен");//иначе вывод ошибки
                    Console.ResetColor();
                }
                else
                    Console.WriteLine($"Коэф.{N_Kof(t)} равен {a[t++]}");//вывод введенногог знач      
            }

            for (int i= t; i < 3; i++)//если через ком. стр. были введены не все коэф. вводим с клав.
            {
                Console.WriteLine($"Введите коэф. {N_Kof(i)}");
                while (!double.TryParse(Console.ReadLine(), out a[i])||a[0]==0)//если ввод- число, записываем
                    BadIn();
            }

            Console.WriteLine();
            Console.WriteLine($"Введенные коэф: A={a[0]} B={a[1]} C={a[2]}");//вывод коэф

            #endregion


            double[] x = new double[4];//массив корней
            double d = 0;//Дискр

            d = (a[1] * a[1]) - (4 * a[0] * a[2]);//выч дискр
            if (d < 0)//Если дискр меньше 0, то корней нет
                UnX();

            #region Вычисление и вывод корней

            d = Math.Sqrt(d);//корень из дискр
            x[0] = -a[1] - d;
            x[2] = -a[1] + d;
            x[0] /= 2 * a[0];
            x[2] /= 2 * a[0];


            d = 0;
            Console.WriteLine("Ответ:");
            Console.ForegroundColor = ConsoleColor.Green;//выбор цвета текста
            if (x[0] > 0)
            {
                x[0] = Math.Sqrt(x[0]);
                x[1] = -x[0];
                Console.WriteLine($"x{++d}={x[0]} x{++d}={x[1]}");//вывод корней
            }
            if (x[2] > 0)
            {
                x[2] = Math.Sqrt(x[2]);
                x[3] = -x[2];
                Console.WriteLine($"x{++d}={x[2]} x{++d}={x[3]}");//вывод корней
            }
            if (d == 0)//если корней нет
                UnX();
            #endregion

            Console.ReadKey();
        }

        static void UnX()//если корней нет
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Корней нет");
            Console.ReadKey();
            Environment.Exit(0);
        }
        static void BadIn()//неправ. вывод
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Неправильный ввод, пожалуйста введите число");
            Console.ResetColor();
        }
        static string N_Kof(int b)// выводит назв. коэф.
        {
            switch (b)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
            }
            throw new Exception("Ошибка! Невозможно сравнить два объекта");
        }
    }
}
