using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Learning_cards.Scripts.UI.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Learning_cards.Scripts.Data.Classes
{
	public class UIDesigner : MonoBehaviour
	{
		private void Awake() { LoadLayout("Files/Layouts.xml"); }

		private const string ScemaPath = "Files/UISchema.xsd";

		void LoadLayout(string path)
		{
			var xmlDoc = LoadDocumentWithSchemaValidation(path);

			foreach (XmlNode node in xmlDoc) {
				if (node.Name == "Layout") {
					CreateUiLayout(node);
				}
			}
		}

		GameObject CreateUiLayout(XmlNode layoutNode, RectTransform parent = null)
		{
			GameObject    layoutObj;
			RectTransform rTrans;
			bool          haveParent = parent;
			if (haveParent) {
				 layoutObj = new GameObject("Rect", typeof(RectTransform));
				 rTrans    = layoutObj.GetComponent<RectTransform>();
				 rTrans.SetParent(parent);
			} else { 
				layoutObj     = new GameObject(layoutNode.Attributes[0].Value, typeof(RectTransform));
				rTrans        = layoutObj.GetComponent<RectTransform>();
				rTrans.SetParent(transform);
			}
			ApplyRectAttribute(ref rTrans, layoutNode.Attributes);

			foreach (XmlNode node in layoutNode) {
				switch (node.Name) {
					case "Rect":
						CreateUiLayout(node, rTrans);
						break;
					case "Text":
						if (haveParent) layoutObj.name = "Text";
						var text = layoutObj.AddComponent<TextMeshProUGUI>();
						foreach (XmlAttribute attribute in node.Attributes) {
							switch (attribute.Name) {
								case "FontSize":
									text.fontSize = float.Parse(attribute.Value);
								break;
								case "Color":
									if (ColorUtility.TryParseHtmlString(attribute.Value, out Color c)) 
										text.color = c;
									break;
							}
						}

						text.text = node.InnerText;
						break;
					case "Fill":
						if (haveParent) layoutObj.name = "Fill";
						var fill = layoutObj.AddComponent<RawImage>();
						foreach (XmlAttribute attribute in node.Attributes) {
							switch (attribute.Name) {
								case "Color":
									if (ColorUtility.TryParseHtmlString(attribute.Value, out Color c)) 
										fill.color = c;
									break;
							}
						}
						break;
					case "Outline":
						var outline = layoutObj.AddComponent<Outline>();
						foreach (XmlAttribute attribute in node.Attributes) {
							switch (attribute.Name) {
								case "Color":
									if (ColorUtility.TryParseHtmlString(attribute.Value, out Color c)) 
										outline.effectColor = c;
									break;
								case "Width":
									float f = float.Parse(attribute.Value);
									outline.effectDistance = new Vector2(f, f);
									break;
							}
						}
						break;
				}
			}

			return layoutObj;
		}

		void ApplyRectAttribute(ref RectTransform rect, XmlAttributeCollection attributes)
		{
			Vector3 pos        = Vector3.zero;
			Vector3 scale      = Vector3.one;
			Vector3 rot        = Vector3.zero;
			Vector2 size       = new Vector2(100,100);
			Vector2 anchorsMin = new Vector2(.5f,.5f);
			Vector2 anchorsMax = new Vector2(.5f,.5f);
			Vector2 pivot      = new Vector2(.5f,.5f);
			
			foreach (XmlAttribute attribute in attributes) {
				switch (attribute.Name) {
					case "X":
						pos.x = float.Parse(attribute.Value);
						break;
					case "Y":
						pos.y = float.Parse(attribute.Value);
						break;
					case "Z":
						pos.z = float.Parse(attribute.Value);
						break;
					case "Width":
						size.x = float.Parse(attribute.Value);
						break;
					case "Height":
						size.y = float.Parse(attribute.Value);
						break;
					case "AnchorsMinX":
						anchorsMin.x = float.Parse(attribute.Value);
						break;
					case "AnchorsMinY":
						anchorsMin.y = float.Parse(attribute.Value);
						break;
					case "AnchorsMaxX":
						anchorsMax.x = float.Parse(attribute.Value);
						break;
					case "AnchorsMaxY":
						anchorsMax.y = float.Parse(attribute.Value);
						break;
					case "PivotX":
						pivot.x = float.Parse(attribute.Value);
						break;
					case "PivotY":
						pivot.y = float.Parse(attribute.Value);
						break;
					case "RotationX":
						rot.x = float.Parse(attribute.Value);
						break;
					case "RotationY":
						rot.y = float.Parse(attribute.Value);
						break;
					case "RotationZ":
						rot.z = float.Parse(attribute.Value);
						break;
				}
			}

			rect.pivot         = pivot;
			rect.anchorMin     = anchorsMin;
			rect.anchorMax     = anchorsMax;
			rect.localPosition = pos;
			rect.localScale    = scale;
			rect.localRotation = Quaternion.Euler(rot);
			//rect.sizeDelta     = size;
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
		}

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
				//	string       xml       = generateXMLString();
				//	byte[]       byteArray = Encoding.UTF8.GetBytes(xml);
				//	MemoryStream stream    = new MemoryStream(byteArray);
				//	reader = XmlReader.Create(stream, settings);
				//} else 
				{
					return null;
				}
			}

			XmlDocument doc = new XmlDocument();
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