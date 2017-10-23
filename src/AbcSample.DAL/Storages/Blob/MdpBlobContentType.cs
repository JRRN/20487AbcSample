using System.ComponentModel;

namespace AbcSample.DAL.Storages.Blob
{
    public enum MdpBlobContentType
    {
        [Description("text/plain")]
        Text,
        [Description("application/xml")]
        Xml,
        [Description("application/pdf")]
        Pdf,
        [Description("application/json")]
        Json,
        [Description("text/html")]
        Html,
        [Description("image/jpeg")]
        Jpeg,
        [Description("image/png")]
        Png,
        [Description("image/gif")]
        Gif,
        [Description("image/bmp")]
        Bmp,
        [Description("image/tiff")]
        Tiff,
        [Description("text/csv")]
        Csv,
    }
}