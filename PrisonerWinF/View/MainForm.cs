using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrisonerWinF.Model;
using PrisonerWinF.View;
using System.Threading;

namespace PrisonerWinF
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainForm : Form
    {

        PrisonerCollections collection;

        /// <summary>
        /// Хранит начальное состояние коллекции при запуске.
        /// </summary>
        PrisonerCollections tempCollect;

        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Метод по привзяке текущего состояния коллекции к dataGridView.
        /// </summary>
        /// <param name="collect"></param>
        private void BindingSourceDGV(PrisonerCollections collect)
        {
            //если коллекция пустая, делаем ее недоступной
            if (collection.Count == 0)
                dataGrid.Enabled = false;
            else dataGrid.Enabled = true;

            if (collect != null)
            {
                collection = collect;

                this.bindingSource1.DataSource = new BindingSource(collection, null);

                SetPhotoPrisoner(collection);

                SetValueLabel();
            }
        }

        /// <summary>
        /// Заполняем коллекцию данными, которые 
        /// хранятся в файле и привязываем к таблице.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DataBase.ReadFileVoid(ref collection);

            BindingSourceDGV(collection);

            //сохраняем начальное состояние коллекции
            tempCollect = collection;
            countOfPrisonerLabel.Tag = collection.Count;
        }

        /// <summary>
        /// При закрытии программы сохраняем состояние коллеции.
        /// </summary>    
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            collection = tempCollect;
            DataBase.WriteToFile(ref collection);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработчик события для показа контекстного меню при щелчке правой кнопки мыши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            dataGrid[e.ColumnIndex, e.RowIndex].Selected = true;
            contextMenuStrip.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
        }


        /// <summary>
        /// Отрисовка фото в таблице.
        /// </summary>
        private void SetPhotoPrisoner(PrisonerCollections pC)
        {
            for (int i = 0; i < pC.Count; i++)
            {
                this.dataGrid.Rows[i].Cells[0].Value = new Bitmap((pC[i].Photos.firstPhoto));
            }
        }

        /// <summary>
        /// Сортировка при нажатии на заголовок таблицы 
        /// (т.к. таблица првязана к bindingsource, то использование встроеного sort невозможно)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<Prisoner> newcollection = null;


            string headerTextProp = this.dataGrid.Columns[e.ColumnIndex].DataPropertyName;
            newcollection = this.collection.OrderBy(headerTextProp).ToList(); 

            if (newcollection != null)
            {
                //использование explisit оператора для конвертирования List<Prisoner> to PrisonerCollections
                PrisonerCollections rezultcollect = (PrisonerCollections)newcollection;

                BindingSourceDGV(rezultcollect);

                //проверка на равенство изначальной длины коллекции с длиной кол. в результате запроса
                //если != то,  перезаписывания tempCollect не происходит
                if ((int)countOfPrisonerLabel.Tag == rezultcollect.Count)
                    tempCollect = rezultcollect;
            }
        }


        /// <summary>
        /// Добавление нового заключенного (вызов формы AddPrison).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddPrison add = new AddPrison();
            collection = tempCollect;

            if (add.ShowDialog() == DialogResult.OK)
            {
                collection.Add(add.GetPrisoner());
                BindingSourceDGV(collection);
            }
            searchTextBox.Clear();
        }

        #region contextMenuStrip
        /// <summary>
        /// Обработчик события удаления строчки из таблицы с помощью контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idOfPosDGV = dataGrid.SelectedCells[0].RowIndex;
            DialogResult dialogResult = DialogResult.No;

            if (collection[idOfPosDGV] != null)
            {
                dialogResult = MessageBox.Show(
                    "Are you sure want to delete the info of prisoner " +
                    collection[idOfPosDGV].ToString() + " ?",
                   "Delete the row",
                   MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1);
            }

            if (dialogResult == DialogResult.OK)
            {
                collection.RemoveAt(idOfPosDGV);
                this.BindingSourceDGV(collection);
            }
        }

        /// <summary>
        /// Обработчик события вызова формы Info из таблицы с помощью контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idOfPosDGV = dataGrid.SelectedCells[0].RowIndex;

            ShowInfo(this.collection, idOfPosDGV);
        }

        /// <summary>
        /// Обработчик события изменения данных о заключенных из таблица, с помощью контекстного меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGrid.SelectedCells[0].RowIndex;
            AddPrison changePrisoner = new AddPrison(collection[index]);
            if (changePrisoner.ShowDialog() == DialogResult.OK)
            {
                collection[index] = changePrisoner.GetPrisoner();
                BindingSourceDGV(collection);
            }
        }

        #endregion

        private void ShowInfo(PrisonerCollections collection, int index)
        {
            Info info = new Info(collection[index]);
            info.ShowDialog();
        }

        #region label

        /// <summary>
        /// Обработчик события нажатия на имя последнего заключенного, который был добавлен в таблицу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void valueLastPrisonerAddedLabel_Click(object sender, EventArgs e)
        {
            int index = this.collection.IndexOf(collection.SingleOrDefault(
                p => p.ID == Convert.ToInt32(valueLastPrisonerAddedLabel.Tag.ToString())
                ));

            ShowInfo(this.collection, index);
        }

        private void valueLastPrisonerAddedLabel_MouseMove(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label.ForeColor = Color.Blue;
        }

        private void valueLastPrisonerAddedLabel_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.ForeColor = Color.Black;
        }

        /// <summary>
        /// Метод по установке состояний  label в главной форме (вызывает каждый раз при изменении коллекции).
        /// </summary>
        private void SetValueLabel()
        {
            this.countOfPrisonerLabel.Text = collection.Count.ToString();

            //для показа последнего добавленного заключенного 
            //из изначальной коллекции (tempCollect всегда равно начальному состояния collection)
            int id;
            if (tempCollect != null)
            {
                this.valueLastPrisonerAddedLabel.Text = tempCollect.GetNameLastAddedPrisoner(out id);
            }
            else
                this.valueLastPrisonerAddedLabel.Text = collection.GetNameLastAddedPrisoner(out id);

            this.valueLastPrisonerAddedLabel.Tag = id.ToString();
        }

        #endregion

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            //при уменьшении текста обновлять collection до начальной версии
            if (textBox.TextLength < Convert.ToInt32(textBox.Tag))
                collection = tempCollect;

            List<Prisoner> query = (from p in this.collection
                                    where p.FirstName.ToLowerInvariant().Contains(this.searchTextBox.Text.ToLowerInvariant()) ||
                                    p.NickName.ToLowerInvariant().Contains(this.searchTextBox.Text.ToLowerInvariant()) ||
                                    p.Profession.ToLowerInvariant().Contains(this.searchTextBox.Text.ToLowerInvariant()) ||
                                    p.Citizenship.ToLowerInvariant().Contains(this.searchTextBox.Text.ToLowerInvariant())
                                    select p).ToList();

            if (query.Count != 0)
                BindingSourceDGV((PrisonerCollections)query);
            else if (string.IsNullOrEmpty(searchTextBox.Text))
                BindingSourceDGV(tempCollect);
            else
            {
                MessageBox.Show("Сoncluded by the specified criteria it is not revealed");
                searchTextBox.Clear();
            }

            textBox.Tag = textBox.TextLength;
        }


    }
}
