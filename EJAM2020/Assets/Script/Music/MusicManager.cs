using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public RoomManager RM;
    public List<Transform> MusicRooms;
    public AudioMixer MuffledMixer;

    bool Ready;

    // Start is called before the first frame update
    private void Awake()
    {
        RM = RoomManager.Instance;
    }

    void activation()
    {
        Ready = true;

        for (int i = 0; i < RM.Rooms.Count; i++)
        {
            if (RM.Rooms[i].Actions_Spéciales.Count > 0)
            {
                MusicRooms.Add(RM.Rooms[i].transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Ready == false)
        {
            activation();
        }
        else if(Ready && MusicRooms.Count > 0)
        {
            Transform theTransform = null;
            float theDistance = 0;

            for (int i = 0; i < MusicRooms.Count; i++)
            {
                float distance = Vector3.Distance(MusicRooms[i].position, Camera.main.transform.position);

                if (theDistance == 0 || theDistance > distance)
                {
                    theDistance = distance;
                    theTransform = MusicRooms[i];
                }

            }

            if (theTransform != null)
            {
                MuffledMixer.SetFloat("Muffle", Mathf.Clamp(30000 - theDistance * theDistance * 100, 360, 22000));
            }
        }        
    }
}
