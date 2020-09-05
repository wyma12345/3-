using System;
using System.Collections;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            double t1, t2;

            #region Ввод даных прямоуольника
            Console.WriteLine("Введите первую сторону прямоугольника");
            while (!double.TryParse(Console.ReadLine(), out t1))//если ввод- число, записываем
                Console.WriteLine("Ввод неверен, попробуйте еще раз");

            Console.WriteLine("Введите вторую сторону прямоугольника");
            while (!double.TryParse(Console.ReadLine(), out t2))//если ввод- число, записываем
                Console.WriteLine("Ввод неверен, попробуйте еще раз");
            #endregion
            Rectangle rec = new Rectangle(t1, t2);// создание объекта класса прямоугольник
            #region Ввод данных квадрата
            Console.WriteLine("Введите сторону квадрата");
            while (!double.TryParse(Console.ReadLine(), out t1))//если ввод- число, записываем
                Console.WriteLine("Ввод неверен, попробуйте еще раз");
            #endregion
            Scuare scuar = new Scuare(t1);// создание объекта класса квадрат
            #region Ввод данных круга
            Console.WriteLine("Введите радиус круга");
            while (!double.TryParse(Console.ReadLine(), out t1))//если ввод- число, записываем
               Console.WriteLine("Ввод неверен, попробуйте еще раз");
            #endregion
            Ciricle cir = new Ciricle(t1);// создание объекта класса круг

            ArrayList arlist = new ArrayList();//создаем ArrayList



            rec.Sum(rec.Height, rec.Width);// подсчет площади прямоугольника
            scuar.Sum(scuar.Height, scuar.Height);// подсчет площади квадрата 
            cir.Sum(cir.Rad);// подсчет площади круга

            arlist.Add(rec.S);//добавляем в него объекты
            arlist.Add(scuar.S);
            arlist.Add(cir.S);

            arlist.Sort();// сортируем по площади с пом. CompereTo

            Console.WriteLine();
            Console.WriteLine("Сортировка по возрастанию");
            foreach (object other in arlist)
            {
                Console.WriteLine($"Фигура с площадью: {other}");
            }


            Console.WriteLine();
            rec.Print();//вывод инормации
            scuar.Print();//вывод инормации
            cir.Print();//вывод инормации

        }
    }
}
