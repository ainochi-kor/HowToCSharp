using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObserverPattern
{
    public class BaggageInfo
    {
        private int flightNo;
        private string origin;
        private int location;
        private string message;

        internal BaggageInfo(int flight, string from, int carousel, string msg)
        {
            this.flightNo = flight;
            this.origin = from;
            this.location = carousel;
            this.message = msg;
        }

        public int FlightNumber
        {
            get { return this.flightNo; }
        }
        public string From
        {
            get { return origin; }
        }
        public int Carousel
        {
            get { return location; }
        }
        public string MSG
        {
            get { return message; }
        }
    }

    public class BaggageHandler : IObservable<BaggageInfo>
    {
        //리스트를 형태(틀)를 만듭니다.
        private List<IObserver<BaggageInfo>> observers;
        private List<BaggageInfo> flights;

        public BaggageHandler()
        {
            //observers와 flight 리스트를 생성합니다.
            observers = new List<IObserver<BaggageInfo>>();
            flights = new List<BaggageInfo>();
        }

        public IDisposable Subscribe(IObserver<BaggageInfo> observer)
        {
            //구독을 신청하였을 때, 옵저버리스트에 옵저버가 포함되어 있지 않다면
            if (!observers.Contains(observer))
            {
                //옵저버리스트에 옵저버를 추가합니다.
                observers.Add(observer);
                //사용자가 넘겨준 항목을 생성된 옵저버 안에 속성으로 추가합니다.
                foreach (var item in flights)
                    observer.OnNext(item);
            }
            //Unsubscriber에 정보를 공유함.
            return new Unsubscriber<BaggageInfo>(observers, observer);
        }

        public void BaggageStatus(int flightNo)
        {
            BaggageStatus(flightNo, String.Empty, 0, "");
        }

        public void BaggageStatus(int flightNo, string from, int carousel, string msg)
        {
            var info = new BaggageInfo(flightNo, from, carousel,msg);
 
            if(carousel > 0 && ! flights.Contains(info))
            {
                flights.Add(info);
                foreach (var observer in observers)
                    observer.OnNext(info);
            }
            else if (carousel == 0)
            {
                var flightsToRemove = new List<BaggageInfo>();
                foreach(var flight in flights)
                {
                    if(info.FlightNumber == flight.FlightNumber)
                    {
                        flightsToRemove.Add(flight);
                        foreach (var observer in observers)
                            observer.OnNext(info);
                    }
                }
                foreach (var flightToRemove in flightsToRemove)
                    flights.Remove(flightToRemove);

                flightsToRemove.Clear();
            }
        }


        public void LastBaggageClaimed()
        {
            foreach (var observer in observers)
                observer.OnCompleted();

            observers.Clear();
        }
    }


    internal class Unsubscriber<BaggageInfo> : IDisposable
    {
        private List<IObserver<BaggageInfo>> _observers;
        private IObserver<BaggageInfo> _observer;

        internal Unsubscriber(List<IObserver<BaggageInfo>> observers, IObserver<BaggageInfo> observser)
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

    public class ArrivalsMonitor : IObserver<BaggageInfo>
    {
        private string name;
        private List<string> flightInfos = new List<string>();
        private IDisposable cancellation;
        private string fmt = "{0,-20} {1,5} {2,3}";

        public ArrivalsMonitor(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("The observer must be assigned a name");

            this.name = name;
        }

        public virtual void Subscribe(BaggageHandler provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
            flightInfos.Clear();
        }

        public virtual void OnCompleted()
        {
            flightInfos.Clear();
        }

        public virtual void OnError(Exception e)
        {
            //오류남.
        }

        public virtual void OnNext(BaggageInfo info)
        {
            bool updated = false;

            if (info.Carousel == 0)
            {
                var flightsToRemove = new List<string>();
                string flightNo = String.Format("{0,5}", info.FlightNumber);
                
                foreach (var flightInfo in flightInfos)
                {
                    if (flightInfo.Substring(21, 5).Equals(flightNo))
                    {
                        flightsToRemove.Add(flightInfo);
                        updated = true;
                    }
                }
                foreach (var flightToRemove in flightsToRemove)
                    flightInfos.Remove(flightToRemove);

                flightsToRemove.Clear();
            }
            else
            {
                string flightInfo = String.Format("{0,-20} {1,5} {2,3} {3,10}", info.From, info.FlightNumber, info.Carousel, info.MSG);
                //Console.WriteLine("flightInfo : " + flightInfo);
                //Console.WriteLine("info.From : " + info.From);
                //Console.WriteLine("info.MSG : " + info.MSG);
                if(! flightInfos.Contains(flightInfo))
                {
                    flightInfos.Add(flightInfo);
                    updated = true;
                }
            }
            if(updated)
            {
                flightInfos.Sort();
                Console.WriteLine("Arrivals information from {0}", this.name);
                foreach (var flightInfo in flightInfos)
                    Console.WriteLine(flightInfo);

                Console.WriteLine();
            }

        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            BaggageHandler provider = new BaggageHandler();
            ArrivalsMonitor observer1 = new ArrivalsMonitor("11111111");
            ArrivalsMonitor observer2 = new ArrivalsMonitor("222222222");
            
            BaggageHandler provider2 = new BaggageHandler();
            ArrivalsMonitor observer3 = new ArrivalsMonitor("33333333");

            provider.BaggageStatus(712, "Detroit", 3,"dsasddas");
            observer1.Subscribe(provider);
            observer2.Subscribe(provider);

            provider2.BaggageStatus(700, "파스타", 2, "aaaaaaaaaaaaaaa");
            observer3.Subscribe(provider2);

            provider.BaggageStatus(711, "Detroit2", 2, "bbbbbbbbbbbbbb");

        }
    }
}
