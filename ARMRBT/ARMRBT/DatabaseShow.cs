using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARMRBT
{
    public partial class DatabaseShow : Form
    {
        public DatabaseShow(string nameDataBase, Database db, EditFormTable editTable, string[] names, string query)
        {
            InitializeComponent();

            _editFormTable = editTable;
            _database = db;
            _query = query;
            _captionsColumns = names;
            
            LoadData(_captionsColumns, _query);
            SetElements(nameDataBase);

            comboBox1.DropDownWidth = 210;
        }

        EditFormTable _editFormTable;
        Database _database;
        DataTable _dataTable;
        Type _currentType;
        string _query;
        string[] _captionsColumns;

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)  //Выделение всей строки
        {
            dataGridView1.CurrentRow.Selected = true;
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)  //Показ ContexMenuStrip
        {
            if (e.Button == MouseButtons.Right)
                dataGridView1.ContextMenuStrip.Show();
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditForm ef = new EditForm(_editFormTable, _database);
            ef.FormClosing += delegate (object senderr, FormClosingEventArgs ev)
            {
                LoadData(_captionsColumns, _query);
                dataGridView1.CurrentCell.Selected = false;

                if (ef.lastInsertIndex != -1)
                {
                    int indexrow = FindIndexRow(ef.lastInsertIndex);
                    if (indexrow == -1)
                        return;

                    dataGridView1.Rows[indexrow].Selected = true;
                }
            };

            ef.ShowDialog();
        }

        private void editRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index == -1)
            {
                MessageBox.Show("Не выбрана запись!");
                return;
            }

            EditForm ef = new EditForm(_editFormTable, _database, (int)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
            ef.FormClosing += delegate (object senderr, FormClosingEventArgs ev)
            {
                LoadData(_captionsColumns, _query);
                dataGridView1.CurrentCell.Selected = false;
                if (ef.ID != -1)
                {
                    int indexrow = FindIndexRow(ef.ID);
                    if (indexrow == -1)
                        return;

                    dataGridView1.Rows[indexrow].Selected = true;
                }

                if (ef.EditFormTable.Type == EditType.Add)
                {
                    if (ef.lastInsertIndex != -1)
                    {
                        int indexrow = FindIndexRow(ef.lastInsertIndex);
                        if (indexrow == -1)
                            return;

                        dataGridView1.Rows[indexrow].Selected = true;
                    }
                }
                else
                {
                    if (ef.ID != -1)
                    {
                        int indexrow = FindIndexRow(ef.ID);
                        if (indexrow == -1)
                            return;

                        dataGridView1.Rows[indexrow].Selected = true;
                    }
                }
            };
            ef.ShowDialog();
        }

        private void LoadData(string[] names, string query)
        {
            _dataTable = _database.SelectQuery(query);
            dataGridView1.DataSource = _dataTable;
            try
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    dataGridView1.Columns[i].HeaderText = names[i];

            }
            catch { }
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private int FindIndexRow(int ID)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if ((int)row.Cells[0].Value == ID)
                    return row.Index;
            return -1;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentCell.Selected = false;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.CurrentRow.Selected = true;
        }

        private void SetElements(string nametable)
        {
            label1.Text = nametable;
            pictureBoxHeader.Controls.Add(label1);
            this.Controls.Remove(label1);
            this.WindowState = FormWindowState.Maximized;
            pictureBoxHeader.Width = this.Width;
            panel1.Location = new Point(this.Width / 2 - panel1.Width/2, panel1.Location.Y);
            dateTimePickerSearch.CustomFormat =  "yyyy-MMM-dd";
            FillSearchCrit();
        }

        private int SetSizeDataGridView()
        {
            int size = 0;
            for(int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                size += dataGridView1.Columns[i].Width;
            }

            return size + 45;
        }

        private void DatabaseShow_Resize(object sender, EventArgs e)
        {
            if (this.Width < 379)
            {
                this.Width = 379;
            }

            dataGridView1.Width = this.Width - 40;
            dataGridView1.Height = this.Height - 190;
            pictureBoxHeader.Width = this.Width;
        }

        private void pictureBoxHeader_Resize(object sender, EventArgs e)
        {
            label1.Location = new Point(pictureBoxHeader.Width /2 - label1.Width/2, label1.Location.Y);
            panel1.Location = new Point(this.Width / 2 - panel1.Width / 2, panel1.Location.Y);
        }

        private void FillSearchCrit()
        {
            foreach (string val in _captionsColumns)
                comboBox1.Items.Add(val);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            DataView dv = new DataView(_dataTable);
            dataGridView1.DataSource = dv;

            if (_dataTable.Columns[comboBox1.SelectedIndex].DataType == typeof(int) || _dataTable.Columns[comboBox1.SelectedIndex].DataType == typeof(string))
            {
                dateTimePickerSearch.Visible = false;
                checkBoxSearch.Visible = false;
                textBoxSearch.Visible = true;
            }
            else if (_dataTable.Columns[comboBox1.SelectedIndex].DataType == typeof(DateTime))
            {
                dateTimePickerSearch.Visible = true;
                checkBoxSearch.Visible = false;
                textBoxSearch.Visible = false;
            }
            else
            {
                dateTimePickerSearch.Visible = false;
                checkBoxSearch.Visible = true;
                textBoxSearch.Visible = false;
            }

            _currentType = _dataTable.Columns[comboBox1.SelectedIndex].DataType;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(_dataTable);

            if (_currentType == typeof(int))
            {
                if (textBoxSearch.Text.Length == 0)
                {
                    dataGridView1.DataSource = dv;
                    return;
                }

                if (!Char.IsDigit(textBoxSearch.Text[textBoxSearch.Text.Length - 1]))
                {
                    MessageBox.Show("Должны вводиться числа!", "Ошибка!", MessageBoxButtons.OK);
                    textBoxSearch.Text = "";
                    return;
                }

                dv.RowFilter = string.Format("{0} = '{1}'", _dataTable.Columns[comboBox1.SelectedIndex].ColumnName, textBoxSearch.Text);
            }
            else
            {
                dv.RowFilter = string.Format("{0} LIKE '%{1}%'", _dataTable.Columns[comboBox1.SelectedIndex].ColumnName, textBoxSearch.Text);
            }

            dataGridView1.DataSource = dv;
        }

        private void dateTimePickerSearch_ValueChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(_dataTable);
            dv.RowFilter = string.Format("{0} = '{1}'", _dataTable.Columns[comboBox1.SelectedIndex].ColumnName, dateTimePickerSearch.Value.Year+"."+dateTimePickerSearch.Value.Month+"."+dateTimePickerSearch.Value.Day);
            dataGridView1.DataSource = dv;
        }

        private void checkBoxSearch_CheckedChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(_dataTable);
            dv.RowFilter = string.Format("{0} = '{1}'", _dataTable.Columns[comboBox1.SelectedIndex].ColumnName, checkBoxSearch.Checked);
            dataGridView1.DataSource = dv;
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Необходимо выбрать строку!", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            _database.Query(string.Format("DELETE FROM {0} WHERE {1} = '{2}'", _editFormTable.NameTable, _editFormTable.NameTable+"."+_dataTable.Columns[0].ColumnName, (int)dataGridView1.CurrentRow.Cells[0].Value));
            LoadData(_captionsColumns, _query);
            dataGridView1.CurrentCell.Selected = false;
        }
    }
}