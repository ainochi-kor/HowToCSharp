using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChattingSystem_Server
{
    public class ClientInfo
    {
        private string _clientIP = null;
        private string _channel = null;
        private int _port;
        private string _message = null;

        internal ClientInfo(string clientIP, int port, string channel)
        {
            this._clientIP = clientIP;
            this._port = port;
            this._channel = channel;
        }
        public string ClientIP
        {
            get { return _clientIP; }
            set { _clientIP = value; }
        }
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        public string Channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

    }

    public class ClientHandler : IObservable<ClientInfo>
    {
        private List<IObserver<ClientInfo>> _observers;
        private List<ClientInfo> _items;

        public ClientHandler()
        {
            _observers = new List<IObserver<ClientInfo>>();
            _items = new List<ClientInfo>();
        }

        public IDisposable Subscribe(IObserver<ClientInfo> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                foreach (var item in _items)
                    observer.OnNext(item);
            }
            return new Unsubscriber<ClientInfo>(_observers, observer);
        }

        public void BaggageStatus(string clinetIP ,int port)
        {
            BaggageStatus(clinetIP, 0, String.Empty);
        }

        public void BaggageStatus(string clientIP, int port, string channel)
        {
            var info = new ClientInfo(clientIP, port, channel);

            if (port > 0 && !_items.Contains(info))
            {
                _items.Add(info);
                foreach (var observer in _observers)
                    observer.OnNext(info);
            }
            else if (port == 0)
            {
                var flightsToRemove = new List<ClientInfo>();
                foreach (var flight in _items)
                {
                    if (info.Port == flight.Port)
                    {
                        flightsToRemove.Add(flight);
                        foreach (var observer in _observers)
                            observer.OnNext(info);
                    }
                }
                foreach (var flightToRemove in flightsToRemove)
                    _items.Remove(flightToRemove);

                flightsToRemove.Clear();
            }
        }


        public void LastBaggageClaimed()
        {
            foreach (var observer in _observers)
                observer.OnCompleted();

            _observers.Clear();
        }
    }

    internal class Unsubscriber<ClientInfo> : IDisposable
    {
        private List<IObserver<ClientInfo>> _observers;
        private IObserver<ClientInfo> _observer;

        internal Unsubscriber(List<IObserver<ClientInfo>> observers, IObserver<ClientInfo> observser)
        {
            this._observers = observers;
            this._observer = observser;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
