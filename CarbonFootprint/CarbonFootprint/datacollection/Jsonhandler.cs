using System;
using System.IO;
using System.Collections.Generic;
using CarbonFootprint.utilities;
using Newtonsoft.Json;

namespace CarbonFootprint.DataCollection
{
    public class Jsonhandler
    {
        public static Jsonhandler Instance;

        private string m_Path;
        
        public Jsonhandler()
        {
            Instance = this;
            m_Path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public bool CheckIfFileExists(string _fileName)
        {
            return File.Exists(Path.Combine(m_Path, _fileName));
        }

        public void UploadJson(string _fileName, object _toSerialize)
        {
            string filePath = Path.Combine(m_Path, _fileName);
            if(!File.Exists(filePath))
                File.Create(filePath).Close();

            string output = JsonConvert.SerializeObject(_toSerialize);
            File.WriteAllText(filePath, String.Empty);
            File.WriteAllText(filePath, output);
        }

        public T RequestObject<T>(string _fileName)
        {
            string filePath = Path.Combine(m_Path, _fileName);
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
        }
    }
}