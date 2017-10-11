using System;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace AbcSample.DAL.Storages.Table
{
    static class TableStorageTokenSerializer
    {
        public static string SerializeToken(TableContinuationToken token)
        {
            string serialized = null;
            if (token != null)
            {
                serialized = JsonConvert.SerializeObject(token);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(serialized);
                serialized = Convert.ToBase64String(plainTextBytes);
            }

            return serialized;
        }

        public static TableContinuationToken DeserializeToken(string token)
        {
            TableContinuationToken contToken = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(token);
                string serialized = Encoding.UTF8.GetString(base64EncodedBytes);
                contToken = JsonConvert.DeserializeObject<TableContinuationToken>(serialized);
            }

            return contToken;
        }
    }
}