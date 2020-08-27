using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoLoadRemover
{
    public class Response
    {
        public string response;
        public float val;

        public Response(string response, float val)
        {
            this.response = response;
            this.val = val;
        }
    }
}
