using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_assembler.Collections;

namespace Nintenlord.Event_assembler
{
    /// <summary>
    /// Extensions for byte and arrays of it
    /// </summary>
    static class ByteExtensions
    {
        static public bool IsInRange(this byte i, byte min, byte max)
        {
            return i <= max && i >= min;
        }

        static public int Clamp(this byte i, byte min, byte max)
        {
            if (i < min)
            {
                return min;
            }
            if (i > max)
            {
                return max;
            }
            return i;
        }

        static public string ToHexString(this byte i, string prefix)
        {
            return prefix + Convert.ToString(i, 16).ToUpper();
        }

        static public byte GetBits(this byte i, int position, int length)
        {
            return (byte)(i & GetMask(position, length));
        }


        static public byte GetMask(int position, int length)
        {
            if (length < 0 || position < 0 || position + length > 8)
            {
                throw new IndexOutOfRangeException();
            }
            byte mask = 0xFF;
            unchecked
            {
                mask >>= sizeof(byte) * 8 - length;
                mask <<= position;
            }

            return mask;
        }

        static public byte[] GetMaskArray(int position, int length)
        {
            byte[] result;

            int begIndex = position / 8;
            int firstByteBits = (8 - position & 0x7) & 0x7;

            int endIndex = (position + length) / 8;
            int lastByteBits = (position + length) & 0x7;

            int resultLength = endIndex;

            if (lastByteBits != 0)
            {
                resultLength++;
            }
            result = new byte[resultLength];
            if (((position & 0x7) + length) < 9)
            {
                result[begIndex] = GetMask(position & 0x7, length);
            }
            else
            {
                if (firstByteBits != 0)
                {
                    result[begIndex] = GetMask(8 - firstByteBits, firstByteBits);
                    begIndex++;
                }

                if (lastByteBits != 0)
                {
                    result[endIndex] = GetMask(0, lastByteBits);
                    //endIndex--;
                }

                for (int j = begIndex; j < endIndex; j++)
                {
                    result[j] = 0xFF;
                }

            }

            return result;
        }

        static public byte[] GetBits(this byte[] i, int position, int length)
        {
            if (position < 0 || length <= 0 || position + length > i.Length * 8)
            {
                throw new IndexOutOfRangeException();
            }
            int byteIndex = position / 8;
            int bitIndex = position % 8;

            int byteLength = length / 8;
            int bitLength = length % 8;
            byte[] result;
            if (bitLength == 0)
                result = new byte[byteLength];
            else
                result = new byte[byteLength + 1];

            if (bitIndex == 0)
            {
                Array.Copy(i, byteIndex, result, 0, result.Length);
            }
            else
            {
                byte[] temp;
                if (bitLength + bitIndex > 8)
                    temp = new byte[result.Length+1];
                else
                    temp = new byte[result.Length];

                Array.Copy(i, byteIndex, temp, 0, temp.Length);

                result = temp.Shift(-bitIndex);
            }

            if (bitLength != 0)
                result[result.Length - 1] &= GetMask(0, bitLength);
            return result;
        }


