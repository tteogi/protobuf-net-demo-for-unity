// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: config.proto
// </auto-generated>

#region Designer generated code
#pragma warning disable CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace config
{

    [global::ProtoBuf.ProtoContract()]
    public partial class Test : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public int intValue { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public long longValue { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string stringValue { get; set; } = "";

        [global::ProtoBuf.ProtoMember(4)]
        public float floatValue { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public double doubleValue { get; set; }

        [global::ProtoBuf.ProtoMember(6, IsPacked = true)]
        public int[] intArrayValue { get; set; }

        [global::ProtoBuf.ProtoMember(7, IsPacked = true)]
        public long[] longArrayValue { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public global::System.Collections.Generic.List<string> stringArrayValue { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(9, IsPacked = true)]
        public float[] floatArrayValue { get; set; }

        [global::ProtoBuf.ProtoMember(10, IsPacked = true)]
        public double[] doubleArrayValue { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public IntStringPair intStringPairValue { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class IntStringPair : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public int key { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string value { get; set; } = "";

    }

}

#pragma warning restore CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
#endregion