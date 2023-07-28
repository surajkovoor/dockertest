using System.IO;
using Microsoft.Extensions.Configuration;

namespace aspCoreWebApp.Utility
{
    public static class ConnectionString
    {
        public static string ConnectionStringUser = "Server=xeon-m2;Database=SachinYadav_Training;Trusted_Connection=True;";
        public static string Con { get => ConnectionStringUser; }
        ////public static string Con  => con  = string.Empty;
        //public static string ConnectionStringuser { get; private set; } = string.Empty;
    }
}
