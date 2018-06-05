using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEnglish.Data
{
    public class DictionaryString
    {
        public String En { get; set; }
        public String Ru { get; set; }

        public DictionaryString(String enWord, String ruWord)
        {
            this.En = enWord;
            this.Ru = ruWord;
        }

        public static List<DictionaryString> GetWords(Int32 id)
        {
            List<DictionaryString> result = new List<DictionaryString>();
            foreach (DictionaryString str in MyDataBase.GetWords(id))
                result.Add(str);
            return result;
        }
    }
}
