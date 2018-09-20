using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace PrisonerWinF.Model
{
    static public class DataBase
    {
        static BinaryFormatter serialization;
        static FileStream fileStream;

        static DataBase()
        {
            serialization = new BinaryFormatter();
        }


        static public void WriteToFile(ref PrisonerCollections collect)
        {
            using (fileStream = new FileStream(@"..\..\_database.txt", FileMode.Open, FileAccess.Write))
            {
                try
                {
                    serialization.Serialize(fileStream, collect);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }


        static public void ReadFileVoid(ref PrisonerCollections collect)
        {
            using (fileStream = new FileStream(@"..\..\_database.txt", FileMode.Open, FileAccess.ReadWrite))
            {
                try
                {
                    collect = serialization.Deserialize(fileStream) as PrisonerCollections;
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
