using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagement.Infrastructure.Configuration
{
    public class NoteManagementConfig
    {
        private string _serviceUrl;

        public string ServiceUrl
        {
            get
            {
                return string.IsNullOrEmpty(_serviceUrl) ? "" : _serviceUrl;
            }
            set { _serviceUrl = value; }
        }
    }
}
