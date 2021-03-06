﻿using System.Threading;

namespace SimpleIRCLib
{
    class Pinger
    {
        //vars needed to ping pong with the server
        private string ping = "PING :";
        private Thread pingSender;
        private SimpleIRC simpleirc;
        private IrcConnect ircConnect;

        //creates a thread for the pinger
        public Pinger(SimpleIRC sirc, IrcConnect ircCon)
        {
            simpleirc = sirc;
            ircConnect = ircCon;
            pingSender = new Thread(new ThreadStart(this.Run));
        }
        //starts the ping thread
        public void Start()
        {
            pingSender.Start();
        }

        //starts the ping thread
        public void Stop()
        {
            if (pingSender.IsAlive) {
                pingSender.Abort();
                simpleirc.DebugCallBack("\n STOPPED PINGER BY ABORTING: ");
            } else
            {
                simpleirc.DebugCallBack("Pinger stopped already :X \n");
            }

        }
        //function that runs in the ping thread, used to keep the connection with the irc server alive
        private void Run()
        {
            while (!simpleirc.shouldClientStop)
            {
                ircConnect.writeIrc(ping + ircConnect.newIP);
                Thread.Sleep(15000);
            }
            simpleirc.DebugCallBack("\n STOPPED PINGER: ");
        }
    }
}
