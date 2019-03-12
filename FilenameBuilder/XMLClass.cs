using System.Collections.Generic;
using System.Xml.Linq;

namespace FilenameBuilder
{
    public static class XMLClass
    {

        public static XDocument CreateXML(List<string> input)
        {
            XDocument output = new XDocument(new XElement("info",
                                                new XElement("jobnumber", input[0]),
                                                new XElement("counter", input[1]),
                                                new XElement("totalpages", input[2]),
                                                new XElement("customercode", input[3]),
                                                new XElement("jobname", input[0] + "_" + input[4] + "_" + input[5]),
                                                new XElement("campaign", input[4]),
                                                new XElement("filename", input[5]),
                                                new XElement("widthsize", input[6]),
                                                new XElement("heightsize", input[7]),
                                                new XElement("stock", input[8]),
                                                new XElement("quantity", input[9]),
                                                new XElement("revision", input[10]),
                                                new XElement("finishing", input[11]),
                                                new XElement("colours", input[12]),
                                                new XElement("comments", input[13])
                                          ));
            return output;
        }

        public static bool SaveXMLToFile(XDocument xdoc, string filename)
        {
            try
            {
                xdoc.Save(filename);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static List<string> ImportXML(string importedFilePath)
        {
            List<string> output = new List<string>();

            XElement userXML = XElement.Load(importedFilePath);

            foreach (var item in userXML.Elements())
            {
                output.Add(item.Value);
            }
            return output;
        }
    }
}
