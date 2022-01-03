using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Stopwatch
{
    public class Countdown
    {
        public EventHandler<CountdownEventArgs> CountdownChanged;

        private DateTime StartTime;
        private DateTime EndTime;
        public int _hours;
        public int _minuts;
        public int _seconds;
        public bool PauseIsAvailable = false;
        public bool IsPaused;
        private TimeSpan Remaining;
        public Countdown(int h, int m, int s)
        {
            this._hours = h;
            this._minuts = m;
            this._seconds = s;
            Thread thread = new Thread(() => TimeRemaining());
            thread.Start();
        }
        //Sets the time, depending on if the time is paused or reset it will set the remaining time to either the remainder of the time before the pause or the time inputed by the user.
        //DateTime is used in case of expansion, wanting days, months or years also, the logic is the same, just needs to add the wanted parameters.
        public void SetTime()
        {
            if (PauseIsAvailable == false)
            {
                StartTime = DateTime.Now;
                EndTime = DateTime.Now.AddHours(_hours).AddMinutes(_minuts).AddSeconds(_seconds);
                Remaining = EndTime - StartTime;
            }
            else if (PauseIsAvailable == true)
            {
                StartTime = DateTime.Now;
                EndTime = DateTime.Now.AddHours(Remaining.Hours).AddMinutes(Remaining.Minutes).AddSeconds(Remaining.Seconds);
                Remaining = EndTime - StartTime;
            }
        }
        //Calculates the time remaining every .25 sec.
        public TimeSpan TimeRemaining()
        {
            while (true)//remaining.TotalSeconds > 0)
            {
                if ((IsPaused == false) && (Remaining.TotalSeconds > 0))
                {
                    Remaining = EndTime - DateTime.Now;
                    CountdownChanged?.Invoke(this, new CountdownEventArgs(Remaining));
                    Thread.Sleep(250);
                }
                else if (Remaining.TotalSeconds < 0)
                {
                    EndOfCount();
                }
            }
        }
        //Plays the sound at the end of the countdown.
        public void EndOfCount()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.PlayEndOfCount();
        }
    }
}
