namespace Vroumed.FSDumb.lib.Enums
{
    /// <summary>
    /// Values for Mode1 register
    /// </summary>
    internal enum Mode1 : byte
    {
        RESTART = 0b10000000, // Bit 7
        EXTCLK = 0b01000000, // Bit 6
        AI = 0b00100000, // Bit 5
        SLEEP = 0b00010000, // Bit 4
        SUB1 = 0b00001000, // Bit 3
        SUB2 = 0b00000100, // Bit 2
        SUB3 = 0b00000010, // Bit 1
        ALLCALL = 0x00000001 // Bit 0
    }

    internal enum Mode2 : byte
    {
        INVRT = 0b00010000, // Bit 4
        OCH = 0b00001000, // Bit 3
        OUTDRV = 0b00000100 // Bit 2
    }

    // Licensed to the .NET Foundation under one or more agreements.
    // The .NET Foundation licenses this file to you under the MIT license.

    internal enum Register : byte
    {
        /// <summary>
        /// Mode register 1
        /// </summary>
        MODE1 = 0x00,

        /// <summary>
        /// Mode register 2
        /// </summary>
        MODE2 = 0x01,

        /// <summary>
        /// I2C-bus subaddress 1
        /// </summary>
        SUBADR1 = 0x02,

        /// <summary>
        /// I2C-bus subaddress 2
        /// </summary>
        SUBADR2 = 0x03,

        /// <summary>
        /// I2C-bus subaddress 3
        /// </summary>
        SUBADR3 = 0x04,

        /// <summary>
        /// LED All Call I2C-bus address
        /// </summary>
        ALLCALLADR = 0x05,

        /// <summary>
        /// LED0 output and brightness control byte 0
        /// </summary>
        LED0_ON_L = 0x06,

        /// <summary>
        /// LED0 output and brightness control byte 1
        /// </summary>
        LED0_ON_H = 0x07,

        /// <summary>
        /// LED0 output and brightness control byte 2
        /// </summary>
        LED0_OFF_L = 0x08,

        /// <summary>
        /// LED0 output and brightness control byte 3
        /// </summary>
        LED0_OFF_H = 0x09,

        /// <summary>
        /// load all the LEDn_ON registers, byte 0
        /// </summary>
        ALL_LED_ON_L = 0xFA,

        /// <summary>
        /// load all the LEDn_ON registers, byte 1
        /// </summary>
        ALL_LED_ON_H = 0xFB,

        /// <summary>
        /// load all the LEDn_OFF registers, byte 0
        /// </summary>
        ALL_LED_OFF_L = 0xFC,

        /// <summary>
        /// load all the LEDn_OFF registers, byte 1
        /// </summary>
        ALL_LED_OFF_H = 0xFD,

        /// <summary>
        /// prescaler for PWM output frequency
        /// </summary>
        PRESCALE = 0xFE
    }
}
