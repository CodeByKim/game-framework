using System;

namespace GameFramework
{
    public abstract class Variable<T> : Variable
    {
        private T value;

        protected Variable()
        {
            this.value = default(T);
        }

        protected Variable(T value)
        {
            this.value = value;
        }

        public override Type Type => typeof(T);

        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public override object GetValue()
        {
            return this.value;
        }

        public override void SetValue(object value)
        {
            this.value = (T)value;
        }

        public override void Reset()
        {
            this.value = default(T);
        }

        public override string ToString()
        {
            return this.value != null ? this.value.ToString() : "<Null>";
        }
    }
}