        static public string ToString(this byte[] i, int bytesPerWord)
        {
            StringBuilder result = new StringBuilder();
            for (int j = 0; j < i.Length; j++)
            {
                result.Append(i[j].ToHexString("").PadLeft(2, '0'));
                if (j % bytesPerWord == bytesPerWord - 1)
                {
                    result.Append(" ");
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Shifts bytes in array. Assumes bytes are in big endian order and 
        /// high priority bits are first
        /// </summary>
        /// <remarks>Could be made faster with using uints...</remarks>
        /// <param name="array">Array to shift</param>
        /// <param name="toShift">Positive means right shifting, negative left</param>
        /// <returns>New shifted array with equal or larger length</returns>
        static public byte[] Shift(this byte[] array, int toShift)
        {
            if (toShift == 0)// <_<
                return array.Clone() as byte[];

            byte[] result;
            int byteMoveAmount = toShift / 8;
            int bitMoveAmount = toShift % 8;
            
            int resultLength = array.Length + byteMoveAmount;
            if (bitMoveAmount > 0)
                resultLength++;

            result = new byte[resultLength];
            
            if (byteMoveAmount > 0)
                Array.Copy(array, 0, result, byteMoveAmount, array.Length);
            else if (byteMoveAmount < 0)
                Array.Copy(array, -byteMoveAmount, result, 0, result.Length);
            else Array.Copy(array, 0, result, 0, array.Length);

            if (bitMoveAmount > 0)
            {
                byte mask = GetMask(8 - bitMoveAmount, bitMoveAmount);
                byte[] masks = new byte[result.Length];
                for (int i = 0; i < masks.Length; i++)
                    masks[i] = mask;

                byte[] masked = result.And(masks);
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] <<= bitMoveAmount;
                }
                for (int i = 0; i < masked.Length - 1; i++)
                {
                    result[i + 1] |= (byte)(masked[i] >> (8 - bitMoveAmount));
                }
            }
            else if (bitMoveAmount < 0)
            {
                byte mask = GetMask(0, -bitMoveAmount);
                byte[] masks = new byte[result.Length];
                for (int i = 0; i < masks.Length; i++)
                    masks[i] = mask;

                byte[] masked = result.And(masks);
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] >>= -bitMoveAmount;
                }
                for (int i = 1; i < masked.Length; i++)
                {
                    result[i - 1] |= (byte)(masked[i] << (8 + bitMoveAmount));
                }
            }
            return result;
            /*
            Random rand = new Random();
            byte[] buffer = new byte[] {0x81,0x18,0x7E,0x11};
            //rand.NextBytes(buffer);
            byte[] shift1 = buffer.Shift(1);
            byte[] shift_1 = buffer.Shift(-1);
            byte[] shift7 = buffer.Shift(7);
            byte[] shift_7 = buffer.Shift(-7);

            string message = "Buffer:\n";
            message += " ".PadLeft(9, '0');
            for (int i = buffer.Length - 1; i >= 0; i--)
            {
                message += Convert.ToString(buffer[i], 2).PadLeft(8, '0') + " ";
            }
            message += "\nShift: 1\n";
            for (int i = shift1.Length - 1; i >= 0; i--)
            {
                message += Convert.ToString(shift1[i], 2).PadLeft(8, '0') + " ";
            }
            message += "\nShift: -1\n";
            message += " ".PadLeft(9, '0');
            for (int i = shift_1.Length - 1; i >= 0; i--)
            {
                message += Convert.ToString(shift_1[i], 2).PadLeft(8, '0') + " ";
            }
            message += "\nShift: 7\n";
            for (int i = shift7.Length - 1; i >= 0; i--)
            {
                message += Convert.ToString(shift7[i], 2).PadLeft(8, '0') + " ";
            }
            message += "\nShift: -7\n";
            message += " ".PadLeft(9, '0');
            for (int i = shift_7.Length - 1; i >= 0; i--)
            {
                message += Convert.ToString(shift_7[i], 2).PadLeft(8, '0') + " ";
            }
            message += "\n";
            MessageBox.Show(message);
             */
        }

        

