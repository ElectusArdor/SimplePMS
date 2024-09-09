using Simple_PMS.Interfaces;

namespace Simple_PMS.Models
{
    internal class UserData: IJsonDeSerializeble
    {
        protected uint id;
        public uint Id { get { return id; } }
    }
}
