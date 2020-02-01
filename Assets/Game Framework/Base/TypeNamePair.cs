using System;

namespace GameFramework
{
    public class TypeNamePair : IEquatable<TypeNamePair>
    {
        private readonly Type type;
        private readonly string name;

        public Type Type => this.type;

        public string Name => this.name;

        public TypeNamePair(Type type)
            : this(type, string.Empty)
        {

        }

        public TypeNamePair(Type type, string name)
        {
            this.type = type;
            this.name = name ?? string.Empty;
        }

        public override string ToString()
        {
            string typeName = this.type.FullName;
            return string.IsNullOrEmpty(this.name) ? typeName : Utility.Text.Format("{0}.{1}", typeName, this.name);
        }

        public override int GetHashCode()
        {
            return this.type.GetHashCode() ^ this.name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is TypeNamePair && Equals((TypeNamePair)obj);
        }

        public bool Equals(TypeNamePair other)
        {
            return this.type == other.type && this.name == other.name;
        }

        public static bool operator==(TypeNamePair a, TypeNamePair b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(TypeNamePair a, TypeNamePair b)
        {
            return !(a == b);
        }
    }
}
