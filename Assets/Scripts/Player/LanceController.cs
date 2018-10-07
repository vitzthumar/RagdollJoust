using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceController : MonoBehaviour {

	public float lanceSpeed = 50;
	public float horizontalRestriction = 25;
	public float verticalRestriction = 25;

	// Called from HorseController to move the lance
	public void UpdateLance(Vector2 axisInput) {

		// rotate the lance about the x and y axes
		// rotating first by Space.Self on the y and then by Space.World on the x ensures that the z axis will not be affected by the other rotations
		transform.Rotate(-axisInput.y * lanceSpeed * Time.deltaTime, 0f, 0f, Space.Self);
		transform.Rotate(0f, axisInput.x * lanceSpeed * Time.deltaTime, 0f, Space.World);

		// record the current rotation to check that the lance did not leave its restriction
		Vector3 currentRotation = transform.localRotation.eulerAngles;

		// check if the vertical movement is outside of its restrictions
		if (currentRotation.x < (360 - verticalRestriction) && currentRotation.x > verticalRestriction) {
			if (currentRotation.x < 180) {
				// the lance is above where it should be - move it back at the uppermost point
				currentRotation.x = verticalRestriction;
			} else {
				// the lance is below where it should be - move it back at the lowermost point
				currentRotation.x = 360 - verticalRestriction;
			}
		} 

		// check if the horizontal movement is outside of its resstrictions
		if (currentRotation.y < (360 - horizontalRestriction) && currentRotation.y > horizontalRestriction) {
			if (currentRotation.y < 180) {
				// the lance is to the right of where it should be - move it back to the rightmost point
				currentRotation.y = horizontalRestriction;
			} else {
				// the lance is to the left of where it should be - move it back ot the leftmost point
				currentRotation.y = 360 - horizontalRestriction;
			}
		}

		// make absolutely sure that the lance has not rotated about its z axis
		currentRotation.z = 0;

		// rotate back to a legal position
		transform.localRotation = Quaternion.Euler(currentRotation);
	}
}
