using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace Machine.Fakes
{
    public abstract class FakeHttpSessionStateBase : HttpSessionStateBase
    {
        readonly SessionStateItemCollection sessionValues = new SessionStateItemCollection();

        public  sealed override void Add(string name, object value)
        {
            this[name] = value;
        }

        public sealed override void Clear()
        {
            sessionValues.Clear();
        }

        public sealed override NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                return sessionValues.Keys;
            }
        }

        public sealed override int Count
        {
            get { return sessionValues.Count; }
        }

        public sealed override void Remove(string name)
        {
            sessionValues.Remove(name);
        }

        public sealed override void RemoveAll()
        {
            sessionValues.Clear();
        }

        public sealed override void RemoveAt(int index)
        {
            sessionValues.RemoveAt(index);
        }

        public sealed override object this[string name]
        {
            get { return sessionValues[name]; }
            set { sessionValues[name] = value; }
        }

        public  sealed override object this[int index]
        {
            get { return sessionValues[index]; }
            set { sessionValues[index] = value; }
        }
    }
}