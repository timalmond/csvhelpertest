using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace CSVHelperRun
{
    public class CSVProcessor<T>
    {
        public CSVProcessor()
        {
            _csvErrors=new List<CSVError>();
        }

        protected List<T> _records { get; set; }

        public List<T> Records
        {
            get { return _records; }
        }

        protected List<CSVError> _csvErrors { get; set; }

        public List<CSVError> CsvErrors
        {
            get { return _csvErrors; }
        }

        public void Validate(StreamReader sr)
        {
            string errors = string.Empty;

            var reader = new CsvReader(sr);
            reader.Configuration.HasHeaderRecord = false;
            
            reader.Configuration.ReadingExceptionOccurred = exception =>
            {
                var x = (CsvHelper.TypeConversion.TypeConverterException) exception;
                var field = x.MemberMapData.Names[0];
                _csvErrors.Add(
                    new CSVError()
                    {
                        rowId = exception.ReadingContext.Row,
                        Field=field,
                        Error = exception.Message
                    }
                    );
                errors += exception.ReadingContext.RawRecord;
            };

            _records = reader.GetRecords<T>().ToList();
        }
    }
}
