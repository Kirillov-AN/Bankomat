using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public partial class Form1 : Form
    {
        Banksoft session = new Banksoft();
        SuperComp comp = new SuperComp();
        string state = "Start";
        int tr = 3;
        string sumouts;
        string cart = "";
        string menutext = "Выберите действие  1) Снять деньги  2) Посмотреть счет  3) Перевести деньги на другой счет и нажмите \"Вперед\"  или нажмите \"Назад\" для выхода";
        string login = "";
        int  moneybank=10600;
        public Form1()
        {
            InitializeComponent();

            textBox1.Text = " Добрый день! Вставьте карту в картоприемник, введите номер в \"поле ввода\"  и нажимте \"далее\"";
          

 
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        //Обработка нажатия на кнопку вперед в зависимости от состояния банкомата
        private void button10_Click(object sender, EventArgs e)
        {
            //Ввод номера карты
            if (state == "Start")
            {
                textBox1.Text = " Добрый день! Вставьте карту в картоприемник, введите номер в \"поле ввода\"  и нажимте \"далее\"";

                if (checkBox1.Checked == true)
                {
                    if (textBox2.Text != "")
                    {

                        session.cartreader(textBox2.Text);
                        checkBox1.Enabled = false;
                        login = textBox2.Text;
                        textBox2.Text = "";
                        textBox1.Text = "Введите пароль";
                        state = "Password";
                    }

                }

            }
          //Проверка пароля
            else if (state == "Password")
            {

                int l = textBox2.Text.Length;

                if (l == 4)
                {

                    if (comp.Checkpass(textBox2.Text, login) == true)
                    {

                        session.passreader(textBox2.Text);
                        textBox1.Text = menutext;
                        state = "Choice-metod";
                    }
                    else {
                        tr -= 1;
                        textBox1.Text = "Пароль неверный, попробуйте снова";
                        if (tr == 0)
                        {
                            textBox1.Text = "Исчерпаны попытки ввести пароль, ваша карта заблокированна";
                            MessageBox.Show(login);

                            state = "Start";

                            checkBox1.Checked = false;


                        }

                    }

                }
                else
                {
                    textBox1.Text = "Пароль содержит четыре символа";

                }
            }
            //Выбор в меню
            else if (state == "Choice-metod")
            {
                if (textBox2.Text == "1")
                {
                    textBox1.Text = "Введите сумму";

                    state = "Outmoneyinfo";


                }
                if (textBox2.Text == "2")
                {


                    textBox1.Text = $"Ваш баланс: {login} {comp.GetBalance(login)}, нажмите \"Назад\" чтобы вернуться в меню";
                    state = "Get-Balance";
                }
                if (textBox2.Text == "3")
                {

                    textBox1.Text = "Введите номер карты получателя";

                    state = "Sendcart";

                }

            }
            //Ввод карты получателя
            else if (state == "Sendcart")
            {

                cart = textBox2.Text;
                if (cart.Length == 4)
                {
                    textBox1.Text = "Введите сумму";

                    state = "Sendsum";
                }



            }
            //Ввод суммы для отправки
            else if (state == "Sendsum")
            {

                var sum = textBox2.Text;
                if (sum.Length != 0)
                {
                    MessageBox.Show(comp.Send(login, cart, sum));
                    state = "Choice-metod";
                    textBox1.Text = menutext;


                }




            }
            //Вывод средств из банкомата
            else if (state == "Outmoneyinfo")
            {
                 int sumout = Convert.ToInt32(textBox2.Text);
                sumouts = textBox2.Text;




                if (sumout % 100 <= moneybank % 100 && sumout % 1000 <= moneybank % 1000 && moneybank >= sumout && sumout % 10 == 0)
                {
                    string lol = comp.Outmoney(login, Convert.ToString(sumout));
                    if (lol == "Выполеннно")
                    {
                        textBox1.Text = comp.Outmoney(login, Convert.ToString(sumout));
                        moneybank -= Convert.ToInt32(sumout);
                    }
                    textBox1.Text = "Заберите карту из картоприемника и нажмите Вперед";
                    checkBox1.Enabled = true;
                    state = "money";

                }
                else
                {
                    textBox1.Text = "Банкомат не способен выдать эту сумму в данный момент, введите новую сумму для выдачи";
                }



            }
    
            else if (state == "money")
            {
                
                if (checkBox1.Checked == false)
                    {

                    label1.Text = sumouts;
                    textBox1.Text = "Заберите деньги";
                }
                state = "Start";
            }



        }
        //Обработка нажатия на кнопку назад
        private void button11_Click(object sender, EventArgs e)
        {
            if (state == "Get-Balance")
            {
               
                textBox1.Text = menutext;
                state = "Choice-metod";
                return;
            }
            if (state == "Choice-metod")
            {
               
                textBox1.Text = " Cнова добрый день! Вставьте карту в картоприемник, введите номер в \"поле ввода\"  и нажимте \"далее\"";


                 state = "Start";
                 login = "";
                checkBox1.Enabled  = true;
            } 
            else if (state == "Sendsum")
            {
                textBox1.Text = menutext;
                state = "Choice-metod";
                return;
            }
            else if (state == "Outmoneyinfo")
            {
                textBox1.Text = menutext;
                state = "Choice-metod";
                return;
            }
            else if (state == "Sendcart")
            {
                textBox1.Text = menutext;
                state = "Choice-metod";
                return;
            }
            else if (state == "money")
            {
                state = "Start";
                textBox1.Text = menutext;
            }



        }
    }
}