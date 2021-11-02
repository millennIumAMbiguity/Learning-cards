using System;
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
		private void Awake()
		{
			LoadDocumentWithSchemaValidation("Files/Layouts.xml");
		}

		private const string ScemaPath = "Files/UISchema.xsd";

		public XmlDocument LoadDocumentWithSchemaValidation(string path)
		{
			XmlReader reader;

			XmlReaderSettings settings = new XmlReaderSettings();

			// Helper method to retrieve schema.
			XmlSchema schema = getSchema();

			if (schema == null) { return null; }

			settings.Schemas.Add(schema);

			settings.ValidationEventHandler += settings_ValidationEventHandler;
			settings.ValidationFlags =
				settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
			settings.ValidationType = ValidationType.Schema;

			try { reader = XmlReader.Create(path, settings); } catch (System.IO.FileNotFoundException) {
				//if (generateXML) {
				//	//string       xml       = generateXMLString();
				//	string       xml       = "";
				//	byte[]       byteArray = Encoding.UTF8.GetBytes(xml);
				//	MemoryStream stream    = new MemoryStream(byteArray);
				//	reader = XmlReader.Create(stream, settings);
				//} else 
				{ return null; }
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
		void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
		{
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

		private XmlSchema getSchema(bool generateSchema = true)
		{
			XmlSchemaSet xs = new XmlSchemaSet();
			XmlSchema    schema;
			try { schema = xs.Add("LearningCards", ScemaPath); } catch (System.IO.FileNotFoundException) {
				Debug.LogWarning(ScemaPath + " not found.");
				if (!generateSchema) return null;
				string       xmlSchemaString = generateXMLSchema();
				byte[]       byteArray       = Encoding.UTF8.GetBytes(xmlSchemaString);
				MemoryStream stream          = new MemoryStream(byteArray);
				XmlReader    reader          = XmlReader.Create(stream);
				schema = xs.Add(null, reader);
			}

			return schema;
		}

		private string generateXMLSchema()
		{
			return
				"<?xml version=\"1.0\"?><xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"><xs:complexType name=\"Rect\"><xs:sequence><xs:element name=\"Text\" minOccurs=\"0\"><xs:complexType><xs:simpleContent><xs:extension base=\"xs:string\"><xs:attribute name=\"FontSize\" type=\"xs:decimal\"/><xs:attribute name=\"Color\" type=\"xs:string\"/></xs:extension></xs:simpleContent></xs:complexType></xs:element><xs:element name=\"Image\" minOccurs=\"0\"><xs:complexType><xs:simpleContent><xs:extension base=\"xs:string\"><xs:attribute name=\"Color\" type=\"xs:string\"/></xs:extension></xs:simpleContent></xs:complexType></xs:element><xs:element type=\"Rect\" name=\"Rect\" minOccurs=\"0\" maxOccurs=\"unbounded\"/></xs:sequence><xs:attribute type=\"xs:float\" name=\"X\"/><xs:attribute type=\"xs:float\" name=\"Y\"/><xs:attribute type=\"xs:float\" name=\"Width\"/><xs:attribute type=\"xs:float\" name=\"Height\"/><xs:attribute type=\"xs:float\" name=\"AnchorsMinX\"/><xs:attribute type=\"xs:float\" name=\"AnchorsMinY\"/><xs:attribute type=\"xs:float\" name=\"AnchorsMaxX\"/><xs:attribute type=\"xs:float\" name=\"AnchorsMaxY\"/><xs:attribute type=\"xs:float\" name=\"PivotX\"/><xs:attribute type=\"xs:float\" name=\"PivotY\"/><xs:attribute type=\"xs:float\" name=\"RotationX\"/><xs:attribute type=\"xs:float\" name=\"RotationY\"/></xs:complexType><xs:element name=\"Layout\"><xs:complexType><xs:simpleContent><xs:extension base=\"Rect\"><xs:attribute name=\"Name\" type=\"xs:string\" use=\"required\"/></xs:extension></xs:simpleContent></xs:complexType></xs:element></xs:schema>";
		}
	}
}