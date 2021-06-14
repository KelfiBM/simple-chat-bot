using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Simple.Chat.Bot.CommandWorker.Helpers
{
  public class CSVParser
  {
    public static List<T> ParseFromUrl<T>(string url)
    {
      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
      using HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
      using StreamReader sr = new(resp.GetResponseStream());
      using var csvParser = new CsvReader(sr, CultureInfo.InvariantCulture);
      var result = new List<T>();
      while (csvParser.Read())
      {
        result.Add(csvParser.GetRecord<T>());
      }
      return result;
    }
  }
}