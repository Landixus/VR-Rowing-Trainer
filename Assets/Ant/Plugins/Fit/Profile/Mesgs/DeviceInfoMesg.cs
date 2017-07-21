#region Copyright
////////////////////////////////////////////////////////////////////////////////
// The following FIT Protocol software provided may be used with FIT protocol
// devices only and remains the copyrighted property of Dynastream Innovations Inc.
// The software is being provided on an "as-is" basis and as an accommodation,
// and therefore all warranties, representations, or guarantees of any kind
// (whether express, implied or statutory) including, without limitation,
// warranties of merchantability, non-infringement, or fitness for a particular
// purpose, are specifically disclaimed.
//
// Copyright 2016 Dynastream Innovations Inc.
////////////////////////////////////////////////////////////////////////////////
// ****WARNING****  This file is auto-generated!  Do NOT edit this file.
// Profile Version = 16.60Release
// Tag = production-akw-16.60.00-0-g5d3d436
////////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;


namespace Dynastream.Fit
{
    /// <summary>
    /// Implements the DeviceInfo profile message.
    /// </summary>
    public class DeviceInfoMesg : Mesg
    {
        #region Fields
        static class DeviceTypeSubfield
        {
            public static ushort AntplusDeviceType = 0;
            public static ushort AntDeviceType = 1;
            public static ushort Subfields = 2;
            public static ushort Active = Fit.SubfieldIndexActiveSubfield;
            public static ushort MainField = Fit.SubfieldIndexMainField;
        }
        static class ProductSubfield
        {
            public static ushort GarminProduct = 0;
            public static ushort Subfields = 1;
            public static ushort Active = Fit.SubfieldIndexActiveSubfield;
            public static ushort MainField = Fit.SubfieldIndexMainField;
        }
        #endregion

        #region Constructors
        public DeviceInfoMesg() : base(Profile.GetMesg(MesgNum.DeviceInfo))
        {
        }

        public DeviceInfoMesg(Mesg mesg) : base(mesg)
        {
        }
        #endregion // Constructors

        #region Methods
        ///<summary>
        /// Retrieves the Timestamp field
        /// Units: s</summary>
        /// <returns>Returns DateTime representing the Timestamp field</returns>
        public DateTime GetTimestamp()
        {
            return TimestampToDateTime((uint?)GetFieldValue(253, 0, Fit.SubfieldIndexMainField));
        }

        

        

