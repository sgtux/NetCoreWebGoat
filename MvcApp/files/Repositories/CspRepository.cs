using System;
using System.Collections.Generic;
using NetCoreWebGoat.Config;
using NetCoreWebGoat.Models;

namespace NetCoreWebGoat.Repositories
{
    public class CspRepository : BaseRepository
    {
        public CspRepository(AppConfig appConfig) : base(appConfig) { }

        public void Add(CspReportModel.CspReportProperty model)
        {
            var sql = $"INSERT INTO \"CspReport\" (Date, BlockedUri, ColumnNumber, DocumentUri, LineNumber, OriginalPolicy, Referrer, SourceFile, ViolatedDirective) VALUES (@Date, @BlockedUri, @ColumnNumber, @DocumentUri, @LineNumber, @OriginalPolicy, @Referrer, @SourceFile, @ViolatedDirective)";

            model.Date = DateTime.Now;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@Date", model.Date);
            dic.Add("@BlockedUri", model.BlockedUri);
            dic.Add("@ColumnNumber", model.ColumnNumber);
            dic.Add("@DocumentUri", model.DocumentUri);
            dic.Add("@LineNumber", model.LineNumber);
            dic.Add("@OriginalPolicy", model.OriginalPolicy);
            dic.Add("@Referrer", model.Referrer);
            dic.Add("@SourceFile", model.SourceFile);
            dic.Add("@ViolatedDirective", model.ViolatedDirective);
            ExecuteNonQuery(sql, dic);
        }

        public List<CspReportModel.CspReportProperty> GetAll()
        {
            List<CspReportModel.CspReportProperty> list = new List<CspReportModel.CspReportProperty>();
            var sql = $"SELECT Id, Date, BlockedUri, ColumnNumber, DocumentUri, LineNumber, OriginalPolicy, Referrer, SourceFile, ViolatedDirective FROM \"CspReport\" ORDER BY Date DESC";
            var result = Query(sql);
            if (result.HasRows)
                while (result.Read())
                    list.Add(new CspReportModel.CspReportProperty(result));
            return list;
        }

        public void Delete(int id)
        {
            var sql = $"DELETE FROM \"CspReport\" WHERE Id = @Id";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@Id", id);
            ExecuteNonQuery(sql, dic);
        }
    }
}