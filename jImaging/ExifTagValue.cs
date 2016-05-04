using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jImaging
{
    public class ExifTagValue
    {
        public static object GetValueObject(PropertyItem property)
        {
            if (property == null) return null;

            switch ((ExifTagType)property.Type)
            {
                case ExifTagType.ASCII:
                    var data = Encoding.ASCII.GetString(property.Value);
                    if (data.Contains('\0'))
                        data = data.Substring(0, data.IndexOf('\0'));
                    return data;
                case ExifTagType.Byte:
                    if (property.Len == 1)
                        return property.Value[0];
                    else
                        return property.Value;
                case ExifTagType.Long:
                    var resultLong = new uint[property.Len / 4];
                    for (var i = 0; i < resultLong.Length; i++)
                        resultLong[i] = BitConverter.ToUInt32(property.Value, i * 4);
                    if (resultLong.Length == 1)
                        return resultLong[0];
                    else
                        return resultLong;
                case ExifTagType.Short:
                    var resultShort = new ushort[property.Len / 2];
                    for (var i = 0; i < resultShort.Length; i++)
                        resultShort[i] = BitConverter.ToUInt16(property.Value, i * 2);
                    if (resultShort.Length == 1)
                        return resultShort[0];
                    else
                        return resultShort;
                case ExifTagType.SLONG:
                    var resultSLong = new int[property.Len / 4];
                    for (var i = 0; i < resultSLong.Length; i++)
                        resultSLong[i] = BitConverter.ToInt32(property.Value, i * 4);
                    if (resultSLong.Length == 1)
                        return resultSLong[0];
                    else
                        return resultSLong;
                case ExifTagType.Rational:
                    var resultRational = new ExifFraction[property.Len / 8];
                    for (var i = 0; i < resultRational.Length; i++)
                    {
                        var uNumerator = BitConverter.ToUInt32(property.Value, i * 8);
                        var uDenumerator = BitConverter.ToUInt32(property.Value, i * 8 + 4);
                        resultRational[i] = new ExifFraction(uNumerator, uDenumerator);
                    }
                    if (resultRational.Length == 1)
                        return resultRational[0];
                    else
                        return resultRational;
                case ExifTagType.SRational:
                    var resultSRational = new ExifFraction[property.Len / 8];
                    for (var i = 0; i < resultSRational.Length; i++)
                    {
                        var sNumerator = BitConverter.ToInt32(property.Value, i * 8);
                        var sDenumerator = BitConverter.ToInt32(property.Value, i * 8 + 4);
                        resultSRational[i] = new ExifFraction(sNumerator, sDenumerator);
                    }
                    if (resultSRational.Length == 1)
                        return resultSRational[0];
                    else
                        return resultSRational;
                default:
                    if (property.Len == 1)
                        return property.Value[0];
                    else
                        return property.Value;
            }
        }
    }
}
