using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrisonerWinF.Model;
using System.Windows.Forms;

namespace PrisonerWinF
{
    [Serializable]    
    public class Prisoner
    {
        int Id;
        public int ID
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }

        string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                //if (!(string.IsNullOrEmpty(value)) && value.CheckNumber())
                //    firstName = value;
                //else throw new ArgumentOutOfRangeException("The value can not be null or have a number!");
            }
        }

        string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        string nickName;
        public string NickName
        {
            get
            {
                return nickName;
            }

            set
            {
                nickName = value;
            }
        }

        string profession;
        public string Profession
        {
            get
            {
                return profession;
            }

            set
            {
                profession = value;
            }
        }

        int growth;
        public int Growth
        {
            get
            {
                return growth;
            }

            set
            {
                growth = value;
            }
        }

        double weight;
        public double Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }

        string hairColor;
        public string HairColor
        {
            get
            {
                return hairColor;
            }

            set
            {
                hairColor = value;
            }
        }

        string eyesColor;
        public string EyesColor
        {
            get
            {
                return eyesColor;
            }

            set
            {
                eyesColor = value;
            }
        }

        string specialSigns;
        /// <summary>
        /// Особые приметы.
        /// </summary>
        public string SpecialSigns
        {
            get
            {
                return specialSigns;
            }

            set
            {
                specialSigns = value;
            }
        }

        string citizenship;
        /// <summary>
        /// Гражданство.
        /// </summary>
        public string Citizenship
        {
            get
            {
                return citizenship;
            }

            set
            {
                citizenship = value;
            }
        }

        string lastPlaceOfResidence;
        /// <summary>
        /// Последнее место проживания.
        /// </summary>
        public string LastPlaceOfResidence
        {
            get
            {
                return lastPlaceOfResidence;
            }

            set
            {
                lastPlaceOfResidence = value;
            }
        }

        string dateOfBirth;
        public string DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }

            set
            {
                dateOfBirth = value;
            }
        }

        string knowledgeOfLanguages;
        public string KnowledgeOfLanguages
        {
            get
            {
                return knowledgeOfLanguages;
            }

            set
            {
                knowledgeOfLanguages = value;
            }
        }

        string lastThing;
        public string LastThing
        {
            get
            {
                return lastThing;
            }

            set
            {
                lastThing = value;
            }
        }

        public Photo Photos { get; set; }


        public Prisoner()
        {

        }

        public override bool Equals(object obj)
        {
            Prisoner prisoner = obj as Prisoner;
            if (this.FirstName == prisoner.FirstName &&
                this.LastName == prisoner.LastName &&
                this.NickName == prisoner.NickName &&
                this.Citizenship == prisoner.Citizenship &&
                this.ID == prisoner.ID &&
                this.KnowledgeOfLanguages == prisoner.KnowledgeOfLanguages &&
                this.HairColor == prisoner.HairColor &&
                this.EyesColor == prisoner.EyesColor &&
                this.Growth == prisoner.Growth &&
                this.LastPlaceOfResidence == prisoner.LastPlaceOfResidence &&
                this.Profession == prisoner.Profession &&
                this.Weight == prisoner.Weight &&
                this.SpecialSigns == prisoner.SpecialSigns)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return this.Profession.GetHashCode() ^ this.FirstName.GetHashCode() ^
                this.LastName.GetHashCode() ^ this.Profession.GetHashCode() ^
                this.Citizenship.GetHashCode() ^ this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format($"{FirstName}");
        }


    }
}
