using System;
using System.Collections.Generic;

public class TypeReverse
{
    public List<Type> typeList = new List<Type>()
    {
        typeof(ProtoBuf.Serializers.VectorSerializer<int>),
        typeof(ProtoBuf.Serializers.VectorSerializer<uint>),
        typeof(ProtoBuf.Serializers.VectorSerializer<short>),
        typeof(ProtoBuf.Serializers.VectorSerializer<ushort>),
        typeof(ProtoBuf.Serializers.VectorSerializer<long>),
        typeof(ProtoBuf.Serializers.VectorSerializer<ulong>),
        typeof(ProtoBuf.Serializers.VectorSerializer<byte>),
        typeof(ProtoBuf.Serializers.VectorSerializer<decimal>),
        typeof(ProtoBuf.Serializers.VectorSerializer<bool>),
        typeof(ProtoBuf.Serializers.VectorSerializer<float>),
        typeof(ProtoBuf.Serializers.VectorSerializer<double>),
        typeof(ProtoBuf.Serializers.VectorSerializer<string>),
    };
}