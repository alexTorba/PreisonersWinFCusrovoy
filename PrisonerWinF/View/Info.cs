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
using System.Reflection;
using System.Threading;

namespace PrisonerWinF.View
{
    public partial class Info : Form
    {
        Prisoner _prisoner = null;
        static bool firstPhotoLock = true;
        static bool secondPhotoLock = false;

        System.Threading.Timer timer;

        public Info(Prisoner prisoner)
        {
            InitializeComponent();

            _prisoner = prisoner;
        }

        private void ChangePhoto(object state)
        {

            if (firstPhotoLock)
            {
                this.pictureBox.Load(_prisoner.Photos.firstPhoto);
                firstPhotoLock = false;
                secondPhotoLock = true;
            }
            else if (secondPhotoLock == true)
            {
                this.pictureBox.Load(_prisoner.Photos.secondPhoto);
                firstPhotoLock = true;
                secondPhotoLock = false;
            }
        }

        private void SetValue()
        {
            foreach (var label in Controls.OfType<Label>().
                Where(x => x.Name.ToLowerInvariant().Contains("value")))
            {

                label.Text = (_prisoner.GetType().GetFields(
                    BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance).SingleOrDefault(
                    x => x.Name.ToLowerInvariant().Contains(label.Tag.ToString().ToLowerInvariant())
                    )).GetValue(_prisoner).ToString();
            }
        }

        private void Info_Load(object sender, EventArgs e)
        {
            SetValue();

            timer = new System.Threading.Timer(ChangePhoto, null, 0, 1270);
        }

        private void Info_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose(true);
        }
    }
}
