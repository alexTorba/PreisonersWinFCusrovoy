using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrisonerWinF.View
{
    public class LabelMistake
    {
        private AddPrison addPrisonerForm;

        public LabelMistake(AddPrison addPrisonerForm)
        {
            this.addPrisonerForm = addPrisonerForm;
        }

        public void AddMistakeLabel(TextBox textBox)
        {
            textBox.ForeColor = Color.Red;

            Label label = this.addPrisonerForm.Controls.OfType<Label>().SingleOrDefault(x => x.Name == textBox.Tag.ToString());
            if (!label.Text.Contains("*"))
                label.Text += "*";
        }

        public void AddMistakeLabel(MonthCalendar monthC)
        {
            Label label = addPrisonerForm.Controls.OfType<Label>().SingleOrDefault(x => x.Name.ToString() == "dateOfBirthLabel");

            if (!label.Text.Contains("*"))
                label.Text += "*";
        }

        public void AddMistakeLabel(ComboBox comboBox)
        {
            Label label = addPrisonerForm.Controls.OfType<Label>().SingleOrDefault(x => x.Name.ToString() == comboBox.Tag.ToString());

            if (!label.Text.Contains("*"))
                label.Text += "*";
        }

        public void DropMistakeLabel(ComboBox comboBox)
        {
            Label label = addPrisonerForm.Controls.OfType<Label>().SingleOrDefault(x => x.Name.ToString() == comboBox.Tag.ToString());

            if (label.Text.Contains("*"))
                label.Text = label.Text.Remove(label.Text.Count() - 1, 1);
        }

        public void DropMistakeLabel(TextBox textBox)
        {
            textBox.ForeColor = Color.Black;

            Label label = addPrisonerForm.Controls.OfType<Label>().SingleOrDefault(x => x.Name == textBox.Tag.ToString());

            if (label.Text.Contains("*") && textBox.Text.Length != 1)
                label.Text = label.Text.Remove(label.Text.Count() - 1, 1);
        }

        public void DropMistakeLabel(MonthCalendar mothC)
        {
            Label label = addPrisonerForm.Controls.OfType<Label>().SingleOrDefault(x => x.Name.ToString() == "dateOfBirthLabel");

            if (label.Text.Contains("*"))
                label.Text = label.Text.Remove(label.Text.Length - 1, 1);
        }

        public void DropMistakeLabel(PictureBox pictureBox)
        {
            Label label = this.addPrisonerForm.Controls.OfType<Label>().SingleOrDefault(x => x.Text == pictureBox.Tag.ToString());

            if (pictureBox.Image == null)
                label.Visible = true;
            else label.Visible = false;
        }

        public bool IsContainStar(TextBox textBox)
        {
            Label label = this.addPrisonerForm.Controls.OfType<Label>().SingleOrDefault(x => x.Name == textBox.Tag.ToString());
            if (label.Text.Contains("*"))
                return true;

            return false;
        }
    }
}
