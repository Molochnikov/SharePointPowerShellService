using System;
using System.Management.Automation.Runspaces;

namespace FuckingExternalWrapperToSuppressCompilerError
{
    public struct FuckingExternalStruct : IEquatable<FuckingExternalStruct>
    {
        private Runspace rs;
        public Runspace Rs { get => rs; set => rs = value; }

        public bool Equals(FuckingExternalStruct other)
        {
            return rs == other.rs;
        }
        public override bool Equals(object obj) => obj is FuckingExternalStruct other && Equals(other);
        public override int GetHashCode() => (rs).GetHashCode();
        public static bool operator ==(FuckingExternalStruct lhs, FuckingExternalStruct rhs) => lhs.Equals(rhs);
        public static bool operator !=(FuckingExternalStruct lhs, FuckingExternalStruct rhs) => !(lhs == rhs);

    }
}
