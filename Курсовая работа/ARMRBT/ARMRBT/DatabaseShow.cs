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
        }

        EditFormTable _editFormTable;
        Database _database;
        DataTable _dataTable;
        string _query;
        string[] _captionsColumns;

        int ID = -1; //ID выбранной записи

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(_dataTable);
            dv.RowFilter = string.Format("familiya LIKE '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = dv;
        }
    }
}