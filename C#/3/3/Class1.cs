using System;
using System.Collections.Generic;
//using System.Text;

namespace _3
{
    #region Абстрактные
    interface IPrint
    {
        void Print();
    }
    

    public class GeomFig : IPrint, IComparable
    {
        public int CompareTo(GeomFig other)
        {
            if (other == null)
                throw new Exception("Ошибка! Невозможно сравнить два объекта");// ручная генерация исключения

            return S.CompareTo(other.S);
        }
        public int CompareTo(object other)
        {
            if (other == null)
                throw new Exception("Ошибка! Невозможно сравнить два объекта");// ручная генерация исключения

            return CompareTo(other as GeomFig);
            
        }

        public void Print()//вывод
        {
            Console.WriteLine(Out());
        }

        public virtual void Sum()// нахождение площади
        {
        }

        public virtual string Out()//вывод
        {
            return "Площадь фигуры равна:" + S.ToString();
        }

        public double S { get; set; }//свойство
        public string Name { get; set; }//свойство

    }
    #endregion

    #region Прямоугольник
    class Rectangle : GeomFig//прямоугольник
    {
        public Rectangle() { }

        public Rectangle(double h, double w)//конструктор
        {
            Height = h;
            Width = w;
            Name = "Прямоугольник";
        }

        public double Height { get; set; }//свойство

        public double Width { get; set; }//свойство

        public override void Sum()
        {
            S = Height * Width;
        }

        public override string Out()//переопр вирт метода возвр значений
        {
            return "Прямоугольник- Длина равна: " + Height.ToString() + "  Ширина равна: " + Width.ToString() + "  " + base.Out();
        }
    }
    #endregion

    #region Квадрат
    class Scuare : Rectangle //квадрат
    {
        public Scuare(double h)//конструктор
        {
            Height = h;
            Name = "Квадрат";
        }

        public override void Sum()
        {
            S = Height * Height;
        }

        public override string Out()//переопр вирт метода возвр значений
        {
            return "Квадрат- Длина равна: " + Height.ToString() + "  Площадь фигуры равна:" + S.ToString();
        }
    }
    #endregion

    #region Круг
    class Ciricle : GeomFig//круг
    {
        public Ciricle(double c)//конструктор
        {
            Rad = c;
            Name = "Круг";
        }

        public double Rad { get; set; }//свойство


        public override void Sum()//мереопр вирт метода подсчета площади
        {
            S = Math.PI * Rad * Rad;
        }

        public override string Out()//переопр вирт метода возвр значений
        {
            return "Круг- Радиус равен: " + Rad.ToString() + "  " + base.Out();
        }
    }
    #endregion

    public class SimpleStack
    {
        List<ValueType> list = new List<ValueType>();

        public void Push(ValueType element)
        {
            list.Add(element);
        }
        public ValueType Pop()
        {
            return list[list.Count - 1];
        }
    }
}
