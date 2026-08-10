using UnityEngine;

public class F2FCrossHair : MonoBehaviour
{


    public GameObject CircleFull_Gameobject;

    bool Caninteract = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if(Caninteract == true)
        {

            Ray ray1 = new Ray(transform.position, transform.forward);
            RaycastHit hit1;

            if(Physics.Raycast(ray1, out hit1, 10f))
            {


                if (hit1.collider.CompareTag("Door"))
                {


                    CircleFull_Gameobject.SetActive(true);





                }
                else if (hit1.collider.CompareTag("Cube"))
                {


                    CircleFull_Gameobject.SetActive(true);


                }
                else
                {


                    CircleFull_Gameobject.SetActive(false);


                }






            }
            else
            {


                CircleFull_Gameobject.SetActive(false);


            }






        }





        
    }
}
