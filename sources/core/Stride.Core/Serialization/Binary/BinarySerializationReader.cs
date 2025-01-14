// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.IO;
using Stride.Core.Annotations;
using Stride.Core.IO;

namespace Stride.Core.Serialization
{
    /// <summary>
    /// Implements <see cref="SerializationStream"/> as a binary reader.
    /// </summary>
    public class BinarySerializationReader : SerializationStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySerializationReader"/> class.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        public BinarySerializationReader([NotNull] Stream inputStream)
        {
            Reader = new BinaryReader(inputStream);
            NativeStream = inputStream;
        }

        private BinaryReader Reader { get; }

        /// <inheritdoc />
        public override void Serialize(ref bool value)
        {
            var result = NativeStream.ReadByte();
            if (result == -1)
                throw new EndOfStreamException();
            value = result != 0;
        }

#pragma warning disable CS0618 // Type or member is obsolete
        /// <inheritdoc />
        public override unsafe void Serialize(ref float value)
        {
            fixed (float* valuePtr = &value)
                *((uint*)valuePtr) = NativeStream.ReadUInt32();
        }

        /// <inheritdoc />
        public override unsafe void Serialize(ref double value)
        {
            fixed (double* valuePtr = &value)
                *((ulong*)valuePtr) = NativeStream.ReadUInt64();
        }

        /// <inheritdoc />
        public override void Serialize(ref short value)
        {
            value = (short)NativeStream.ReadUInt16();
        }

        /// <inheritdoc />
        public override void Serialize(ref int value)
        {
            value = (int)NativeStream.ReadUInt32();
        }

        /// <inheritdoc />
        public override void Serialize(ref long value)
        {
            value = (long)NativeStream.ReadUInt64();
        }

        /// <inheritdoc />
        public override void Serialize(ref ushort value)
        {
            value = NativeStream.ReadUInt16();
        }

        /// <inheritdoc />
        public override void Serialize(ref uint value)
        {
            value = NativeStream.ReadUInt32();
        }

        /// <inheritdoc />
        public override void Serialize(ref ulong value)
        {
            value = NativeStream.ReadUInt64();
        }
#pragma warning restore CS0618 // Type or member is obsolete

        /// <inheritdoc />
        public override void Serialize([NotNull] ref string value)
        {
            value = Reader.ReadString();
        }

        /// <inheritdoc />
        public override void Serialize(ref char value)
        {
            value = Reader.ReadChar();
        }

        /// <inheritdoc />
        public override void Serialize(ref byte value)
        {
            var result = NativeStream.ReadByte();
            if (result == -1)
                throw new EndOfStreamException();
            value = (byte)result;
        }

        /// <inheritdoc />
        public override void Serialize(ref sbyte value)
        {
            var result = NativeStream.ReadByte();
            if (result == -1)
                throw new EndOfStreamException();
            value = (sbyte)(byte)result;
        }

        /// <inheritdoc />
        public override void Serialize([NotNull] byte[] values, int offset, int count)
        {
            Reader.Read(values, offset, count);
        }
        /// <inheritdoc/>
        public override void Serialize(Span<byte> buffer) => NativeStream.Read(buffer);

        /// <inheritdoc />
        public override void Flush()
        {
        }
    }
}
