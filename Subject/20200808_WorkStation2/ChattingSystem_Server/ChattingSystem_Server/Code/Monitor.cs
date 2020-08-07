using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChattingSystem_Server
{
    public class Monitor : IObserver<ClientInfo>
    {
        private string name;
        private List<string> _clinetInfos = new List<string>();
        private IDisposable cancellation;
        private string fmt = "{0,-20} {1,5} {2,3} {3,10}";

        public Monitor(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("The observer must be assigned a name");

            this.name = name;
        }

        public virtual void Subscribe(ClientHandler provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
            _clinetInfos.Clear();
        }

        public virtual void OnCompleted()
        {
            _clinetInfos.Clear();
        }

        public virtual void OnError(Exception e)
        {
            //오류남.
        }
        
        public virtual void OnNext(ClientInfo info)
        {
            bool updated = false;

            if (info.Port == 0)
            {
                var flightsToRemove = new List<string>();
                string Channel = String.Format(info.Channel);

                foreach (var clientInfo in _clinetInfos)
                {
                    if (clientInfo.Substring(21, 5).Equals(Channel))
                    {
                        flightsToRemove.Add(clientInfo);
                        updated = true;
                    }
                }
                foreach (var flightToRemove in flightsToRemove)
                    _clinetInfos.Remove(flightToRemove);

                flightsToRemove.Clear();
            }
            else
            {
                string flightInfo = String.Format(fmt, info.ClientIP, info.Port, info.Channel);
                MessageBox.Show(flightInfo);
                if (!_clinetInfos.Contains(flightInfo))
                {
                    _clinetInfos.Add(flightInfo);
                    updated = true;
                }
            }
            if (updated)
            {
                MessageBox.Show("여기에요 여기!");
                _clinetInfos.Sort();
                Console.WriteLine("Arrivals information from {0}", this.name);
                foreach (var flightInfo in _clinetInfos)
                    Console.WriteLine(flightInfo);

                Console.WriteLine();
            }
         

        }
    }
}
