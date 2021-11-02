using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Learning_cards.Scripts.UI.Messages;
using UnityEngine;

namespace Learning_cards.Scripts.Data.Classes
{
	public class UIDesigner : MonoBehaviour
	{
		void GenerateDesign(string path) { }

		public XmlDocument LoadDocumentWithSchemaValidation(bool generateXML, bool generateSchema) {
			XmlReader reader;

			XmlReaderSettings settings = new XmlReaderSettings();

			// Helper method to retrieve schema.
			XmlSchema schema = getSchema(generateSchema);

			if (schema == null) { return null; }

			settings.Schemas.Add(schema);

			settings.ValidationEventHandler += settings_ValidationEventHandler;
			settings.ValidationFlags =
				settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
			settings.ValidationType = ValidationType.Schema;

			try { reader = XmlReader.Create("booksData.xml", settings); } catch (System.IO.FileNotFoundException) {
				if (generateXML) {
					string       xml       = generateXMLString();
					byte[]       byteArray = Encoding.UTF8.GetBytes(xml);
					MemoryStream stream    = new MemoryStream(byteArray);
					reader = XmlReader.Create(stream, settings);
				} else { return null; }
			}

			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = true;
			doc.Load(reader);
			reader.Close();

			return doc;
		}

		//************************************************************************************
//
//  Event handler that is raised when XML doesn't validate against the schema.
//
//************************************************************************************
		void settings_ValidationEventHandler(object sender, ValidationEventArgs e) {
			switch (e.Severity) {
				case XmlSeverityType.Warning:
					MessageHandler.ShowMessage
						("The following validation warning occurred: " + e.Message);
					break;
				case XmlSeverityType.Error:
					MessageHandler.ShowMessage
						("The following critical validation errors occurred: " + e.Message);
					break;
			}
		}
	}
}