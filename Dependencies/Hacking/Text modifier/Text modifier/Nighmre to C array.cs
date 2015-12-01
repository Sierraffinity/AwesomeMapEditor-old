using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsApplication1
{
    class Nighmre_to_C_array
    {
        public Nighmre_to_C_array(string input, string output)
        {
            StreamReader streamReader = new StreamReader(input);
            StreamWriter streamWriter = new StreamWriter(output);
            string line = streamReader.ReadLine();
            while (!streamReader.EndOfStream)
            {
                line = line.ToUpper();
                line = line.Replace(" ", "_");
                streamWriter.WriteLine("\"" + line + "\"" + ",");
                line = streamReader.ReadLine(); 
            }
            streamReader.Close();
            streamWriter.Close();
            MessageBox.Show("Finished");
        }
    }
}
