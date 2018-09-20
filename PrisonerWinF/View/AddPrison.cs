using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrisonerWinF.Model;
using System.Reflection;

namespace PrisonerWinF.View
{
    public partial class AddPrison : Form
    {
        private Prisoner _addPrisoner;
        private LabelMistake labelMistake;
        private Photo photos;

        int pointsPBImportant = 6;
        int pointsPB = 9;

        bool state = true;
        bool isCloseWhithSaveButton = false;
        /// <summary>
        /// Флаг, для разрешения увеличения progressBar.Value
        /// </summary>
        bool lockerForPB = false;

        /// <summary>
        /// Флаг, для разрешения уменьшения progressBar.Value
        /// </summary>
        bool backLockerPB = false;

        /// <summary>
        /// Длина  textBox.Text.Lenth  в момент нажатия на TextBox.
        /// </summary>
        int lenthTextBox;

        private void ResetLocker()
        {
            backLockerPB = false;
            lockerForPB = false;
        }

        public AddPrison()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор для изменения (ПКМ - change)
        /// </summary>
        /// <param name="prisoner">Обьект Prisoner, который необходимо поменять.</param>
        public AddPrison(Prisoner prisoner) : this()
        {
            _addPrisoner = prisoner;
        }

        /// <summary>
        /// Метод для заполнения формы полями обьекта Prisoner (ПКМ - change)
        /// </summary>
        private void FillingControls()
        {
            if (_addPrisoner != null)
            {
                foreach (Control control in Controls)
                {
                    if (control is PictureBox)
                    {
                        PictureBox pictureBox = control as PictureBox;
                        if (pictureBox.Tag.ToString().Contains("Photo1"))
                        {
                            pictureBox.Load(_addPrisoner.Photos.firstPhoto);
                            photos.firstPhoto = _addPrisoner.Photos.firstPhoto;
                        }
                        else
                        {
                            pictureBox.Load(_addPrisoner.Photos.secondPhoto);
                            photos.secondPhoto = _addPrisoner.Photos.secondPhoto;
                        }

                        this.progressBar1.Value += pointsPB;
                    }
                    else if (control is MonthCalendar)
                    {
                        MonthCalendar monthCalendar = control as MonthCalendar;
                        monthCalendar.SelectionStart = Convert.ToDateTime(_addPrisoner.DateOfBirth);
                        monthCalendar.SelectionEnd = monthCalendar.SelectionStart;
                        monthCalendar.Select();
                        this.progressBar1.Value += 4;

                    }
                    else if (control is TextBox || control is ComboBox)
                    {
                        lockerForPB = true;

                        control.Text = (_addPrisoner.GetType().GetFields(
                            BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance).SingleOrDefault(
                            p => control.Name.Contains(p.Name))).GetValue(_addPrisoner).ToString();
                    }
                }
            }
        }

        public Prisoner GetPrisoner()
        {
            return _addPrisoner;
        }

        private void SetControls()
        {
            this.saveButton.Enabled = false;
            ToolTip tip = new ToolTip();
            photos = new Photo();

            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = control as TextBox;

                    textBox.TextChanged += TextChangedHandler;
                    textBox.MouseDown += MouseDownHandler;

                    labelMistake.AddMistakeLabel(textBox);
                }
                if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;

                    comboBox.MouseDown += ComboBox_MouseDown;
                    comboBox.SelectionChangeCommitted += ComboBox_SelectionChangeCommitted;
                    comboBox.TextUpdate += ComboBox_TextUpdate;
                    comboBox.TextChanged += ComboBox_TextChanged;

