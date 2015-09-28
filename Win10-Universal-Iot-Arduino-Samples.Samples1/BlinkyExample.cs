using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Serial;
using Windows.Storage.Streams;
using Windows.System.Threading;

namespace Win10_Universal_Iot_Arduino_Samples.Samples1
{
    class BlinkyExample
    {
        public int Interval { get; set; }
        public RemoteDevice Arduino { get; set; }
        private ThreadPoolTimer timer;
        public BlinkyExample(RemoteDevice arduino, int millisecodInterval)
        {
            Arduino = arduino;
            Interval = millisecodInterval;
            timer = ThreadPoolTimer.CreatePeriodicTimer(OnTimerElapsed,
                                        TimeSpan.FromMilliseconds(millisecodInterval));

        }

        private void OnTimerElapsed(ThreadPoolTimer timer)
        {
            var pin13 = Arduino.digitalRead(13);
            if (pin13 == PinState.HIGH)
            {
                Helpers.ShowMessage("Pin 13 is HIGH, Changing to LOW");
                Arduino.digitalWrite(13, PinState.LOW);
            }
            else
            {
                Helpers.ShowMessage("Pin 13 is LOW, Changing to HIGH");
                Arduino.digitalWrite(13, PinState.HIGH);
            }
        }
    }

}

