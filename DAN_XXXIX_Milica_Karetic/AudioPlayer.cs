using System;
using System.Collections.Generic;
using System.Threading;

namespace DAN_XXXIX_Milica_Karetic
{
    class AudioPlayer
    {
        public Song song = new Song();
        public Validation v = new Validation();

        public static AutoResetEvent event1 = new AutoResetEvent(false);

        public Song PickSong()
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

        public string GetSongDuration(Song s)
        {
            string hours = s.SongTimeHours;
            string minutes = s.SongTimeMinutes;
            string seconds = s.SongTimeSeconds;

            string time = hours + ":" + minutes + ":" + seconds;

            return time;
        }

        public static bool end = false;
        public static void PlayingSong(object duration)
        {
            TimeSpan endTime = TimeSpan.Parse( DateTime.Now.ToLongTimeString()) + TimeSpan.Parse(duration.ToString());

            t = new Timer(new TimerCallback(Print), endTime, 0, 1000);

        }

        public static void Print(object endTime)
        {
            if(TimeSpan.Parse(DateTime.Now.ToLongTimeString()) < (TimeSpan)endTime)
            {
                Console.WriteLine("Playing song...");
            }
            else
            {
                Console.WriteLine("Song ends. Press return to exit or any other key to continue...");               
                event1.Set();
            }
            

        }
        

        static Timer t;
        public void OpenPlayer()
        {                  
            List<Song> songs = new List<Song>();
            
            
            Validation v = new Validation();

            do
            {
                song.WriteSongs(song.GetAllSongs());
                Song pickedSong = PickSong();
                string timeNow = DateTime.Now.ToLongTimeString();
                string timeSong = GetSongDuration(pickedSong);

                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + pickedSong.Name);

                Thread t1 = new Thread(PlayingSong);
                t1.Start(timeSong);

                event1.WaitOne();
                t.Dispose();

            } while (Console.ReadLine() != "return");


        }
    }
}
