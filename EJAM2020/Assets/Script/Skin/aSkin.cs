using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aSkin : MonoBehaviour
{
    IAMovement myIA;

    [Header("Parties du Skin")]
    public SpriteRenderer Corps;
    public SpriteRenderer Tête, EpauleDroite, BrasDroit, MainDroite, EpauleGauche, BrasGauche, MainGauche;

    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer[] LesSprites = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < LesSprites.Length; i++)
        {
            switch (LesSprites[i].gameObject.name)
            {
                case "Corps":
                    Corps = LesSprites[i];
                    break;
                case "Tête":
                    Tête = LesSprites[i];
                    break;
                case "EpauleDroite":
                    EpauleDroite = LesSprites[i];
                    break;
                case "BrasDroite":
                    BrasDroit = LesSprites[i];
                    break;
                case "MainDroite":
                    MainDroite = LesSprites[i];
                    break;
                case "EpauleGauche":
                    EpauleGauche = LesSprites[i];
                    break;
                case "BrasGauche":
                    BrasGauche = LesSprites[i];
                    break;
                case "MainGauche":
                    MainGauche = LesSprites[i];
                    break;
            }
        }

        myIA = GetComponentInParent<IAMovement>();

        if(myIA != null)
        {
            LoadSkin(myIA);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadSkin(IAMovement theIA)
    {
        if(theIA.myType == Type.Guard)
        {
            Corps.sprite = Resources.Load<Sprite>("Skins/Guard/Corps");
            Tête.sprite = Resources.Load<Sprite>("Skins/Guard/Tête");
            EpauleDroite.sprite = Resources.Load<Sprite>("Skins/Guard/Epaule");
            EpauleGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Epaule");
            BrasDroit.sprite = Resources.Load<Sprite>("Skins/Guard/Bras");
            BrasGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Bras");
            MainDroite.sprite = Resources.Load<Sprite>("Skins/Guard/Main");
            MainGauche.sprite = Resources.Load<Sprite>("Skins/Guard/Main");
        }
    }
}
