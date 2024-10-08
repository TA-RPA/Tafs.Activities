﻿//
//  Bit.cs
//
//  Author:
//       Devin Duanne <dduanne@tafs.com>
//
//  Copyright (c) TAFS, LLC.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Tafs.Activities.Finance.Models
{
#if NET7_0_OR_GREATER
    /// <summary>
    /// Represents a single bit.
    /// </summary>
    public readonly struct Bit
        : IBinaryInteger<Bit>
    {
        private readonly bool _value;

        private Bit(bool value)
        {
            _value = value;
        }

        /// <inheritdoc/>
        public static Bit One => new(true);

        /// <inheritdoc/>
        public static int Radix => 2;

        /// <inheritdoc/>
        public static Bit Zero => new(false);

        /// <inheritdoc/>
        public static Bit AdditiveIdentity => Zero;

        /// <inheritdoc/>
        public static Bit MultiplicativeIdentity => One;

        /// <inheritdoc/>
        public static Bit Abs(Bit value) => value;

        /// <inheritdoc/>
        public static bool IsCanonical(Bit value) => true;

        /// <inheritdoc/>
        public static bool IsComplexNumber(Bit value) => false;

        /// <inheritdoc/>
        public static bool IsEvenInteger(Bit value)
            => value == Zero;

        /// <inheritdoc/>
        public static bool IsFinite(Bit value) => true;

        /// <inheritdoc/>
        public static bool IsImaginaryNumber(Bit value) => false;

        /// <inheritdoc/>
        public static bool IsInfinity(Bit value)
            => false;

        /// <inheritdoc/>
        public static bool IsInteger(Bit value)
            => true;

        /// <inheritdoc/>
        public static bool IsNaN(Bit value)
            => false;

        /// <inheritdoc/>
        public static bool IsNegative(Bit value)
            => false;

        /// <inheritdoc/>
        public static bool IsNegativeInfinity(Bit value)
            => false;

        /// <inheritdoc/>
        public static bool IsNormal(Bit value)
            => true;

        /// <inheritdoc/>
        public static bool IsOddInteger(Bit value)
            => !IsEvenInteger(value);

        /// <inheritdoc/>
        public static bool IsPositive(Bit value)
            => true;

        /// <inheritdoc/>
        public static bool IsPositiveInfinity(Bit value)
            => false;

        /// <inheritdoc/>
        public static bool IsPow2(Bit value)
        {
            return value == Zero;
        }

        /// <inheritdoc/>
        public static bool IsRealNumber(Bit value)
            => true;

        /// <inheritdoc/>
        public static bool IsSubnormal(Bit value)
            => false;

        /// <inheritdoc/>
        public static bool IsZero(Bit value)
            => value == Zero;

        /// <inheritdoc />
        public static Bit Log2(Bit value)
        {
            return value._value
                ? Zero // Log2(1) == 0
                : throw new InvalidOperationException("Cannot perform log2(0)");
        }

        /// <inheritdoc/>
        public static Bit MaxMagnitude(Bit x, Bit y)
            => x > y ? x : y;

        /// <inheritdoc/>
        public static Bit MaxMagnitudeNumber(Bit x, Bit y)
            => MaxMagnitude(x, y);

        /// <inheritdoc/>
        public static Bit MinMagnitude(Bit x, Bit y)
            => x > y ? y : x;

        /// <inheritdoc/>
        public static Bit MinMagnitudeNumber(Bit x, Bit y)
            => MinMagnitude(x, y);

        /// <inheritdoc/>
        public static Bit Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
            => Parse(s, provider);

        /// <inheritdoc/>
        public static Bit Parse(string s, NumberStyles style, IFormatProvider? provider)
            => Parse(s, provider);

        /// <inheritdoc/>
        public static Bit Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            int value = int.Parse(s, provider);

            return FromNumber(value);
        }

        /// <inheritdoc/>
        public static Bit Parse(string s, IFormatProvider? provider)
        {
            int value = int.Parse(s, provider);

            return FromNumber(value);
        }

        /// <inheritdoc cref="INumberBase{TSelf}.TryParse(ReadOnlySpan{char}, NumberStyles, IFormatProvider?, out TSelf)"/>
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Bit result)
            => TryParse(s, style, provider, out result);

        /// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{char}, IFormatProvider?, out TSelf)"/>
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Bit result)
        {
            result = default;
            if (s.IsEmpty || s.IsWhiteSpace())
            {
                return false;
            }

            if (!int.TryParse(s, provider, out int parseResult))
            {
                return false;
            }

            result = FromNumber(parseResult);
            return true;
        }

        /// <inheritdoc cref="INumberBase{TSelf}.TryParse(string?, NumberStyles, IFormatProvider?, out TSelf)"/>
        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Bit result)
            => TryParse(s, provider, out result);

        /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Bit result)
        {
            result = default;
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            if (!int.TryParse(s, provider, out int parseResult))
            {
                return false;
            }

            result = FromNumber(parseResult);
            return true;
        }

        /// <inheritdoc />
        public static Bit PopCount(Bit value) => value;

        /// <inheritdoc/>
        public static Bit TrailingZeroCount(Bit value) => Zero;

        /// <inheritdoc/>
        public static bool TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Bit value)
        {
            value = FromNumber(source[0]);
            return true;
        }

        /// <inheritdoc/>
        public static bool TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out Bit value)
            => TryReadBigEndian(source, isUnsigned, out value);

        /// <inheritdoc/>
        public int CompareTo(object? obj)
        {
            if (obj is Bit bit)
            {
                return CompareTo(bit);
            }

            if (obj is bool @bool)
            {
                return _value.CompareTo(@bool);
            }

            return 1;
        }

        /// <inheritdoc/>
        public int CompareTo(Bit other)
        {
            return _value.CompareTo(other._value);
        }

        /// <inheritdoc/>
        public override bool Equals([NotNullWhen(true)] object? obj)
            => CompareTo(obj) == 0;

        /// <inheritdoc/>
        public bool Equals(Bit other)
        {
            return _value == other._value;
        }

        /// <inheritdoc/>
        public int GetByteCount() => sizeof(byte);

        /// <inheritdoc/>
        public int GetShortestBitLength() => 1;

        /// <summary>
        /// Returns a string representation of the bit.
        /// </summary>
        /// <param name="format">Case insensitive.
        /// <list type="bullet">
        /// <item>
        ///     <term>B</term>
        ///     <description>Boolean format (true, false)</description>
        /// </item>
        /// <item>
        ///     <term>I</term>
        ///     <description>Integer format (1, 0)</description>
        /// </item>
        /// </list>
        /// </param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>A string representation of the bit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="format"/> is not an expected value.</exception>
        public string ToString(string? format = "B", IFormatProvider? formatProvider = null)
        {
            return format switch
            {
                "b" or "B" => _value.ToString(formatProvider),
                "i" or "I" => ToNumber<byte>(this).ToString(formatProvider),
                _ => throw new ArgumentOutOfRangeException(nameof(format))
            };
        }

        /// <inheritdoc/>
        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            string formattedString = ToString(format.ToString(), provider);
            charsWritten = destination.Length;
            return formattedString.TryCopyTo(destination);
        }

        /// <inheritdoc/>
        public bool TryWriteBigEndian(Span<byte> destination, out int bytesWritten)
        {
            bytesWritten = GetByteCount();
            byte value = ToNumber<byte>(this);
            destination[0] = value;
            return true;
        }

        /// <inheritdoc/>
        public bool TryWriteLittleEndian(Span<byte> destination, out int bytesWritten)
            => TryWriteBigEndian(destination, out bytesWritten);

        /// <summary>
        /// Converts a number to a <see cref="Bit"/>.
        /// </summary>
        /// <typeparam name="TNumber">The type of number to use.</typeparam>
        /// <param name="value">The number value to convert.</param>
        /// <returns>A <see cref="Bit"/>.</returns>
        /// <exception cref="OverflowException">Thrown if <paramref name="value"/> is not 0 or 1.</exception>
        internal static Bit FromNumber<TNumber>(TNumber value)
            where TNumber : INumberBase<TNumber>
            => value == TNumber.Zero ? Zero : One;

        /// <summary>
        /// Converts the bit to the specified number.
        /// </summary>
        /// <typeparam name="TNumber">The type of number to convert to.</typeparam>
        /// <param name="bit">The bit to convert.</param>
        /// <returns>The number.</returns>
        internal static TNumber ToNumber<TNumber>(Bit bit)
            where TNumber : INumberBase<TNumber>
        {
            return bit._value ? TNumber.One : TNumber.Zero;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool INumberBase<Bit>.TryConvertFromChecked<TOther>(TOther value, out Bit result)
        {
            result = FromNumber(value);
            return true;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool INumberBase<Bit>.TryConvertFromSaturating<TOther>(TOther value, out Bit result)
        {
            result = FromNumber(value);
            return true;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool INumberBase<Bit>.TryConvertFromTruncating<TOther>(TOther value, out Bit result)
        {
            result = FromNumber(value);
            return true;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool INumberBase<Bit>.TryConvertToChecked<TOther>(Bit value, out TOther result)
        {
            result = ToNumber<TOther>(value);
            return true;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool INumberBase<Bit>.TryConvertToSaturating<TOther>(Bit value, out TOther result)
        {
            result = ToNumber<TOther>(value);
            return true;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool INumberBase<Bit>.TryConvertToTruncating<TOther>(Bit value, out TOther result)
        {
            result = ToNumber<TOther>(value);
            return true;
        }

        public static explicit operator int(Bit value)
            => ToNumber<int>(value);

        public static Bit operator +(Bit value) => value;

        public static Bit operator +(Bit left, Bit right)
        {
            int leftValue = (int)left;
            int rightValue = (int)right;

            return FromNumber(leftValue + rightValue);
        }

        public static Bit operator -(Bit value) => value;

        public static Bit operator -(Bit left, Bit right)
        {
            int leftValue = (int)left;
            int rightValue = (int)right;

            return FromNumber(leftValue - rightValue);
        }

        public static Bit operator ~(Bit value)
        {
            return !value;
        }

        public static Bit operator ++(Bit value)
        {
            int valueValue = (int)value;
            return FromNumber(++valueValue);
        }

        public static Bit operator --(Bit value)
        {
            int valueValue = (int)value;
            return FromNumber(--valueValue);
        }

        public static Bit operator *(Bit left, Bit right)
        {
            int leftValue = (int)left;
            int rightValue = (int)right;

            return FromNumber(leftValue * rightValue);
        }

        public static Bit operator /(Bit left, Bit right)
        {
            int leftValue = (int)left;
            int rightValue = (int)right;

            return FromNumber(leftValue / rightValue);
        }

        public static Bit operator %(Bit left, Bit right)
        {
            int leftValue = (int)left;
            int rightValue = (int)right;

            return FromNumber(leftValue % rightValue);
        }

        public static Bit operator &(Bit left, Bit right)
            => new(left._value & right._value);

        public static Bit operator |(Bit left, Bit right)
            => new(left._value | right._value);

        public static Bit operator ^(Bit left, Bit right)
            => new(left._value ^ right._value);

        /// <inheritdoc />
        /// <remarks><see cref="Bit"/> does not support byte shifting.</remarks>
        /// <exception cref="NotSupportedException">Bit does not support byte shifting.</exception>
        public static Bit operator <<(Bit value, int shiftAmount)
            => throw new NotSupportedException();

        /// <inheritdoc />
        /// <remarks><see cref="Bit"/> does not support byte shifting.</remarks>
        /// <exception cref="NotSupportedException">Bit does not support byte shifting.</exception>
        public static Bit operator >>(Bit value, int shiftAmount)
            => throw new NotSupportedException();

        public static bool operator ==(Bit left, Bit right)
            => left.Equals(right);

        public static bool operator !=(Bit left, Bit right)
            => !left.Equals(right);

        public static bool operator <(Bit left, Bit right)
            => left.CompareTo(right) < 0;

        public static bool operator >(Bit left, Bit right)
            => left.CompareTo(right) > 0;

        public static bool operator <=(Bit left, Bit right)
            => left.CompareTo(right) <= 0;

        public static bool operator >=(Bit left, Bit right)
            => left.CompareTo(right) >= 0;

        /// <inheritdoc />
        /// <remarks><see cref="Bit"/> does not support byte shifting.</remarks>
        /// <exception cref="NotSupportedException">Bit does not support byte shifting.</exception>
        public static Bit operator >>>(Bit value, int shiftAmount)
        {
            throw new NotSupportedException();
        }

        public static Bit operator !(Bit value)
        {
            return value._value
                ? Zero
                : One;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }
    }
#endif
}
