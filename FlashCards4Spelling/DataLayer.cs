using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace FlashCards4Spelling
{
    internal class DataLayer
    {
        private DataSet ds;
        //store the following columns/tables as strings to prevent typos
        private const string tbl_words = "words";
        private const string tbl_results = "results";
        private const string col_words_word = "word";
        private const string col_words_active = "active";
        private const string col_words_category = "category";
        private const string col_words_mastered = "mastered";
        private const string col_results_time = "timestamp";
        private const string col_results_correct = "correct";

        public DataLayer()
        { 
            this.ds = new DataSet();
            

            DataTable tableWord = new DataTable(tbl_words);
            //foreign/primary key
            DataColumn dcWordsWord = new DataColumn(col_words_word, typeof(string));
            dcWordsWord.Unique = true;
            tableWord.Columns.Add(dcWordsWord);
            DataColumn dcWordsActive = new DataColumn(col_words_active, typeof(bool));
            dcWordsActive.DefaultValue = true;
            tableWord.Columns.Add(dcWordsActive);
            tableWord.Columns.Add(new DataColumn(col_words_category, typeof(string)));
            DataColumn dcWordsMastered = new DataColumn(col_words_mastered, typeof(bool));
            dcWordsMastered.DefaultValue = false;
            tableWord.Columns.Add(dcWordsMastered);
            tableWord.PrimaryKey = new DataColumn[] { tableWord.Columns[col_words_word] };

            ds.Tables.Add(tableWord);

            DataTable tableResult = new DataTable(tbl_results);
            DataColumn dcResultsWord = new DataColumn(col_words_word,typeof(string));
            dcResultsWord.Unique = true;
            tableResult.Columns.Add(dcResultsWord);
            DataColumn colResultTimestamp = new DataColumn(col_results_time, typeof(DateTime));
            colResultTimestamp.AllowDBNull = false;
            colResultTimestamp.DateTimeMode = DataSetDateTime.Utc;
            tableResult.Columns.Add(colResultTimestamp);
            DataColumn colResultCorrect = new DataColumn(col_results_correct, typeof(bool));
            colResultCorrect.DefaultValue = false;
            tableResult.PrimaryKey = new DataColumn[] { tableResult.Columns[col_words_word], tableResult.Columns[col_results_time] };
            ds.Tables.Add(tableResult);

            ds.Relations.Add(new DataRelation(tbl_words + "_" + tbl_results
                , ds.Tables[tbl_words].Columns[col_words_word]
                , ds.Tables[tbl_results].Columns[col_words_word]));
        }

        public void addWord(string word, string category = "")
        {
            //insert
            DataRow dr = ds.Tables[tbl_words].NewRow();
            dr[col_words_word ] = word;
            dr[col_words_category ] = category;
            //active and mastered are set by default values
            ds.Tables[tbl_words].Rows.Add(dr);
            DataLayerChanged?.Invoke(word);
        }

        public void updateWordSpelling(string word, string wordNewSpelling)
        {
            //check for word existing, update spelling or delete/insert new word
            throw new NotImplementedException();
        }

        public void updateWordCategory(string word, string category)
        {
            throw new NotImplementedException();
        }
        public void updateWordMastered(string word, bool isMastered = true)
        {
            throw new NotImplementedException();
        }

        public void updateWordActive(string word, bool isActive)
        {
            throw new NotImplementedException();
        }

        public void deleteWord(string word)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all words, regardless of whether active or mastered
        /// </summary>
        /// <returns>string[]</returns>
        public string[] getWordsAll()
        {
            string filter = string.Empty;
            return select(filter);
        }
        
        /// <summary>
        /// Returns words, filtered by category (optional), Mastered (default false) and Active (default true)
        /// </summary>
        /// <param name="category">Category to filter by, String.empty if should not be filtered by Category</param>
        /// <param name="isMastered">true if word is mastered, false if not (default false)</param>
        /// <param name="isActive">true if word is active, false if not (default true)</param>
        /// <returns>string[]</returns>
        public string[] getWordsByCategory(string category, bool isMastered = false, bool isActive = true)
        {
            string filter = col_words_active + " = " + isActive.ToString();
            filter += " AND " + col_words_mastered + " = " + isMastered.ToString();
            if (category != string.Empty) { filter += " AND " + col_words_category + " = " + category; }
            return select(filter);
        }

        private string[] select(string filter)
        {
            List<string> words = new List<string>();
            foreach (DataRow dr in ds.Tables[tbl_words].Select(filter, col_words_word + " ASC"))
            {
                words.Add(dr.Field<string>(col_words_word));
            }
            return words.ToArray();
        }

        /// <summary>
        /// returns the number of completed attempts for given word
        /// </summary>
        /// <param name="word">word for which to retrieve results</param>
        /// <returns>int</returns>
        public int getResultsAttempts(string word)
        {
            int attempts = 0;
            attempts = getResults(word, DateTime.MinValue, DateTime.MaxValue);
            return attempts;
        }

        /// <summary>
        /// returns the number of attempts with a correct response for the given word
        /// </summary>
        /// <param name="word">word for which to retrieve results</param>
        /// <returns>int</returns>
        public int getResultsCorrect(string word)
        {
            int correct = getResults(word, DateTime.MinValue, DateTime.MaxValue, true);
            return correct;
        }

        public int getResultsAttemptsRange(string word, DateTime start, DateTime end)
        {
            int attempts = getResults(word, start, end, true);
            return attempts;
        }

        public int getResultsAttemptsSince(string word, DateTime start)
        {
            int attempts = getResults(word, start, DateTime.MaxValue);
            return attempts;
        }

        public int getResultsCorrectRange(string word, DateTime start, DateTime end)
        {
            int correct = getResults(word, start, end, true);
            return correct;
        }

        public int getResultsCorrectSince(string word, DateTime start)
        {
            int correct = getResults(word, start, DateTime.MaxValue, true);
            return correct;
        }

        private int getResults(string word, DateTime start, DateTime end, bool onlyCorrect = false)
        {
            int result = -1;
            string filter = col_words_word + " = " + word;
            if (onlyCorrect) { filter += " AND " + col_results_correct + " = " + true.ToString(); }
            if (start > DateTime.MinValue) { filter += " AND " + col_results_time + " > " + start; }
            if (end < DateTime.MaxValue) { filter += " AND " + col_results_time + " < " + end; }
            result = ds.Tables[tbl_results].Select(filter).Length;

            return result;
        }

        public delegate void DataLayerChangedHandler(string word);
        public event DataLayerChangedHandler DataLayerChanged;
    }
}
