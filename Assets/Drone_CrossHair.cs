using UnityEngine;
using UnityEngine.UI;

public class Drone_CrossHair
{  
    public static float speed=1000;

    Image Crosshair;

    public Drone_CrossHair(Image Crosshair)
    {
        //instance = this;
        // no deberia usar tags, pero por el momento...
        this.Crosshair = Crosshair;
        Crosshair.gameObject.SetActive(true);
    }
 
    public void AddCrossHairPos(Vector2 newPos)
    {
        Vector3 newdir = new Vector3(newPos.x, newPos.y,0) - Crosshair.transform.position;
        //si la os
        if (newdir.magnitude < 6)
            return;
        // una de las variables para calcular mi velocidad va a ser la magnitud, asi si estoy mas lejos me muevo mas rapido
        float t = newdir.magnitude * 1000 * Time.deltaTime;
        Vector3 addPos = Crosshair.transform.position + newdir;
        Crosshair.transform.position = Vector3.Slerp(Crosshair.transform.position, addPos, t);

        Crosshair.transform.position = CheckOutOfBounds(Crosshair.transform.position);
    }
    //chequea que la mira no se pase de la pantalla
    Vector3 CheckOutOfBounds(Vector2 myPos)
    {
        float x = Mathf.Clamp(myPos.x,0,Screen.width);
        float y = Mathf.Clamp(myPos.y, 0, Screen.height);
        return new Vector3(x,y,0);

    }
   public Ray GetCrossHairScreenRay() => Camera.main.ScreenPointToRay(Crosshair.transform.position);
  
   
}
