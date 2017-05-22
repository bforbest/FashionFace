using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tweetinvi;
using Twitter.Hubs;

namespace Twitter.Extentions
{
    public class TwitterStream
    {
        private readonly IHubContext _context;
        private int MaleFaces = 0;
        private int FemaleFaces = 0;

        public TwitterStream(string track)
        {
            _context = GlobalHost.ConnectionManager.GetHubContext<TwitterHub>();
            Auth.SetCredentials(TwitterCredential.GenerateCredentials());
            GetTweetWithImage(track);

        }

        public void GetTweetWithImage(string track)
        {
            var timer = Stopwatch.StartNew();
            List<string> pics = new List<string>();
            var tweetCount = 0;
            var stream = Stream.CreateFilteredStream();
            stream.AddTrack(track);
            stream.MatchingTweetReceived += (sender, args) =>
            {
                tweetCount++;
                var tweet = args.Tweet;
                if (tweet.Entities.Medias.Count > 0 &&
                    tweet.Entities.Medias[0].MediaType == "photo")
                {
                    var faces = FaceApi.MakeDetectRequest(tweet.Entities.Medias[0].MediaURL).Result;
                    if (faces != null)
                    {
                        foreach (var item in faces)
                        {
                            if (item.faceAttributes.gender == "male")
                            {
                                MaleFaces++;
                            }
                            else
                            {
                                FemaleFaces++;
                            }
                        }
                    }
                    pics.Add(tweet.Entities.Medias[0].MediaURL);
                }

                _context.Clients.All.broadcast(new
                {
                    females = FemaleFaces,
                    males = MaleFaces,
                    TweetCount = tweetCount,
                    etime = timer.Elapsed.Seconds
                });

                if (tweetCount >= 100)
                {
                    var elapsedTime = timer.Elapsed.Seconds;
                    timer.Stop();
                    var eTimeTweetsPerHour = tweetCount / elapsedTime * 3600;
                    var eFemalesPerHour = (double) (FemaleFaces * 3600) / (double) elapsedTime;
                    var eMalesPerHour = (double) (MaleFaces * 3600) / (double) elapsedTime;
                    _context.Clients.All.statistics(new
                    {
                        eTime = eTimeTweetsPerHour,
                        eFemale = Convert.ToInt32(eFemalesPerHour),
                        eMales = Convert.ToInt32(eMalesPerHour)
                    });
                    _context.Clients.All.pics(pics.Take(10).Reverse());
                    stream.StopStream();
                }
            };
            stream.StartStreamMatchingAllConditions();
        }
    }
}