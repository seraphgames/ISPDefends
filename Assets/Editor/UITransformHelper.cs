using UnityEngine;
using System.Collections;
using UnityEditor;

public class UITransformHelper : MonoBehaviour 
{	
	[MenuItem("UI Helpers/Snap Anchor %&S")]
	private static void SnapAnchor(){
		Transform[] tfs = Selection.transforms;
		if (tfs != null && tfs.Length > 0) {
			foreach (Transform tf in tfs) {
				_SnapAnchorElement (tf);
			}
		}
	}

	private static void _SnapAnchorElement(Transform ele){
		RectTransform rectTf = ele as RectTransform;
		//get parent transform
		RectTransform parent = rectTf.parent as RectTransform;

        //reset pivot first        

        Vector2 pivot = rectTf.pivot;

        Vector3 pos = rectTf.localPosition;
        pos.x += (0.5f - pivot.x) * rectTf.rect.size.x;
        pos.y += (0.5f - pivot.y) * rectTf.rect.size.y;
        rectTf.pivot = Vector2.one * 0.5f;

        //convert pos into new coordinate
        Debug.Log(rectTf.rect);
		Debug.Log(pos.x);
		pos.x += parent.rect.size.x / 2f;
		pos.y += parent.rect.size.y / 2f;

		Debug.Log (pos);

		float minX = pos.x - rectTf.rect.size.x / 2f;
		float maxX = pos.x + rectTf.rect.size.x / 2f;
		float minY = pos.y - rectTf.rect.size.y / 2f;
		float maxY = pos.y + rectTf.rect.size.y / 2f;

		//convert min max into 0-1
		minX /= parent.rect.size.x;
		maxX /= parent.rect.size.x;
		minY /= parent.rect.size.y;
		maxY /= parent.rect.size.y;

		// reapply to scaler
		Vector2 minScale = new Vector2(minX,minY);
		Vector2 maxScale = new Vector2 (maxX, maxY);
        
		rectTf.anchorMax = maxScale;
		rectTf.anchorMin = minScale;


		//repos all other elements left-top-right... to 0
		rectTf.offsetMax = Vector2.zero;
		rectTf.offsetMin = Vector2.zero;
		//Debug.Log (rectTf.localPosition);
	}
}