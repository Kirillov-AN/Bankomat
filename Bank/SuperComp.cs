using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class SuperComp
    {
        public Dictionary<string, int> balan = new Dictionary<string, int>();
        public Dictionary<string, string> pass = new Dictionary<string, string>();

        //Реестры карта-баланс и карта-пароль 
        public SuperComp()
        {
            balan.Add("1234", 400);
            balan.Add("1488", 10000);
            balan.Add("5666", 12000);
            pass.Add( "1234", "1323");
            pass.Add("1488", "1488");
            pass.Add("5666", "5666");

        }
        public string GetBalance(string id)
           {
                return Convert.ToString(balan[id]);
            


          
            }
        //Отправка денег на другую карту
        public string Send(string sender, string recipient, string sum)
        {
            int intsum;

            try
            {

                intsum = Convert.ToInt32(sum);
                int b = balan[recipient];

            }
            catch
            {
                return "Некорректный счет получателя ";
            }
            if (balan[sender] >= intsum)
            {
                balan[sender] -= intsum;
                balan[recipient] += intsum;

                return "Выполеннно";
            }
            else
            {
                return $"Недостаточно средств на счету {balan[sender]} - {balan[recipient]}";
            }




        }
        //Снятие денег из банкомата, вычеты средств со счета 
        public string Outmoney(string login, string outm)
        {
            int intout = Convert.ToInt32(outm);
            if (balan[login] >= intout)
            {
                balan[login] -= intout;
                return "Деньги выданы, нажмите \"Назад\" для возвращения в меню выбора";

            }
            else
            {
                return $"Недостаточно средств {balan[login]}";
            }

        }
        //Проверка номера карты
        public bool Checkcard(string card)
        {
            try
            {
                var balance = balan[card];
                 return true;
            }
            catch
            {
                return false;
            }
        }
        //Проверка пинкода
        public bool Checkpass(string password, string card)
        {
            try
            {
              
                if (password == pass[card])
                { return true; }
                else
                { return false; }
            }
            catch
            {
                return false;
            }
        }
    }
}
