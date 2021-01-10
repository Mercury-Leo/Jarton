using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleApi : MonoBehaviour
{
    public RawImage img;

	string url;

	public float lat;
	public float lon;

	LocationInfo li;

	public int zoom = 14;
	public int mapWidth =640;
	public int mapHeight = 640;

	public enum mapType {roadmap,satellite,hybrid,terrain}
	public mapType mapSelected;
	public int scale;
    public string city;
	public LatLng p;
	List<LatLng> poly;

	IEnumerator Map()
	{
		// url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
		// 	"&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale 
		// 	+"&maptype=" + mapSelected +
		// 	"&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=AIzaSyBJ5EXH-PN6Yyr6Z5CHPn7gYQYs4ltcfn0";
		
		// show map
        // url = "https://maps.googleapis.com/maps/api/staticmap?"+
        // // "center="+city+
        // "center=יפה+רום+24,+ירושלים"+
        // "&zoom="+zoom+
        // "&size="+mapWidth+"x"+mapHeight+
        // "&maptype=roadmap"+
        // "&markers=color:blue%7Clabel:S%7C40.702147,-74.015794"+
        // "&markers=color:green%7Clabel:G%7C40.711614,-74.012318"+
        // "&markers=color:red%7Clabel:C%7C40.718217,-73.998284"+
        // "&key=AIzaSyDBZmSam1_33DN5nFsqZF74Jv-wv2k37qY";

		// direction points
		// 	url ="https://maps.googleapis.com/maps/api/directions/json?"+
		// "origin=יפה+רום+16,+ירושלים&destination=התאנה+2,+ירושלים"+
		// "&avoid=highways &mode=walking"+
		// "&key=AIzaSyBJ5EXH-PN6Yyr6Z5CHPn7gYQYs4ltcfn0";

		// url="https://www.google.com/maps/embed/v1/directions"+
		// // "?key=AIzaSyDBZmSam1_33DN5nFsqZF74Jv-wv2k37qY"+//android
		// "?key=AIzaSyBJ5EXH-PN6Yyr6Z5CHPn7gYQYs4ltcfn0"+//ip
		// "&origin=יפה+רום+16,+ירושלים"+
		// "&destination=התאנה+2,+ירושלים"+
		// "&avoid=tolls|highways";


url = "https://maps.googleapis.com/maps/api/staticmap?center=31.73347,35.19032" +
			"&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale 
			+"&maptype=" + mapSelected +
			"&markers=color:red%7Clabel:S%7C31.73314,35.19043"+
			"&markers=color:blue%7Clabel:E%7C31.73381,35.19015"+
			"&path=color:0x0000ff|weight:5|"+
			"31.73314,35.19043|"+
			"31.73344,35.19055|"+
			"31.73345,35.19046|"+
			"31.73345,35.19039|"+
			"31.73347,35.19032|"+
			"31.73354,35.19023|"+
			"31.73371,35.19013|"+
			"31.73377,35.19015|"+
			"31.73381,35.19015"+
			"&key=AIzaSyBJ5EXH-PN6Yyr6Z5CHPn7gYQYs4ltcfn0";
		

// |31.73314,35.19043|31.73344,35.19055|31.73345,35.19046|31.73345,35.19039|31.73347,35.19032|31.73354,35.19023|31.73371,35.19013|31.73377,35.19015|31.73381,35.19015


        WWW www = new WWW (url);
		yield return www;
		img.texture = www.texture;
		Debug.Log(www.text);
		img.SetNativeSize ();

	}
	// Use this for initialization

	// public Application s;
	string allPoint="";
	void Start () {
    //   poly = decodePoly("c{t`EecxuE{@WAP?LCLMPa@RKCG?");
	  
	//   for (int i = 0; i <poly.ToArray().Length; i++)
	//   {
	//   	allPoint+="|"+poly.ToArray()[i].Lat+","+poly.ToArray()[i].Lng;
	//   }
	//   Debug.Log(allPoint);
		
		Application.OpenURL("http://unity3d.com/");


		// img = gameObject.GetComponent<RawImage> ();
		// StartCoroutine (Map());
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}



private List<LatLng> decodePoly(string encoded) {
    List<LatLng> poly = new List<LatLng>();
    int index = 0, len = encoded.Length;
    int lat = 0, lng = 0;

    while (index < len) {
        int b, shift = 0, result = 0;
        do {
            b = encoded[index++] - 63;
            result |= (b & 0x1f) << shift;
            shift += 5;
        } while (b >= 0x20);
        int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
        lat += dlat;

        shift = 0;
        result = 0;
        do {
            b = encoded[index++] - 63;
            result |= (b & 0x1f) << shift;
            shift += 5;
        } while (b >= 0x20);
        int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
        lng += dlng;

		LatLng p = new LatLng();
		p.Lat = (double) lat / 1E5;
		p.Lng = (double) lng / 1E5;
		// Debug.Log(p.Lat+" "+p.Lng);
        poly.Add(p);
    }

    return poly;
}

    public class LatLng
    {
		public double Lat;
		public double Lng;


		

    }
}
