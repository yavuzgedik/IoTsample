using System;
using Windows.Devices.Gpio;

namespace Turta
{
    public class RelayController4Ch : IDisposable
    {
        private static GpioPin relay1, relay2, relay3, relay4;

        public enum RelayCh
        {
            Ch1,
            Ch2,
            Ch3,
            Ch4
        };

        public enum RelayState
        {
            On,
            Off
        };

        public RelayController4Ch()
        {
            GpioController gpioController = GpioController.GetDefault();

            // 21, 22, 23 ve 24. Çıkışları rölelere bağla.
            relay1 = gpioController.OpenPin(21);
            relay2 = gpioController.OpenPin(22);
            relay3 = gpioController.OpenPin(23);
            relay4 = gpioController.OpenPin(24);

            // Röleleri pasifleştir.
            relay1.Write(GpioPinValue.Low);
            relay2.Write(GpioPinValue.Low);
            relay3.Write(GpioPinValue.Low);
            relay4.Write(GpioPinValue.Low);

            // Pinleri çıkış moduna ayarla.
            relay1.SetDriveMode(GpioPinDriveMode.Output);
            relay2.SetDriveMode(GpioPinDriveMode.Output);
            relay3.SetDriveMode(GpioPinDriveMode.Output);
            relay4.SetDriveMode(GpioPinDriveMode.Output);
        }

        /// <summary>
        /// Röleyi kontrol eder.
        /// </summary>
        /// <param name="relayCh">Röle kanalı</param>
        /// <param name="relayState">Röle durumu</param>
        public void SetRelay(RelayCh relayCh, RelayState relayState)
        {
            switch (relayCh)
            {
                case RelayCh.Ch1:
                    relay1.Write(relayState == RelayState.On ? GpioPinValue.High : GpioPinValue.Low);
                    break;

                case RelayCh.Ch2:
                    relay2.Write(relayState == RelayState.On ? GpioPinValue.High : GpioPinValue.Low);
                    break;

                case RelayCh.Ch3:
                    relay3.Write(relayState == RelayState.On ? GpioPinValue.High : GpioPinValue.Low);
                    break;

                case RelayCh.Ch4:
                    relay4.Write(relayState == RelayState.On ? GpioPinValue.High : GpioPinValue.Low);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Röleleri pasifleştirir ve pinleri serbest bırakır.
        /// </summary>
        public void Dispose()
        {
            // Röleleri pasifleştir.
            relay1.Write(GpioPinValue.Low);
            relay2.Write(GpioPinValue.Low);
            relay3.Write(GpioPinValue.Low);
            relay4.Write(GpioPinValue.Low);

            // Pinleri serbest bırak.
            relay1.Dispose();
            relay2.Dispose();
            relay3.Dispose();
            relay4.Dispose();
        }
    }
}
