﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _3
{
    public class Matrix<T>
    {
        /// <summary>
        /// Словарь для хранения значений
        /// </summary>
        Dictionary<string, T> _matrix = new Dictionary<string, T>();

        /// <summary>
        /// Количество элементов по горизонтали (максимальное количество столбцов)
        /// </summary>
        int maxX;

        /// <summary>
        /// Количество элементов по вертикали (максимальное количество строк)
        /// </summary>
        int maxY;

        /// <summary>
        /// Количество элементов по глубине (максимальное количество строк)
        /// </summary>
        int maxZ;

        /// <summary>
        /// Пустой элемент, который возвращается если элемент с нужными координатами не был задан
        /// </summary>
        T nullElement;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Matrix(int px, int py, int pz, T nullElementParam)
        {
            maxX = px;
            maxY = py;
            maxZ = pz;
            nullElement = nullElementParam;
        }

        /// <summary>
        /// Индексатор для доступа к данных
        /// </summary>
        public T this[int x, int y, int z]
        {
            get
            {
                CheckBounds(x, y, z);
                string key = DictKey(x, y, z);
                if (this._matrix.ContainsKey(key))
                {
                    return this._matrix[key];
                }
                else
                {
                    return this.nullElement;
                }
            }
            set
            {
                CheckBounds(x, y, z);
                string key = DictKey(x, y, z);
                this._matrix.Add(key, value);
            }
        }

        /// <summary>
        /// Проверка границ
        /// </summary>
        void CheckBounds(int x, int y, int z)
        {
            if (x < 0 || x >= this.maxX) throw new Exception("x=" + x + " выходит за границы");
            if (y < 0 || y >= this.maxY) throw new Exception("y=" + y + " выходит за границы");
            if (z < 0 || z >= this.maxZ) throw new Exception("z=" + z + " выходит за границы");
        }

        /// <summary>
        /// Формирование ключа
        /// </summary>
        string DictKey(int x, int y, int z)
        {
            return x.ToString() + "_" + y.ToString()+ "_" + z.ToString();
        }

        /// <summary>
        /// Приведение к строке
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //Класс StringBuilder используется для построения длинных строк
            //Это увеличивает производительность по сравнению с созданием и склеиванием 
            //большого количества обычных строк

            StringBuilder b = new StringBuilder();

            for (int j = 0; j < maxZ; j++)
            {
                b.Append("z=");
                b.Append(j);
                for (int i = 0; i < maxY; i++)
                {
                    b.Append("[");
                    for (int m = 0; m < maxX; m++)
                    {
                        if (i > 0) b.Append("\t");
                        b.Append(this[m , i, j].ToString());
                    }
                    b.Append("]\n");
                }
                b.Append("\n");

            }

            return b.ToString();
        }

    }
}
