using System;

namespace _2
{

    #region Абстрактные
    interface IPrint
    {
       void Print();
    }

    abstract class GeomFig: IPrint
    {
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
    class Rectangle :GeomFig//прямоугольник
    {
        public Rectangle() {}

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
            return  "Прямоугольник- Длина равна: "+Height.ToString()+"  Ширина равна: " + Width.ToString()+ "  "+base.Out();
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
            return "Квадрат- Длина равна: "+Height.ToString()+"  Площадь фигуры равна:" + S.ToString();
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

        public override void Sum(double h, double w =0)//мереопр вирт метода подсчета площади
        {
            S = Math.PI * h * h;
        }

        public override string Out()//переопр вирт метода возвр значений
        {
            return "Круг- Радиус равен: " + Rad.ToString()+"  "+ base.Out();
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            double t1,t2;

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
            Scuare scu = new Scuare(t1);// создание объекта класса квадрат
            #region Ввод данных круга
            Console.WriteLine("Введите радиус круга");
            while (!double.TryParse(Console.ReadLine(), out t1))//если ввод- число, записываем
                Console.WriteLine("Ввод неверен, попробуйте еще раз");
            #endregion
            Ciricle cir = new Ciricle(t1);// создание объекта класса круг

            rec.Sum(rec.Height, rec.Width);// подсчет площади прямоугольника
            scu.Sum(scu.Height, scu.Height);// подсчет площади квадрата 
            cir.Sum(cir.Rad);// подсчет площади круга

            rec.Print();//вывод инормации
            scu.Print();//вывод инормации
            cir.Print();//вывод инормации
        }
    }
}
