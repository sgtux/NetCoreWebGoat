using System;
using NetCoreWebGoat.Helpers;
using Newtonsoft.Json;
using Npgsql;

namespace NetCoreWebGoat.Models
{
    public class CspReportModel
    {
        [JsonProperty(PropertyName = "csp-report")]
        public CspReportModel.CspReportProperty CspReport { get; set; }

        public class CspReportProperty
        {
            public int Id { get; set; }

            public DateTime Date { get; set; }

            public string DateFormatted => Date.ToString("dd/MM/yy HH:mm:ss");

            [JsonProperty(PropertyName = "blocked-uri")]
            public string BlockedUri { get; set; }

            [JsonProperty(PropertyName = "column-number")]
            public int ColumnNumber { get; set; }

            [JsonProperty(PropertyName = "document-uri")]
            public string DocumentUri { get; set; }

            [JsonProperty(PropertyName = "line-number")]
            public int LineNumber { get; set; }

            [JsonProperty(PropertyName = "original-policy")]
            public string OriginalPolicy { get; set; }

            [JsonProperty(PropertyName = "referrer")]
            public string Referrer { get; set; }

            [JsonProperty(PropertyName = "source-file")]
            public string SourceFile { get; set; }

            [JsonProperty(PropertyName = "violated-directive")]
            public string ViolatedDirective { get; set; }

            public CspReportProperty() { }

            public CspReportProperty(NpgsqlDataReader dataReader) => FillFromDataReader(dataReader);

            public void FillFromDataReader(NpgsqlDataReader dataReader)
            {
                Id = DatabaseHelper.GetValueOrNull<int>(dataReader, "Id");
                Date = DatabaseHelper.GetValueOrNull<DateTime>(dataReader, "Date");
                BlockedUri = DatabaseHelper.GetValueOrNull<string>(dataReader, "BlockedUri");
                ColumnNumber = DatabaseHelper.GetValueOrNull<int>(dataReader, "ColumnNumber");
                DocumentUri = DatabaseHelper.GetValueOrNull<string>(dataReader, "DocumentUri");
                LineNumber = DatabaseHelper.GetValueOrNull<int>(dataReader, "LineNumber");
                OriginalPolicy = DatabaseHelper.GetValueOrNull<string>(dataReader, "OriginalPolicy");
                Referrer = DatabaseHelper.GetValueOrNull<string>(dataReader, "Referrer");
                SourceFile = DatabaseHelper.GetValueOrNull<string>(dataReader, "SourceFile");
                ViolatedDirective = DatabaseHelper.GetValueOrNull<string>(dataReader, "ViolatedDirective");
            }
        }
    }
}