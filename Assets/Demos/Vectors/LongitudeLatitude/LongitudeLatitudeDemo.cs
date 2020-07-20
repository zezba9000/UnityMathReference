using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMathReference;

public class LongitudeLatitudeDemo : MonoBehaviour
{
    public float latitude, longitude;
    private Vec3 vec;

    private void Update()
    {
        vec = Vec3.FromSphericalRotation(MathUtilities.DegToRad(latitude), MathUtilities.DegToRad(longitude));
        Debug.DrawRay(Vector3.zero, vec.ToVector3() * .75f, Color.red);
    }

	private void OnGUI()
	{
        vec.SphericalRotation_PreNormalized(out float currentLatitude, out float currentLongitude);
		GUI.Button(new Rect(0, 0, 512, 32), $"LAT:{MathUtilities.RadToDeg(currentLatitude)}, LOG:{MathUtilities.RadToDeg(currentLongitude)}");
	}
}
