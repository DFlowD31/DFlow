using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DFlow
{
    public class quality : IDisposable
    {
        //Columns
        public Nullable<Int64> id { get; set; } = null; //AutoIncrement
        public string name { get; set; }

        public string alternate_name { get; set; }

        //Properties
        public List<string> properties { get; } = new List<string>() { "database_name", "properties", "AIs", "Clauses" };
        public List<string> AIs { get; } = new List<string>() { "id" };
        public string database_name { get; } = "qualities";
        public List<string> Clauses { get; } = new List<string> { "id" };

        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }
    }
}