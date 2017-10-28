using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ARMRBT
{
    public partial class EditForm : Form
    {
        public EditForm(EditFormTable eft, Database db, int ID)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            EditFormTable = eft;
            _database = db;
            this.ID = ID;
            this.lastInsertIndex = -1;
            CreateControlElements(eft);
            FillValues();
            label1.Text = "Редактирование " + eft.NameForm;
            EditFormTable.Type = EditType.Edit;
        }

        public EditForm(EditFormTable eft, Database db)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            EditFormTable = eft;
            _database = db;
            this.ID = -1;
            this.lastInsertIndex = -1;
            CreateControlElements(eft);
            label1.Text = "Добавление " + eft.NameForm;
            EditFormTable.Type = EditType.Add;
        }

        public int ID;
        public int lastInsertIndex;
        private Database _database;
        public EditFormTable EditFormTable;
        private List<Control> CreatedControls = new List<Control>();

        private void CreateControlElements(EditFormTable eft)
        {
            int X = 10;
            int Y = 30;
            int maxwidth = 0;
            for (int i = 0; i < eft.Fields.Count; i++)
            {
                Label label = new Label();
                label.Text = eft.Fields[i].FieldCaption;
                label.Width = eft.Fields[i].FieldCaption.Length * 10;
                maxwidth = (maxwidth < label.Width? label.Width : maxwidth);
                label.Location = new Point(X, Y);
                panel1.Controls.Add(label);
                Y += 25;
            }

            Y = 30;

            for (int i = 0; i < eft.Fields.Count; i++)
            {
                if (eft.Fields[i].HaveLink)
                {
                    ComboBox comboBox = new ComboBox();
                    comboBox.Tag = eft.Fields[i];
                    comboBox.Name = "comboBox" + i;
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox.Width = 200;
                    comboBox.Location = new Point(maxwidth + X, Y);
                    /*
                    List<string> valcombobox = GetValuesForComboBox(eft.Fields[i]);
                    for (int q = 0; q < valcombobox.Count; q++)
                        comboBox.Items.Add(valcombobox[q]);
                    */
                    UpdateComboBox(comboBox, eft.Fields[i]);
                    panel1.Controls.Add(comboBox);

                    CreatedControls.Add(comboBox);

                    Button buttonf = new Button();
                    //button
                    buttonf.Text = "Добавить запись";
                    buttonf.Width = 100;
                    buttonf.Location = new Point(comboBox.Width + comboBox.Location.X + 5, Y);
                    buttonf.Click += delegate (object sender, EventArgs arg)
                    {
                        EditForm ef = new EditForm((comboBox.Tag as FieldForm).TableField, _database);
                        ef.FormClosing += Ef_FormClosing;
                        ef.Tag = comboBox;
                        ef.ShowDialog();

                    };
                    panel1.Controls.Add(buttonf);

                    Button buttong = new Button();
                    //button
                    buttong.Text = "Изменить запись";
                    buttong.Width = 100;
                    buttong.Location = new Point(buttonf.Width + buttonf.Location.X + 5, Y);
                    buttong.Click += delegate (object sender, EventArgs arg)
                    {
                        if (comboBox.SelectedIndex == -1)
                        {
                            MessageBox.Show("Необходимо выбрать запись!");
                            return;
                        }

                        EditForm ef = new EditForm((comboBox.Tag as FieldForm).TableField, _database, (comboBox.Tag as FieldForm).IDs[comboBox.SelectedIndex]);
                        ef.FormClosing += Ef_FormClosing;
                        ef.Tag = comboBox;
                        ef.ShowDialog();

                    };
                    panel1.Controls.Add(buttong);
                }
                else if (eft.Fields[i].Date)
                {
                    DateTimePicker dateTimePicker = new DateTimePicker();
                    dateTimePicker.Tag = eft.Fields[i];
                    dateTimePicker.CustomFormat = "yyyy-MMM-dd";
                    dateTimePicker.Format = DateTimePickerFormat.Custom;
                    dateTimePicker.Name = "dateTimePicker" + i;
                    dateTimePicker.Width = 200;
                    dateTimePicker.Location = new Point(maxwidth + X, Y);
                    panel1.Controls.Add(dateTimePicker);
                    CreatedControls.Add(dateTimePicker);
                }
                else if (eft.Fields[i].Check)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Tag = eft.Fields[i];
                    checkBox.Name = "checkBox" + i;
                    checkBox.Width = 200;
                    checkBox.Location = new Point(maxwidth + X, Y);
                    panel1.Controls.Add(checkBox);
                    CreatedControls.Add(checkBox);
                }
                else
                {
                    TextBox textBox = new TextBox();
                    textBox.Tag = eft.Fields[i];
                    textBox.Name = "textBox" + i;
                    textBox.Width = 200;
                    textBox.Location = new Point(maxwidth + X, Y);
                    panel1.Controls.Add(textBox);
                    CreatedControls.Add(textBox);
                }

                Y += 25;
            }

            Button button = new Button();
            button.Click += SendQuery;
            button.Name = "ButtonOk";
            button.Text = "OK";
            button.Width = 100;
            button.Location = new Point(X, Y);
            
            panel1.Controls.Add(button);
        }

        private void Ef_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show((sender as EditForm).lastInsertIndex.ToString());
            if ((sender as EditForm).EditFormTable.Type == EditType.Add)
            {
                if ((sender as EditForm).lastInsertIndex == -1)
                    return;

                UpdateComboBox(((sender as EditForm).Tag as ComboBox), ((sender as EditForm).Tag as ComboBox).Tag as FieldForm);
                ((sender as EditForm).Tag as ComboBox).SelectedIndex = (((sender as EditForm).Tag as ComboBox).Tag as FieldForm).IDs.IndexOf((sender as EditForm).lastInsertIndex);
            }
            else
            {
                UpdateComboBox(((sender as EditForm).Tag as ComboBox), ((sender as EditForm).Tag as ComboBox).Tag as FieldForm);
                ((sender as EditForm).Tag as ComboBox).SelectedIndex = (((sender as EditForm).Tag as ComboBox).Tag as FieldForm).IDs.IndexOf((sender as EditForm).ID);
            }
        }

        private List<string> GetValuesForComboBox(FieldForm fieldf)
        {
            fieldf.IDs.Clear();
            List<string> stringsvalues = new List<string>();
            DataTable dt;
            string query = "SELECT";
            for (int i = 0; i < fieldf.FieldFullNames.Count; i++)
                query += (i == 0? " ": ", ") + fieldf.FieldFullNames[i];
            query += " FROM " + fieldf.FieldFullNames[0].Split('.')[0];

            dt = _database.SelectQuery(query);

            bool firstid;
            string str = string.Empty;
            foreach(DataRow row in dt.Rows)
            {
                str = "";
                firstid = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (firstid)
                        fieldf.IDs.Add(int.Parse(row[column].ToString()));
                    else
                        str += row[column] + " ";
                    firstid = false;
                }
                stringsvalues.Add(str);
            }

            return stringsvalues;
        }

        private void FillValues()
        {
            DataTable dt = _database.SelectQuery(string.Format("SELECT * FROM {0}", EditFormTable.NameTable));
            dt = _database.SelectQuery(string.Format("SELECT * FROM {0} WHERE {1} = {2}", EditFormTable.NameTable, dt.Columns[0].ColumnName, ID));

            for(int i = 1; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i] == null)
                    continue;

                if (CreatedControls[i - 1] is ComboBox)
                {
                    FieldForm ff = (CreatedControls[i - 1] as ComboBox).Tag as FieldForm;
                    (CreatedControls[i - 1] as ComboBox).SelectedIndex = ff.IDs.IndexOf((int)dt.Rows[0][dt.Columns[i]]);
                }
                else if (CreatedControls[i - 1] is TextBox)
                {
                    (CreatedControls[i - 1] as TextBox).Text = dt.Rows[0][dt.Columns[i]].ToString();
                }
                else if (CreatedControls[i - 1] is DateTimePicker)
                {
                    string date = dt.Rows[0][dt.Columns[i]].ToString();
                    string[] dateval;
                    date = date.Split(' ')[0];
                    dateval = date.Split('/');
                    bool f = true;
                    DateTime dtime = new DateTime(int.Parse(dateval[2]), int.Parse(dateval[0]), int.Parse(dateval[1]));
                    (CreatedControls[i - 1] as DateTimePicker).Value = dtime; //= dtime;
                    //(CreatedControls[i - 1] as DateTimePicker).Value = new DateTime(int.Parse(dateval[2]), int.Parse(dateval[1]), int.Parse(dateval[0]));//.Year = (int)dateval[2];
                    //(CreatedControls[i - 1] as DateTimePicker).SelectedIndex = ff.IDs.IndexOf((int)dt.Rows[0][dt.Columns[i]]);
                }
                else
                {
                    bool valcheck = (bool)dt.Rows[0][dt.Columns[i]];
                    (CreatedControls[i - 1] as CheckBox).Checked = valcheck;
                    //(CreatedControls[i - 1] as CheckBox).Checked = valcheck == 1 ? true : false;
                }
            }
        }

        private void UpdateComboBox(ComboBox cb, FieldForm fieldf)  //Заполняем ComboBox данными
        {
            cb.Items.Clear();
            List<string> valcombobox = GetValuesForComboBox(fieldf);
            for (int q = 0; q < valcombobox.Count; q++)
                cb.Items.Add(valcombobox[q]);
        }

        private void SendQuery(object sender, EventArgs e)  //Выполняем запрос
        {
            if (EditFormTable.Type == EditType.Add)
                _database.Query(CreateInsertQuery());
            else
                _database.Query(CreateUpdateQuery());

            lastInsertIndex = (int)_database.mysqlcommand.LastInsertedId;
        }

        private string CreateInsertQuery() //Создание запроса на добавление запись в таблицу
        {
            string query = "INSERT INTO " + EditFormTable.NameTable + " (";
            DataTable dt = _database.SelectQuery("SELECT * FROM " + EditFormTable.NameTable);
            bool first = true;
            bool cont = true;
            foreach (DataColumn dc in dt.Columns)
            {
                if (cont)
                {
                    cont = false;
                    continue;
                }

                if (first)
                {
                    query += dc.ColumnName;
                    first = false;
                }
                else
                    query += ", " + dc.ColumnName;
            }
            query += ") values(";
            first = true;
            //CreatedControls
            foreach (Control control in CreatedControls)
            {
                if (control is Label || control is Button)
                    continue;

                if (first)
                {
                    query += " '";
                    //query += (control is ComboBox) ? " '" + GetValueFieldComboBox((control as ComboBox), ((control as ComboBox).Tag as FieldForm)) + "' " : " '"+(control as TextBox).Text+"' ";
                    first = false;
                }
                else
                    query += ", '";
                    //query += (control is ComboBox) ? ", '"+GetValueFieldComboBox((control as ComboBox), ((control as ComboBox).Tag as FieldForm))+"' " : ", '" + (control as TextBox).Text + "' ";
                    //MessageBox.Show(control.Name);

                if (control is ComboBox)
                {
                    query += GetValueFieldComboBox((control as ComboBox), (control as ComboBox).Tag as FieldForm) + "' ";
                }
                else if (control is TextBox)
                {
                    query += (control as TextBox).Text + "' ";
                }
                else if (control is DateTimePicker)
                {
                    //query += (control as DateTimePicker).Value.Date + "' ";
                    query += string.Format("{0}.{1}.{2}", (control as DateTimePicker).Value.Year, (control as DateTimePicker).Value.Month, (control as DateTimePicker).Value.Date.Day) + "' ";
                }
                else
                {
                    query += ((control as CheckBox).Checked ? 1 : 0) + "' ";
                }

            }
            query += ");";

            return query;
        }
        private string CreateUpdateQuery() //Создание запроса на изменении записи в таблице
        {
            string query = "UPDATE " + EditFormTable.NameTable + " SET ";
            DataTable dt = _database.SelectQuery("SELECT * FROM " + EditFormTable.NameTable);
            bool first = true;
            for(int i = 1; i < dt.Columns.Count; i++)
            {
                if (first)
                {
                    //query += dt.Columns[i].ColumnName + " = '" + ((CreatedControls[i - 1] is ComboBox) ? GetValueFieldComboBox((CreatedControls[i - 1] as ComboBox), (CreatedControls[i - 1] as ComboBox).Tag as FieldForm)+"' ": (CreatedControls[i - 1] as TextBox).Text+"' ");
                    first = false;
                }
                else
                    query += ", ";//query += ", "+dt.Columns[i].ColumnName + " = '" + ((CreatedControls[i - 1] is ComboBox) ? GetValueFieldComboBox((CreatedControls[i - 1] as ComboBox), (CreatedControls[i - 1] as ComboBox).Tag as FieldForm) + "' " : (CreatedControls[i - 1] as TextBox).Text + "' ");

                query += dt.Columns[i].ColumnName + " = '";
                if (CreatedControls[i - 1] is ComboBox)
                {
                    query += GetValueFieldComboBox((CreatedControls[i - 1] as ComboBox), (CreatedControls[i - 1] as ComboBox).Tag as FieldForm) + "' ";
                }
                else if (CreatedControls[i - 1] is TextBox)
                {
                    query += (CreatedControls[i - 1] as TextBox).Text + "' ";
                }
                else if (CreatedControls[i - 1] is DateTimePicker)
                {
                    query += string.Format("{0}.{1}.{2}", (CreatedControls[i - 1] as DateTimePicker).Value.Year, (CreatedControls[i - 1] as DateTimePicker).Value.Month, (CreatedControls[i - 1] as DateTimePicker).Value.Date.Day) + "' ";
                }
                else 
                {
                    query += ((CreatedControls[i - 1] as CheckBox).Checked? 1 : 0) + "' ";
                }

            }
            query += "WHERE " + dt.Columns[0] + " = '" + ID + "'";

            return query;
        }
        private int GetValueFieldComboBox(ComboBox cb, FieldForm fieldf)  //Получаем значение ID из другой таблицы
        {
            return fieldf.IDs[cb.SelectedIndex];
            /*
            for (int i = 0; i < fieldf.IDs.Count; i++)
                if (cb.SelectedIndex == i)
                    return fieldf.IDs[i];
            return -1;
            */
        }
    }
}
