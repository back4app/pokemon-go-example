using UnityEngine;
using System.Collections;
using Parse;
using Parse.Utilities;
using System.Threading.Tasks;
public class ParseSample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParseObject gameScore = new ParseObject ("GameSocre");
		gameScore["score"] = 1337;
		gameScore["playerName"] = "Sean Plott";
		Task saveTask = gameScore.SaveAsync();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
