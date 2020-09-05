using System;
using System.Collections.Generic;
using System.Text;

namespace _3
{
    #region Абстрактные
    interface IPrint
    {
        void Print();
    }
    interface IComparable//интерфейс сортировки
    {
        int CompareTo(object other);
    }

    abstract class GeomFig : IPrint, IComparable
    {
        int name;
        public int CompareTo(object other)// используем CompareTo
        {
            GeomFig geom = other as GeomFig;
            if (geom != null)
                return s.CompareTo(geom.s);
            else
                throw new Exception("Ошибка! Невозможно сравнить два объекта");
        }

        public void Print()
        {
            Console.WriteLine(Out());
        }

        public virtual void Sum(double h, double w)// нахождение площади
        {
            s = h * w;
        }

        public virtual string Out()//вывод
        {
            return "Площадь фигуры равна:" + S.ToString();
        }

        private double s;
        public double S
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
            }
        }//свойство
    }
    #endregion

    #region Прямоугольник
    class Rectangle : GeomFig//прямоугольник
    {
        public Rectangle() { }

        public Rectangle(double h, double w)//конструктор
        {
            height = h;
            width = w;
        }

        private double height;
        private double width;

        public double Height//свойство
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        public double Width//свойство
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
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
            rad = c;
        }

        private double rad;//радиус

        public double Rad//свойство
        {
            get
            {
                return rad;
            }
            set
            {
                rad = value;
            }
        }

        public override void Sum(double h, double w = 0)//мереопр вирт метода подсчета площади
        {
            S = Math.PI * h * h;
        }

        public override string Out()//переопр вирт метода возвр значений
        {
            return "Круг- Радиус равен: " + Rad.ToString() + "  " + base.Out();
        }
    }
    #endregion
}
