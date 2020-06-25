using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XXXIX_Milica_Karetic
{
    public class Song
    {
        public int SongID { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string SongTimeHours { get; set; }
        public string SongTimeMinutes { get; set; }
        public string SongTimeSeconds { get; set; }

        public string FileName = @"..\..\Files\Music.txt";

        public Song()
        {

        }

        public Song(string author, string name)
        {
            Author = author;
            Name = name;
        }

        public Song(string author, string name, string songTimeHours, string songTimeMinutes, string songTimeSeconds) : this(author, name)
        {
            SongTimeHours = songTimeHours;
            SongTimeMinutes = songTimeMinutes;
            SongTimeSeconds = songTimeSeconds;
        }

        public List<Song> GetAllSongs()
        {
            List<Song> songs = new List<Song>();
            List<string> list = new List<string>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Song song = new Song();
                    list = line.Split(':', '[').ToList();
                    song.Author = list[0];
                    song.Name = list[1];
                    song.SongTimeHours = list[2];                   
                    song.SongTimeMinutes = list[3];
                    song.SongTimeSeconds = list[4];

                    songs.Add(song);
                }
            }
            return songs;
        }

        /// <summary>
        /// Creates new mentor
        /// </summary>
        public  void AddSong()
        {
            Validation v = new Validation();
            //get mentors from file
            List<Song> songs = new List<Song>();
            songs = GetAllSongs();

            string author = v.GetInput("Enter author");
            string name = v.GetInput("Enter song name");
            Console.WriteLine("Enter duration time");
            string time = v.IsValidTimeFormat();

            List<string> list = new List<string>();
            list = time.Split(':').ToList();
            string timeH = list[0];
            string timeM = list[1];
            string timeS = list[2];

            Song song = new Song(author, name, timeH, timeM, timeS);
            songs.Add(song);
            //apply changes to file
            WriteSongsFile(songs);
        }

        

        /// <summary>
        /// Write songs in file
        /// </summary>
        /// <param name="members"></param>
        public void WriteSongsFile(List<Song> songs)
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                for (int i = 0; i < songs.Count; i++)
                {
                    sw.WriteLine(songs[i].Author + ":" + songs[i].Name + "[" + songs[i].SongTimeHours + ":" + songs[i].SongTimeMinutes + ":" + songs[i].SongTimeSeconds + "]");
                }
            }
        }

        /// <summary>
        /// Write songs in console
        /// </summary>
        /// <param name="members"></param>
        public void WriteSongs(List<Song> songs)
        {

            for (int i = 0; i < songs.Count; i++)
            {
                Console.WriteLine(songs[i].Author + ":" + songs[i].Name + "[" + songs[i].SongTimeHours + ":" + songs[i].SongTimeMinutes + ":" + songs[i].SongTimeSeconds);
            }

        }
    }
}
