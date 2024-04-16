using System.Runtime.CompilerServices;
using System.Text;

namespace FileProcessing
{
    /// <summary>
    /// Methods for csv reading and writing.
    /// </summary>
    public static class CsvProcessing
    {
        /// <summary>
        /// Reading data from file.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>List of Recreatos obj from file.</returns>
        public static List<Recreator> Read(MemoryStream stream)
        {
            var recreators = new List<Recreator>();

            stream.Position = 0;

            using (var reader = new StreamReader(stream))
            {
                string line;
                int lineNumber = 0;
                string s = reader.ReadToEnd();
                string[] data = s.Replace("\n", "").Split(';');
                byte count = 0;
                List<string> tekObj = new List<string>();
                // Skip header. 
                for(int i = 12; i < data.Length; i++)
                {
                    tekObj.Add(data[i]);
                    if (tekObj.Count == 6)
                    {
                        for (int j = 0; j < tekObj.Count; j++)
                        {
                            tekObj[j] = tekObj[j].Trim('\"');
                            if (tekObj[j] is null || tekObj[j].Equals(""))
                            {
                                if (j == 1 || j == 5)
                                {
                                    tekObj[j] = "1111";
                                }
                                else
                                {
                                    tekObj[j] = "No Data";
                                }
                            }
                        }
                        if (int.TryParse(tekObj[1], out int t) && long.TryParse(tekObj[5], out long t2))
                        {
                            Recreator tRec = new Recreator(tekObj[0], t, tekObj[2], tekObj[3], tekObj[4], t2);
                            recreators.Add(tRec);
                        }
                        tekObj = new List<string>();
                    }
                    
                }
            }
            return recreators;

        }

        /// <summary>
        /// Write data from List to memory stream.
        /// </summary>
        /// <param name="recreators"></param>
        /// <returns> stream with data on 0 position</returns>
        public static MemoryStream Write(List<Recreator> recreators)
        {
            MemoryStream stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
            {
                writer.WriteLine("\"Name\";\"RankYear\";\"MainObjects\";\"Workplace\";\"Photo\";\"global_id\";");
                writer.WriteLine("\"ФИО почетного реставратора\";\"Год присуждения звания\";" +
                    "\"Основные объекты реставрации\";\"Место работы\";\"Фотография\";\"global_id\";");
                writer.NewLine = "\n";
                foreach (var recreator in recreators)
                {
                    writer.WriteLine(recreator.ToCsv());
                }
            }
            stream.Position = 0;
            return stream;
        }
    }
}
