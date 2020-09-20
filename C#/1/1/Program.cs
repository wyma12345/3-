using System;

namespace _1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            double[] koaf = new double[3];// массив 3 коэф.
            byte t = 0;//своб. переменная
            Console.WriteLine("Олейников Илья ИУ5-32Б");

            #region Ввод и его проверка

            static string N_Kof(int nKoaf) => nKoaf == 0 ? "A" : (nKoaf == 1 ? "B" : "C");// выводит назв. коэф.

            for (int i = 0; i < args.Length && t<3; i++)//если есть параметры ком. строки(берутся первые 3)
            {
                if (!double.TryParse(args[i], out koaf[t])||koaf[0]==0)//если параметр-число, он записывается в след коэф.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введенный параметр неверен");//иначе вывод ошибки
                    Console.ResetColor();
                }
                else
                    Console.WriteLine($"Коэф.{N_Kof(t)} равен {koaf[t++]}");//вывод введенного знач      
            }

            for (int i= t; i < 3; i++)//если через ком. стр. были введены не все коэф. вводим с клав.
            {
                Console.WriteLine($"Введите коэф. {N_Kof(i)}");
                while (!double.TryParse(Console.ReadLine(), out koaf[i])||koaf[0]==0)//если ввод- число, записываем
                    BadIn();
            }

            Console.WriteLine($"\nВведенные коэф: A={koaf[0]} B={koaf[1]} C={koaf[2]}");//вывод коэф
            #endregion


           
            double d;//Дискр

            d = (koaf[1] * koaf[1]) - (4 * koaf[0] * koaf[2]);//выч дискр
            if (d < 0)//Если дискр меньше 0, то корней нет
                UnX();

            #region Вычисление и вывод корней

            double[] x = new double[4];//массив корней
            d = Math.Sqrt(d);//корень из дискр
            x[0] = -koaf[1] - d;
            x[2] = -koaf[1] + d;
            x[0] /= 2 * koaf[0];
            x[2] /= 2 * koaf[0];


            t = 0;
            Console.WriteLine("Ответ:");
            Console.ForegroundColor = ConsoleColor.Green;//выбор цвета текста
            if (x[0] > 0)
            {
                x[0] = Math.Sqrt(x[0]);
                x[1] = -x[0];
                Console.WriteLine($"x{++t}={x[0]} x{++t}={x[1]}");//вывод корней
            }
            if (x[2] > 0)
            {
                x[2] = Math.Sqrt(x[2]);
                x[3] = -x[2];
                Console.WriteLine($"x{++t}={x[2]} x{++t}={x[3]}");//вывод корней
            }
            if (t == 0)//если корней нет
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
    }
}
