using System.Text;
using System.Text.Json;

namespace FileProcessing
{
    /// <summary>
    /// Class for reading and writing json file.
    /// </summary>
    public class JsonProcessing
    {
        /// <summary>
        /// Read file from stream and create a List
        /// Return empty list if something in file is wrong.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>List of Recreators from file.</returns>
        public static List<Recreator> Read(Stream stream)
        {
            stream.Position = 0;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            using (StreamReader reader = new StreamReader(stream))
            {
                List<Recreator> list;
                string jsonString = reader.ReadToEnd();
                try
                {
                     list = JsonSerializer.Deserialize<List<Recreator>>(jsonString, options);
                }
                catch
                {
                    List<Recreator> list2 = new List<Recreator>();
                    return list2;
                }
                return list;
            }
        }


        /// <summary>
        /// Write data from List in Memory stream.
        /// </summary>
        /// <param name="recreators"></param>
        /// <returns>Memory string with data and position in start.</returns>
        public static MemoryStream Write(List<Recreator> recreators)
        {
            MemoryStream stream = new MemoryStream();
            
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            {
                writer.WriteLine("[");
                for (int i = 0; i < recreators.Count; i++)
                {
                    string json = recreators[i].ToJson();
                    json = "  " + json.Replace("\n", "\n  ");
                    if (i == recreators.Count - 1)
                    {
                        writer.WriteLine(json);
                    }
                    else
                    {
                        writer.WriteLine(json + ',');
                    }
                }
                writer.WriteLine("]");
            }
            stream.Position = 0;
            return stream;
        }
    }
}
