using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using _5;

namespace _4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool limsht = true;//Левенштейн или Дамерау-Левенштейн
        List<string> list = new List<string>();//создаем лист
        private void button1_Click(object sender, EventArgs e)//открытие файла
        {
            #region настройка OpenFileDialog
            OpenFileDialog opendial = new OpenFileDialog();//экз. класса
            opendial.InitialDirectory = @"";//указываем начальную дерикторию
            opendial.Filter = "Файлы txt *.txt|*.txt";//ставим фильтир только на txt
            opendial.ReadOnlyChecked = true;// режим только для чтения
            #endregion
 

            Stopwatch stopWatch = new Stopwatch();//экз. класса
            
            if (opendial.ShowDialog() == DialogResult.OK)//если диал. окно открыто
            {
                stopWatch.Start();//запускаем отсчет времени

                string str = File.ReadAllText(opendial.FileName);//записываем в переменную стринг текст из файла
                
                string[] s_mas = str.Split();//записываем в массив стринг слова из str

                foreach(string o in s_mas)
                    if (!list.Contains(o) && o != "")//если в листе нет такой строки и она не путая
                        list.Add(o.ToLower());//заносим в лист делая все слова маленькими


                stopWatch.Stop();//останавливаем счетчик времени
                textBox1.Text = stopWatch.Elapsed.ToString();//вводим в текстбокс сколько по времени это считалось

                #region появление кнопок
                textBox2.Visible = textBox3.Visible = textBox4.Visible = true;
                listBox1.Visible = true;
                label2.Visible= label3.Visible= label4.Visible = true;
                button2.Visible = true;
                radioButton1.Visible = radioButton2.Visible = true;
                Width = 700;
                #endregion
            }
        }

        private void button2_Click(object sender, EventArgs e)// поиск элемента
        {
            Stopwatch stopWatch = new Stopwatch();//экз. класса
            stopWatch.Start();//запускаем отсчет времени
            string s = textBox3.Text.ToLower();//добавляем сод. текст бокса маленькими буквами
            if (!int.TryParse(textBox4.Text, out int dLim)||dLim<0)//пока вводится не число выдаем ошибку, иначе записываем в переменную
                dLim = 2;//если проблемы с вводом приравниваем к 2


            Limshteyn lim = new Limshteyn();//создаем экз. нашего класса
            int t;
            foreach (string o in list)//ищем его
                if ((t=lim.DamerauLevenshteinDist(o, s, limsht/*смотря каким способом хотим искать*/)) <= dLim)//если растояние меньше или равно нужному
                {
                    listBox1.BeginUpdate();//
                    listBox1.Items.Add($"{s} - {o} - {t}");// добавляем слово
                    listBox1.EndUpdate();//
                    stopWatch.Stop();//останавливаем подсчет времени
                    textBox2.Text = stopWatch.Elapsed.ToString();//выводим время
                    break;
                }
        }


        #region остальные элементы
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();//по наж. на листбокс он очищается
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = !radioButton1.Checked;
            limsht = true;//используем метод Лемштейна
            label3.Text= "Максимальное\nрасстояние Левенштейна";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = !radioButton2.Checked;
            limsht = false;//используем метод Дамерау-Лемштейна
            label3.Text = "Максимальное расстояние\nДамерау-Левенштейна";
        }
        #endregion

    }
}