                    labelMistake.AddMistakeLabel(comboBox);
                }
                if (control is PictureBox)
                {
                    PictureBox pB = control as PictureBox;

                    pB.MouseClick += PictureBox_MouseClickHandler;
                    pB.Click += PictureBox_ClickHandler;
                    pB.Paint += PictureBox_PaintHandler;
                }
                if (control is Label)
                {
                    tip.SetToolTip(control, control.Tag.ToString());
                }
                if (control is CustomProgressBar)
                {
                    CustomProgressBar progressBar = control as CustomProgressBar;
                    progressBar.Maximum = 100;

                    tip.SetToolTip(progressBar, "Прогресс заполнения анкеты");
                }
                if (control is MonthCalendar)
                {
                    MonthCalendar monthCalendar = control as MonthCalendar;

                    labelMistake.AddMistakeLabel(monthCalendar);

                    //начальное состояние календаря
                    monthCalendar.Tag = monthCalendar.SelectionRange.ToString();
                }
            }
        }

        /// <summary>
        /// Обработчик события при загрузки формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPrison_Load(object sender, EventArgs e)
        {
            labelMistake = new LabelMistake(this);
            SetControls();

            //Будет вызван, если необходимо изменить обьект Prisoner (ПКМ - Change).
            FillingControls();
        }

        private bool IsEdit()
        {
            if (_addPrisoner.FirstName == firstNameTextBox.Text &&
                        _addPrisoner.LastName == lastNameTextBox.Text &&
                        _addPrisoner.NickName == nickNameTextBox.Text &&
                        _addPrisoner.Profession == professionComboBox.Text &&
                        _addPrisoner.Growth == Convert.ToInt32(growthComboBox.Text) &&
                        _addPrisoner.HairColor == hairColorComboBox.Text &&
                        _addPrisoner.EyesColor == eyesColorComboBox.Text &&
                        _addPrisoner.Weight == Convert.ToDouble(weightComboBox.Text) &&
                        _addPrisoner.DateOfBirth == Convert.ToString(monthCalendar.SelectionStart) &&
                        _addPrisoner.Citizenship == citizenshipTextBox.Text &&
                        _addPrisoner.LastThing == lastThingTextBox.Text &&
                        _addPrisoner.SpecialSigns == specialSignsTextBox.Text &&
                        _addPrisoner.KnowledgeOfLanguages == knowledgeOfLanguagesTextBox.Text &&
                        _addPrisoner.LastPlaceOfResidence == lastPlaceOfResidenceTextBox.Text &&
                        _addPrisoner.Photos.Equals(photos)

               )
            {
                return false;
            }

            return true;
        }

        private void Add()
        {
            if (state)
            {
                //если обьект создается изначально
                if (_addPrisoner == null)
                {

                    _addPrisoner = new Prisoner
                    {
                        FirstName = firstNameTextBox.Text,
                        LastName = lastNameTextBox.Text,
                        NickName = nickNameTextBox.Text,
                        Profession = professionComboBox.Text,
                        Citizenship = citizenshipTextBox.Text,
                        LastPlaceOfResidence = lastPlaceOfResidenceTextBox.Text,
                        DateOfBirth = monthCalendar.SelectionStart.ToString(),
                        EyesColor = eyesColorComboBox.Text,
                        Growth = Convert.ToInt32(growthComboBox.Text),
                        HairColor = hairColorComboBox.Text,
                        KnowledgeOfLanguages = knowledgeOfLanguagesTextBox.Text,
                        LastThing = lastThingTextBox.Text,
                        SpecialSigns = specialSignsTextBox.Text,
                        Weight = Convert.ToInt32(weightComboBox.Text),
                        Photos = new Photo()
                        {
                            firstPhoto = this.photos.firstPhoto,
                            secondPhoto = this.photos.secondPhoto
                        }
                    };
                }
                //если изменяется (уже существует)
                else
                {
                    //если поля обьекта равны значенияm control, значит изменений не было.
                    if (IsEdit())
                    {

                        _addPrisoner.FirstName = firstNameTextBox.Text;
                        _addPrisoner.LastName = lastNameTextBox.Text;
                        _addPrisoner.NickName = nickNameTextBox.Text;
                        _addPrisoner.Profession = professionComboBox.Text.ToString();
                        _addPrisoner.Growth = Convert.ToInt32(growthComboBox.Text);
                        _addPrisoner.HairColor = hairColorComboBox.Text.ToString();
                        _addPrisoner.EyesColor = eyesColorComboBox.Text;
                        _addPrisoner.Weight = Convert.ToDouble(weightComboBox.Text);
                        _addPrisoner.DateOfBirth = monthCalendar.SelectionStart.ToString();
                        _addPrisoner.Citizenship = citizenshipTextBox.Text;
                        _addPrisoner.LastThing = lastThingTextBox.Text;
                        _addPrisoner.SpecialSigns = specialSignsTextBox.Text;
                        _addPrisoner.KnowledgeOfLanguages = knowledgeOfLanguagesTextBox.Text;
                        _addPrisoner.LastPlaceOfResidence = lastPlaceOfResidenceTextBox.Text;
                        _addPrisoner.Photos = new Photo() { firstPhoto = photos.firstPhoto, secondPhoto = photos.secondPhoto };
                    }

                }
                DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show("Information about the prisoner is incorrect ! (*)", "Warning !",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Add();
            isCloseWhithSaveButton = true;
            this.Close();
        }

        private void progressBar1_ChangeValue(object sender, EventArgs e)
        {
            CustomProgressBar progressBar = sender as CustomProgressBar;

            if (progressBar.Value == 100)
                this.saveButton.Enabled = true;
            else this.saveButton.Enabled = false;
        }

        /// <summary>
        /// Метод по заполнению progressBar.
        /// </summary>
        /// <param name="textBox"></param>
        private void FillingProgressBarLogic(int points)
        {
            if (lockerForPB == true)
            {
                this.progressBar1.Value += points;

                lockerForPB = false;
                backLockerPB = true;
            }

            state = true;
        }

        private void DecreaseProgressBarLogic(int points)
        {
            if (backLockerPB == true)
            {
                if (this.progressBar1.Value != 0)
                    this.progressBar1.Value -= points;

                lockerForPB = true;
                backLockerPB = false;
            }

            state = false;
        }


        #region textBox Logic

        /// <summary>
        /// Обработчик события изменения текста с логикой заполнения progressBar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextChangedHandler(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Check())
            {
                labelMistake.AddMistakeLabel(textBox);

                DecreaseProgressBarLogic(pointsPBImportant);
            }
            else
            {
                labelMistake.DropMistakeLabel(textBox);

                FillingProgressBarLogic(pointsPBImportant);
            }
        }

        /// <summary>
        /// Обработчки события нажатия на textBox ( установка состояния флагов заполнения progressBar ).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            this.lenthTextBox = textBox.Text.Length;

            //если textBox.Text пустой или 
            //заполненный с ошибкой - поведенеие идентичное.
            if (lenthTextBox == 0 || labelMistake.IsContainStar(textBox))
            {
                lockerForPB = true;
                backLockerPB = false;
            }
            else
            {
                lockerForPB = false;
                backLockerPB = true;
            }
        }

        #endregion

        #region comboBox logic

        /// <summary>
        /// Происходит при нажатии на comboBox, устанавливая локеры в начальное состояние.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (String.IsNullOrWhiteSpace(comboBox.Text))
            {
                lockerForPB = true;
                backLockerPB = false;
            }
            else
            {
                lockerForPB = false;
                backLockerPB = true;
            }
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            FillingProgressBarLogic(pointsPBImportant);
            labelMistake.DropMistakeLabel(comboBox);
        }

        private void ComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            DecreaseProgressBarLogic(pointsPBImportant);
            labelMistake.AddMistakeLabel(comboBox);

            comboBox.Text = "";
        }

        /// <summary>
        /// Служит когда необходимо заполнить progressBar при этом не нажимая на comboBox (ПКМ-change).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (_addPrisoner != null)
            {
                ComboBox comboBox = sender as ComboBox;

                FillingProgressBarLogic(pointsPBImportant);
                labelMistake.DropMistakeLabel(comboBox);
            }
        }

        #endregion

        #region monthCalendar logic

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            MonthCalendar monthCalendar = sender as MonthCalendar;
            //filling
            FillingProgressBarLogic(4);

            if (backLockerPB == true)
                labelMistake.DropMistakeLabel(monthCalendar);
        }

        /// <summary>
        /// Обработчик события момента нажатия на
        /// календарь до выбора даты (установка состояния флагов).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar_Enter(object sender, EventArgs e)
        {
            MonthCalendar monthC = (MonthCalendar)sender;

            object tag = monthC.Tag;

            //Если первое условие вернет false, это означает, 
            //что заполнения progress bar уже было.
            if (monthC.SelectionRange.ToString() == tag.ToString())
            {
                lockerForPB = true;
                backLockerPB = false;
            }
            else ResetLocker();
        }

        /// <summary>
        /// Возникает при изменении обьекта (ПКМ-change)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (_addPrisoner != null)
            {
                MonthCalendar monthCalendar = sender as MonthCalendar;
                //filling
                FillingProgressBarLogic(4);

                if (backLockerPB == true)
                    labelMistake.DropMistakeLabel(monthCalendar);
            }
        }

        #endregion

        #region pictureBox logic

        private void PictureBox_MouseClickHandler(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            string path = null;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;

                pictureBox.Load(path);

                //filling
                FillingProgressBarLogic(pointsPB);
            }
            else ResetLocker();

            if (!string.IsNullOrEmpty(path))
            {
                if (pictureBox.Tag.ToString() == "Photo1")
                    photos.firstPhoto = path;
                else photos.secondPhoto = path;
            }
        }

        private void PictureBox_ClickHandler(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox.Image == null)
            {
                lockerForPB = true;
                backLockerPB = false;
            }
            else lockerForPB = false;
        }


        /// <summary>
        /// Отрисока label visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_PaintHandler(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            labelMistake.DropMistakeLabel(pictureBox);
        }

        #endregion


        #region generate logic

        private void GenerateComboBox()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            foreach (ComboBox comboBox in Controls.OfType<ComboBox>())
            {
                comboBox.Text = (comboBox.Items[rand.Next(0, comboBox.Items.Count - 1)]).ToString();

                labelMistake.DropMistakeLabel(comboBox);

                this.progressBar1.Value += 6;
            }
        }

        private void GenerateTextBoxValues()
        {
            foreach (Control textBox in Controls.OfType<TextBox>())
            {
                textBox.Text = typeof(Generate).GetMethod(textBox.Name.FindMethod()).Invoke(null, null).ToString();

                this.progressBar1.Value += 6;
            }
        }

        private void GeneratePictureBoxValues()
        {
            this.photos = Generate.GetPhotos();

            foreach (var pictureBox in Controls.OfType<PictureBox>())
            {
                if (pictureBox.Tag.ToString() == "Photo1")
                    pictureBox.Load(photos.firstPhoto);
                else
                    pictureBox.Load(photos.secondPhoto);

                progressBar1.Value += pointsPB;
            }
        }

        private void GenerateMonthCalendar()
        {
            // this.monthCalendar.SelectionStart = Convert.ToDateTime(_addPrisoner.DateOfBirth);

            monthCalendar.SelectionStart = monthCalendar.GenerateDateTime();
            monthCalendar.SelectionEnd = monthCalendar.SelectionStart;
            monthCalendar.Select();

            labelMistake.DropMistakeLabel(this.monthCalendar);
            this.progressBar1.Value += 4;
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            Clear();

            GenerateComboBox();

            GenerateTextBoxValues();

            GeneratePictureBoxValues();

            GenerateMonthCalendar();
        }

        #endregion

        private void Clear()
        {
            this.progressBar1.Value = 0;

            foreach (Control control in Controls)
            {
                if (control is TextBox)
                    control.Text = "";

                if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;
                    comboBox.Text = "";
                    labelMistake.AddMistakeLabel(comboBox);
                }

                if (control is MonthCalendar)
                {
                    MonthCalendar monthCalendar = control as MonthCalendar;
                    monthCalendar.Tag = monthCalendar?.SelectionRange.ToString();
                    labelMistake.AddMistakeLabel(monthCalendar);
                }
                if (control is PictureBox)
                {
                    PictureBox pictureBox = control as PictureBox;
                    pictureBox.Image = null;
                }
            }

            ResetLocker();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddPrison_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isCloseWhithSaveButton)
            {
                if (_addPrisoner != null && IsEdit())
                {
                    DialogResult dialogRezult = MessageBox.Show("New changes can be lost, are you sure ?", "Warning !",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (dialogRezult == DialogResult.No)
                        e.Cancel = true;
                }
            }
        }

    }
}