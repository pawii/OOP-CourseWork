using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexLinguistics.NET;
using System.Windows;

namespace LearnEnglish
{
    static class MyTranslator
    {
        static Translator tr;
        const String translatorKey = "trnsl.1.1.20180411T013745Z.f564d05ed65b66d8.bf0dcf0d166f7d7efda3be460bd2c6bfda0ed6ad";

        static MyTranslator()
        {
            try { tr = new Translator(translatorKey); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public static String TranslateToRus(String input)
        {
            try { return tr.Translate(input, LangPair.EnRu).Text; }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static String TranslateToEn(String input)
        {

            try { return tr.Translate(input, LangPair.RuEn).Text; }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static YandexLinguistics.NET.Lang GetLanguage(String input)
        {
            try
            { return tr.DetectLang(input); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return YandexLinguistics.NET.Lang.None;
            }
        }
    }
}
