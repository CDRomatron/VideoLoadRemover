using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoLoadRemover
{
    public class INI
    {
        Dictionary<string, int> settings;

        public INI(string filepath)
        {
            settings = new Dictionary<string, int>();

            if (File.Exists(filepath))
            {
                string[] lines = File.ReadAllLines(filepath);
                foreach (var line in lines)
                {
                    string[] split = line.Split('=');
                    settings.Add(split[0], Convert.ToInt32(split[1]));
                }
            }
            else
            {
                settings.Add("x", 0);
                settings.Add("y", 0);
                settings.Add("w", 100);
                settings.Add("h", 100);
                settings.Add("t", 0);
                settings.Add("fps", 30);
                Save(filepath);
            }
            
        }

        public void Save(string filepath)
        {
            string[] lines = new string[6];

            for (int i = 0; i < settings.Count; i++)
            {
                lines[i] = settings.Keys.ElementAt(i).ToString() + "=" + settings.Values.ElementAt(i).ToString();
            }

            File.WriteAllLines(filepath, lines);
        }

        public int GetValue(string key)
        {
            settings.TryGetValue(key, out int val);
            return val;
        }

        public void SetValue(string key, int value)
        {
            settings[key] = value;
        }
    }
}
