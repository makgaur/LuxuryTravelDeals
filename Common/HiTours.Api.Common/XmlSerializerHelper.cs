using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HiTours.Api.Common
{
    public class XmlSerializerHelper : ISerializer
    {
        public string SerializeObject<T>(T obj)
        {
            string retval;
            if (obj == null)
            {
                return null;
            }

            var settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.OmitXmlDeclaration = true;
            settings.NewLineChars = string.Empty;
            settings.NewLineHandling = NewLineHandling.None;

            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);

                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(xmlWriter, obj, namespaces);

                    retval = stringWriter.ToString();
                    xmlWriter.Close();
                }

                stringWriter.Close();
            }

            return retval;
        }

        public T DeserializeObject<T>(string serializedObject)
        {
            var deserializer = new XmlSerializer(typeof(T));
            var serializedXmlReader = XmlReader.Create(new StringReader(serializedObject));
            return (T)deserializer.Deserialize(serializedXmlReader);
        }
    }
}