        static public byte[] And(this byte[] array, byte[] array2)
        {
            byte[] result = new byte[Math.Max(array.Length,array2.Length)];
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                result[i] = (byte)(array[i] & array2[i]);
            }
            return result;
        }
        static public byte[] Or(this byte[] array, byte[] array2)
        {
            byte[] result = new byte[Math.Max(array.Length, array2.Length)];
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                result[i] = (byte)(array[i] | array2[i]);
            }
            if (array.Length > array2.Length)
            {
                Array.Copy(array, index, result, index, result.Length - index);
            }
            else if (array.Length < array2.Length)
            {
                Array.Copy(array2, index, result, index, result.Length - index);
            }

            return result;
        }
        static public byte[] Xor(this byte[] array, byte[] array2)
        {
            byte[] result = new byte[Math.Max(array.Length, array2.Length)];
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                result[i] = (byte)(array[i] ^ array2[i]);
            }
            if (array.Length > array2.Length)
            {
                Array.Copy(array, index, result, index, result.Length - index);
            }
            else if (array.Length < array2.Length)
            {
                Array.Copy(array2, index, result, index, result.Length - index);
            }
            return result;
        }
        static public byte[] Neg(this byte[] array)
        {
            byte[] result = new byte[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(~array[i]);
            }
            return result;
        }
        static public void AndWith(this byte[] array, byte[] array2)
        {
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                array[i] = (byte)(array[i] & array2[i]);
            }
        }
        static public void OrWith(this byte[] array, byte[] array2)
        {
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                array[i] = (byte)(array[i] | array2[i]);
            }
        }
        static public void XorWith(this byte[] array, byte[] array2)
        {
            int index = Math.Min(array.Length, array2.Length);
            for (int i = 0; i < index; i++)
            {
                array[i] = (byte)(array[i] ^ array2[i]);
            }
        }
        static public void NegWith(this byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)(~array[i]);
            }
        }

        static public void WriteTo(this byte[] array, int destination, byte[] source, int length)
        {
            int sourceIndex = 0;
            if (destination + length > array.Length * 8
                || sourceIndex + length > array.Length * 8 
                || destination < 0)
            {
                throw new IndexOutOfRangeException();
            }
            int byteDestination = destination / 8;
            int bitDestination = destination % 8;
            int byteLastDestination = (destination + length) / 8;
            int amountOfBytesToCopy = length / 8;
            int amoutnOfBitsToCopy = length % 8;

            if (bitDestination == 0)
            {
                Array.Copy(source, 0, array, byteDestination, amountOfBytesToCopy);
                if (amoutnOfBitsToCopy != 0)
                {
                    byte mask = GetMask(0, amoutnOfBitsToCopy);
                    byte last = (byte)(source[amountOfBytesToCopy] & mask);
                    array[byteDestination + amountOfBytesToCopy] = last;

                }
            }
            else
            {
                int destShift = 8 - bitDestination;
                //Used in the end and beginning
                byte destBegMask = GetMask(0, bitDestination);
                byte destEndMask = (byte)~destBegMask;

                byte sourceBegMask = GetMask(0, 8 - bitDestination);
                byte sourceEndMask = (byte)~sourceBegMask;

                int temp = (byte)(array[byteDestination] & destBegMask);
                for (int i = byteDestination; i < byteLastDestination; i++)
                {
                    array[i] = (byte)(temp | 
                        ((source[i - byteDestination] & sourceBegMask) << bitDestination));
                    temp = (source[i - byteDestination] & sourceEndMask) >> destShift;
                }
                if (byteLastDestination < array.Length)
                {
                array[byteLastDestination] = (byte)(temp | 
                    (array[byteLastDestination] & destEndMask));
                }
            }
            /*             
            //byte[] buffer = new byte[] {0x81,0x18,0x7E,0x11};
            //byte[] buffer2 = new byte[] { 0x3C, 0x2D };
            byte[] buffer = new byte[] { 0x40, 0, 0x80, 0 };
            byte[] buffer2 = new byte[] { 0xFF, 0xFF };

            //Random rand = new Random();
            //rand.NextBytes(buffer);
            string message = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                message += Convert.ToString(buffer[i], 2).PadLeft(8, '0') + " ";
            }
            //for (int i = buffer.Length - 1; i >= 0; i--)
            //{
            //    message += Convert.ToString(buffer[i], 2).PadLeft(8, '0') + " ";
            //}
            message += "\n";
            //for (int i = buffer2.Length - 1; i >= 0; i--)
            //{
            //    message += Convert.ToString(buffer2[i], 2).PadLeft(8, '0') + " ";
            //}
            for (int i = 0; i < buffer2.Length; i++)
            {
                message += Convert.ToString(buffer2[i], 2).PadLeft(8, '0') + " ";
            }
            message += "\n";
            buffer.WriteTo(7, buffer2);
            for (int i = 0; i < buffer.Length; i++)
            {
                message += Convert.ToString(buffer[i], 2).PadLeft(8, '0') + " ";
            }
            //for (int i = buffer.Length - 1; i >= 0; i--)
            //{
            //    message += Convert.ToString(buffer[i], 2).PadLeft(8, '0') + " ";
            //}
            message += "\n";
            MessageBox.Show(message);
             */
        }
    }
}
