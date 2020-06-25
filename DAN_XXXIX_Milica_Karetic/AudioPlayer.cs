using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DAN_XXXIX_Milica_Karetic
{
    class AudioPlayer
    {
        public Song song = new Song();
        public Validation v = new Validation();

        private string fileName = @"..\..\Files\Adds.txt";
        /// <summary>
        /// event for song end
        /// </summary>
        private static AutoResetEvent event1 = new AutoResetEvent(false);
        /// <summary>
        /// timer for playing song
        /// </summary>
        public static Timer t;
        /// <summary>
        /// timer for writing adds
        /// </summary>
        public static Timer tAdds;
        /// <summary>
        /// list all adds from file
        /// </summary>
        private static List<string> addsList = new List<string>();
        private static Random rnd = new Random();

        /// <summary>
        /// Pick song from list
        /// </summary>
        /// <returns>Picked song</returns>
        private Song PickSong()
        {
            List<Song> songs = new List<Song>();
            songs = song.GetAllSongs();
            Song s = new Song();
            int num;
            do
            {
                int a = 0;
                Console.WriteLine("Pick song to listen");
                num = v.ValidPositiveNumber();
                for (int i = 0; i < songs.Count; i++)
                {
                    if ((num - 1) == i)
                        a++;
                }
                if(a != 0)
                    break;
                else
                {
                    Console.WriteLine("Wrong input. Try again.");
                }
            } while (true);

            s = songs[num - 1];
            return s;

        }

        /// <summary>
        /// Get song duration time from song properties hours, minutes and seconds
        /// </summary>
        /// <param name="s">Song</param>
        /// <returns>Song duration</returns>
        private string GetSongDuration(Song s)
        {
            string hours = s.SongTimeHours;
            string minutes = s.SongTimeMinutes;
            string seconds = s.SongTimeSeconds;

            string time = hours + ":" + minutes + ":" + seconds;

            return time;
        }

        /// <summary>
        /// Playing song - two timerCallbacks for playing song and adds
        /// </summary>
        /// <param name="duration"></param>
        private static void PlayingSong(object duration)
        {
            //end song time
            TimeSpan endTime = TimeSpan.Parse( DateTime.Now.ToLongTimeString()) + TimeSpan.Parse(duration.ToString());

            //write playing song per 1000 msc
            t = new Timer(new TimerCallback(Print), endTime, 0, 1000);
            
            //write random add from file per 200 msc
            tAdds = new Timer(new TimerCallback(PrintAdds), endTime, 0, 200);
        }

        /// <summary>
        /// Get all adds from file
        /// </summary>
        /// <returns>List adds</returns>
        private List<string> GetAllAdds()
        {
            List<string> adds = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    adds.Add(line);
                }
            }

            return adds;
        }

        /// <summary>
        /// Get random add from add list
        /// </summary>
        /// <param name="adds">Add list</param>
        /// <returns>Random add</returns>
        private static  string GetRandomAdd(List<string> adds)
        {
            int maxIndex = adds.Count;
            int num = rnd.Next(0, maxIndex);
            return adds[num];
        }

        /// <summary>
        /// Method called by timer
        /// </summary>
        /// <param name="endTime">End song time</param>
        public static void PrintAdds(object endTime)
        {
            //random add
            string add = GetRandomAdd(addsList);

            //if song is in progress
            if (TimeSpan.Parse(DateTime.Now.ToLongTimeString()) < (TimeSpan)endTime)
            {
                Console.WriteLine(add);
            }
            else
            {
                Console.WriteLine("Song ends. Press return to exit or any other key to continue...");
                //signal end song
                event1.Set();
            }
        }

        /// <summary>
        /// Method called by timer
        /// </summary>
        /// <param name="endTime"></param>
        public static void Print(object endTime)
        {
            if(TimeSpan.Parse(DateTime.Now.ToLongTimeString()) < (TimeSpan)endTime)
            {
                Console.WriteLine("Playing song...");
            }
            else
            {
                Console.WriteLine("Song ends. Press return to exit or any other key to continue..."); 
                //signal end song
                event1.Set();
            }
            
        }
        
        /// <summary>
        /// Open player - list all songs and you can pick song to play
        /// </summary>
        internal void OpenPlayer()
        {                  
            List<Song> songs = new List<Song>();
                    
            Validation v = new Validation();
            addsList = GetAllAdds();

            do
            {
                //show all songs
                song.WriteSongs(song.GetAllSongs());
                //picked song
                Song pickedSong = PickSong();
                //now
                string timeNow = DateTime.Now.ToLongTimeString();
                //song duration
                string timeSong = GetSongDuration(pickedSong);
                
                //message to user
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + pickedSong.Name);

                //thread that notify user that song is playing
                Thread t1 = new Thread(PlayingSong);
                t1.Start(timeSong);

                //wait song to end
                event1.WaitOne();
                //dispose timers
                t.Dispose();
                tAdds.Dispose();

            } while (Console.ReadLine() != "return");


        }
    }
}
