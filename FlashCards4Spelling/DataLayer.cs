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
        private DataSet db;

        public DataLayer()
        { 
            this.db = new DataSet();
            
            //foreign/primary key
            DataColumn colWord = new DataColumn("word", typeof(string));
            colWord.Unique = true;

            DataTable tableWord = new DataTable();
            tableWord.Columns.Add(colWord);
            tableWord.Columns.Add(new DataColumn("active", typeof(bool)));
            tableWord.Columns.Add(new DataColumn("category", typeof(string)));
            DataColumn colResultsMastered = new DataColumn("mastered", typeof(bool));
            colResultsMastered.DefaultValue = false;
            tableWord.Columns.Add(colResultsMastered);
            tableWord.PrimaryKey = new DataColumn[] { tableWord.Columns["word"] };

            db.Tables.Add(tableWord);

            DataTable tableResult = new DataTable();
            tableResult.Columns.Add(colWord);
            DataColumn colResultTimestamp = new DataColumn("timestamp", typeof(DateTime));
            colResultTimestamp.AllowDBNull = false;
            colResultTimestamp.DateTimeMode = DataSetDateTime.Utc;
            tableResult.Columns.Add(colResultTimestamp);
            DataColumn colResultCorrect = new DataColumn("correct", typeof(bool));
            colResultCorrect.DefaultValue = false;
            tableResult.PrimaryKey = new DataColumn[] { tableResult.Columns["word"], tableResult.Columns["timestamp"] };
            db.Tables.Add(tableResult);

        }

        public void addWord(string word, string category = "")
        {
            //insert
            throw new NotImplementedException();
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

        public string[] getWords(bool isActive = true, bool isMastered = false)
        {
            throw new NotImplementedException();
            List<string> words = new List<string>();
            return words.ToArray();
        }

        public string[] getWordsByCategory(string category, bool isMastered = false, bool isActive = true)
        {
            throw new NotImplementedException();
            List<string> words = new List<string>();
            return words.ToArray();
        }

        public int getResultsAttempts(string word)
        {
            int attempts = 0;
            attempts = getResults(word, DateTime.MinValue, DateTime.MaxValue);
            return attempts;
        }

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
            throw new NotImplementedException();
            int result = 0;
            //select based on parameters to get a count of rows
            return result;
        }
    }
}
