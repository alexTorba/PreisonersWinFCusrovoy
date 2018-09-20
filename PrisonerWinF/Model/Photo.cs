using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrisonerWinF.Model
{
    [Serializable]
    public class Photo
    {        
        public string firstPhoto { get; set; }
        public string secondPhoto { get; set; }

        public override bool Equals(object obj)
        {
            Photo photo = obj as Photo;
            if (this.firstPhoto == photo.firstPhoto &&
                this.secondPhoto == photo.secondPhoto)
                return true;

            return false;
        }
    }
}
