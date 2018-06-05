using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEnglish
{
    public class DictionaryString
    {
        String enWord;
        String ruWord;

        public DictionaryString(String enWord, String ruWord)
        {
            this.enWord = enWord;
            this.ruWord = ruWord;
        }

        public String En
        { get { return enWord; } }

        public String Ru
        { get { return ruWord; } }
    }
}