        /// <summary>
        /// Set Timestamp field
        /// Units: s</summary>
        /// <param name="timestamp_">Nullable field value to be set</param>
        public void SetTimestamp(DateTime timestamp_)
        {
            SetFieldValue(253, 0, timestamp_.GetTimeStamp(), Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the DeviceIndex field</summary>
        /// <returns>Returns nullable byte representing the DeviceIndex field</returns>
        public byte? GetDeviceIndex()
        {
            return (byte?)GetFieldValue(0, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set DeviceIndex field</summary>
        /// <param name="deviceIndex_">Nullable field value to be set</param>
        public void SetDeviceIndex(byte? deviceIndex_)
        {
            SetFieldValue(0, 0, deviceIndex_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the DeviceType field</summary>
        /// <returns>Returns nullable byte representing the DeviceType field</returns>
        public byte? GetDeviceType()
        {
            return (byte?)GetFieldValue(1, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set DeviceType field</summary>
        /// <param name="deviceType_">Nullable field value to be set</param>
        public void SetDeviceType(byte? deviceType_)
        {
            SetFieldValue(1, 0, deviceType_, Fit.SubfieldIndexMainField);
        }
        

        /// <summary>
        /// Retrieves the AntplusDeviceType subfield</summary>
        /// <returns>Nullable byte representing the AntplusDeviceType subfield</returns>
        public byte? GetAntplusDeviceType()
        {
            return (byte?)GetFieldValue(1, 0, DeviceTypeSubfield.AntplusDeviceType);
        }

        /// <summary>
        ///
        /// Set AntplusDeviceType subfield</summary>
        /// <param name="antplusDeviceType">Subfield value to be set</param>
        public void SetAntplusDeviceType(byte? antplusDeviceType)
        {
            SetFieldValue(1, 0, antplusDeviceType, DeviceTypeSubfield.AntplusDeviceType);
        }

        /// <summary>
        /// Retrieves the AntDeviceType subfield</summary>
        /// <returns>Nullable byte representing the AntDeviceType subfield</returns>
        public byte? GetAntDeviceType()
        {
            return (byte?)GetFieldValue(1, 0, DeviceTypeSubfield.AntDeviceType);
        }

        /// <summary>
        ///
        /// Set AntDeviceType subfield</summary>
        /// <param name="antDeviceType">Subfield value to be set</param>
        public void SetAntDeviceType(byte? antDeviceType)
        {
            SetFieldValue(1, 0, antDeviceType, DeviceTypeSubfield.AntDeviceType);
        }
        ///<summary>
        /// Retrieves the Manufacturer field</summary>
        /// <returns>Returns nullable ushort representing the Manufacturer field</returns>
        public ushort? GetManufacturer()
        {
            return (ushort?)GetFieldValue(2, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set Manufacturer field</summary>
        /// <param name="manufacturer_">Nullable field value to be set</param>
        public void SetManufacturer(ushort? manufacturer_)
        {
            SetFieldValue(2, 0, manufacturer_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the SerialNumber field</summary>
        /// <returns>Returns nullable uint representing the SerialNumber field</returns>
        public uint? GetSerialNumber()
        {
            return (uint?)GetFieldValue(3, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set SerialNumber field</summary>
        /// <param name="serialNumber_">Nullable field value to be set</param>
        public void SetSerialNumber(uint? serialNumber_)
        {
            SetFieldValue(3, 0, serialNumber_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the Product field</summary>
        /// <returns>Returns nullable ushort representing the Product field</returns>
        public ushort? GetProduct()
        {
            return (ushort?)GetFieldValue(4, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set Product field</summary>
        /// <param name="product_">Nullable field value to be set</param>
        public void SetProduct(ushort? product_)
        {
            SetFieldValue(4, 0, product_, Fit.SubfieldIndexMainField);
        }
        

        /// <summary>
        /// Retrieves the GarminProduct subfield</summary>
        /// <returns>Nullable ushort representing the GarminProduct subfield</returns>
        public ushort? GetGarminProduct()
        {
            return (ushort?)GetFieldValue(4, 0, ProductSubfield.GarminProduct);
        }

        /// <summary>
        ///
        /// Set GarminProduct subfield</summary>
        /// <param name="garminProduct">Subfield value to be set</param>
        public void SetGarminProduct(ushort? garminProduct)
        {
            SetFieldValue(4, 0, garminProduct, ProductSubfield.GarminProduct);
        }
        ///<summary>
        /// Retrieves the SoftwareVersion field</summary>
        /// <returns>Returns nullable float representing the SoftwareVersion field</returns>
        public float? GetSoftwareVersion()
        {
            return (float?)GetFieldValue(5, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set SoftwareVersion field</summary>
        /// <param name="softwareVersion_">Nullable field value to be set</param>
        public void SetSoftwareVersion(float? softwareVersion_)
        {
            SetFieldValue(5, 0, softwareVersion_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the HardwareVersion field</summary>
        /// <returns>Returns nullable byte representing the HardwareVersion field</returns>
        public byte? GetHardwareVersion()
        {
            return (byte?)GetFieldValue(6, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set HardwareVersion field</summary>
        /// <param name="hardwareVersion_">Nullable field value to be set</param>
        public void SetHardwareVersion(byte? hardwareVersion_)
        {
            SetFieldValue(6, 0, hardwareVersion_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the CumOperatingTime field
        /// Units: s
        /// Comment: Reset by new battery or charge.</summary>
        /// <returns>Returns nullable uint representing the CumOperatingTime field</returns>
        public uint? GetCumOperatingTime()
        {
            return (uint?)GetFieldValue(7, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set CumOperatingTime field
        /// Units: s
        /// Comment: Reset by new battery or charge.</summary>
        /// <param name="cumOperatingTime_">Nullable field value to be set</param>
        public void SetCumOperatingTime(uint? cumOperatingTime_)
        {
            SetFieldValue(7, 0, cumOperatingTime_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the BatteryVoltage field
        /// Units: V</summary>
        /// <returns>Returns nullable float representing the BatteryVoltage field</returns>
        public float? GetBatteryVoltage()
        {
            return (float?)GetFieldValue(10, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set BatteryVoltage field
        /// Units: V</summary>
        /// <param name="batteryVoltage_">Nullable field value to be set</param>
        public void SetBatteryVoltage(float? batteryVoltage_)
        {
            SetFieldValue(10, 0, batteryVoltage_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the BatteryStatus field</summary>
        /// <returns>Returns nullable byte representing the BatteryStatus field</returns>
        public byte? GetBatteryStatus()
        {
            return (byte?)GetFieldValue(11, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set BatteryStatus field</summary>
        /// <param name="batteryStatus_">Nullable field value to be set</param>
        public void SetBatteryStatus(byte? batteryStatus_)
        {
            SetFieldValue(11, 0, batteryStatus_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the SensorPosition field
        /// Comment: Indicates the location of the sensor</summary>
        /// <returns>Returns nullable BodyLocation enum representing the SensorPosition field</returns>
        public BodyLocation? GetSensorPosition()
        {
            object obj = GetFieldValue(18, 0, Fit.SubfieldIndexMainField);
            BodyLocation? value = obj == null ? (BodyLocation?)null : (BodyLocation)obj;
            return value;
        }

        

        

        /// <summary>
        /// Set SensorPosition field
        /// Comment: Indicates the location of the sensor</summary>
        /// <param name="sensorPosition_">Nullable field value to be set</param>
        public void SetSensorPosition(BodyLocation? sensorPosition_)
        {
            SetFieldValue(18, 0, sensorPosition_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the Descriptor field
        /// Comment: Used to describe the sensor or location</summary>
        /// <returns>Returns byte[] representing the Descriptor field</returns>
        public byte[] GetDescriptor()
        {
            return (byte[])GetFieldValue(19, 0, Fit.SubfieldIndexMainField);
        }

        
        ///<summary>
        /// Retrieves the Descriptor field
        /// Comment: Used to describe the sensor or location</summary>
        /// <returns>Returns String representing the Descriptor field</returns>
        public String GetDescriptorAsString()
        {
            return Encoding.UTF8.GetString((byte[])GetFieldValue(19, 0, Fit.SubfieldIndexMainField));
        }
        

        
        ///<summary>
        /// Set Descriptor field
        /// Comment: Used to describe the sensor or location</summary>
        /// <param name="descriptor_"> field value to be set</param>
        public void SetDescriptor(String descriptor_)
        {
            SetFieldValue(19, 0, System.Text.Encoding.UTF8.GetBytes(descriptor_), Fit.SubfieldIndexMainField);
        }
        

        /// <summary>
        /// Set Descriptor field
        /// Comment: Used to describe the sensor or location</summary>
        /// <param name="descriptor_">field value to be set</param>
        public void SetDescriptor(byte[] descriptor_)
        {
            SetFieldValue(19, 0, descriptor_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the AntTransmissionType field</summary>
        /// <returns>Returns nullable byte representing the AntTransmissionType field</returns>
        public byte? GetAntTransmissionType()
        {
            return (byte?)GetFieldValue(20, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set AntTransmissionType field</summary>
        /// <param name="antTransmissionType_">Nullable field value to be set</param>
        public void SetAntTransmissionType(byte? antTransmissionType_)
        {
            SetFieldValue(20, 0, antTransmissionType_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the AntDeviceNumber field</summary>
        /// <returns>Returns nullable ushort representing the AntDeviceNumber field</returns>
        public ushort? GetAntDeviceNumber()
        {
            return (ushort?)GetFieldValue(21, 0, Fit.SubfieldIndexMainField);
        }

        

        

        /// <summary>
        /// Set AntDeviceNumber field</summary>
        /// <param name="antDeviceNumber_">Nullable field value to be set</param>
        public void SetAntDeviceNumber(ushort? antDeviceNumber_)
        {
            SetFieldValue(21, 0, antDeviceNumber_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the AntNetwork field</summary>
        /// <returns>Returns nullable AntNetwork enum representing the AntNetwork field</returns>
        public AntNetwork? GetAntNetwork()
        {
            object obj = GetFieldValue(22, 0, Fit.SubfieldIndexMainField);
            AntNetwork? value = obj == null ? (AntNetwork?)null : (AntNetwork)obj;
            return value;
        }

        

        

        /// <summary>
        /// Set AntNetwork field</summary>
        /// <param name="antNetwork_">Nullable field value to be set</param>
        public void SetAntNetwork(AntNetwork? antNetwork_)
        {
            SetFieldValue(22, 0, antNetwork_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the SourceType field</summary>
        /// <returns>Returns nullable SourceType enum representing the SourceType field</returns>
        public SourceType? GetSourceType()
        {
            object obj = GetFieldValue(25, 0, Fit.SubfieldIndexMainField);
            SourceType? value = obj == null ? (SourceType?)null : (SourceType)obj;
            return value;
        }

        

        

        /// <summary>
        /// Set SourceType field</summary>
        /// <param name="sourceType_">Nullable field value to be set</param>
        public void SetSourceType(SourceType? sourceType_)
        {
            SetFieldValue(25, 0, sourceType_, Fit.SubfieldIndexMainField);
        }
        
        ///<summary>
        /// Retrieves the ProductName field
        /// Comment: Optional free form string to indicate the devices name or model</summary>
        /// <returns>Returns byte[] representing the ProductName field</returns>
        public byte[] GetProductName()
        {
            return (byte[])GetFieldValue(27, 0, Fit.SubfieldIndexMainField);
        }

        
        ///<summary>
        /// Retrieves the ProductName field
        /// Comment: Optional free form string to indicate the devices name or model</summary>
        /// <returns>Returns String representing the ProductName field</returns>
        public String GetProductNameAsString()
        {
            return Encoding.UTF8.GetString((byte[])GetFieldValue(27, 0, Fit.SubfieldIndexMainField));
        }
        

        
        ///<summary>
        /// Set ProductName field
        /// Comment: Optional free form string to indicate the devices name or model</summary>
        /// <param name="productName_"> field value to be set</param>
        public void SetProductName(String productName_)
        {
            SetFieldValue(27, 0, System.Text.Encoding.UTF8.GetBytes(productName_), Fit.SubfieldIndexMainField);
        }
        

        /// <summary>
        /// Set ProductName field
        /// Comment: Optional free form string to indicate the devices name or model</summary>
        /// <param name="productName_">field value to be set</param>
        public void SetProductName(byte[] productName_)
        {
            SetFieldValue(27, 0, productName_, Fit.SubfieldIndexMainField);
        }
        
        #endregion // Methods
    } // Class
} // namespace
