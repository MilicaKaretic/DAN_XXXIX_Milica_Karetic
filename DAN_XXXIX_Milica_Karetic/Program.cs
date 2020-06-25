using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XXXIX_Milica_Karetic
{
   public  class Program
    {
        public static List<Song> songs = new List<Song>();

        public static void PrintOptions()
        {
            Console.WriteLine("1. Add new song");
            Console.WriteLine("2. Get all songs");
            Console.WriteLine("3. Open audio player");
            Console.WriteLine("4. Exit");
            Console.WriteLine("*You can use 'return' to return back to the main menu ");
        }
        static void Main(string[] args)
        {
            Song song = new Song();
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
                        song.AddSong();
                        Console.WriteLine();
                        break;
                    case "2":
                        //song.GetAllSongs();
                        song.WriteSongs(song.GetAllSongs());
                        Console.WriteLine();
                        break;
                    case "3":
                        
                        Console.WriteLine();
                        break;
                    case "4":
                        break;
                    default:
                        Console.Write("Invalid input.Try again: ");
                        break;
                }

            } while (selected != "4");

            Console.ReadKey();
        }
    }
}
