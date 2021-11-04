using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Learning_cards.Scripts.Parse;
using Learning_cards.Scripts.UI.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.UI.XML
{
	public class UIXmlDesigner : MonoBehaviour
	{
		private const string ScemaPath = "Files/UISchema.xsd";
		private       void   Awake() => LoadLayout("Files/Layouts.xml");

		private void LoadLayout(string path)
		{
			XmlDocument xmlDoc = LoadDocumentWithSchemaValidation(path);

			foreach (XmlNode node in xmlDoc)
				if (node.Name == "Layout")
					CreateUiLayout(node);
		}

		private GameObject CreateUiLayout(XmlNode layoutNode, RectTransform parent = null)
		{
			GameObject    layoutObj;
			RectTransform rTrans;
			bool          haveParent = parent;
			if (haveParent) {
				layoutObj = new GameObject("Rect", typeof(RectTransform));
				rTrans    = layoutObj.GetComponent<RectTransform>();
				rTrans.SetParent(parent);
			} else {
				layoutObj = new GameObject(layoutNode.Attributes[0].Value, typeof(RectTransform));
				rTrans    = layoutObj.GetComponent<RectTransform>();
				rTrans.SetParent(transform);
			}

			rTrans.ApplyAttributes(layoutNode.Attributes);

			foreach (XmlNode node in layoutNode)
				switch (node.Name) {
					case "rect":
						CreateUiLayout(node, rTrans);
						break;
					case "text":
						if (haveParent) layoutObj.name = "Text";
						layoutObj.AddComponent<TextMeshProUGUI>().ParseNode(node);
						break;
					case "fill":
						if (haveParent) layoutObj.name = "Fill";
						layoutObj.AddComponent<RawImage>().ParseAttributes(node.Attributes);
						break;
					case "outline":
						layoutObj.AddComponent<Outline>().ParseAttributes(node.Attributes);
						break;
				}

			return layoutObj;
		}

		public XmlDocument LoadDocumentWithSchemaValidation(string path)
		{
			XmlReader reader;

			var settings = new XmlReaderSettings();

			// Helper method to retrieve schema.
			XmlSchema schema = getSchema();

			if (schema == null) return null;

			settings.Schemas.Add(schema);

			settings.ValidationEventHandler += settings_ValidationEventHandler;
			settings.ValidationFlags =
				settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
			settings.ValidationType = ValidationType.Schema;

			try { reader = XmlReader.Create(path, settings); } catch (FileNotFoundException) {
				//if (generateXML) {
				//	string       xml       = generateXMLString();
				//	byte[]       byteArray = Encoding.UTF8.GetBytes(xml);
				//	MemoryStream stream    = new MemoryStream(byteArray);
				//	reader = XmlReader.Create(stream, settings);
				//} else 
				{
					return null;
				}
			}

			var doc = new XmlDocument();
			doc.PreserveWhitespace = false;
			doc.Load(reader);
			reader.Close();

			return doc;
		}

//************************************************************************************
//
//  Event handler that is raised when XML doesn't validate against the schema.
//
//************************************************************************************
		private void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			switch (e.Severity) {
				case XmlSeverityType.Warning:
					MessageHandler.ShowWarning
						("The following validation warning occurred: " + e.Message);
					break;
				case XmlSeverityType.Error:
					MessageHandler.ShowError
						("The following critical validation errors occurred: " + e.Message);
					break;
			}
		}

		private XmlSchema getSchema(bool generateSchema = true)
		{
			var       xs = new XmlSchemaSet();
			XmlSchema schema;
			try { schema = xs.Add(null, ScemaPath); } catch (FileNotFoundException) {
				Debug.LogWarning(ScemaPath + " not found.");
				if (!generateSchema) return null;
				string xmlSchemaString = generateXMLSchema();
				byte[] byteArray       = Encoding.UTF8.GetBytes(xmlSchemaString);
				var    stream          = new MemoryStream(byteArray);
				var    reader          = XmlReader.Create(stream);
				schema = xs.Add(null, reader);
			}

			return schema;
		}

		private string generateXMLSchema() =>
			"<?xml version=\"1.0\"?> <xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"> <xs:complexType name=\"Rect\"> <xs:choice> <xs:sequence> <xs:element name=\"Text\"> <xs:complexType> <xs:simpleContent> <xs:extension base=\"xs:string\"> <xs:attribute name=\"FontSize\" type=\"xs:decimal\"/> <xs:attribute name=\"Color\" type=\"xs:string\"/> </xs:extension> </xs:simpleContent> </xs:complexType> </xs:element> <xs:element type=\"Rect\" name=\"Rect\" minOccurs=\"0\" maxOccurs=\"unbounded\"/> </xs:sequence> <xs:sequence> <xs:element name=\"Image\"> <xs:complexType> <xs:simpleContent> <xs:extension base=\"xs:string\"> <xs:attribute name=\"Color\" type=\"xs:string\"/> </xs:extension> </xs:simpleContent> </xs:complexType> </xs:element> <xs:element type=\"Rect\" name=\"Rect\" minOccurs=\"0\" maxOccurs=\"unbounded\"/> </xs:sequence> <xs:sequence> <xs:element name=\"Fill\"> <xs:complexType> <xs:attribute name=\"Color\" type=\"xs:string\"/> </xs:complexType> </xs:element> <xs:element name=\"Outline\" minOccurs=\"0\"> <xs:complexType> <xs:attribute name=\"Color\" type=\"xs:string\"/> <xs:attribute name=\"Width\" type=\"xs:decimal\"/> </xs:complexType> </xs:element> <xs:element type=\"Rect\" name=\"Rect\" minOccurs=\"0\" maxOccurs=\"unbounded\"/> </xs:sequence> <xs:sequence> <xs:element type=\"Rect\" name=\"Rect\" minOccurs=\"0\" maxOccurs=\"unbounded\"/> </xs:sequence> </xs:choice> <xs:attribute type=\"xs:float\" name=\"X\"/> <xs:attribute type=\"xs:float\" name=\"Y\"/> <xs:attribute type=\"xs:float\" name=\"Z\"/> <xs:attribute type=\"xs:float\" name=\"Width\"/> <xs:attribute type=\"xs:float\" name=\"Height\"/> <xs:attribute type=\"xs:float\" name=\"AnchorsMinX\"/> <xs:attribute type=\"xs:float\" name=\"AnchorsMinY\"/> <xs:attribute type=\"xs:float\" name=\"AnchorsMaxX\"/> <xs:attribute type=\"xs:float\" name=\"AnchorsMaxY\"/> <xs:attribute type=\"xs:float\" name=\"PivotX\"/> <xs:attribute type=\"xs:float\" name=\"PivotY\"/> <xs:attribute type=\"xs:float\" name=\"RotationX\"/> <xs:attribute type=\"xs:float\" name=\"RotationY\"/> <xs:attribute type=\"xs:float\" name=\"RotationZ\"/> </xs:complexType> <xs:element name=\"Layout\"> <xs:complexType> <xs:complexContent> <xs:extension base=\"Rect\"> <xs:attribute name=\"Name\" type=\"xs:string\" use=\"required\"/> </xs:extension> </xs:complexContent> </xs:complexType> </xs:element> </xs:schema>";
	}
}