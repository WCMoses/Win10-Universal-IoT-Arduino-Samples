using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;

// *** IMPORTANT ***
// It looks like the nuget package for RemoteWiring is very out of
// date and not being updated. Use the projects directly from Git:
//      https://github.com/ms-iot/remote-wiring
// When creating a solution, don't forget to:
//  1. Build the projects once you add them to the solution
//  2. Add references to them in your main project
//
using Microsoft.Maker.RemoteWiring;     //From git. 
using Microsoft.Maker.Serial;           //From git. 
using Windows.Storage.Streams;          //From git. 

// *** IMPORTANT ***
// Also, don't forget to enable Device Capabilities in Package.appmainifest
// when using a Win Universal app.
// For USB you need the  
//      <DeviceCapability Name="serialcommunication">
//  tag.

namespace Win10_Universal_Iot_Arduino_Samples.Samples1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IStream connection;
        RemoteDevice arduino;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            Helpers.OutputTextBox = txtMessages;
            Helpers.ShowMessage("About to Connect");


            // *** IMPORTANT ***
            // You need to add the VID and PID for your device.
            // I'll post screen shots of finding it on my blog
            // at http://MosesSoftware.com
            //
            // NOTE: The line below is, obviously, for a USB cnnected 
            // device.  You can also use a BlueTooth.
            connection = new UsbSerial("VID_2341", "PID_0042");
            arduino = new RemoteDevice(connection);
            arduino.DeviceReady += OnDeviceReady;
            arduino.AnalogPinUpdated += OnAnalogPinUpdated;
            arduino.DeviceConnectionFailed += OnDeviceConnectionFailed;
            arduino.DeviceConnectionLost += OnDeviceConnectionLost;
            arduino.DigitalPinUpdated += OnDigitialPinUpdated;
            arduino.StringMessageReceived += OnStringMessageReceived;
            arduino.SysexMessageReceived += OnSysexMessageReceived;
            arduino.I2c.I2cReplyEvent += OnI2CReplyEvent;

            // *** IMPORTANT ***
            // Make sure the baud rate is the same as the one specified
            //  in the Firmata program uploaded to the arduino.
            connection.begin(57600, SerialConfig.SERIAL_8N1);

        }
        // ** CONNECTION EVENTS
        private void OnDeviceReady()
        {
            Helpers.ShowMessage("DeviceReady Event Fired");
        }
        private void OnDeviceConnectionLost(string message)
        {
            Helpers.ShowMessage("OnDeviceConnectionLost Event Fired");
        }
        private void OnDeviceConnectionFailed(string message)
        {
            Helpers.ShowMessage("OnDeviceConnectionFailed Event Fired");
        }
        //** PIN EVENTS
        private void OnDigitialPinUpdated(byte pin, PinState state)
        {
            Helpers.ShowMessage("OnDigitialPinUpdated Event Fired");
        }
        private void OnAnalogPinUpdated(byte pin, ushort value)
        {
            Helpers.ShowMessage("OnAnalogPinUpdated Event Fired");
        }

        //** I2C EVENTS
        private void OnI2CReplyEvent(byte address_, byte reg_, DataReader response)
        {
            Helpers.ShowMessage("OnI2CReplyEvent Event Fired");
        }

        //** STRING AND SYSTEX EVENTS
        private void OnStringMessageReceived(string message)
        {
            Helpers.ShowMessage("OnStringMessageReceived Event Fired");
        }

        private void OnSysexMessageReceived(byte command, DataReader message)
        {
            Helpers.ShowMessage("OnSysexMessageReceived Event Fired");
        }

        private void btnBlinky_Click(object sender, RoutedEventArgs e)
        {
            var blinky = new BlinkyExample(arduino, 500);
        }
    }

}
