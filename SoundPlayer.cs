using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace Stopwatch
{
    public class SoundPlayer
    {
        private System.Media.SoundPlayer soundPlayer;

        public SoundPlayer()
        {
            soundPlayer = new System.Media.SoundPlayer();
        }

        //Gets the file location.
        String FileDir()
        {
            string folder = Directory.GetCurrentDirectory();
            string file = @"\Files\FinalCountDown.wav";
            return folder + file;
        }
        //Plays the file.
        public void PlayEndOfCount()
        {
            soundPlayer.SoundLocation = FileDir();
            soundPlayer.PlaySync();
        }
    }
}
