// Decompiled with JetBrains decompiler
// Type: NtcipTester.NTCIPUtils
// Assembly: NTCIPDriver, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 1D4AEFEE-5431-4831-9EB4-64046511B1A7
// Assembly location: C:\Users\dafmartinez\Documents\NTCIP Driver\v.1.0.2.0\NTCIPDriver.exe

using Aesys.MS.Utilities;
using test_snmp;
using System;
using System.Net;

namespace test_snmp
{
  public static class NTCIPUtils
  {
    public static string GetOidForMsgTypeAndIndex(NtcipMessageTypes msgType, Decimal msgNumber)
    {
      string empty = string.Empty;
      switch (msgType)
      {
        case NtcipMessageTypes.Permanent:
          empty += ".2";
          break;
        case NtcipMessageTypes.Changeable:
          empty += ".3";
          break;
        case NtcipMessageTypes.Volatile:
          empty += ".4";
          break;
        case NtcipMessageTypes.CurrentBuffer:
          empty += ".5";
          break;
        case NtcipMessageTypes.Schedule:
          empty += ".6";
          break;
        case NtcipMessageTypes.Blank:
          empty += ".7";
          break;
      }
      return empty + "." + msgNumber.ToString();
    }

    public static bool DecodeActivationCode(
      string messageActivationCode,
      out int duration,
      out int priority,
      out NtcipMessageTypes type,
      out int number,
      out int crc,
      out string sourceAddress)
    {
      if (messageActivationCode == "")
      {
        duration = 0;
        priority = 0;
        type = NtcipMessageTypes.Error;
        number = 0;
        crc = 0;
        sourceAddress = "";
        return false;
      }
      byte[] bytes1 = new byte[2]
      {
        (byte) messageActivationCode[0],
        (byte) messageActivationCode[1]
      };
      duration = Utils.B2ToI_HL(bytes1);
      priority = (int) messageActivationCode[2];
      type = (NtcipMessageTypes) messageActivationCode[3];
      byte[] bytes2 = new byte[2]
      {
        (byte) messageActivationCode[4],
        (byte) messageActivationCode[5]
      };
      number = Utils.B2ToI_HL(bytes2);
      byte[] bytes3 = new byte[2]
      {
        (byte) messageActivationCode[6],
        (byte) messageActivationCode[7]
      };
      crc = Utils.B2ToI_HL(bytes3);
      sourceAddress = ((int) messageActivationCode[8]).ToString() + ".";
      ref string local1 = ref sourceAddress;
      local1 = local1 + ((int) messageActivationCode[9]).ToString() + ".";
      ref string local2 = ref sourceAddress;
      local2 = local2 + ((int) messageActivationCode[10]).ToString() + ".";
      sourceAddress += ((int) messageActivationCode[11]).ToString();
      byte[] numArray = new byte[2]
      {
        (byte) messageActivationCode[4],
        (byte) messageActivationCode[5]
      };
      return true;
    }

    public static bool DecodeMessageIDCode(
      string messageIDCode,
      out NtcipMessageTypes msgType,
      out int msgNumber,
      out int crc)
    {
      if (messageIDCode == "")
      {
        msgType = NtcipMessageTypes.Error;
        msgNumber = 0;
        crc = 0;
        return false;
      }
      msgType = (NtcipMessageTypes) messageIDCode[0];
      byte[] bytes = new byte[2]
      {
        (byte) messageIDCode[1],
        (byte) messageIDCode[2]
      };
      msgNumber = Utils.B2ToI_HL(bytes);
      bytes[0] = (byte) messageIDCode[3];
      bytes[1] = (byte) messageIDCode[4];
      crc = Utils.B2ToI_HL(bytes);
      return true;
    }

    public static byte[] EncodeMessageIDCode(
      NtcipMessageTypes msgType,
      ushort msgNumber,
      string crc)
    {
      byte[] numArray1 = new byte[5]
      {
        (byte) msgType,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
      byte[] numArray2 = Utils.IToB2_HL((int) msgNumber);
      numArray1[1] = numArray2[0];
      numArray1[2] = numArray2[1];
      byte[] numArray3 = Utils.IToB2_HL(int.Parse(crc));
      numArray1[3] = numArray3[0];
      numArray1[4] = numArray3[1];
      return numArray1;
    }

    public static byte[] EncodeActivationCode(
      NtcipMessageTypes msgType,
      ushort msgNumber,
      ushort crc,
      byte priority)
    {
      byte[] numArray1 = new byte[12];
      numArray1[0] = byte.MaxValue;
      numArray1[1] = byte.MaxValue;
      numArray1[2] = priority;
      numArray1[3] = (byte) msgType;
      byte[] numArray2 = Utils.IToB2_HL((int) msgNumber);
      numArray1[4] = numArray2[0];
      numArray1[5] = numArray2[1];
      byte[] numArray3 = Utils.IToB2_HL((int) crc);
      numArray1[6] = numArray3[0];
      numArray1[7] = numArray3[1];
      byte[] addressBytes = Dns.GetHostAddresses(Dns.GetHostName())[0].GetAddressBytes();
      numArray1[8] = addressBytes[0];
      numArray1[9] = addressBytes[1];
      numArray1[10] = addressBytes[2];
      numArray1[11] = addressBytes[3];
      return numArray1;
    }
  }
}
