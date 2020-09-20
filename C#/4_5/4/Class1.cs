using System;

namespace _5
{
    public class Limshteyn
    {
        static int Min(int a, int b, int c)//функция нахождения мин элемента
        {
            if (a > b)
                a = b;

            if (a > c)
                a = c;

            return a;
        }

        static int Min(int a, int b) => a < b ? a : b;//лямда-вырожение нахождения мин элемента

        #region Расстояние Дамерау-Левенштейна
        public int DamerauLevenshteinDist(string word1, string word2, bool limht)
        {
            var n = word1.Length + 1;//размер матрицы по вертикали
            var m = word2.Length + 1;//размер матрицы по горизонтали
            var mD = new int[n, m];//создаем матрицу


            for (var i = 0; i < n; i++)//заполняем первый столбец матрицы по вертикали
                mD[i, 0] = i;

            for (var j = 0; j < m; j++)//заполяем первый столбец матрицы по горизонтали
                mD[0, j] = j;


            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    int costWord = word1[i - 1] == word2[j - 1] ? 0 : 1;//равна ли буква 1 слова букве 2, если да то 0 если нет то 1

                    mD[i, j] = Min(mD[i - 1, j] + 1 /*удаление*/, mD[i, j - 1] + 1  /* вставка*/, mD[i - 1, j - 1] + costWord /* замена*/); 


                    if (i > 1 && j > 1 && word1[i - 1] == word2[j - 2] && word1[i - 2] == word2[j - 1]&&!limht)//если это не 1 буквы и они "перепутыны крест накрест" и это не метод Лимштейна
                        mD[i, j] = Min(mD[i, j], mD[i - 2, j - 2] + costWord); // если число меньше посчит до этого, то записываем его
                    
                }
            }

            return mD[n - 1, m - 1];//выводим расстояние
        }
        #endregion

    }
}
