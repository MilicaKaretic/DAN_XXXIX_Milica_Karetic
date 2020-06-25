using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XXXIX_Milica_Karetic
{
    class AudioPlayer
    {
        public Song song = new Song();

        public void OpenPlayer()
        {
            song.WriteSongs(song.GetAllSongs());
            Console.WriteLine("Pick song to listen");
            List<Song> songs = new List<Song>();

        }
    }
}
