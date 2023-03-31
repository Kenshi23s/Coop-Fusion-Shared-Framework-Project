using UnityEngine;
using UnityEngine.UI;

public class Drone_CrossHair
{
    public static Sprite crosshair_Sprite;
    public static float speed=1000;
    public static Drone_CrossHair instance;

    Image Crosshair;

    public Drone_CrossHair()
    {
        instance = this;
        Crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
        if (Crosshair!=null)
        {
            Crosshair.color = Color.red;
        }
        Crosshair.sprite = crosshair_Sprite;
    }
 
    public void AddCrossHairPos(Vector2 newPos)
    {
        Vector3 newdir = new Vector3(newPos.x, newPos.y,0) - Crosshair.transform.position;
        if (newdir.magnitude < 6)
            return;

        float t = newdir.magnitude * speed * Time.deltaTime;
        Vector3 addPos = Crosshair.transform.position + newdir.normalized;
        Crosshair.transform.position = Vector3.Slerp(Crosshair.transform.position, addPos, t);


        Crosshair.transform.position = CheckOutOfBounds(Crosshair.transform.position);





    }

    Vector3 CheckOutOfBounds(Vector2 myPos)
    {
        float x = Mathf.Clamp(myPos.x,0,Screen.width);
        float y = Mathf.Clamp(myPos.y, 0, Screen.height);
        return new Vector3(x,y,0);

    }
   public Ray GetCrossHairScreenRay() => Camera.main.ScreenPointToRay(Crosshair.transform.position);
  
   
}
