using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour {

	public GameObject map;
	public GameObject spawn;
	public GameObject playerCapsule;
	public float lat=0;
	public float lon=0;
	float lastlat=0,lastlon=0;
	public GameObject latText;
	public GameObject lonText;

	// Use this for initialization
	void Start () {
		Input.location.Start (); // enable the mobile device GPS
		if (Input.location.isEnabledByUser) { // if mobile device GPS is enabled
			lat = Input.location.lastData.latitude; //get GPS Data
			lon = Input.location.lastData.longitude;
			map.GetComponent<GoogleMap> ().centerLocation.latitude = lat;
			map.GetComponent<GoogleMap> ().centerLocation.longitude = lon;
		}
//		string url="http://maps.googleapis.com/maps/api/geocode/json?latlng="+lat.ToString()+","+lon.ToString();
//		WWW www = new WWW (url);
//		StartCoroutine (WaitForRequest (www));
	
	}

	
	// Update is called once per frame
	void Update () {
//      <---------Mobile Device Code----------->
		if (Input.location.isEnabledByUser) {
			lat = Input.location.lastData.latitude;
			lon = Input.location.lastData.longitude;
			if (lastlat != lat || lastlon != lon) {
				DebugConsole.Log ("Lon:" + lon.ToString () + " Lat:" + lat.ToString ());
				DebugConsole.Log ("lastLon:" + lastlon.ToString () + " lastLat:" + lastlat.ToString ());
				map.GetComponent<GoogleMap> ().centerLocation.latitude = lat;
				map.GetComponent<GoogleMap> ().centerLocation.longitude = lon;
				latText.GetComponent<Text> ().text = "Lat" + lat.ToString ();
				lonText.GetComponent<Text> ().text = "Lon" + lon.ToString ();
				//spawn.GetComponent<Spawn> ().updateMonstersPosition (lon, lat);

				if (lastlon != 0 && lastlat != 0) {//skip at move player at the first update of GPS
					double[] tempXZ = GeoDistance.convertXZ (lastlon, lastlat, lon, lat);
					Debug.Log ("Last lon:" + lastlon.ToString () + "lastlat:" + lastlat.ToString () + "lon:" + lon.ToString () + "lat:" + lat.ToString ());
					Debug.Log ("Player should move to X:" + tempXZ [0].ToString () + " Z:" + tempXZ [1].ToString ());
					Vector3 newPositionTarget = new Vector3 (playerCapsule.transform.position.x+(float)tempXZ [0], 0.07f, playerCapsule.transform.position.z+(float)tempXZ [1]);
					playerCapsule.transform.position = newPositionTarget;
				}else map.GetComponent<GoogleMap> ().Refresh ();

				//map.GetComponent<GoogleMap> ().Refresh ();
			}
			lastlat = lat;
			lastlon = lon;}
//      <---------Mobile Device Code----------->

//      <---------PC Test Code----------->
//		if (lastlat != lat || lastlon != lon) {
//		map.GetComponent<GoogleMap> ().centerLocation.latitude = lat;
//		map.GetComponent<GoogleMap> ().centerLocation.longitude = lon;
//		latText.GetComponent<Text> ().text = "Lat" + lat.ToString ();
//		lonText.GetComponent<Text> ().text = "Lon" + lon.ToString ();
//		//spawn.GetComponent<Spawn> ().updateMonstersPosition (lon, lat);
//			if (lastlon != 0 && lastlat != 0) {//skip at move player at the first update of GPS
//				double[] tempXZ = GeoDistance.convertXZ (lastlon, lastlat, lon, lat);
//				Debug.Log ("Last lon:" + lastlon.ToString () + "lastlat:" + lastlat.ToString () + "lon:" + lon.ToString () + "lat:" + lat.ToString ());
//				Debug.Log ("Player should move to X:" + tempXZ [0].ToString () + " Z:" + tempXZ [1].ToString ());
//				Vector3 newPositionTarget = new Vector3 (playerCapsule.transform.position.x+(float)tempXZ [0], 0.07f, playerCapsule.transform.position.z+(float)tempXZ [1]);
//				playerCapsule.transform.position = newPositionTarget;
//			}else map.GetComponent<GoogleMap> ().Refresh ();
//		}
//					lastlat = lat;
//					lastlon = lon;
//      <---------PC Test Code----------->

	}

	public float getLon(){
		return lon;
	}
	public float getLat(){
		return lat;
	}

//	public IEnumerator WaitForRequest(WWW www){
//		yield return www;
//		if (www.error == null)
//		{
//			Debug.Log("WWW Ok!: " + www.data);
//		} else {
//			Debug.Log("WWW Error: "+ www.error);
//		}   
//	}

}
