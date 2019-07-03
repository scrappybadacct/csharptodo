using System.Xml.Linq;

namespace Models
{
  interface IXElementSerializable
  {
    XElement ToXElement();
  }
}