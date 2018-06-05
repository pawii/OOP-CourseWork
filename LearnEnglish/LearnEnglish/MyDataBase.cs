using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LearnEnglish
{
    static partial class MyDataBase
    {
        static SqlConnection sqlConnection;
        const String connectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pawii\Desktop\Учеба\Курсач\LearnEnglish\LearnEnglish\Database.mdf;
Integrated Security=True;MultipleActiveResultSets=True;";

        static MyDataBase()
        {
            /*try
            {
                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open(); //НАДО АСИНХРОННО
            }
            catch (Exception ex)
            {
                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open(); //НАДО АСИНХРОННО
            }*/
        }

        public static Boolean Create()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open(); //НАДО АСИНХРОННО

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void Close()
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }

    static partial class MyDataBase
    {
        public static async Task<Int32> GetId(String login)
        {
            Object result;
            SqlCommand command = new SqlCommand("Select Id from [dbo].[Authorization] Where login=@login", sqlConnection);
            command.Parameters.AddWithValue("@login", login);

            try
            {
                //sqlConnection = new SqlConnection(connectionString);

                //sqlConnection.Open(); 


                result = await command.ExecuteScalarAsync();
                if (result != null)
                    return (Int32)result;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        public static async Task<String> GetPassword(Int32 id)
        {
            Object result;
            SqlCommand command = new SqlCommand("Select password from [dbo].[Authorization] Where Id='" + id + "'", sqlConnection);

            try
            {
                result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    return (String)result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static async Task<Int32> GetCountWords(Int32 id)
        {
            Object result;
            SqlCommand command = new SqlCommand("Select count (UserId) from [dbo].[Dictionary] Where UserId = '" + id + "'", sqlConnection);

            try
            {
                result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    return (Int32)result;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        public static async Task<Int32> GetCountOffers(Int32 id)
        {
            Object result;
            SqlCommand command = new SqlCommand("Select count (UserId) from [dbo].[Offer Dictionary] Where UserId = '" + id + "'", sqlConnection);

            try
            {
                result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    return (Int32)result;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        public static async Task Registration(String login, String password)
        {
            SqlCommand command = new SqlCommand("Insert into [dbo].[Authorization] (login, password) values " +
                    "(@login, @password)", sqlConnection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task<Boolean> CheckLogins(String currentLogin)
        {
            SqlCommand command = new SqlCommand("Select login from [dbo].[Authorization]", sqlConnection);
            SqlDataReader reader = null;

            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (String.Equals(currentLogin, reader.GetValue(0).ToString()))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            { reader.Close(); }
        }

        public static async Task AddWord(Int32 id, String en, String ru)
        {
            SqlCommand command = new SqlCommand("Insert into [dbo].[Dictionary] (UserId, en, ru) values " +
                  "(@id, @en, @ru)", sqlConnection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@en", en);
            command.Parameters.AddWithValue("@ru", ru);

            try
            {
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Слово добавлено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task<List<DictionaryString>> GetWords(Int32 id)
        {
            List<DictionaryString> result = new List<DictionaryString>();
            SqlCommand command = new SqlCommand("Select [En], [Ru] from [dbo].[Dictionary] Where UserId = '" + id + "'", sqlConnection);
            SqlDataReader reader = null;

            try
            {
                reader = await command.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    result.Add(new DictionaryString(reader.GetString(0), reader.GetString(1)));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            { reader.Close(); }
        }

        public static async Task<List<DictionaryString>> GetCountWords(Int32 id, Int32 count)
        {
            List<DictionaryString> result = new List<DictionaryString>();
            SqlCommand command = new SqlCommand("Select top(" + count + ") [En], [Ru] from [dbo].[Dictionary] Where UserId = '" + id + 
                "' Order by [WordPriority] desc", sqlConnection);
            SqlDataReader reader = null;

            try
            {
                reader = await command.ExecuteReaderAsync();
                for(var i = 0; i < count; i++)
                {
                    await reader.ReadAsync();
                    result.Add(new DictionaryString(reader.GetString(0), reader.GetString(1)));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            { reader.Close(); }
        }

        public static async Task<List<DictionaryString>> GetCountOffers(Int32 id, Int32 count)
        {
            List<DictionaryString> result = new List<DictionaryString>();
            SqlCommand command = new SqlCommand("Select top(" + count + ") [En], [Ru] from [dbo].[Offer Dictionary] Where UserId = '" + id +
                "' Order by [OfferPriority] desc", sqlConnection);
            SqlDataReader reader = null;

            try
            {
                reader = await command.ExecuteReaderAsync();
                for (var i = 0; i < count; i++)
                {
                    await reader.ReadAsync();
                    result.Add(new DictionaryString(reader.GetString(0), reader.GetString(1)));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            { reader.Close(); }
        }

        public static async Task ChangeWordPriority(Int32 id, String enWord, Int32 value)
        {
            try
            {
                SqlCommand command1 = new SqlCommand("Select [WordPriority] from [dbo].[Dictionary] Where UserId = '" + id +
                "' And [En] Like @enWord", sqlConnection);
                command1.Parameters.AddWithValue("@enWord", enWord);
                Int32 wordPriority = (Int32)(await command1.ExecuteScalarAsync());

                wordPriority += value;
                SqlCommand command2 = new SqlCommand("Update [dbo].[Dictionary] set [WordPriority] = " + wordPriority 
                    + " Where UserId = '" + id + "' And [En] Like @enWord", sqlConnection);
                command2.Parameters.AddWithValue("@enWord", enWord);
                await command2.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task ChangeOfferPriority(Int32 id, String enWord, Int32 value)
        {
            try
            {
                SqlCommand command1 = new SqlCommand("Select [OfferPriority] from [dbo].[Offer Dictionary] Where UserId = '" + id +
                "' And [En] Like @enWord", sqlConnection);
                command1.Parameters.AddWithValue("@enWord", enWord);
                Int32 offerPriority = (Int32)(await command1.ExecuteScalarAsync());

                offerPriority += value;
                SqlCommand command2 = new SqlCommand("Update [dbo].[Offer Dictionary] set [OfferPriority] = " + offerPriority
                    + " Where UserId = '" + id + "' And [En] Like @enWord", sqlConnection);
                command2.Parameters.AddWithValue("@enWord", enWord);
                await command2.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task<List<DictionaryString>> GetWordsLevel(Int32 id)
        {
            List<DictionaryString> result = new List<DictionaryString>();
            SqlCommand command = new SqlCommand(
                "Select En, Ru from WordsLevel" + 
                " inner join Levels on" +
                " WordsLevel.Level = Levels.Level" +
                " Where Levels.[Min Count Words] <= (Select count(UserId) from Dictionary Where UserId = " + id +")" +
                " And Levels.[Max Count Words] >= (Select count(UserId) from Dictionary Where UserId = "+ id + ")", 
                sqlConnection);

            SqlDataReader reader = null;

            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new DictionaryString(reader.GetString(0), reader.GetString(1)));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            { reader.Close(); }
        }

        public static async Task<Int32> GetLevel(Int32 id)
        {
            Object result;
            SqlCommand command = new SqlCommand("Select [Level] from [dbo].[Levels] Where [Min Count Words] <= (Select count(UserId) from Dictionary Where UserId = " + id +")" +
                " And [Max Count Words] >= (Select count(UserId) from Dictionary Where UserId = " + id + ")", sqlConnection);

            try
            {
                result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    return (Int32)result;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }

        public static async Task<List<DictionaryString>> GetOffers(Int32 id)
        {
            List<DictionaryString> result = new List<DictionaryString>();
            SqlCommand command = new SqlCommand("Select [En], [Ru] from [dbo].[Offer Dictionary] Where UserId = '" + id + "'", sqlConnection);
            SqlDataReader reader = null;

            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new DictionaryString(reader.GetString(0), reader.GetString(1)));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            { reader.Close(); }
        }

        public static async Task AddOffer(Int32 id, String en, String ru)
        {
            SqlCommand command = new SqlCommand("Insert into [dbo].[Offer Dictionary] (UserId, En, Ru) values " +
                  "(@id, @en, @ru)", sqlConnection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@en", en);
            command.Parameters.AddWithValue("@ru", ru);

            try
            {
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Предложение добавлено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}