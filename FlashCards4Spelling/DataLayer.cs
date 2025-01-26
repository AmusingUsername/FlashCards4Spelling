using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

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
        private const string filePathWords = "words.xml";
        private const string filePathResults = "results.xml";

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
            tableResult.Columns.Add(colResultCorrect);
            tableResult.PrimaryKey = new DataColumn[] { tableResult.Columns[col_words_word], tableResult.Columns[col_results_time] };
            ds.Tables.Add(tableResult);

            ds.Relations.Add(new DataRelation(tbl_words + "_" + tbl_results
                , ds.Tables[tbl_words].Columns[col_words_word]
                , ds.Tables[tbl_results].Columns[col_words_word]));

            loadData();
        }

        private void loadData()
        {
            loadDataTableWords();
            loadDataTableResults();
        }

        private void loadDataTableWords()
        {
            if (File.Exists(filePathWords))
            {
                ds.Tables[tbl_words].ReadXml(filePathWords);
            }
        }

        private void loadDataTableResults()
        {
            if (File.Exists(filePathResults))
            {
                ds.Tables[tbl_results].ReadXml(filePathResults);
            }
        }

        internal void exportData()
        {
            exportDataTableWords();
        }

        private void exportDataTableWords()
        {
            DataView dv = new DataView(ds.Tables[tbl_words]);
            dv.Sort = col_words_word + " ASC";
            dv.ToTable(tbl_words).WriteXml(filePathWords);
        }

        private void exportDataTableResults()
        {
            DataView dv = new DataView(ds.Tables[tbl_results]);
            dv.Sort = col_words_word + " ASC, " + col_results_time + " DESC";
            dv.ToTable(tbl_results).WriteXml(filePathResults);
        }

        internal void saveResults()
        {
            exportDataTableResults();
        }

        internal void addWord(string word, string category = "")
        {
            //insert
            DataRow dr = ds.Tables[tbl_words].NewRow();
            dr[col_words_word ] = word.Trim();
            dr[col_words_category ] = category;
            //active and mastered are set by default values
            ds.Tables[tbl_words].Rows.Add(dr);
            DataLayerChanged?.Invoke(word.Trim());
        }

        internal void updateWordSpelling(string word, string wordNewSpelling)
        {
            //check for word existing, update spelling or delete/insert new word
            throw new NotImplementedException();
        }

        internal void updateWordCategory(string word, string category)
        {
            throw new NotImplementedException();
        }
        internal void updateWordMastered(string word, bool isMastered = true)
        {
            throw new NotImplementedException();
        }

        internal void updateWordActive(string word, bool isActive)
        {
            throw new NotImplementedException();
        }

        internal void deleteWord(string word)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all words, regardless of whether active or mastered
        /// </summary>
        /// <returns>string[]</returns>
        internal string[] getWordsAll()
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
        private string[] getWordsByCategory(string category, bool isMastered = false, bool isActive = true)
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

        internal string getWordCategory(string word)
        {
            string category = string.Empty;
            DataRow dr = getWordDataRow(word);
            category = dr.Field<string>(col_words_category);
            return category;
        }

        internal bool getWordMastered(string word)
        {
            bool mastered = false;
            DataRow dr = getWordDataRow(word);
            mastered = dr.Field<bool>(col_words_mastered);
            return mastered;
        }

        internal bool getWordActive(string word)
        {
            bool active = true;
            DataRow dr = getWordDataRow(word);
            active = dr.Field<bool>(col_words_active);
            return active;
        }

        private DataRow getWordDataRow(string word)
        {
            DataRow dr = null;
            string filter = col_words_word + " = '" + word + "'";
            DataRow[] drs = ds.Tables[tbl_words].Select(filter);
            if (drs.Length > 0)
            {
                dr = drs[0];
            }
            return dr;
        }

        internal List<string> getResultsList(string word)
        {
            List<string> results = new List<string>();
            string filter = col_words_word + " = '" + word + "'";
            DataRow[] drs = ds.Tables[tbl_results].Select(filter);
            foreach (DataRow dr in drs)
            {
                string time = dr.Field<DateTime>(col_results_time).ToString("yyyy/MM/dd");
                string correct = (dr.Field<bool>(col_results_correct) ? "Correct" : "Incorrect");
                results.Add(time + "   " + correct);
            }
            return results;
        }

        /// <summary>
        /// returns the number of completed attempts for given word
        /// </summary>
        /// <param name="word">word for which to retrieve results</param>
        /// <returns>int</returns>
        internal int getResultsAttempts(string word)
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
        internal int getResultsCorrect(string word)
        {
            int correct = getResults(word, DateTime.MinValue, DateTime.MaxValue, true);
            return correct;
        }

        internal int getResultsAttemptsRange(string word, DateTime start, DateTime end)
        {
            int attempts = getResults(word, start, end, true);
            return attempts;
        }

        internal int getResultsAttemptsSince(string word, DateTime start)
        {
            int attempts = getResults(word, start, DateTime.MaxValue);
            return attempts;
        }

        internal int getResultsCorrectRange(string word, DateTime start, DateTime end)
        {
            int correct = getResults(word, start, end, true);
            return correct;
        }

        internal int getResultsCorrectSince(string word, DateTime start)
        {
            int correct = getResults(word, start, DateTime.MaxValue, true);
            return correct;
        }

        private int getResults(string word, DateTime start, DateTime end, bool onlyCorrect = false)
        {
            int result = -1;
            string filter = col_words_word + " = '" + word + "'";
            if (onlyCorrect) { filter += " AND " + col_results_correct + " = " + true.ToString(); }
            if (start > DateTime.MinValue) { filter += " AND " + col_results_time + " > " + start; }
            if (end < DateTime.MaxValue) { filter += " AND " + col_results_time + " < " + end; }
            result = ds.Tables[tbl_results].Select(filter).Length;

            return result;
        }

        internal void setAttemptResult(string word, bool responseWasCorrect)
        {
            DataRow dr = ds.Tables[tbl_results].NewRow();
            dr[col_words_word] = word;
            dr[col_results_correct] = responseWasCorrect;
            dr[col_results_time] = DateTime.UtcNow;
            try
            {
                ds.Tables[tbl_results].Rows.Add(dr);
            }
            catch(Exception ex) 
            {
                //Debugger.Break();
                Console.WriteLine(ex.ToString());
            }
            
        }

        internal List<string> getWordsList(int count)
        {
            List<string> words = new List<string>();
            if (count > 0)
            {
                List<string> tempWords = getWordsByCategory("").ToList<string>();
                if(count > tempWords.Count) { count = tempWords.Count; }
                for (int i = 0; i < count; i++)
                {
                    int tempIndex = new Random().Next(0, tempWords.Count);
                    string tempWord = tempWords[tempIndex];
                    tempWords.RemoveAt(tempIndex);
                    words.Add(tempWord);
                }
            }
            return words;
        }

        public delegate void DataLayerChangedHandler(string word);
        public event DataLayerChangedHandler DataLayerChanged;
    }
}
