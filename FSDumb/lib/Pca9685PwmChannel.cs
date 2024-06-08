// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Device.Pwm;
using Vroumed.FSDumb.lib;

namespace Iot.Device.Pwm
{
    public class Pca9685PwmChannel
    {
        private Pca9685 _parent;
        private int _channel;
        private bool _running = true;
        private double _dutyCycle;
        private PwmChannel source;

        public int Frequency
        {
            get => (int)Math.Round(_parent.PwmFrequency);
            set => _parent.PwmFrequency = value;
        }

        private double ActualDutyCycle
        {
            get => _parent.GetDutyCycle(_channel);
            set => _parent.SetDutyCycleInternal(_channel, value);
        }

        public double DutyCycle
        {
            get => _running ? ActualDutyCycle : _dutyCycle;
            set
            {
                _dutyCycle = value;

                if (_running)
                {
                    ActualDutyCycle = value;
                }
            }
        }

        public Pca9685PwmChannel(Pca9685 parent, int channel)
        {
            _parent = parent;
            _channel = channel;
            _dutyCycle = ActualDutyCycle;
            _running = true;
            this.source = new PwmChannel(parent.Adresss, channel, (int)parent.PwmFrequency, _dutyCycle);
            source.DutyCycle = ActualDutyCycle;
        }

        public void Start()
        {
            _running = true;
            ActualDutyCycle = _dutyCycle;
            source.DutyCycle = ActualDutyCycle;
            source.Start();
        }

        public void Stop()
        {
            _running = false;
            ActualDutyCycle = 0.0;
            source.DutyCycle = ActualDutyCycle;
            source.Stop();
        }

        protected void Dispose(bool disposing)
        {
            _parent?.SetChannelAsDestroyed(_channel);
            _parent = null!;
            source.Dispose();
        }
    }
}