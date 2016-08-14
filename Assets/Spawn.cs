using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class Spawn : MonoBehaviour {
	public List<GameObject> monster;
	public List<double[]> monsterXZCoordination;
	public List<double[]> monsterLL;
	public GameObject locationManager;
	public double playerlon;
	public double playerlat;
	public GameObject m;
	double lastlon,lastlat;
	public bool spawn = false;
	public int timeOfupdate = 0;
	// Use this for initialization
	void Start () {
		monsterXZCoordination = new List<double[]>();
		monsterLL = new List<double[]> ();
		var query = ParseObject.GetQuery ("Monster");
		//you can use WhereWithinGeoBox or WhereNear or WhereWithinDistance to simulate pkmgo serach range
		playerlon = locationManager.GetComponent<LocationManager>().getLon();
		playerlat = locationManager.GetComponent<LocationManager>().getLat();
		query.FindAsync ().ContinueWith (t => {
			IEnumerable<ParseObject> results = t.Result;
			foreach (var result in results) {
				ParseGeoPoint temp = result.Get<ParseGeoPoint>("Location");
				double[] tempxz = GeoDistance.convertXZ(playerlon,playerlat,temp.Longitude,temp.Latitude);
				double[] trueLL = {temp.Longitude,temp.Latitude};
				monsterLL.Add(trueLL);
				monsterXZCoordination.Add(tempxz);
			}
			spawn = true;
		});


	}
	
	// Update is called once per frame
	void Update () {

		playerlon = locationManager.GetComponent<LocationManager>().getLon();
		playerlat = locationManager.GetComponent<LocationManager> ().getLat ();

		if (spawn == true) {
			monsterSpawn ();
		}
//		if (monster.Count != 0) {
//			if (lastlon != playerlon || lastlat != playerlat) {
//				DebugConsole.Log ("Changing");
//				updateMonstersPosition ();
//			}
//		}
		lastlat = playerlat;
		lastlon = playerlon;

	}
	void monsterSpawn(){
		DebugConsole.Log ("HIHIHI");
		for (int i = 0; i < monsterXZCoordination.Count; i++) {
			GameObject temp = Instantiate (m, new Vector3 ((float)monsterXZCoordination [i][0], 0.07f, (float)monsterXZCoordination [i][1]), new Quaternion (0, 0, 0, 0)) as GameObject;

			DebugConsole.Log (temp.transform.position.ToString());
			monster.Add (temp);
		}
		spawn = false;
	}
	void updateMonstersPosition(){
		timeOfupdate++;
		for (int i = 0; i < monster.Count; i++) {
			double[] tempxz = GeoDistance.convertXZ(playerlon,playerlat,monsterLL[i][0],monsterLL[i][1]);
			monster [i].gameObject.transform.position = new Vector3 ((float)tempxz[0],0.07f,(float)tempxz[1]);
			DebugConsole.Log (timeOfupdate.ToString()+"th update:"+i.ToString()+" "+monster [i].gameObject.transform.position.ToString ());
		}
	}
	public void updateMonstersPosition(double lon,double lat){
		timeOfupdate++;
		for (int i = 0; i < monster.Count; i++) {
			double[] tempxz = GeoDistance.convertXZ(lon,lat,monsterLL[i][0],monsterLL[i][1]);
			monster [i].gameObject.transform.position = new Vector3 ((float)tempxz[0],0.07f,(float)tempxz[1]);
			DebugConsole.Log (timeOfupdate.ToString()+"th update:"+i.ToString()+" "+monster [i].gameObject.transform.position.ToString ());
		}
	}
}
