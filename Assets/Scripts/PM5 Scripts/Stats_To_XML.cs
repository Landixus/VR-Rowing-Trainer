using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Stats_To_XML : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var document = new XDocument( new XElement( "test1", new XElement ("test2")));
		document.Save("D:\\test.xml");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
