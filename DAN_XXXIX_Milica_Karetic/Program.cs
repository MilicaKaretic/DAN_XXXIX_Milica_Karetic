using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XXXIX_Milica_Karetic
{
   public  class Program
    {
        public static List<Song> songs = new List<Song>();

        /// <summary>
        /// Menu options
        /// </summary>
        public static void PrintOptions()
        {
            Console.WriteLine("1. Add new song");
            Console.WriteLine("2. Get all songs");
            Console.WriteLine("3. Open audio player");
            Console.WriteLine("4. Exit");
            Console.WriteLine("*You can use 'return' to return back to the main menu ");
        }

        public static void Main(string[] args)
        {
            Song song = new Song();
            AudioPlayer audio = new AudioPlayer();
            string selected = "";

            do
            {
                Console.WriteLine("Welcome! We offer next options:");
                PrintOptions();
                Console.Write("Your choice is: ");
                selected = Console.ReadLine();
                switch (selected)
                {
                    case "1":
                        Thread t1 = new Thread(song.AddSong);
                        t1.Start();
                        t1.Join();
                        Console.WriteLine();
                        break;
                    case "2":
                        songs = song.GetAllSongs();
                        Thread t2 = new Thread(() => song.WriteSongs(songs));
                        t2.Start();
                        t2.Join();
                        Console.WriteLine();
                        break;
                    case "3":
                        Thread t3 = new Thread(audio.OpenPlayer);
                        t3.Start();
                        t3.Join();
                        Console.WriteLine();
                        break;
                    case "4":
                        break;
                    default:
                        Console.Write("Invalid input.Try again.");
                        break;
                }

            } while (selected != "4");

        }
    }
}
