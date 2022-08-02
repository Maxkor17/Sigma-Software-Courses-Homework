using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Reader
{
    public static List<string> ReadText(string filePath)
    {
        List<string> result = new List<string>();
        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
                result.Add(reader.ReadLine());
        }
        return result;
    }

    public static Dictionary<string, string> ReadDictionary(string filePath)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        if (!File.Exists(filePath)) throw new FileNotFoundException("Not found dictionary");
        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string temp = reader.ReadLine();
                try
                {
                    var str = temp.Split('-');
                    if (str.Length != 2) throw new ArgumentException("Incorrect pair of key - value");
                    result.Add(str[0], str[1]);
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }
        }
        return result;
    }

    public static void WriteToDictionary(string key, string value, string filePath)
    {
        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.Write("\n");
            writer.Write($"{key}-{value}");
        }
    }
}

