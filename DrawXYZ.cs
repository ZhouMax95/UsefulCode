/*
 运行时使用鼠标通过坐标系箭头对物体进行移动
 */
using UnityEngine;
using System.Collections;

public class DrawXYZ : MonoBehaviour {

	public RotateObject Ro;

	void Start () {
		//Ro=GameObject.Find("Worm").GetComponent<RotateObject>();
	}
	
	void Update () {
		if (Input.GetMouseButtonUp(0))
		{
			Ro.isMouseOnXYZ = false;
		}
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButton(0))
		{
			Ro.isMouseOnXYZ = true;
		}
	}

	IEnumerator OnMouseDown()
	{
		Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.parent.position);
		Vector3 offset = transform.parent.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));
		while(Input.GetMouseButton(0))
		{	
			Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
			Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
			switch (this.gameObject.name)
			{
				case "X":
					//transform.parent.position = new Vector3((transform.position.x + CurPosition.x) * Time.deltaTime * 8,
					//  transform.position.y,
					// transform.position.z);
					transform.parent.position = Vector3.MoveTowards(transform.parent.position, new Vector3(
							  CurPosition.x, transform.parent.position.y, transform.parent.position.z), 1f);
					break;
				case "Y":
					transform.parent.position = new Vector3(transform.position.x,
				  (transform.position.y + CurPosition.y) * Time.deltaTime * 8,
				  transform.position.z);
					break;
				case "Z":
					transform.parent.position = new Vector3(transform.position.x,
				   transform.position.y,
				  (transform.position.z + CurPosition.z) * Time.deltaTime * 8);
					break;

				default:
					break;
			}
			yield return new WaitForFixedUpdate();
		}
		
	}

	//IEnumerator OnMouseDown()
	//{
	//	Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(SelectObj.moveObj.transform.position);

	//	Vector3 offset = SelectObj.moveObj.transform.position -
	//	Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

	//	while (Input.GetMouseButton(0))
	//	{
	//		Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);

	//		Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

	//		if (SelectObj.Flags == true)
	//		{
	//			switch (gameObject.name)
	//			{
	//				case "X":
	//					SelectObj.moveObj.transform.Translate(Vector3.right * -offset.x * Time.deltaTime, Space.Self);
	//					//     SelectObj.moveObj.transform.position = new Vector3((SelectObj.moveObj.transform.position.x + CurPosition.x) * Time.deltaTime * 8,
	//					//SelectObj.moveObj.transform.position.y,
	//					//SelectObj.moveObj.transform.position.z);
	//					break;
	//				case "Y":
	//					SelectObj.moveObj.transform.position = new Vector3(SelectObj.moveObj.transform.position.x,
	//			  (SelectObj.moveObj.transform.position.y + CurPosition.y) * Time.deltaTime * 8,
	//			  SelectObj.moveObj.transform.position.z);
	//					break;
	//				case "Z":
	//					SelectObj.moveObj.transform.position = new Vector3(SelectObj.moveObj.transform.position.x,
	//			   SelectObj.moveObj.transform.position.y,
	//			  (SelectObj.moveObj.transform.position.z + CurPosition.z) * Time.deltaTime * 8);
	//					break;
	//				default:
	//					break;
	//			}
	//		}
	//		else
	//		{
	//			switch (gameObject.name)
	//			{
	//				case "X":
	//					SelectObj.moveObj.transform.position = Vector3.MoveTowards(SelectObj.moveObj.transform.position, new Vector3(
	//						CurPosition.x, SelectObj.moveObj.transform.position.y, SelectObj.moveObj.transform.position.z), 1f);
	//					break;
	//				case "Y":
	//					SelectObj.moveObj.transform.position = Vector3.MoveTowards(SelectObj.moveObj.transform.position, new Vector3(SelectObj.moveObj.transform.position.x
	//				 , CurPosition.y, SelectObj.moveObj.transform.position.z), 1f);
	//					break;
	//				case "Z":
	//					SelectObj.moveObj.transform.position = Vector3.MoveTowards(SelectObj.moveObj.transform.position, new Vector3(
	//				   SelectObj.moveObj.transform.position.x, SelectObj.moveObj.transform.position.y, CurPosition.z), 1f);
	//					break;
	//				default:
	//					break;
	//			}
	//		}
	//		yield return new WaitForFixedUpdate();
	//	}
	//}
}
