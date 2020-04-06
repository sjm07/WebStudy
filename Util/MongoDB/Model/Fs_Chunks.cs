using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.MongoDB.Model
{
    public class Fs_Chunks
    {
        public Fs_Chunks()
        { }

        public ObjectId _id { get; set; }
        public ObjectId files_id { get; set; }
        public int n { set; get; }
        public byte[] data { set; get; }
    }
}
