using System;
using System.Web;
using System.Web.SessionState;

namespace Machine.Fakes
{
    public class FakeHttpSessionState : HttpSessionStateBase
    {
        readonly SessionStateItemCollection _items;

        public FakeHttpSessionState()
        {
            _items = new SessionStateItemCollection();
        }

        public override void Add(string name, object value)
        {
            _items[name] = value;
        }

        public override void Remove(string name)
        {
            _items.Remove(name);
        }

        public override void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public override int Count
        {
            get { return _items.Count; }
        }
        public override void Clear()
        {
            _items.Clear();
        }

        public override object this[string name]
        {
            get { return _items[name]; }
            set { _items[name] = value; }
        }
    }
}