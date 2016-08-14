using UnityEngine;
using System.Collections;
using System;

public static class GeoDistance{

	public static double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
	{
		float R = 6371000; // km
		double sLat1 = Math.Sin(deg2rad(lat1));
		double sLat2 = Math.Sin(deg2rad(lat2));
		double cLat1 = Math.Cos(deg2rad(lat1));
		double cLat2 = Math.Cos(deg2rad(lat2));
		double cLon = Math.Cos(deg2rad(lon1) - deg2rad(lon2));

		double cosD = sLat1*sLat2 + cLat1*cLat2*cLon;

		double d = Math.Acos(cosD);

		double dist = R * d;

		return dist;
	}

	public static double BearingBetweenPlaces(double lon1,double lat1,double lon2,double lat2){
		double y = Math.Sin (deg2rad (lon2) - deg2rad (lon1)) * Math.Cos (deg2rad (lat2));
		double x = Math.Cos (deg2rad (lat1)) * Math.Sin (deg2rad (lat2)) - Math.Sin (deg2rad (lat1)) * Math.Cos (deg2rad (lat2)) * Math.Cos (deg2rad (lon2) - deg2rad (lon1));
		double bearing = Math.Atan2 (y, x);
		return bearing;
	}

	public static double[] convertXZ(double lon1,double lat1,double lon2,double lat2){
		double ratio = 0.0162626572;
		double bearing = BearingBetweenPlaces (lon1, lat1, lon2, lat2);
		double distance = DistanceBetweenPlaces(lon1, lat1, lon2, lat2);
		double x = Math.Sin (-bearing) * distance * ratio;
		double z = -Math.Cos (bearing) * distance * ratio;
//		Debug.Log ("X" + x.ToString () + "Z" + z.ToString ());
		double[] xz = { x, z };
		return xz;
	}
	private static double deg2rad(double deg) {
		return (deg * Math.PI / 180.0);
	}
	private static double rad2deg(double rad){
		return (rad * 180 / Math.PI);
	}
}
