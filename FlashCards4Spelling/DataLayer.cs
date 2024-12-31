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
    }
}
