using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Distanse1;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> list = new List<string>();

        private void button4_Click(object sender, EventArgs e) //чтение
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "текстовые файлы|*.txt"; //только файлы с расширением «.txt»

            if (fd.ShowDialog() == DialogResult.OK)
            {
                Stopwatch t = new Stopwatch();
                t.Start();

                string text = File.ReadAllText(fd.FileName); //чтение файла в виде строки

                char[] separators = new char[] { ' ', '.', ',', '!', '?', '/', '\t', '\n' }; //разделительные символы для чтения из файла
                string[] textArray = text.Split(separators);
                foreach (string strTemp in textArray) //дубликаты слов не записываются
                {
                    string str = strTemp.Trim();
                    if (!list.Contains(str)) list.Add(str);
                }

                t.Stop();
                this.textBox6.Text = t.Elapsed.ToString(); // вывод времени в поле textBox2
            }
            else
            {
                MessageBox.Show("Выберете файл!");
            }
        }

        private void button5_Click(object sender, EventArgs e) //четкий поиск
        {
            string word = this.textBox7.Text.Trim();

            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0) //если не пусто
            {

                string wordUpper = word.ToUpper(); //поиск в верхнем регистре

                List<string> tempList = new List<string>();
                Stopwatch t = new Stopwatch();
                t.Start();
                foreach (string str in list)
                {
                    if (str.ToUpper().Contains(wordUpper))
                    {
                        tempList.Add(str);
                    }
                }
                t.Stop();
                this.textBox8.Text = t.Elapsed.ToString(); //вывод времени
                this.listBox2.BeginUpdate();

                this.listBox2.Items.Clear();

                foreach (string str in tempList) // вывод результатов в список listBox1
                {
                    this.listBox2.Items.Add(str);
                }
                this.listBox2.EndUpdate();
            }
            else
            {
                MessageBox.Show("Выберете файл и введите слово, которые необходимо найти!");
            }
        }

        private void button6_Click(object sender, EventArgs e) //нечеткий поиск
        {
            string word = this.textBox7.Text.Trim();

            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)
            {
                int maxDist;
                if (!int.TryParse(this.textBox9.Text.Trim(), out maxDist))
                {
                    MessageBox.Show("Необходимо указать максимальное расстояние");
                    return;
                }
                if (maxDist < 1 || maxDist > 5)
                {
                    MessageBox.Show("Максимальное расстояние должно быть в диапазоне от 1 до 5");
                    return;
                }

                string wordUpper = word.ToUpper();

                List<Tuple<string, int>> tempList = new List<Tuple<string,
               int>>();
                Stopwatch t = new Stopwatch();
                t.Start();
                foreach (string str in list)
                {
                    //Вычисление расстояния Дамерау-Левенштейна
                    int dist = EditDistance.Distance(str.ToUpper(), wordUpper);

                    if (dist <= maxDist)
                    {
                        tempList.Add(new Tuple<string, int>(str, dist));
                    }

                }

                t.Stop();
                this.textBox10.Text = t.Elapsed.ToString();
                this.listBox2.BeginUpdate();
                //Очистка списка
                this.listBox2.Items.Clear();
                //Вывод результатов поиска
                foreach (var x in tempList)
                {
                    string temp = x.Item1 + "(расстояние=" + x.Item2.ToString() +
                   ")";
                    this.listBox2.Items.Add(temp);
                }
                this.listBox2.EndUpdate();
            }

            else
            {
                MessageBox.Show("Необходимо выбрать файл и ввести слово для поиска");
            }


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}