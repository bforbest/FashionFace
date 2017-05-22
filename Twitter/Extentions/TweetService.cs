using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Twitter.Models;

namespace Twitter.Extentions
{
    public class TweetService
    {
        private readonly  List<FaceDetection> _faces;
         public TweetService(List<FaceDetection> faces)
        {
            
            _faces = faces;
        }
        public int CountFaces()
        {
            return 0;
        }
    }
}