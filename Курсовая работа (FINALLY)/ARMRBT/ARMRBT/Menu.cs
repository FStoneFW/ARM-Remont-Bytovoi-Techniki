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

using MySql.Data;
using MySql.Data.MySqlClient;

namespace ARMRBT
{
    public partial class Menu : Form
    {
        public Menu(Authorization auth)
        {
            InitializeComponent();
            this.auth = auth;
            auth.Hide();
            CreateFormsForTables();
        }

        private Authorization auth;

        ///ДЛЯ ДОБАВЛЕНИЯ И РЕДАКТИРОВАНИЯ///
        EditFormTable eftZakazy;
        EditFormTable eftClients;
        EditFormTable eftVidremonta;
        EditFormTable eftDetali;
        EditFormTable eftTovary;
        EditFormTable eftGarantii;
        EditFormTable eftSotrudniki;
        EditFormTable eftSex;
        EditFormTable eftDolgnosti;
        EditFormTable eftModels;
        EditFormTable eftCategories;
        EditFormTable eftFirms;
        ////////////////////////////////////


        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            auth.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Категория", "Название"};
            new DatabaseShow("Категории", auth.database, eftCategories, names, "SELECT * FROM categories").ShowDialog();
        }

        public void CreateFormsForTables()
        {
            eftZakazy = new EditFormTable() { NameForm = "заказа", NameTable = "zakazy" };
            eftClients = new EditFormTable() { NameForm = "клиента", NameTable = "clients" };
            eftVidremonta = new EditFormTable() { NameForm = "вида ремонта", NameTable = "vidremonta" };
            eftDetali = new EditFormTable() { NameForm = "детали", NameTable = "detali" };
            eftTovary = new EditFormTable() { NameForm = "товара", NameTable = "tovary" };
            eftGarantii = new EditFormTable() { NameForm = "гарантии", NameTable = "garantii" };
            eftSotrudniki = new EditFormTable() { NameForm = "сотрудника", NameTable = "sotrudniki" };
            eftSex = new EditFormTable() { NameForm = "пола", NameTable = "sex" };
            eftDolgnosti = new EditFormTable() { NameForm = "должности", NameTable = "dolgnosti" };
            eftModels = new EditFormTable() { NameForm = "модели", NameTable = "models" };
            eftCategories = new EditFormTable() { NameForm = "категории", NameTable = "categories" };
            eftFirms = new EditFormTable() { NameForm = "фирмы", NameTable = "firms" };

            ////////////////ЗАКАЗЫ/////////////////
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "clients.id_client", "clients.familiya", "clients.imya" }, FieldCaption = "Клиент", HaveLink = true, IDs = new List<int>(), TableField = eftClients });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "tovary.id_tovar", "tovary.seriyniy_nomer" }, FieldCaption = "Товары", HaveLink = true, IDs = new List<int>(), TableField = eftTovary });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "garantii.id_garantiya", "garantii.garantiyniy_srok" }, FieldCaption = "Гарантия", HaveLink = true, IDs = new List<int>(), TableField = eftGarantii });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "vidremonta.id_vidremont", "vidremonta.nazvanie" }, FieldCaption = "Вид ремонта", HaveLink = true, IDs = new List<int>(), TableField = eftVidremonta });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.id_sotrudnik", "sotrudniki.familiya", "sotrudniki.imya" }, FieldCaption = "Сотрудник", HaveLink = true, IDs = new List<int>(), TableField = eftSotrudniki });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "detali.id_detal", "detali.nazvanie", "detali.kolichesstvo" }, FieldCaption = "Деталь", HaveLink = true, IDs = new List<int>(), TableField = eftDetali });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "zakazy.stoimost_remonta" }, FieldCaption = "Стоимость ремонта", CheckInt = true  });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "zakazy.alert_client" }, FieldCaption = "Сообщить клиенту", Check = true });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "zakazy.data_postupleniya_zakaza" }, FieldCaption = "Дата поступления заказа", Date = true });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "zakazy.data_ispolneniya_zakaza" }, FieldCaption = "Дата исполнения заказа", Date = true });
            eftZakazy.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "zakazy.data_polucheniya_tovara" }, FieldCaption = "Дата получения товара", Date = true });
            ///////////////КЛИЕНТЫ////////////////
            eftClients.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "clients.familiya" }, FieldCaption = "Фамилия" });
            eftClients.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "clients.imya" }, FieldCaption = "Имя" });
            eftClients.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "clients.otchestvo" }, FieldCaption = "Отчество" });
            eftClients.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "clients.telephon" }, FieldCaption = "Телефон" });
            eftClients.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "clients.addres" }, FieldCaption = "Адрес" });
            //////////////ВИД РЕМОНТА/////////////
            eftVidremonta.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "vidremonta.nazvanie" }, FieldCaption = "Вид ремонта" });
            ////////////////ДЕТАЛИ////////////////
            eftDetali.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "detali.nazvanie" }, FieldCaption = "Название" });
            eftDetali.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "detali.kolichesstvo" }, FieldCaption = "Количество", CheckInt = true  });
            ////////////////ТОВАРЫ////////////////
            eftTovary.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "firms.id_firm", "firms.firma" }, FieldCaption = "Фирма", HaveLink = true, IDs = new List<int>(), TableField = eftFirms });
            eftTovary.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "categories.id_category", "categories.category" }, FieldCaption = "Категория", HaveLink = true, IDs = new List<int>(), TableField = eftCategories });
            eftTovary.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "models.id_model", "models.nazvanie" }, FieldCaption = "Модель", HaveLink = true, IDs = new List<int>(), TableField = eftModels });
            eftTovary.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "tovary.seriyniy_nomer" }, FieldCaption = "Серийный номер" });
            eftTovary.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "tovary.tehnichiskie_haracteristici" }, FieldCaption = "Технические характеристики" });
            eftTovary.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "tovary.garantiyniy_srok" }, FieldCaption = "Гарантийный срок", Date = true });
            ////////////////ГАРАНТИИ//////////////
            eftGarantii.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "garantii.garantiyniy_srok" }, FieldCaption = "Гарантийный срок", Date = true });
            eftGarantii.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "garantii.nazvanie_centra" }, FieldCaption = "Название центра" });
            eftGarantii.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "garantii.addres" }, FieldCaption = "Адрес" });
            ////////////////Сотрудники//////////////
            eftSotrudniki.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sex.id_sex", "sex.sex" }, FieldCaption = "Пол", HaveLink = true, IDs = new List<int>(), TableField = eftSex });
            eftSotrudniki.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "dolgnosti.id_dolgnost", "dolgnosti.nazvanie" }, FieldCaption = "Должность", HaveLink = true, IDs = new List<int>(), TableField = eftDolgnosti });
            eftSotrudniki.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.familiya" }, FieldCaption = "Фамилия" });
            eftSotrudniki.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.imya" }, FieldCaption = "Имя" });
            eftSotrudniki.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.otchestvo" }, FieldCaption = "Отчество" });
            eftSotrudniki.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.telephon" }, FieldCaption = "Телефон" });
            eftSotrudniki.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.addres" }, FieldCaption = "Адрес" });
            ///////////////////ПОЛ//////////////////
            eftSex.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sex.sex" }, FieldCaption = "Пол" });
            ////////////////Должности///////////////
            eftDolgnosti.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "dolgnosti.nazvanie" }, FieldCaption = "Должность" });
            ////////////////Модели///////////////
            eftModels.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "models.nazvanie" }, FieldCaption = "Модель" });
            ////////////////Категории///////////////
            eftCategories.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "categories.category" }, FieldCaption = "Категория" });
            ////////////////Фирмы///////////////
            eftFirms.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "firms.firma" }, FieldCaption = "Фирма" });
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Клиент", "Фамилия", "Имя", "Отчество", "Телефон", "Адрес" };
            new DatabaseShow("Клиенты", auth.database, eftClients, names, "SELECT * FROM clients").ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Детали", "Название", "Количество"};
            new DatabaseShow("Детали", auth.database, eftDetali, names, "SELECT * FROM detali").ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Должности", "Название"};
            new DatabaseShow("Должности", auth.database, eftDolgnosti, names, "SELECT * FROM dolgnosti").ShowDialog();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Сотрудник", "Пол", "Должность", "Фамилия", "Имя", "Отчество", "Телефон", "Адрес" };
            new DatabaseShow("Сотрудники", auth.database, eftSotrudniki, names, "SELECT  sotrudniki.id_sotrudnik, sex.sex, dolgnosti.nazvanie, sotrudniki.familiya, sotrudniki.imya, sotrudniki.otchestvo, sotrudniki.telephon, sotrudniki.addres FROM dolgnosti INNER JOIN(sex INNER JOIN sotrudniki ON sex.id_sex = sotrudniki.id_sex) ON dolgnosti.id_dolgnost = sotrudniki.id_dolgnost; "/*"SELECT * FROM sotrudniki"*/).ShowDialog();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            //string[] names = { "ID_Заказа", "Клиент", "Товар", "Гарантия", "Вид ремонта", "Сотрудник", "Деталь", "Стоимость ремонта", "Сообщение клиенту", "Дата поступления заказа", "Дата исполнения заказа", "Дата получения товара" };
            string[] names = { "ID_Заказа", "(Клиент) Фамилия", "(Клиент) Имя", "(Клиент) Отчество", "(Клиент) Телефон", "(Клиент) Адрес", "(Товар) Фирма", "(Товар) Категория", "(Товар) Модель", "(Товар) Серийный номер", "(Товар) Технические характеристики", "(Товар) Гарантийный срок", "(Гарантия) Гарантийный срок", "(Гарантия) Название центра", "(Гарантия) Адрес", "Вид ремонта", "(Сотрудник) Пол", "(Сотрудник) Должность", "(Сотрудник) Фамилия", "(Сотрудник) Имя", "(Сотрудник) Отчество", "(Сотрудник) Телефон", "(Сотрудник) Адрес", "(Деталь) Название", "(Деталь) Количество", "Стоимость ремонта", "Сообщение клиенту", "Дата поступления заказа", "Дата исполнения заказа", "Дата получения товара" };
            new DatabaseShow("Заказы", auth.database, eftZakazy, names, "SELECT zakazy.id_zakaz, clients.familiya, clients.imya, clients.otchestvo, clients.telephon, clients.addres, firms.firma, categories.category, models.nazvanie, tovary.seriyniy_nomer, tovary.tehnichiskie_haracteristici, tovary.garantiyniy_srok, garantii.garantiyniy_srok, garantii.nazvanie_centra, garantii.addres, vidremonta.nazvanie, sex.sex, dolgnosti.nazvanie, sotrudniki.familiya, sotrudniki.imya, sotrudniki.otchestvo, sotrudniki.telephon, sotrudniki.addres, detali.nazvanie, detali.kolichesstvo, zakazy.stoimost_remonta, zakazy.alert_client, zakazy.data_postupleniya_zakaza, zakazy.data_ispolneniya_zakaza, zakazy.data_polucheniya_tovara FROM vidremonta INNER JOIN((models INNER JOIN(firms INNER JOIN(categories INNER JOIN tovary ON categories.id_category = tovary.id_category) ON firms.id_firm = tovary.id_firm) ON models.id_model = tovary.id_model) INNER JOIN((sex INNER JOIN(dolgnosti INNER JOIN sotrudniki ON dolgnosti.id_dolgnost = sotrudniki.id_dolgnost) ON sex.id_sex = sotrudniki.id_sex) INNER JOIN(garantii INNER JOIN(detali INNER JOIN(clients INNER JOIN zakazy ON clients.id_client = zakazy.id_client) ON detali.id_detal = zakazy.id_detal) ON garantii.id_garantiya = zakazy.id_garantiya) ON sotrudniki.id_sotrudnik = zakazy.id_sotrudnik) ON tovary.id_tovar = zakazy.id_tovar) ON vidremonta.id_vidremont = zakazy.id_vidremont; "/*"SELECT zakazy.id_zakaz, clients.familiya, clients.imya, clients.otchestvo, clients.telephon, clients.addres, firms.firma, categories.category, models.nazvanie, tovary.seriyniy_nomer, tovary.tehnichiskie_haracteristici, tovary.garantiyniy_srok, garantii.garantiyniy_srok, garantii.nazvanie_centra, garantii.addres, vidremonta.nazvanie, sotrudniki.id_sex, sotrudniki.id_dolgnost, sotrudniki.familiya, sotrudniki.imya, sotrudniki.otchestvo, sotrudniki.telephon, sotrudniki.addres, detali.nazvanie, detali.kolichesstvo, zakazy.stoimost_remonta, zakazy.alert_client, zakazy.data_postupleniya_zakaza, zakazy.data_ispolneniya_zakaza, zakazy.data_polucheniya_tovara FROM vidremonta INNER JOIN((models INNER JOIN(firms INNER JOIN(categories INNER JOIN tovary ON categories.id_category = tovary.id_category) ON firms.id_firm = tovary.id_firm) ON models.id_model = tovary.id_model) INNER JOIN((sex INNER JOIN(dolgnosti INNER JOIN sotrudniki ON dolgnosti.id_dolgnost = sotrudniki.id_dolgnost) ON sex.id_sex = sotrudniki.id_sex) INNER JOIN(garantii INNER JOIN(detali INNER JOIN(clients INNER JOIN zakazy ON clients.id_client = zakazy.id_client) ON detali.id_detal = zakazy.id_detal) ON garantii.id_garantiya = zakazy.id_garantiya) ON sotrudniki.id_sotrudnik = zakazy.id_sotrudnik) ON tovary.id_tovar = zakazy.id_tovar) ON vidremonta.id_vidremont = zakazy.id_vidremont; "*//*"SELECT * FROM zakazy"*/).ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Модель", "Название" };
            new DatabaseShow("Модели", auth.database, eftModels, names, "SELECT * FROM models").ShowDialog();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Ремонт", "Название" };
            new DatabaseShow("Виды ремонта", auth.database, eftVidremonta, names, "SELECT * FROM vidremonta").ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Товар", "Фирма", "Категория", "Модель", "Серийный номер", "Технические характеристики", "Гарантийный срок" };
            new DatabaseShow("Товары", auth.database, eftTovary, names, "SELECT tovary.id_tovar, firms.firma, categories.category, models.nazvanie, tovary.seriyniy_nomer, tovary.tehnichiskie_haracteristici, tovary.garantiyniy_srok FROM models INNER JOIN(categories INNER JOIN(firms INNER JOIN tovary ON firms.id_firm = tovary.id_firm) ON categories.id_category = tovary.id_category) ON models.id_model = tovary.id_model;"/*"SELECT * FROM tovary"*/).ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Гарантия", "Гарантийный срок", "Название центра", "Адрес" };
            new DatabaseShow("Гарантии", auth.database, eftGarantii, names, "SELECT * FROM garantii").ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string[] names = { "ID_Фирма", "Название"};
            new DatabaseShow("Фирмы", auth.database, eftFirms, names, "SELECT * FROM firms").ShowDialog();
        }
    }
}

/*
EditFormTable eft = new EditFormTable();
//eft.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.id_sotrudnik"}, FieldCaption = "ID_Сотрудник"});
eft.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sex.id_sex", "sex.sex" }, FieldCaption = "Пол", HaveLink = true, IDs = new List<int>() });
eft.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "dolgnosti.id_dolgnost", "dolgnosti.nazvanie" }, FieldCaption = "Должность", HaveLink = true, IDs = new List<int>() });
eft.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.familiya" }, FieldCaption = "Фамилия" });
eft.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.imya" }, FieldCaption = "Имя" });
eft.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.otchestvo" }, FieldCaption = "Отчество" });
eft.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.telephon" }, FieldCaption = "Телефон" });
eft.Fields.Add(new FieldForm() { FieldFullNames = new List<string>() { "sotrudniki.addres" }, FieldCaption = "Адрес" });
*/
