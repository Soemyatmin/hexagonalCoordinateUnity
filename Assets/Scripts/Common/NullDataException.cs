
using System;
using System.Runtime.Serialization;

[Serializable]
internal class NullDataException : Exception {
    public NullDataException() {
    }

    public NullDataException(string message) : base(message) {
    }

    public NullDataException(string message, Exception innerException) : base(message, innerException) {
    }

    protected NullDataException(SerializationInfo info, StreamingContext context) : base(info, context) {
    }
}
